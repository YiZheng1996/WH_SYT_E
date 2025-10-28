using AntdUI;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System.Text;
using Button = AntdUI.Button;
using Label = AntdUI.Label;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 点位定义工具窗体
    /// 用于管理 PLC 模块和点位配置
    /// 支持 Excel/CSV 批量导入
    /// </summary>
    public partial class Form_DefinePoint : Sunny.UI.UIForm
    {
        #region 私有字段

        private readonly IPLCConfigurationService _plcConfigService;
        private readonly ILogger<Form_DefinePoint> _logger;

        // 数据结构: 模块名 -> (ServerName, 点位字典)
        private Dictionary<string, ModuleConfig> _moduleConfigs = new();
        private string _currentModule = null;
        private bool _isModified = false;

        #endregion

        #region 构造函数

        /// <summary>
        /// 依赖注入构造函数
        /// </summary>
        public Form_DefinePoint(
            IPLCConfigurationService plcConfigService,
            ILogger<Form_DefinePoint> logger)
        {
            _plcConfigService = plcConfigService ?? throw new ArgumentNullException(nameof(plcConfigService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            // 注册 GB2312 编码提供程序
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            InitializeComponent();
            InitializeFormStyle();
        }

        /// <summary>
        /// 默认构造函数(设计器用)
        /// </summary>
        public Form_DefinePoint()
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

            // 设置 EPPlus 许可证
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void Form_DefinePoint_Load(object sender, EventArgs e)
        {
            try
            {
                _logger?.LogInformation("点位定义窗体加载");

                // 加载现有配置
                LoadConfiguration();

                // 刷新模块列表
                RefreshModuleList();

                _logger?.LogInformation("点位定义窗体加载完成");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "窗体加载时发生错误");
                MessageHelper.MessageOK($"加载失败: {ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 配置加载和保存

        /// <summary>
        /// 从配置服务加载配置
        /// </summary>
        private void LoadConfiguration()
        {
            try
            {
                _logger?.LogInformation("开始加载PLC配置");

                var config = _plcConfigService.Configuration;

                _moduleConfigs.Clear();

                foreach (var module in config)
                {
                    var moduleName = module.Key;
                    var moduleParams = module.Value;

                    var moduleConfig = new ModuleConfig
                    {
                        ModuleName = moduleName,
                        Points = new Dictionary<string, string>()
                    };

                    // 提取 ServerName 和点位
                    foreach (var param in moduleParams)
                    {
                        if (param.Key == "ServerName")
                        {
                            moduleConfig.ServerName = param.Value;
                        }
                        else
                        {
                            moduleConfig.Points[param.Key] = param.Value;
                        }
                    }

                    _moduleConfigs[moduleName] = moduleConfig;
                }

                _logger?.LogInformation("成功加载 {Count} 个模块配置", _moduleConfigs.Count);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载配置时发生错误");
                throw;
            }
        }

        /// <summary>
        /// 保存配置到文件
        /// </summary>
        private bool SaveConfiguration()
        {
            try
            {
                _logger?.LogInformation("开始保存PLC配置");

                // 保存前先同步当前编辑的点位到 _moduleConfigs
                SyncCurrentModulePoints();

                // 获取配置文件路径
                var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules", "MyModules.ini");

                // 确保目录存在
                var directory = Path.GetDirectoryName(configPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // 使用 StringBuilder 构建 INI 内容
                var sb = new StringBuilder();

                foreach (var module in _moduleConfigs.Values)
                {
                    // 写入模块节
                    sb.AppendLine($"[{module.ModuleName}]");

                    // 写入 ServerName
                    if (!string.IsNullOrWhiteSpace(module.ServerName))
                    {
                        sb.AppendLine($"ServerName={module.ServerName}");
                    }

                    // 写入点位
                    foreach (var point in module.Points)
                    {
                        sb.AppendLine($"{point.Key}={point.Value}");
                    }

                    // 空行分隔
                    sb.AppendLine();
                }

                // 使用 GB2312 编码保存
                File.WriteAllText(configPath, sb.ToString(), Encoding.GetEncoding("GB2312"));

                _isModified = false;

                _logger?.LogInformation("配置保存成功,共 {Count} 个模块", _moduleConfigs.Count);

                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "保存配置时发生错误");
                MessageHelper.MessageOK($"保存失败: {ex.Message}", TType.Error);
                return false;
            }
        }

        /// <summary>
        /// 同步当前模块的点位数据从 DataGridView 到 _moduleConfigs
        /// </summary>
        private void SyncCurrentModulePoints()
        {
            try
            {
                if (string.IsNullOrEmpty(_currentModule) || !_moduleConfigs.ContainsKey(_currentModule))
                {
                    return;
                }

                var moduleConfig = _moduleConfigs[_currentModule];

                // 清空现有点位
                moduleConfig.Points.Clear();

                // 从 DataGridView 读取所有点位
                foreach (DataGridViewRow row in dgvPoints.Rows)
                {
                    if (row.IsNewRow) continue;

                    var pointName = row.Cells["ColPointName"].Value?.ToString()?.Trim();
                    var pointAddress = row.Cells["ColPointAddress"].Value?.ToString()?.Trim();

                    if (!string.IsNullOrWhiteSpace(pointName) && !string.IsNullOrWhiteSpace(pointAddress))
                    {
                        moduleConfig.Points[pointName] = pointAddress;
                    }
                }

                _logger?.LogDebug("同步模块 {ModuleName} 的 {Count} 个点位到内存",
                    _currentModule, moduleConfig.Points.Count);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "同步当前模块点位时发生错误");
            }
        }

        #endregion

        #region 模块管理

        /// <summary>
        /// 刷新模块列表
        /// </summary>
        private void RefreshModuleList()
        {
            try
            {
                var selectedModule = _currentModule;

                lstModules.Items.Clear();

                foreach (var module in _moduleConfigs.Keys.OrderBy(k => k))
                {
                    lstModules.Items.Add(module);
                }

                // 恢复选择
                if (!string.IsNullOrEmpty(selectedModule) && lstModules.Items.Contains(selectedModule))
                {
                    lstModules.SelectedItem = selectedModule;
                }
                else if (lstModules.Items.Count > 0)
                {
                    lstModules.SelectedIndex = 0;
                }

                UpdateStatusLabel();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "刷新模块列表时发生错误");
            }
        }

        /// <summary>
        /// 模块选择变化事件
        /// </summary>
        private void LstModules_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 切换前先同步当前模块的点位
            if (!string.IsNullOrEmpty(_currentModule))
            {
                SyncCurrentModulePoints();
            }

            if (lstModules.SelectedItem != null)
            {
                _currentModule = lstModules.SelectedItem.ToString();
                LoadModulePoints(_currentModule);
            }
        }

        /// <summary>
        /// 加载模块的点位
        /// </summary>
        private void LoadModulePoints(string moduleName)
        {
            try
            {
                if (string.IsNullOrEmpty(moduleName) || !_moduleConfigs.ContainsKey(moduleName))
                {
                    dgvPoints.Rows.Clear();
                    txtServerName.Text = "";
                    return;
                }

                var moduleConfig = _moduleConfigs[moduleName];

                // 加载 ServerName
                txtServerName.Text = moduleConfig.ServerName ?? "";

                // 加载点位
                dgvPoints.Rows.Clear();
                foreach (var point in moduleConfig.Points)
                {
                    dgvPoints.Rows.Add(point.Key, point.Value);
                }

                UpdateStatusLabel();

                _logger?.LogDebug("加载模块 {ModuleName} 的 {Count} 个点位", moduleName, moduleConfig.Points.Count);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "加载模块点位时发生错误");
            }
        }

        /// <summary>
        /// 添加模块按钮点击
        /// </summary>
        private void BtnAddModule_Click(object sender, EventArgs e)
        {
            try
            {
                var inputForm = CreateInputDialog("添加模块", "请输入模块名称:", "");
                VarHelper.ShowDialogWithOverlay(this, inputForm);

                if (inputForm.DialogResult == DialogResult.OK)
                {
                    var moduleName = inputForm.Tag as string;

                    if (string.IsNullOrWhiteSpace(moduleName))
                    {
                        MessageHelper.MessageOK("模块名称不能为空!", TType.Warn);
                        return;
                    }

                    if (_moduleConfigs.ContainsKey(moduleName))
                    {
                        MessageHelper.MessageOK($"模块 '{moduleName}' 已存在!", TType.Warn);
                        return;
                    }

                    // 添加新模块
                    _moduleConfigs[moduleName] = new ModuleConfig
                    {
                        ModuleName = moduleName,
                        ServerName = "",
                        Points = []
                    };

                    _isModified = true;

                    RefreshModuleList();

                    // 选中新添加的模块
                    lstModules.SelectedItem = moduleName;

                    _logger?.LogInformation("添加模块: {ModuleName}", moduleName);
                    MessageHelper.MessageOK($"模块 '{moduleName}' 添加成功!", TType.Success);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "添加模块时发生错误");
                MessageHelper.MessageOK($"添加失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 删除模块按钮点击
        /// </summary>
        private void BtnDeleteModule_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_currentModule))
                {
                    MessageHelper.MessageOK("请先选择要删除的模块!", TType.Warn);
                    return;
                }

                var result = MessageBox.Show(
                    $"确定要删除模块 '{_currentModule}' 吗?\n该操作将删除模块下的所有点位配置!",
                    "确认删除",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    _moduleConfigs.Remove(_currentModule);
                    _isModified = true;

                    _logger?.LogInformation("删除模块: {ModuleName}", _currentModule);

                    var deletedModule = _currentModule;
                    _currentModule = null;

                    RefreshModuleList();

                    MessageHelper.MessageOK($"模块 '{deletedModule}' 已删除!", TType.Success);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "删除模块时发生错误");
                MessageHelper.MessageOK($"删除失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 重命名模块按钮点击
        /// </summary>
        private void BtnRenameModule_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_currentModule))
                {
                    MessageHelper.MessageOK("请先选择要重命名的模块!", TType.Warn);
                    return;
                }

                var inputForm = CreateInputDialog("重命名模块", "请输入新的模块名称:", _currentModule);
                VarHelper.ShowDialogWithOverlay(this, inputForm);
                if (inputForm.DialogResult == DialogResult.OK)
                {
                    var newName = inputForm.Tag as string;

                    if (string.IsNullOrWhiteSpace(newName))
                    {
                        MessageHelper.MessageOK("模块名称不能为空!", TType.Warn);
                        return;
                    }

                    if (newName == _currentModule)
                    {
                        return;
                    }

                    if (_moduleConfigs.ContainsKey(newName))
                    {
                        MessageHelper.MessageOK($"模块 '{newName}' 已存在!", TType.Warn);
                        return;
                    }

                    // 重命名模块
                    var moduleConfig = _moduleConfigs[_currentModule];
                    moduleConfig.ModuleName = newName;

                    _moduleConfigs.Remove(_currentModule);
                    _moduleConfigs[newName] = moduleConfig;

                    _isModified = true;

                    _logger?.LogInformation("重命名模块: {OldName} -> {NewName}", _currentModule, newName);

                    _currentModule = newName;
                    RefreshModuleList();

                    MessageHelper.MessageOK($"模块已重命名为 '{newName}'!", TType.Success);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "重命名模块时发生错误");
                MessageHelper.MessageOK($"重命名失败: {ex.Message}", TType.Error);
            }
        }

        #endregion

        #region ServerName 管理

        /// <summary>
        /// 设置 ServerName 按钮点击
        /// </summary>
        private void BtnSetServerName_Click(object sender, EventArgs e)
        {
            try
            {
                var serverName = txtServerName.Text.Trim();

                if (string.IsNullOrWhiteSpace(serverName))
                {
                    MessageHelper.MessageOK("ServerName 不能为空!", TType.Warn);
                    return;
                }

                // 显示选择对话框
                var dialog = new UIForm
                {
                    Text = "应用 ServerName",
                    Size = new Size(400, 200),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false,
                    ControlBox = false,
                    ShowIcon = false,
                    TitleColor = Color.FromArgb(65, 100, 204),
                    BackColor = Color.White,
                };

                var lblPrompt = new Label
                {
                    Text = "请选择应用范围:",
                    Location = new Point(20, 50),
                    Size = new Size(350, 20),
                    Font = new Font("微软雅黑", 10F)
                };

                var rbCurrent = new RadioButton
                {
                    Text = $"仅应用到当前模块 ({_currentModule})",
                    Location = new Point(40, 80),
                    Size = new Size(320, 25),
                    Checked = true,
                    Font = new Font("微软雅黑", 9F)
                };

                var rbAll = new RadioButton
                {
                    Text = $"应用到所有模块 (共 {_moduleConfigs.Count} 个)",
                    Location = new Point(40, 110),
                    Size = new Size(320, 25),
                    Font = new Font("微软雅黑", 9F)
                };

                var btnOK = new UIButton
                {
                    Text = "确定",
                    Location = new Point(190, 150),
                    Size = new Size(85, 35),
                    DialogResult = DialogResult.OK
                };

                var btnCancel = new UIButton
                {
                    Text = "取消",
                    Location = new Point(285, 150),
                    Size = new Size(85, 35),
                    DialogResult = DialogResult.Cancel
                };

                dialog.Controls.AddRange([lblPrompt, rbCurrent, rbAll, btnOK, btnCancel]);
                dialog.AcceptButton = btnOK;
                dialog.CancelButton = btnCancel;
                VarHelper.ShowDialogWithOverlay(this, dialog);
                if (dialog.DialogResult == DialogResult.OK)
                {
                    if (rbCurrent.Checked)
                    {
                        // 应用到当前模块
                        if (string.IsNullOrEmpty(_currentModule))
                        {
                            MessageHelper.MessageOK("请先选择模块!", TType.Warn);
                            return;
                        }

                        _moduleConfigs[_currentModule].ServerName = serverName;
                        _isModified = true;

                        _logger?.LogInformation("设置模块 {ModuleName} 的 ServerName: {ServerName}",
                            _currentModule, serverName);
                        MessageHelper.MessageOK($"已应用到当前模块:\n{_currentModule}", TType.Success);
                    }
                    else
                    {
                        // 应用到所有模块
                        var count = 0;
                        foreach (var module in _moduleConfigs.Values)
                        {
                            module.ServerName = serverName;
                            count++;
                        }

                        _isModified = true;

                        _logger?.LogInformation("批量设置 {Count} 个模块的 ServerName: {ServerName}",
                            count, serverName);
                        MessageHelper.MessageOK($"已应用到所有 {count} 个模块!", TType.Success);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "设置ServerName时发生错误");
                MessageHelper.MessageOK($"设置失败: {ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 点位管理

        /// <summary>
        /// 添加点位按钮点击
        /// </summary>
        private void BtnAddPoint_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_currentModule))
                {
                    MessageHelper.MessageOK("请先选择模块!", TType.Warn);
                    return;
                }

                var dialog = CreatePointInputDialog("", "");
                VarHelper.ShowDialogWithOverlay(this, dialog);
                if (dialog.DialogResult == DialogResult.OK)
                {
                    var pointData = dialog.Tag as Tuple<string, string>;
                    var pointName = pointData.Item1;
                    var pointAddress = pointData.Item2;

                    if (string.IsNullOrWhiteSpace(pointName) || string.IsNullOrWhiteSpace(pointAddress))
                    {
                        MessageHelper.MessageOK("点位名称和地址不能为空!", TType.Warn);
                        return;
                    }

                    var moduleConfig = _moduleConfigs[_currentModule];

                    if (moduleConfig.Points.ContainsKey(pointName))
                    {
                        MessageHelper.MessageOK($"点位 '{pointName}' 已存在!", TType.Warn);
                        return;
                    }

                    // 添加点位
                    moduleConfig.Points[pointName] = pointAddress;
                    _isModified = true;

                    // 刷新显示
                    dgvPoints.Rows.Add(pointName, pointAddress);

                    UpdateStatusLabel();

                    _logger?.LogInformation("添加点位: {ModuleName}.{PointName} = {Address}",
                        _currentModule, pointName, pointAddress);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "添加点位时发生错误");
                MessageHelper.MessageOK($"添加失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 删除点位按钮点击
        /// </summary>
        private void BtnDeletePoint_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_currentModule))
                {
                    MessageHelper.MessageOK("请先选择模块!", TType.Warn);
                    return;
                }

                if (dgvPoints.SelectedRows.Count == 0)
                {
                    MessageHelper.MessageOK("请先选择要删除的点位!", TType.Warn);
                    return;
                }

                var selectedRows = dgvPoints.SelectedRows.Cast<DataGridViewRow>().ToList();
                var deleteCount = selectedRows.Count;

                var result = MessageBox.Show(
                    $"确定要删除选中的 {deleteCount} 个点位吗?",
                    "确认删除",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    var moduleConfig = _moduleConfigs[_currentModule];

                    foreach (var row in selectedRows)
                    {
                        var pointName = row.Cells["ColPointName"].Value?.ToString();
                        if (!string.IsNullOrEmpty(pointName))
                        {
                            moduleConfig.Points.Remove(pointName);
                            dgvPoints.Rows.Remove(row);
                        }
                    }

                    _isModified = true;
                    UpdateStatusLabel();

                    _logger?.LogInformation("删除 {Count} 个点位", deleteCount);
                    MessageHelper.MessageOK($"已删除 {deleteCount} 个点位!", TType.Success);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "删除点位时发生错误");
                MessageHelper.MessageOK($"删除失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 清空点位按钮点击
        /// </summary>
        private void BtnClearPoints_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_currentModule))
                {
                    MessageHelper.MessageOK("请先选择模块!", TType.Warn);
                    return;
                }

                var moduleConfig = _moduleConfigs[_currentModule];
                var pointCount = moduleConfig.Points.Count;

                if (pointCount == 0)
                {
                    MessageHelper.MessageOK("当前模块没有点位!", TType.Info);
                    return;
                }

                var result = MessageBox.Show(
                    $"确定要清空模块 '{_currentModule}' 的所有点位吗?\n共 {pointCount} 个点位将被删除!",
                    "确认清空",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    moduleConfig.Points.Clear();
                    dgvPoints.Rows.Clear();
                    _isModified = true;

                    UpdateStatusLabel();

                    _logger?.LogInformation("清空模块 {ModuleName} 的所有点位", _currentModule);
                    MessageHelper.MessageOK($"已清空 {pointCount} 个点位!", TType.Success);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "清空点位时发生错误");
                MessageHelper.MessageOK($"清空失败: {ex.Message}", TType.Error);
            }
        }

        #endregion

        #region Excel 导入导出

        /// <summary>
        /// 导入 Excel 按钮点击
        /// </summary>
        private void BtnImportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "Excel文件 (*.xlsx)|*.xlsx|所有文件 (*.*)|*.*",
                    Title = "选择Excel文件 - 导入所有模块点位"
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var result = ImportAllModulesFromExcel(openFileDialog.FileName);

                    if (result.TotalPoints > 0)
                    {
                        // 刷新当前模块显示
                        if (!string.IsNullOrEmpty(_currentModule))
                        {
                            LoadModulePoints(_currentModule);
                        }

                        RefreshModuleList();

                        var message = $"导入完成!\n\n" +
                            $"📦 模块数: {result.ModuleCount}\n" +
                            $"📍 点位数: {result.TotalPoints}\n" +
                            $"✅ 新增: {result.AddedPoints}\n" +
                            $"⚠️ 跳过: {result.SkippedPoints}";

                        MessageHelper.MessageOK(message, TType.Success);
                    }
                    else
                    {
                        MessageHelper.MessageOK("未找到有效的点位数据!", TType.Warn);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "导入Excel时发生错误");
                MessageHelper.MessageOK($"导入失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 从 Excel 导入所有模块的点位
        /// </summary>
        private ImportResult ImportAllModulesFromExcel(string filePath)
        {
            var result = new ImportResult();

            try
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension?.Rows ?? 0;

                    // 从第2行开始读取(第1行是标题)
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var moduleName = worksheet.Cells[row, 1].Value?.ToString()?.Trim();
                        var pointName = worksheet.Cells[row, 2].Value?.ToString()?.Trim();
                        var pointAddress = worksheet.Cells[row, 3].Value?.ToString()?.Trim();
                        var serverName = worksheet.Cells[row, 4].Value?.ToString()?.Trim();

                        if (string.IsNullOrWhiteSpace(moduleName) ||
                            string.IsNullOrWhiteSpace(pointName) ||
                            string.IsNullOrWhiteSpace(pointAddress))
                        {
                            continue;
                        }

                        // 如果模块不存在,创建新模块
                        if (!_moduleConfigs.ContainsKey(moduleName))
                        {
                            _moduleConfigs[moduleName] = new ModuleConfig
                            {
                                ModuleName = moduleName,
                                ServerName = serverName ?? "",
                                Points = new Dictionary<string, string>()
                            };
                            result.ModuleCount++;
                        }

                        var moduleConfig = _moduleConfigs[moduleName];

                        // 更新 ServerName (如果提供)
                        if (!string.IsNullOrWhiteSpace(serverName))
                        {
                            moduleConfig.ServerName = serverName;
                        }

                        // 添加点位
                        if (!moduleConfig.Points.ContainsKey(pointName))
                        {
                            moduleConfig.Points[pointName] = pointAddress;
                            result.AddedPoints++;
                        }
                        else
                        {
                            result.SkippedPoints++;
                        }

                        result.TotalPoints++;
                    }

                    _isModified = true;
                }

                _logger?.LogInformation("从Excel导入 {ModuleCount} 个模块, {TotalPoints} 个点位",
                    result.ModuleCount, result.TotalPoints);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "解析Excel文件时发生错误");
                throw new Exception($"解析Excel文件失败: {ex.Message}", ex);
            }

            return result;
        }


        /// <summary>
        /// 从 Excel 导入点位
        /// </summary>
        private List<PointItem> ImportFromExcel(string filePath)
        {
            var points = new List<PointItem>();

            try
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension?.Rows ?? 0;

                    // 跳过第一行标题
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var pointName = worksheet.Cells[row, 1].Value?.ToString()?.Trim();
                        var pointAddress = worksheet.Cells[row, 2].Value?.ToString()?.Trim();

                        if (!string.IsNullOrWhiteSpace(pointName) && !string.IsNullOrWhiteSpace(pointAddress))
                        {
                            points.Add(new PointItem
                            {
                                Name = pointName,
                                Address = pointAddress
                            });
                        }
                    }
                }

                _logger?.LogInformation("从Excel导入 {Count} 个点位", points.Count);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "解析Excel文件时发生错误");
                throw new Exception($"解析Excel文件失败: {ex.Message}", ex);
            }

            return points;
        }

        /// <summary>
        /// 导入 CSV 按钮点击
        /// </summary>
        private void BtnImportCsv_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_currentModule))
                {
                    MessageHelper.MessageOK("请先选择模块!", TType.Warn);
                    return;
                }

                var openFileDialog = new OpenFileDialog
                {
                    Filter = "CSV文件 (*.csv)|*.csv|所有文件 (*.*)|*.*",
                    Title = "选择CSV文件"
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var points = ImportFromCsv(openFileDialog.FileName);

                    if (points.Count > 0)
                    {
                        AddImportedPoints(points);
                        MessageHelper.MessageOK($"成功导入 {points.Count} 个点位!", TType.Success);
                    }
                    else
                    {
                        MessageHelper.MessageOK("未找到有效的点位数据!", TType.Warn);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "导入CSV时发生错误");
                MessageHelper.MessageOK($"导入失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 从 CSV 导入点位
        /// </summary>
        private List<PointItem> ImportFromCsv(string filePath)
        {
            var points = new List<PointItem>();

            try
            {
                var lines = File.ReadAllLines(filePath, Encoding.UTF8);

                // 跳过第一行标题
                for (int i = 1; i < lines.Length; i++)
                {
                    var parts = lines[i].Split(',');

                    if (parts.Length >= 2)
                    {
                        var pointName = parts[0].Trim().Trim('"');
                        var pointAddress = parts[1].Trim().Trim('"');

                        if (!string.IsNullOrWhiteSpace(pointName) && !string.IsNullOrWhiteSpace(pointAddress))
                        {
                            points.Add(new PointItem
                            {
                                Name = pointName,
                                Address = pointAddress
                            });
                        }
                    }
                }

                _logger?.LogInformation("从CSV导入 {Count} 个点位", points.Count);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "解析CSV文件时发生错误");
                throw new Exception($"解析CSV文件失败: {ex.Message}", ex);
            }

            return points;
        }

        /// <summary>
        /// 添加导入的点位
        /// </summary>
        private void AddImportedPoints(List<PointItem> points)
        {
            var moduleConfig = _moduleConfigs[_currentModule];
            var addedCount = 0;
            var skippedCount = 0;

            foreach (var point in points)
            {
                if (!moduleConfig.Points.ContainsKey(point.Name))
                {
                    moduleConfig.Points[point.Name] = point.Address;
                    addedCount++;
                }
                else
                {
                    skippedCount++;
                }
            }

            if (addedCount > 0)
            {
                _isModified = true;
                LoadModulePoints(_currentModule);
            }

            _logger?.LogInformation("导入点位: 添加 {Added} 个, 跳过 {Skipped} 个", addedCount, skippedCount);

            if (skippedCount > 0)
            {
                MessageHelper.MessageOK($"添加了 {addedCount} 个点位\n跳过了 {skippedCount} 个重复点位", TType.Info);
            }
        }

        /// <summary>
        /// 导出按钮点击
        /// </summary>
        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (_moduleConfigs.Count == 0)
                {
                    MessageHelper.MessageOK("没有可导出的数据!", TType.Warn);
                    return;
                }

                // 统计总点位数
                var totalPoints = _moduleConfigs.Values.Sum(m => m.Points.Count);

                if (totalPoints == 0)
                {
                    MessageHelper.MessageOK("所有模块都没有点位可导出!", TType.Warn);
                    return;
                }

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel文件 (*.xlsx)|*.xlsx|CSV文件 (*.csv)|*.csv",
                    FileName = $"PLC点位配置_全部_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    Title = "导出所有模块点位配置"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                    {
                        ExportAllModulesToExcel(saveFileDialog.FileName);
                    }

                    MessageHelper.MessageOK(
                        $"导出成功!\n\n" +
                        $"📦 模块数: {_moduleConfigs.Count}\n" +
                        $"📍 点位数: {totalPoints}\n" +
                        $"📁 文件: {Path.GetFileName(saveFileDialog.FileName)}",
                        TType.Success);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "导出时发生错误");
                MessageHelper.MessageOK($"导出失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 导出所有模块到 Excel
        /// </summary>
        private void ExportAllModulesToExcel(string filePath)
        {
            try
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("点位配置");

                    // 设置标题行
                    worksheet.Cells[1, 1].Value = "模块名称";
                    worksheet.Cells[1, 2].Value = "点位名称";
                    worksheet.Cells[1, 3].Value = "点位地址";
                    worksheet.Cells[1, 4].Value = "ServerName";

                    // 设置标题样式
                    using (var range = worksheet.Cells[1, 1, 1, 4])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(65, 100, 204));
                        range.Style.Font.Color.SetColor(Color.White);
                        range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    }

                    // 填充数据
                    int row = 2;
                    foreach (var module in _moduleConfigs.Values.OrderBy(m => m.ModuleName))
                    {
                        foreach (var point in module.Points.OrderBy(p => p.Key))
                        {
                            worksheet.Cells[row, 1].Value = module.ModuleName;
                            worksheet.Cells[row, 2].Value = point.Key;
                            worksheet.Cells[row, 3].Value = point.Value;
                            worksheet.Cells[row, 4].Value = module.ServerName;
                            row++;
                        }
                    }

                    // 自动调整列宽
                    worksheet.Cells.AutoFitColumns();

                    // 冻结首行
                    worksheet.View.FreezePanes(2, 1);

                    // 保存文件
                    package.SaveAs(new FileInfo(filePath));
                }

                _logger?.LogInformation("导出到Excel: {FilePath}, 共 {Count} 个模块",
                    filePath, _moduleConfigs.Count);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "导出到Excel时发生错误");
                throw;
            }
        }

        /// <summary>
        /// 导出到 Excel
        /// </summary>
        private void ExportToExcel(string filePath, ModuleConfig moduleConfig)
        {
            try
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("点位配置");

                    // 设置标题行
                    worksheet.Cells[1, 1].Value = "点位名称";
                    worksheet.Cells[1, 2].Value = "点位地址";

                    // 设置标题样式
                    using (var range = worksheet.Cells[1, 1, 1, 2])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(65, 100, 204));
                        range.Style.Font.Color.SetColor(Color.White);
                    }

                    // 填充数据
                    int row = 2;
                    foreach (var point in moduleConfig.Points)
                    {
                        worksheet.Cells[row, 1].Value = point.Key;
                        worksheet.Cells[row, 2].Value = point.Value;
                        row++;
                    }

                    // 自动调整列宽
                    worksheet.Cells.AutoFitColumns();

                    // 保存文件
                    package.SaveAs(new FileInfo(filePath));
                }

                _logger?.LogInformation("导出到Excel: {FilePath}, 共 {Count} 个点位", filePath, moduleConfig.Points.Count);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "导出到Excel时发生错误");
                throw;
            }
        }

        /// <summary>
        /// 导出到 CSV
        /// </summary>
        private void ExportToCsv(string filePath, ModuleConfig moduleConfig)
        {
            try
            {
                var csv = new StringBuilder();

                // 标题行
                csv.AppendLine("点位名称,点位地址");

                // 数据行
                foreach (var point in moduleConfig.Points)
                {
                    csv.AppendLine($"\"{point.Key}\",\"{point.Value}\"");
                }

                File.WriteAllText(filePath, csv.ToString(), Encoding.GetEncoding("GB2312"));

                _logger?.LogInformation("导出到CSV: {FilePath}, 共 {Count} 个点位", filePath, moduleConfig.Points.Count);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "导出到CSV时发生错误");
                throw;
            }
        }

        #endregion

        #region 底部按钮

        /// <summary>
        /// 帮助按钮点击
        /// </summary>
        private void BtnHelp_Click(object sender, EventArgs e)
        {
            ShowHelpDialog();
        }

        /// <summary>
        /// 保存按钮点击
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_isModified)
                {
                    MessageHelper.MessageOK("没有需要保存的修改!", TType.Info);
                    return;
                }

                var result = MessageBox.Show(
                    "确定要保存配置到文件吗?",
                    "确认保存",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (SaveConfiguration())
                    {
                        MessageHelper.MessageOK("配置保存成功!", TType.Success);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "保存配置时发生错误");
                MessageHelper.MessageOK($"保存失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 取消/关闭按钮点击
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (_isModified)
            {
                var result = MessageBox.Show(
                    "有未保存的修改,确定要关闭吗?",
                    "确认关闭",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    return;
                }
            }

            this.Close();
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 更新状态标签
        /// </summary>
        private void UpdateStatusLabel()
        {
            if (string.IsNullOrEmpty(_currentModule))
            {
                lblStatus.Text = "提示: 选择模块后可以编辑点位,支持Excel/CSV批量导入";
            }
            else
            {
                var moduleConfig = _moduleConfigs[_currentModule];
                lblStatus.Text = $"当前模块: {_currentModule}  |  " +
                    $"点位数: {moduleConfig.Points.Count}  |  " +
                    $"ServerName: {(string.IsNullOrEmpty(moduleConfig.ServerName) ? "未设置" : moduleConfig.ServerName)}";
            }
        }

        /// <summary>
        /// 创建输入对话框
        /// </summary>
        private Form CreateInputDialog(string title, string label, string defaultValue)
        {
            var form = new UIForm
            {
                Text = title,
                Size = new Size(450, 180),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ControlBox = false,
                ShowIcon = false,
                TitleColor = Color.FromArgb(65, 100, 204),
                BackColor = Color.White,
            };

            var lblPrompt = new Label
            {
                Text = label,
                Location = new Point(20, 50),
                Size = new Size(400, 20)
            };

            var txtInput = new UITextBox
            {
                Text = defaultValue,
                Location = new Point(20, 80),
                Size = new Size(400, 30)
            };

            var btnOK = new UIButton
            {
                Text = "确定",
                Location = new Point(240, 130),
                Size = new Size(85, 35),
                DialogResult = DialogResult.OK
            };

            var btnCancel = new UIButton
            {
                Text = "取消",
                Location = new Point(335, 130),
                Size = new Size(85, 35),
                DialogResult = DialogResult.Cancel
            };

            btnOK.Click += (s, e) => { form.Tag = txtInput.Text.Trim(); };

            form.Controls.AddRange([lblPrompt, txtInput, btnOK, btnCancel]);
            form.AcceptButton = btnOK;
            form.CancelButton = btnCancel;

            return form;
        }

        /// <summary>
        /// 创建点位输入对话框
        /// </summary>
        private Form CreatePointInputDialog(string defaultName, string defaultAddress)
        {
            var form = new UIForm
            {
                Text = "添加点位",
                Size = new Size(500, 230),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ControlBox = false,
                ShowIcon = false,
                TitleColor = Color.FromArgb(65, 100, 204),
                BackColor = Color.White,
            };

            var lblName = new Label
            {
                Text = "点位名称:",
                Location = new Point(20, 65),
                Size = new Size(100, 20)
            };

            var txtName = new UITextBox
            {
                Text = defaultName,
                Location = new Point(120, 60),
                Size = new Size(350, 30),
            };

            var lblAddress = new Label
            {
                Text = "点位地址:",
                Location = new Point(20, 115),
                Size = new Size(100, 20)
            };

            var txtAddress = new UITextBox
            {
                Text = defaultAddress,
                Location = new Point(120, 110),
                Size = new Size(350, 30)
            };

            var lblExample = new Label
            {
                Text = "示例: SMART.PLC.DO.CD000",
                Location = new Point(120, 145),
                Size = new Size(350, 20),
                ForeColor = Color.Gray,
                Font = new Font("微软雅黑", 9F)
            };

            var btnOK = new UIButton
            {
                Text = "确定",
                Location = new Point(290, 180),
                Size = new Size(85, 30),
                DialogResult = DialogResult.OK
            };

            var btnCancel = new UIButton
            {
                Text = "取消",
                Location = new Point(385, 180),
                Size = new Size(85, 30),
                DialogResult = DialogResult.Cancel
            };

            btnOK.Click += (s, e) =>
            {
                form.Tag = Tuple.Create(txtName.Text.Trim(), txtAddress.Text.Trim());
            };

            form.Controls.AddRange(new Control[] { lblName, txtName, lblAddress, txtAddress, lblExample, btnOK, btnCancel });
            form.AcceptButton = btnOK;
            form.CancelButton = btnCancel;

            return form;
        }

        /// <summary>
        /// 显示帮助对话框
        /// </summary>
        private void ShowHelpDialog()
        {
            var helpText = @"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
📖 点位定义工具 - 使用说明
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
🔹 基本操作

1️ 模块管理
   • 添加模块: 点击左侧""添加""按钮
   • 删除模块: 选中模块后点击""删除""
   • 重命名: 点击""重命名""按钮

2️ ServerName 设置
   • 在顶部输入框输入 ServerName
   • 点击""应用到当前模块""保存
   • ServerName 对整个模块通用

3️ 点位管理
   • 添加点位: 点击""添加点位""按钮
   • 删除点位: 选中后点击""删除点位""
   • 清空模块: 清除当前模块所有点位

📥 Excel 导入
   • 文件格式: .xlsx
   • 第一行: 标题行(点位名称,点位地址)
   • 第二行起: 数据行
   • 示例:
     点位名称     | 点位地址
     EP01        | SMART.PLC.AO.CA00
     EP02        | SMART.PLC.AO.CA01

🔹 数据导出

   • 支持导出为 Excel
   • 包含当前模块的所有点位
   • 可用于备份或迁移
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
💡 使用技巧
• 建议使用有意义的模块名称
  如: ""AO控制"", ""DO控制"", ""AI检测""

• ServerName 通常格式:
  KEPware.KEPServerEx.V4

• 点位地址格式参考图片要求:
  SMART.PLC.AO.CA00
  SMART.PLC.DO.CD000

• 批量导入前建议先导出模板
  使用""导出Excel""生成标准格式

• 重复的点位名称会被跳过
  不会覆盖现有点位
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
⚠️ 注意事项
1. 修改后记得点击""保存到配置""
2. ServerName 对整个模块通用
3. 点位名称在同一模块内不能重复
4. 导入时会跳过重复的点位
5. 删除操作不可恢复,请谨慎操作
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━";
            MessageHelper.MessageOK(helpText, TType.Info);
        }

        #endregion

        #region 内部类

        /// <summary>
        /// 模块配置
        /// </summary>
        private class ModuleConfig
        {
            public string ModuleName { get; set; }
            public string ServerName { get; set; }
            public Dictionary<string, string> Points { get; set; }
        }

        /// <summary>
        /// 点位项
        /// </summary>
        private class PointItem
        {
            public string Name { get; set; }
            public string Address { get; set; }
        }

        /// <summary>
        /// 导入结果
        /// </summary>
        private class ImportResult
        {
            public int ModuleCount { get; set; }
            public int TotalPoints { get; set; }
            public int AddedPoints { get; set; }
            public int SkippedPoints { get; set; }
        }
        #endregion

        #region 事件
        /// <summary>
        /// DataGridView 单元格值变化事件
        /// </summary>
        private void DgvPoints_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || string.IsNullOrEmpty(_currentModule))
                return;

            _isModified = true;

            // 实时同步到 _moduleConfigs
            SyncCurrentModulePoints();
        }

        /// <summary>
        /// DataGridView 删除行事件
        /// </summary>
        private void DgvPoints_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (string.IsNullOrEmpty(_currentModule))
                return;

            _isModified = true;

            // 实时同步到 _moduleConfigs
            SyncCurrentModulePoints();
        }

        #endregion

        #region 下载模板
        /// <summary>
        /// 下载模板按钮点击
        /// </summary>
        private void BtnDownloadTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel文件 (*.xlsx)|*.xlsx|CSV文件 (*.csv)|*.csv",
                    FileName = $"PLC点位配置_模板_{DateTime.Now:yyyyMMdd}.xlsx",
                    Title = "下载导入模板"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                    {
                        CreateExcelTemplate(saveFileDialog.FileName);
                    }
                    else
                    {
                        CreateCsvTemplate(saveFileDialog.FileName);
                    }

                    var result = MessageBox.Show(
                        $"模板下载成功!\n\n文件: {Path.GetFileName(saveFileDialog.FileName)}\n\n是否立即打开文件?",
                        "模板下载",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = saveFileDialog.FileName,
                            UseShellExecute = true
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "下载模板时发生错误");
                MessageHelper.MessageOK($"下载失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 创建 Excel 模板
        /// </summary>
        private void CreateExcelTemplate(string filePath)
        {
            try
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("点位配置模板");

                    // 设置标题行
                    worksheet.Cells[1, 1].Value = "模块名称";
                    worksheet.Cells[1, 2].Value = "点位名称";
                    worksheet.Cells[1, 3].Value = "点位地址";
                    worksheet.Cells[1, 4].Value = "ServerName";

                    // 设置标题样式
                    using (var range = worksheet.Cells[1, 1, 1, 4])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(65, 100, 204));
                        range.Style.Font.Color.SetColor(Color.White);
                        range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    }

                    // 添加示例数据
                    worksheet.Cells[2, 1].Value = "AO控制";
                    worksheet.Cells[2, 2].Value = "EP01";
                    worksheet.Cells[2, 3].Value = "SMART.PLC.AO.CA00";
                    worksheet.Cells[2, 4].Value = "KEPware.KEPServerEx.V4";

                    worksheet.Cells[3, 1].Value = "AO控制";
                    worksheet.Cells[3, 2].Value = "EP02";
                    worksheet.Cells[3, 3].Value = "SMART.PLC.AO.CA01";
                    worksheet.Cells[3, 4].Value = "KEPware.KEPServerEx.V4";

                    worksheet.Cells[4, 1].Value = "DO控制";
                    worksheet.Cells[4, 2].Value = "电磁阀VX01";
                    worksheet.Cells[4, 3].Value = "SMART.PLC.DO.CD000";
                    worksheet.Cells[4, 4].Value = "KEPware.KEPServerEx.V4";

                    worksheet.Cells[5, 1].Value = "DO控制";
                    worksheet.Cells[5, 2].Value = "电磁阀VX02";
                    worksheet.Cells[5, 3].Value = "SMART.PLC.DO.CD001";
                    worksheet.Cells[5, 4].Value = "KEPware.KEPServerEx.V4";

                    // 添加说明
                    worksheet.Cells[7, 1].Value = "📝 填写说明:";
                    worksheet.Cells[7, 1].Style.Font.Bold = true;

                    worksheet.Cells[8, 1].Value = "1. 模块名称: 如 AO控制、DO控制、AI检测、DI检测";
                    worksheet.Cells[9, 1].Value = "2. 点位名称: 自定义点位名称,同一模块内不能重复";
                    worksheet.Cells[10, 1].Value = "3. 点位地址: 格式如 SMART.PLC.AO.CA00";
                    worksheet.Cells[11, 1].Value = "4. ServerName: PLC服务器地址,同一模块可使用相同值";
                    worksheet.Cells[12, 1].Value = "5. 从第2行开始填写数据,可添加任意多行";
                    worksheet.Cells[13, 1].Value = "6. 相同的模块名称会被归类到同一个模块";

                    // 设置列宽
                    worksheet.Column(1).Width = 15;
                    worksheet.Column(2).Width = 20;
                    worksheet.Column(3).Width = 30;
                    worksheet.Column(4).Width = 30;

                    // 冻结首行
                    worksheet.View.FreezePanes(2, 1);

                    // 保存文件
                    package.SaveAs(new FileInfo(filePath));
                }

                _logger?.LogInformation("创建Excel模板: {FilePath}", filePath);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "创建Excel模板时发生错误");
                throw;
            }
        }

        /// <summary>
        /// 创建 CSV 模板
        /// </summary>
        private void CreateCsvTemplate(string filePath)
        {
            try
            {
                var csv = new StringBuilder();

                // 标题行
                csv.AppendLine("模块名称,点位名称,点位地址,ServerName");

                // 示例数据
                csv.AppendLine("AO控制,EP01,SMART.PLC.AO.CA00,KEPware.KEPServerEx.V4");
                csv.AppendLine("AO控制,EP02,SMART.PLC.AO.CA01,KEPware.KEPServerEx.V4");
                csv.AppendLine("DO控制,电磁阀VX01,SMART.PLC.DO.CD000,KEPware.KEPServerEx.V4");
                csv.AppendLine("DO控制,电磁阀VX02,SMART.PLC.DO.CD001,KEPware.KEPServerEx.V4");

                File.WriteAllText(filePath, csv.ToString(), Encoding.GetEncoding("GB2312"));

                _logger?.LogInformation("创建CSV模板: {FilePath}", filePath);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "创建CSV模板时发生错误");
                throw;
            }
        }

        #endregion
    }
}