using AntdUI;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.Logging;
using System.Text;
using Button = AntdUI.Button;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 变量监控工具窗体
    /// 实时监控工作流中所有全局变量的状态和变化
    /// </summary>
    public partial class Form_VariableMonitor : Sunny.UI.UIForm
    {
        #region 私有字段

        private readonly IWorkflowStateService _workflowState;
        private readonly GlobalVariableManager _variableManager;
        private readonly ILogger<Form_VariableMonitor> _logger;

        // 刷新控制
        private int _refreshInterval = 500; // 默认500ms
        private bool _isPaused = false;

        // 数据缓存
        private Dictionary<string, object> _previousValues = new();
        private Dictionary<string, List<VariableHistoryEntry>> _variableHistory = new();
        private Dictionary<string, DateTime> _lastChangedTime = new();
        private List<VariableSnapshot> _snapshots = new();

        // 统计信息
        private int _totalCount = 0;
        private int _assignedCount = 0;
        private int _changedCount = 0;
        private DateTime _lastRefreshTime = DateTime.MinValue;

        // 选中的变量
        private string _selectedVariableName = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 依赖注入构造函数
        /// </summary>
        public Form_VariableMonitor(
            IWorkflowStateService workflowState,
            GlobalVariableManager variableManager,
            ILogger<Form_VariableMonitor> logger)
        {
            _workflowState = workflowState ?? throw new ArgumentNullException(nameof(workflowState));
            _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            InitializeComponent();
            InitializeFormStyle();
        }

        /// <summary>
        /// 默认构造函数(设计器用)
        /// </summary>
        public Form_VariableMonitor()
        {
            InitializeComponent();
        }

        #endregion

        #region 初始化方法

        /// <summary>
        /// 初始化窗体样式
        /// </summary>
        private void InitializeFormStyle()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.ShowIcon = false;
            this.ShowInTaskbar = true;

            // 设置 Sunny.UI 主题
            this.Style = Sunny.UI.UIStyle.Custom;
            this.StyleCustomMode = true;
            this.TitleColor = Color.FromArgb(65, 100, 204);
            this.TitleFont = new Font("微软雅黑", 12F, FontStyle.Bold);
            this.RectColor = Color.FromArgb(65, 100, 204);
            this.BackColor = Color.FromArgb(248, 249, 250);
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void Form_VariableMonitor_Load(object sender, EventArgs e)
        {
            try
            {
                _logger?.LogInformation("变量监控窗体加载");

                // 首次刷新数据
                RefreshVariableData();

                // 启动定时器
                refreshTimer.Start();

                _logger?.LogInformation("变量监控窗体加载完成");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "窗体加载时发生错误");
                MessageHelper.MessageOK($"加载失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        private void Form_VariableMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // 停止定时器
                refreshTimer?.Stop();

                _logger?.LogInformation("变量监控窗体关闭");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "窗体关闭时发生错误");
            }
        }

        #endregion

        #region 定时刷新

        /// <summary>
        /// 定时器触发事件
        /// </summary>
        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            if (!_isPaused)
            {
                RefreshVariableData();
            }
        }

        /// <summary>
        /// 刷新变量数据
        /// </summary>
        private void RefreshVariableData()
        {
            try
            {
                // 获取所有变量
                var variables = _variableManager.GetAllVariables();

                if (variables == null)
                {
                    _logger?.LogWarning("获取变量列表为空");
                    return;
                }

                // 应用过滤
                var filteredVariables = ApplyFilters(variables);

                // 更新数据表格
                UpdateDataGridView(filteredVariables);

                // 更新统计信息
                _totalCount = variables.Count;
                _assignedCount = variables.Count(v => v.IsAssignedByStep);
                _lastRefreshTime = DateTime.Now;
                UpdateStatistics();

                // 如果有选中的变量，更新详情
                if (!string.IsNullOrEmpty(_selectedVariableName))
                {
                    UpdateVariableDetails(_selectedVariableName);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "刷新变量数据时发生错误");
            }
        }

        /// <summary>
        /// 应用过滤条件
        /// </summary>
        private List<VarItem_Enhanced> ApplyFilters(List<VarItem_Enhanced> variables)
        {
            var filtered = variables.AsEnumerable();

            // 搜索过滤
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                var keyword = txtSearch.Text.Trim();
                filtered = filtered.Where(v =>
                    v.VarName?.Contains(keyword, StringComparison.OrdinalIgnoreCase) == true ||
                    v.VarType?.Contains(keyword, StringComparison.OrdinalIgnoreCase) == true);
            }

            // 只显示已赋值
            if (chkOnlyAssigned.Checked)
            {
                filtered = filtered.Where(v => v.IsAssignedByStep);
            }

            return filtered.ToList();
        }

        /// <summary>
        /// 更新 DataGridView
        /// </summary>
        private void UpdateDataGridView(List<VarItem_Enhanced> variables)
        {
            try
            {
                dgvVariables.SuspendLayout();
                _changedCount = 0;

                // 如果行数不匹配，重新构建
                if (dgvVariables.Rows.Count != variables.Count)
                {
                    RebuildDataGridView(variables);
                }
                else
                {
                    // 只更新值变化的行
                    for (int i = 0; i < variables.Count && i < dgvVariables.Rows.Count; i++)
                    {
                        UpdateRow(i, variables[i]);
                    }
                }

                dgvVariables.ResumeLayout();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "更新DataGridView时发生错误");
            }
        }

        /// <summary>
        /// 重新构建 DataGridView
        /// </summary>
        private void RebuildDataGridView(List<VarItem_Enhanced> variables)
        {
            dgvVariables.Rows.Clear();

            foreach (var variable in variables)
            {
                var rowIndex = dgvVariables.Rows.Add(
                    variable.VarName,
                    variable.VarType,
                    FormatValue(variable.VarValue),
                    variable.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    variable.AssignedByStepInfo ?? "未赋值"
                );

                // 检测值变化
                CheckValueChange(variable, rowIndex);
            }
        }

        /// <summary>
        /// 更新单行数据
        /// </summary>
        private void UpdateRow(int rowIndex, VarItem_Enhanced variable)
        {
            var row = dgvVariables.Rows[rowIndex];

            row.Cells["ColVarName"].Value = variable.VarName;
            row.Cells["ColVarType"].Value = variable.VarType;
            row.Cells["ColCurrentValue"].Value = FormatValue(variable.VarValue);
            row.Cells["ColLastUpdated"].Value = variable.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss.fff");
            row.Cells["ColAssignedBy"].Value = variable.AssignedByStepInfo ?? "未赋值";

            // 检测值变化
            CheckValueChange(variable, rowIndex);
        }

        /// <summary>
        /// 检测值变化并高亮
        /// </summary>
        private void CheckValueChange(VarItem_Enhanced variable, int rowIndex)
        {
            var varName = variable.VarName;
            var currentValue = variable.VarValue;

            // 记录历史
            RecordHistory(variable);

            // 检测是否变化
            if (_previousValues.TryGetValue(varName, out var previousValue))
            {
                if (!object.Equals(previousValue, currentValue))
                {
                    _changedCount++;
                    _lastChangedTime[varName] = DateTime.Now;

                    // 高亮显示
                    if (chkHighlightChanges.Checked)
                    {
                        HighlightRow(rowIndex, true);
                    }
                }
                else
                {
                    // 检查是否需要取消高亮(超过2秒)
                    if (_lastChangedTime.TryGetValue(varName, out var lastChanged))
                    {
                        if ((DateTime.Now - lastChanged).TotalSeconds > 2)
                        {
                            HighlightRow(rowIndex, false);
                        }
                    }
                }
            }

            // 更新缓存
            _previousValues[varName] = currentValue;
        }

        /// <summary>
        /// 高亮显示行
        /// </summary>
        private void HighlightRow(int rowIndex, bool highlight)
        {
            if (rowIndex < 0 || rowIndex >= dgvVariables.Rows.Count)
                return;

            var row = dgvVariables.Rows[rowIndex];
            var valueCell = row.Cells["ColCurrentValue"];

            if (highlight)
            {
                valueCell.Style.BackColor = Color.FromArgb(255, 243, 205); // 浅黄色
                valueCell.Style.ForeColor = Color.FromArgb(48, 48, 48);
            }
            else
            {
                valueCell.Style.BackColor = Color.White;
                valueCell.Style.ForeColor = Color.FromArgb(48, 48, 48);
            }
        }

        /// <summary>
        /// 记录变量历史
        /// </summary>
        private void RecordHistory(VarItem_Enhanced variable)
        {
            var varName = variable.VarName;

            if (!_variableHistory.ContainsKey(varName))
            {
                _variableHistory[varName] = new List<VariableHistoryEntry>();
            }

            var history = _variableHistory[varName];

            // 只有值真正变化时才记录
            if (history.Count == 0 || !object.Equals(history.Last().Value, variable.VarValue))
            {
                history.Add(new VariableHistoryEntry
                {
                    Timestamp = DateTime.Now,
                    Value = variable.VarValue,
                    Source = variable.AssignedByStepInfo ?? "未知"
                });

                // 限制历史记录数量(最多100条)
                if (history.Count > 100)
                {
                    history.RemoveAt(0);
                }
            }
        }

        /// <summary>
        /// 格式化值显示
        /// </summary>
        private string FormatValue(object value)
        {
            if (value == null)
                return "<null>";

            if (value is bool boolValue)
                return boolValue ? "True" : "False";

            if (value is double doubleValue)
                return doubleValue.ToString("F3");

            if (value is float floatValue)
                return floatValue.ToString("F3");

            return value.ToString();
        }

        #endregion

        #region 变量详情

        /// <summary>
        /// DataGridView 选择变化事件
        /// </summary>
        private void DgvVariables_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVariables.SelectedRows.Count > 0)
            {
                var selectedRow = dgvVariables.SelectedRows[0];
                var varName = selectedRow.Cells["ColVarName"].Value?.ToString();

                if (!string.IsNullOrEmpty(varName))
                {
                    _selectedVariableName = varName;
                    UpdateVariableDetails(varName);
                }
            }
        }

        /// <summary>
        /// 更新变量详情面板
        /// </summary>
        private void UpdateVariableDetails(string varName)
        {
            try
            {
                var variable = _variableManager.FindVariableByName(varName);

                if (variable == null)
                {
                    ClearVariableDetails();
                    return;
                }

                lblDetailVarName.Text = $"变量名: {variable.VarName}";
                lblDetailVarType.Text = $"类型: {variable.VarType}";
                lblDetailCurrentValue.Text = $"当前值: {FormatValue(variable.VarValue)}";
                lblDetailLastUpdated.Text = $"上次更新: {variable.LastUpdated:yyyy-MM-dd HH:mm:ss.fff}";
                lblDetailIsAssigned.Text = $"是否被步骤赋值: {(variable.IsAssignedByStep ? "是" : "否")}";
                lblDetailSource.Text = $"赋值来源: {variable.AssignedByStepInfo ?? "未赋值"}";

                // 更新历史列表
                UpdateHistoryList(varName);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "更新变量详情时发生错误");
            }
        }

        /// <summary>
        /// 清空变量详情
        /// </summary>
        private void ClearVariableDetails()
        {
            lblDetailVarName.Text = "变量名: 未选择";
            lblDetailVarType.Text = "类型: -";
            lblDetailCurrentValue.Text = "当前值: -";
            lblDetailLastUpdated.Text = "上次更新: -";
            lblDetailIsAssigned.Text = "是否被步骤赋值: -";
            lblDetailSource.Text = "赋值来源: -";
            lstHistory.Items.Clear();
        }

        /// <summary>
        /// 更新历史列表
        /// </summary>
        private void UpdateHistoryList(string varName)
        {
            lstHistory.Items.Clear();

            if (_variableHistory.TryGetValue(varName, out var history))
            {
                // 显示最近10条
                var recentHistory = history.TakeLast(10).Reverse();

                foreach (var entry in recentHistory)
                {
                    var historyText = $"{entry.Timestamp:HH:mm:ss} → {FormatValue(entry.Value)} ({entry.Source})";
                    lstHistory.Items.Add(historyText);
                }

                if (history.Count > 10)
                {
                    lstHistory.Items.Add($"... 还有 {history.Count - 10} 条历史记录");
                }
            }
            else
            {
                lstHistory.Items.Add("暂无历史记录");
            }
        }

        #endregion

        #region 统计信息

        /// <summary>
        /// 更新统计信息
        /// </summary>
        private void UpdateStatistics()
        {
            var unassignedCount = _totalCount - _assignedCount;
            var refreshIntervalText = GetRefreshIntervalText();

            lblStats.Text = $"总变量数: {_totalCount}  已赋值: {_assignedCount}  " +
                $"未赋值: {unassignedCount}  刷新频率: {refreshIntervalText}  " +
                $"最后刷新: {_lastRefreshTime:HH:mm:ss.fff}  数据变化: {_changedCount}个";
        }

        /// <summary>
        /// 获取刷新间隔文本
        /// </summary>
        private string GetRefreshIntervalText()
        {
            return _refreshInterval switch
            {
                100 => "100ms",
                500 => "500ms",
                1000 => "1秒",
                2000 => "2秒",
                5000 => "5秒",
                _ => "手动"
            };
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// 搜索框文本变化
        /// </summary>
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            RefreshVariableData();
        }

        /// <summary>
        /// 刷新间隔选择变化
        /// </summary>
        private void CmbRefreshInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selected = cmbRefreshInterval.Text;

                _refreshInterval = selected switch
                {
                    "100ms" => 100,
                    "500ms" => 500,
                    "1秒" => 1000,
                    "2秒" => 2000,
                    "5秒" => 5000,
                    "手动" => 0,
                    _ => 500
                };

                if (_refreshInterval > 0)
                {
                    refreshTimer.Interval = _refreshInterval;
                    refreshTimer.Start();
                    _isPaused = false;
                    btnPauseResume.Text = "暂停刷新";
                    btnPauseResume.Symbol = 61516; // 暂停图标
                }
                else
                {
                    refreshTimer.Stop();
                    _isPaused = true;
                    btnPauseResume.Text = "恢复刷新";
                    btnPauseResume.Symbol = 61515; // 播放图标
                }

                UpdateStatistics();

                _logger?.LogInformation("刷新间隔已更改为: {Interval}ms", _refreshInterval);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "更改刷新间隔时发生错误");
            }
        }

        /// <summary>
        /// 只显示已赋值复选框状态变化
        /// </summary>
        private void ChkOnlyAssigned_CheckedChanged(object sender, EventArgs e)
        {
            RefreshVariableData();
        }

        /// <summary>
        /// 暂停/恢复按钮点击
        /// </summary>
        private void BtnPauseResume_Click(object sender, EventArgs e)
        {
            _isPaused = !_isPaused;

            if (_isPaused)
            {
                btnPauseResume.Text = "恢复刷新";
                btnPauseResume.Symbol = 61515; // 播放图标
                btnPauseResume.FillColor = Color.FromArgb(40, 167, 69);
                btnPauseResume.FillColor2 = Color.FromArgb(34, 139, 34);
            }
            else
            {
                btnPauseResume.Text = "暂停刷新";
                btnPauseResume.Symbol = 61516; // 暂停图标
                btnPauseResume.FillColor = Color.FromArgb(255, 193, 7);
                btnPauseResume.FillColor2 = Color.FromArgb(245, 166, 35);
            }
        }

        /// <summary>
        /// 快照按钮点击
        /// </summary>
        private void BtnSnapshot_Click(object sender, EventArgs e)
        {
            try
            {
                TakeSnapshot();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "保存快照时发生错误");
                MessageHelper.MessageOK($"保存快照失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 历史按钮点击
        /// </summary>
        private void BtnHistory_Click(object sender, EventArgs e)
        {
            try
            {
                ShowSnapshotHistory();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "查看历史时发生错误");
                MessageHelper.MessageOK($"查看历史失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 导出按钮点击
        /// </summary>
        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToCsv();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "导出数据时发生错误");
                MessageHelper.MessageOK($"导出失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 手动修改值按钮点击
        /// </summary>
        private void BtnModifyValue_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedVariableName))
            {
                MessageHelper.MessageOK("请先选择一个变量!", TType.Warn);
                return;
            }

            ModifyVariableValueDialog(_selectedVariableName);
        }

        /// <summary>
        /// 查看完整历史按钮点击
        /// </summary>
        private void BtnViewFullHistory_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedVariableName))
            {
                MessageHelper.MessageOK("请先选择一个变量!", TType.Warn);
                return;
            }

            ShowFullHistory(_selectedVariableName);
        }

        /// <summary>
        /// 清除历史按钮点击
        /// </summary>
        private void BtnClearHistory_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedVariableName))
            {
                MessageHelper.MessageOK("请先选择一个变量!", TType.Warn);
                return;
            }

            var result = MessageBox.Show(
                $"确定要清除变量 '{_selectedVariableName}' 的所有历史记录吗?",
                "确认清除",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                ClearVariableHistory(_selectedVariableName);
            }
        }

        /// <summary>
        /// 帮助按钮点击
        /// </summary>
        private void BtnHelp_Click(object sender, EventArgs e)
        {
            ShowHelpDialog();
        }

        /// <summary>
        /// 刷新数据按钮点击
        /// </summary>
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshVariableData();
            MessageHelper.MessageOK("数据已刷新!", TType.Success);
        }

        /// <summary>
        /// 修改变量按钮点击
        /// </summary>
        private void BtnModify_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedVariableName))
            {
                MessageHelper.MessageOK("请先选择一个变量!", TType.Warn);
                return;
            }

            ModifyVariableValueDialog(_selectedVariableName);
        }

        /// <summary>
        /// 关闭按钮点击
        /// </summary>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 高级功能实现

        /// <summary>
        /// 保存快照
        /// </summary>
        private void TakeSnapshot()
        {
            var snapshot = new VariableSnapshot
            {
                Timestamp = DateTime.Now,
                Variables = [.. _variableManager.GetAllVariables().Select(v => CloneVariable(v))],
                Description = $"快照 {DateTime.Now:yyyy-MM-dd HH:mm:ss}"
            };

            _snapshots.Add(snapshot);

            _logger?.LogInformation("已保存变量快照，共 {Count} 个变量", snapshot.Variables.Count);
            MessageHelper.MessageOK($"快照已保存!\n时间: {snapshot.Timestamp:yyyy-MM-dd HH:mm:ss}\n变量数: {snapshot.Variables.Count}", TType.Success);
        }

        /// <summary>
        /// 克隆变量(深拷贝)
        /// </summary>
        private VarItem_Enhanced CloneVariable(VarItem_Enhanced source)
        {
            return new VarItem_Enhanced
            {
                VarName = source.VarName,
                VarType = source.VarType,
                VarValue = source.VarValue,
                VarText = source.VarText,
                LastUpdated = source.LastUpdated,
                IsAssignedByStep = source.IsAssignedByStep,
                AssignmentType = source.AssignmentType,
                AssignedByStepIndex = source.AssignedByStepIndex,
                AssignedByStepInfo = source.AssignedByStepInfo
            };
        }

        /// <summary>
        /// 显示快照历史
        /// </summary>
        private void ShowSnapshotHistory()
        {
            if (_snapshots.Count == 0)
            {
                MessageHelper.MessageOK("暂无快照记录!", TType.Info);
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            sb.AppendLine("📸 变量快照历史");
            sb.AppendLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

            for (int i = _snapshots.Count - 1; i >= 0; i--)
            {
                var snapshot = _snapshots[i];
                sb.AppendLine($"{i + 1}. {snapshot.Timestamp:yyyy-MM-dd HH:mm:ss}");
                sb.AppendLine($"   变量数: {snapshot.Variables.Count}");
                sb.AppendLine($"   描述: {snapshot.Description}\n");
            }

            sb.AppendLine($"总计 {_snapshots.Count} 个快照");

            MessageHelper.MessageOK(sb.ToString(), TType.Info);
        }

        /// <summary>
        /// 导出为 CSV
        /// </summary>
        private void ExportToCsv()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV文件 (*.csv)|*.csv|所有文件 (*.*)|*.*",
                FileName = $"变量监控_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                Title = "导出变量数据"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var variables = _variableManager.GetAllVariables();
                var csv = new StringBuilder();

                // CSV 标题
                csv.AppendLine("变量名,类型,当前值,最后更新,赋值来源,是否被步骤赋值");

                // 数据行
                foreach (var v in variables)
                {
                    csv.AppendLine($"\"{v.VarName}\",\"{v.VarType}\",\"{FormatValue(v.VarValue)}\"," +
                        $"\"{v.LastUpdated:yyyy-MM-dd HH:mm:ss.fff}\"," +
                        $"\"{v.AssignedByStepInfo ?? "未赋值"}\"," +
                        $"\"{(v.IsAssignedByStep ? "是" : "否")}\"");
                }

                File.WriteAllText(saveFileDialog.FileName, csv.ToString(), Encoding.UTF8);

                _logger?.LogInformation("变量数据已导出到: {FilePath}", saveFileDialog.FileName);
                MessageHelper.MessageOK($"导出成功!\n文件: {saveFileDialog.FileName}", TType.Success);
            }
        }

        /// <summary>
        /// 修改变量值对话框
        /// </summary>
        private void ModifyVariableValueDialog(string varName)
        {
            var variable = _variableManager.FindVariableByName(varName);
            if (variable == null)
            {
                MessageHelper.MessageOK("变量不存在!", TType.Error);
                return;
            }

            // 使用输入对话框
            var inputForm = new Form
            {
                Text = "修改变量值",
                Size = new Size(400, 200),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var lblInfo = new AntdUI.Label
            {
                Text = $"变量名: {variable.VarName}\n类型: {variable.VarType}\n当前值: {FormatValue(variable.VarValue)}",
                Location = new Point(20, 20),
                Size = new Size(350, 60)
            };

            var lblNewValue = new AntdUI.Label
            {
                Text = "新值:",
                Location = new Point(20, 90),
                Size = new Size(50, 20)
            };

            var txtNewValue = new TextBox
            {
                Text = FormatValue(variable.VarValue),
                Location = new Point(80, 88),
                Size = new Size(280, 20)
            };

            var btnOK = new Button
            {
                Text = "确定",
                Location = new Point(200, 120),
                Size = new Size(75, 30),
                DialogResult = DialogResult.OK
            };

            var btnCancel = new Button
            {
                Text = "取消",
                Location = new Point(285, 120),
                Size = new Size(75, 30),
                DialogResult = DialogResult.Cancel
            };

            inputForm.Controls.AddRange(new Control[] { lblInfo, lblNewValue, txtNewValue, btnOK, btnCancel });
            inputForm.AcceptButton = btnOK;
            inputForm.CancelButton = btnCancel;

            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var newValue = ConvertValue(txtNewValue.Text, variable.VarType);
                    ModifyVariableValue(varName, newValue);
                }
                catch (Exception ex)
                {
                    MessageHelper.MessageOK($"修改失败: {ex.Message}", TType.Error);
                }
            }
        }

        /// <summary>
        /// 修改变量值
        /// </summary>
        private void ModifyVariableValue(string varName, object newValue)
        {
            var variable = _variableManager.FindVariableByName(varName);
            if (variable == null)
            {
                throw new Exception("变量不存在");
            }

            // 记录修改前的值
            RecordHistory(variable);

            // 修改值
            variable.VarValue = (string)newValue;
            variable.LastUpdated = DateTime.Now;
            variable.AssignedByStepInfo = "手动修改";

            _logger?.LogInformation("变量 {VarName} 已手动修改为 {NewValue}", varName, newValue);
            MessageHelper.MessageOK($"变量 '{varName}' 已修改为: {FormatValue(newValue)}", TType.Success);

            // 立即刷新显示
            RefreshVariableData();
        }

        /// <summary>
        /// 转换值类型
        /// </summary>
        private object ConvertValue(string input, string targetType)
        {
            return targetType?.ToLower() switch
            {
                "int" => int.Parse(input),
                "double" => double.Parse(input),
                "float" => float.Parse(input),
                "bool" => bool.Parse(input),
                "string" => input,
                _ => input
            };
        }

        /// <summary>
        /// 显示完整历史
        /// </summary>
        private void ShowFullHistory(string varName)
        {
            if (!_variableHistory.TryGetValue(varName, out var history) || history.Count == 0)
            {
                MessageHelper.MessageOK("该变量暂无历史记录!", TType.Info);
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            sb.AppendLine($"变量 '{varName}' 的完整历史");
            sb.AppendLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

            var recentHistory = history.TakeLast(50).Reverse(); // 显示最近50条

            foreach (var entry in recentHistory)
            {
                sb.AppendLine($"{entry.Timestamp:yyyy-MM-dd HH:mm:ss.fff}");
                sb.AppendLine($"  → {FormatValue(entry.Value)}");
                sb.AppendLine($"  来源: {entry.Source}\n");
            }

            if (history.Count > 50)
            {
                sb.AppendLine($"... 还有 {history.Count - 50} 条更早的记录");
            }

            sb.AppendLine($"\n总计 {history.Count} 条历史记录");

            MessageHelper.MessageOK(sb.ToString(), TType.Info);
        }

        /// <summary>
        /// 清除变量历史
        /// </summary>
        private void ClearVariableHistory(string varName)
        {
            if (_variableHistory.ContainsKey(varName))
            {
                _variableHistory[varName].Clear();
                UpdateHistoryList(varName);

                _logger?.LogInformation("已清除变量 {VarName} 的历史记录", varName);
                MessageHelper.MessageOK($"变量 '{varName}' 的历史记录已清除!", TType.Success);
            }
        }

        /// <summary>
        /// 显示帮助对话框
        /// </summary>
        private void ShowHelpDialog()
        {
            var helpText = @"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
📖 变量监控工具 - 使用说明
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
🔹 主要功能

1️ 实时监控
   • 自动刷新显示所有全局变量
   • 支持自定义刷新间隔(100ms~5秒)
   • 可暂停/恢复自动刷新

2️ 值变化追踪
   • 自动高亮显示值发生变化的变量
   • 记录变量值的变化历史
   • 支持查看完整历史记录

3️ 搜索过滤
   • 实时搜索变量名或类型
   • 可选只显示已赋值的变量

4️ 变量详情
   • 显示选中变量的详细信息
   • 查看最近10次值变化历史
   • 显示赋值来源和更新时间

5️ 手动修改
   • 支持手动修改变量值(调试用)
   • 自动验证类型匹配
   • 记录修改操作到历史

6️ 快照功能
   • 保存当前所有变量状态
   • 查看历史快照记录

7️ 数据导出
   • 导出变量数据为CSV文件
   • 包含所有变量信息和当前值

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━


💡 使用技巧

• 在工作流执行时保持监控窗口打开
• 使用搜索功能快速定位变量
• 调试时可暂停刷新并手动修改值
• 定期保存快照以便对比分析
• 导出数据用于离线分析

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━";

            MessageHelper.MessageOK(helpText, TType.Info);
        }

        #endregion

        #region 内部类

        /// <summary>
        /// 变量历史记录项
        /// </summary>
        public class VariableHistoryEntry
        {
            public DateTime Timestamp { get; set; }
            public object Value { get; set; }
            public string Source { get; set; }
        }

        /// <summary>
        /// 变量快照
        /// </summary>
        public class VariableSnapshot
        {
            public DateTime Timestamp { get; set; }
            public List<VarItem_Enhanced> Variables { get; set; }
            public string Description { get; set; }
        }

        #endregion
    }
}