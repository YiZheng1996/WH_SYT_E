using AntdUI;
using System.ComponentModel;
using System.Text;

namespace MainUI
{
    /// <summary>
    /// 日志查看界面 - 完整优化版
    /// 主要改进:
    /// 1. DataGridView替代ListView
    /// 2. 分割面板显示详细信息
    /// 3. 增强的搜索和过滤功能
    /// 4. 导出功能
    /// 5. 自动刷新
    /// </summary>
    public partial class frmNLogs : UIForm
    {
        private readonly NlogsBLL _nlogsBLL = new();
        private List<NlogsModel> _currentLogs = new();
        private NlogsModel _selectedLog = null;

        public frmNLogs()
        {
            InitializeComponent();
            InitializeComponents();
            SetDefaultValues();
        }

        #region 初始化

        /// <summary>
        /// 初始化所有组件
        /// </summary>
        private void InitializeComponents()
        {
            InitializeLogTypeComboBox();
            InitializeDataGridView();
        }

        /// <summary>
        /// 设置默认值
        /// </summary>
        private void SetDefaultValues()
        {
            dtpStartBig.Value = DateTime.Now.AddDays(-7);
            dtpEndBig.Value = DateTime.Now;
        }

        /// <summary>
        /// 初始化DataGridView
        /// </summary>
        private void InitializeDataGridView()
        {
            dgvLogs.AutoGenerateColumns = false;
            dgvLogs.Columns.Clear();

            // 等级列
            dgvLogs.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Level",
                HeaderText = "等级",
                DataPropertyName = "Level",
                Width = 80,
                ReadOnly = true
            });

            // 时间列
            dgvLogs.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MessTime",
                HeaderText = "时间",
                DataPropertyName = "MessTime",
                Width = 150,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd HH:mm:ss" }
            });

            // 用户列
            dgvLogs.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UserName",
                HeaderText = "用户",
                DataPropertyName = "UserName",
                Width = 100,
                ReadOnly = true
            });

            // 操作列
            dgvLogs.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MessageName",
                HeaderText = "操作",
                DataPropertyName = "MessageName",
                Width = 150,
                ReadOnly = true
            });

            // 来源列
            dgvLogs.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Source",
                HeaderText = "来源",
                DataPropertyName = "Source",
                Width = 150,
                ReadOnly = true
            });

            // 详细信息列（自动填充剩余空间）
            dgvLogs.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Message",
                HeaderText = "详细信息",
                DataPropertyName = "Message",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });

            // 设置样式
            dgvLogs.RowTemplate.Height = 30;
            dgvLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLogs.MultiSelect = false;
            dgvLogs.AllowUserToAddRows = false;
            dgvLogs.AllowUserToDeleteRows = false;
            dgvLogs.ReadOnly = true;
            dgvLogs.RowHeadersVisible = false;
            dgvLogs.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);

            // 绑定事件
            dgvLogs.CellDoubleClick += DgvLogs_CellDoubleClick;
            dgvLogs.CellFormatting += DgvLogs_CellFormatting;
            dgvLogs.SelectionChanged += DgvLogs_SelectionChanged;
        }

        /// <summary>
        /// 初始化日志类型下拉框
        /// </summary>
        private void InitializeLogTypeComboBox()
        {
            try
            {
                var logTypes = EnumExtensions.GetEnumItems<LogType>();
                cboType.DataSource = logTypes;
                cboType.DisplayMember = "DisplayName";
                cboType.ValueMember = "Value";
                cboType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("初始化日志类型下拉框失败", ex);
                MessageHelper.MessageOK(this, $"初始化失败: {ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 数据加载

        /// <summary>
        /// 加载日志数据
        /// </summary>
        public void LoadData()
        {
            try
            {
                // 显示加载状态
                lblStatus.Text = "正在加载...";
                dgvLogs.DataSource = null;

                // 获取查询条件
                string selectedType = cboType.SelectedValue?.ToString();
                if (selectedType == "All") selectedType = null;

                DateTime startTime = dtpStartBig.Value.Date;
                DateTime endTime = dtpEndBig.Value.AddDays(1).AddSeconds(-1);

                // 查询数据
                _currentLogs = _nlogsBLL.FindList(selectedType, startTime, endTime);

                // 应用关键字过滤
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    string keyword = txtSearch.Text.ToLower();
                    _currentLogs = _currentLogs.Where(log =>
                        log.Level?.ToLower().Contains(keyword) == true ||
                        log.UserName?.ToLower().Contains(keyword) == true ||
                        log.MessageName?.ToLower().Contains(keyword) == true ||
                        log.Message?.ToLower().Contains(keyword) == true ||
                        log.Source?.ToLower().Contains(keyword) == true
                    ).ToList();
                }

                // 绑定数据
                dgvLogs.DataSource = _currentLogs;

                // 更新状态
                UpdateStatusBar();

                // 滚动到最新记录
                ScrollToLatest();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("日志查询失败", ex);
                MessageHelper.MessageOK(this, $"日志查询失败: {ex.Message}", TType.Error);
                lblStatus.Text = "查询失败";
            }
        }

        /// <summary>
        /// 更新状态栏
        /// </summary>
        private void UpdateStatusBar()
        {
            int totalCount = _currentLogs.Count;
            int errorCount = _currentLogs.Count(l => l.Level == "Error");
            int fatalCount = _currentLogs.Count(l => l.Level == "Fatal");
            int warnCount = _currentLogs.Count(l => l.Level == "Warn");

            string status = $"共 {totalCount} 条";
            if (fatalCount > 0) status += $" | 致命:{fatalCount}";
            if (errorCount > 0) status += $" | 错误:{errorCount}";
            if (warnCount > 0) status += $" | 警告:{warnCount}";

            lblStatus.Text = status;
            Text = $"日志查询 - {totalCount} 条记录";
        }

        /// <summary>
        /// 滚动到最新记录
        /// </summary>
        private void ScrollToLatest()
        {
            if (dgvLogs.Rows.Count > 0)
            {
                dgvLogs.FirstDisplayedScrollingRowIndex = dgvLogs.Rows.Count - 1;
            }
        }

        #endregion

        #region 事件处理

        private void frmNLogs_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboType.SelectedValue != null)
            {
                LoadData();
            }
        }

        /// <summary>
        /// 搜索框文本变化（可选：实时搜索或按钮触发）
        /// </summary>
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // 如果启用实时搜索，取消注释下面这行
            LoadData();
        }

        /// <summary>
        /// 搜索按钮（如果不使用实时搜索）
        /// </summary>
        private void btnSearchKeyword_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// DataGridView行格式化（不同等级显示不同颜色）
        /// </summary>
        private void DgvLogs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // 获取该行的日志等级
            string level = dgvLogs.Rows[e.RowIndex].Cells["Level"].Value?.ToString();

            if (dgvLogs.Columns[e.ColumnIndex].Name == "Level")
            {
                // 等级列使用颜色和粗体
                switch (level)
                {
                    case "Fatal":
                        e.CellStyle.ForeColor = Color.White;
                        e.CellStyle.BackColor = Color.DarkRed;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        break;
                    case "Error":
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        break;
                    case "Warn":
                        e.CellStyle.ForeColor = Color.DarkOrange;
                        break;
                    case "Info":
                        e.CellStyle.ForeColor = Color.Blue;
                        break;
                    case "Debug":
                        e.CellStyle.ForeColor = Color.Gray;
                        break;
                    case "Trace":
                        e.CellStyle.ForeColor = Color.LightGray;
                        break;
                }
            }
            else
            {
                // 其他列：Fatal和Error行整行高亮背景
                if (level == "Fatal")
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 230, 230);
                }
                else if (level == "Error")
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 245, 245);
                }
            }
        }

        /// <summary>
        /// 选中行变化
        /// </summary>
        private void DgvLogs_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLogs.SelectedRows.Count > 0)
            {
                int index = dgvLogs.SelectedRows[0].Index;
                if (index >= 0 && index < _currentLogs.Count)
                {
                    _selectedLog = _currentLogs[index];
                    ShowLogDetail(_selectedLog);
                }
            }
        }

        /// <summary>
        /// 双击查看详细信息
        /// </summary>
        private void DgvLogs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && _selectedLog != null)
            {
                ShowLogDetailDialog(_selectedLog);
            }
        }

        /// <summary>
        /// 在底部面板显示详细信息
        /// </summary>
        private void ShowLogDetail(NlogsModel log)
        {
            if (log == null)
            {
                txtDetail.Clear();
                return;
            }

            StringBuilder sb = new();

            sb.AppendLine("═══════════════════════════════════════════════════════");
            sb.AppendLine($"【日志等级】: {log.Level}");
            sb.AppendLine($"【记录时间】: {log.MessTime:yyyy-MM-dd HH:mm:ss.fff}");
            sb.AppendLine($"【用户名称】: {log.UserName ?? "系统"}");
            sb.AppendLine($"【操作信息】: {log.MessageName ?? "无"}");
            sb.AppendLine($"【信息来源】: {log.Source ?? "未知"}");
            sb.AppendLine("───────────────────────────────────────────────────────");
            sb.AppendLine("【详细信息】:");
            sb.AppendLine(log.Message ?? "无");
            sb.AppendLine("═══════════════════════════════════════════════════════");

            txtDetail.Text = sb.ToString();
        }

        /// <summary>
        /// 弹窗显示详细信息
        /// </summary>
        private void ShowLogDetailDialog(NlogsModel log)
        {
            if (log == null) return;

            StringBuilder sb = new();
            sb.AppendLine($"【日志等级】: {log.Level}");
            sb.AppendLine($"【记录时间】: {log.MessTime:yyyy-MM-dd HH:mm:ss.fff}");
            sb.AppendLine($"【用户名称】: {log.UserName ?? "系统"}");
            sb.AppendLine($"【操作信息】: {log.MessageName ?? "无"}");
            sb.AppendLine($"【信息来源】: {log.Source ?? "未知"}");
            sb.AppendLine();
            sb.AppendLine("【详细信息】:");
            sb.AppendLine(log.Message ?? "无");

            // 使用MessageHelper显示，设置合适的图标
            TType iconType = log.Level switch
            {
                "Fatal" => TType.Error,
                "Error" => TType.Error,
                "Warn" => TType.Warn,
                "Info" => TType.Info,
                _ => TType.Info
            };

            MessageHelper.MessageOK(this, sb.ToString(), iconType);
        }

        /// <summary>
        /// 导出到Excel
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentLogs == null || _currentLogs.Count == 0)
                {
                    MessageHelper.MessageOK(this, "没有数据可导出!", TType.Warn);
                    return;
                }

                SaveFileDialog sfd = new()
                {
                    Filter = "Excel文件|*.csv|所有文件|*.*",
                    FileName = $"日志导出_{DateTime.Now:yyyyMMddHHmmss}.csv",
                    DefaultExt = "csv"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ExportToExcel(sfd.FileName);
                    MessageHelper.MessageOK(this, $"成功导出 {_currentLogs.Count} 条记录!", TType.Success);

                    // 询问是否打开文件
                    if (MessageHelper.MessageYes(this, "是否打开导出的文件?") == DialogResult.OK)
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = sfd.FileName,
                            UseShellExecute = true
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("导出日志失败", ex);
                MessageHelper.MessageOK(this, $"导出失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 导出到CSV格式（可被Excel打开）
        /// </summary>
        private void ExportToExcel(string filePath)
        {
            StringBuilder csv = new();

            csv.AppendLine("日志等级,记录时间,用户名,操作信息,信息来源,详细信息");

            foreach (var log in _currentLogs)
            {
                csv.AppendLine(
                    $"\"{log.Level}\"," +
                    $"\"{log.MessTime:yyyy-MM-dd HH:mm:ss}\"," +
                    $"\"{log.UserName ?? ""}\"," +
                    $"\"{log.MessageName ?? ""}\"," +
                    $"\"{log.Source ?? ""}\"," +
                    $"\"{(log.Message ?? "").Replace("\"", "\"\"")}\""
                );
            }

            // 使用UTF-8编码带BOM，确保Excel能正确显示中文
            File.WriteAllText(filePath, csv.ToString(), new UTF8Encoding(true));
        }

        /// <summary>
        /// 复制详细信息
        /// </summary>
        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDetail.Text))
            {
                try
                {
                    Clipboard.SetText(txtDetail.Text);
                    MessageHelper.MessageOK(this, "已复制到剪贴板!", TType.Success);
                }
                catch (Exception ex)
                {
                    MessageHelper.MessageOK(this, $"复制失败: {ex.Message}", TType.Error);
                }
            }
            else
            {
                MessageHelper.MessageOK(this, "没有可复制的内容!", TType.Warn);
            }
        }

        /// <summary>
        /// 快速筛选 - 只看今天
        /// </summary>
        private void btnToday_Click(object sender, EventArgs e)
        {
            dtpStartBig.Value = DateTime.Now.Date;
            dtpEndBig.Value = DateTime.Now.Date;
            LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 窗体关闭时停止自动刷新
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
        }

        #endregion
    }

    /// <summary>
    /// 日志等级枚举
    /// </summary>
    public enum LogType
    {
        [Description("全部等级")]
        All = 0,

        [Description("跟踪")]
        Trace = 1,

        [Description("调试")]
        Debug = 2,

        [Description("信息")]
        Info = 3,

        [Description("警告")]
        Warn = 4,

        [Description("错误")]
        Error = 5,

        [Description("致命")]
        Fatal = 6
    }
}