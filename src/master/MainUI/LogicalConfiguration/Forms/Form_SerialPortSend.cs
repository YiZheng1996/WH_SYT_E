using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using MainUI.LogicalConfiguration.Services.ServicesCommunication;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;


namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 串口发送参数配置表单
    /// 支持串口数据发送，可配置串口参数、超时、编码、响应等
    /// </summary>
    public partial class Form_SerialPortSend : UIForm
    {
        #region 私有字段

        private readonly IWorkflowStateService _workflowState;
        private readonly GlobalVariableManager _variableManager;
        private readonly ILogger<Form_SerialPortSend> _logger;
        private Parameter_SerialPortSend _parameter;
        private bool _isLoading = false;
        private CancellationTokenSource _testCts;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数 - 依赖注入
        /// </summary>
        public Form_SerialPortSend(
            IWorkflowStateService workflowState,
            GlobalVariableManager variableManager,
            ILogger<Form_SerialPortSend> logger)
        {
            _workflowState = workflowState ?? throw new ArgumentNullException(nameof(workflowState));
            _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            InitializeComponent();
            InitializeForm();

            _logger.LogDebug("Form_SerialPortSend 初始化完成");
        }

        #endregion

        #region 初始化方法

        /// <summary>
        /// 初始化表单
        /// </summary>
        private void InitializeForm()
        {
            try
            {
                _isLoading = true;

                // 初始化下拉框数据
                InitializeComboBoxes();

                // 加载变量列表
                LoadVariables();

                // 绑定事件
                BindEvents();

                // 设置默认值
                SetDefaultValues();

                _isLoading = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "初始化表单时发生错误");
                UIMessageTip.ShowError($"初始化失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 初始化下拉框
        /// </summary>
        private void InitializeComboBoxes()
        {
            // 串口名称 - 首次加载
            RefreshSerialPorts();

            // 波特率
            cmbBaudRate.Items.Clear();
            cmbBaudRate.Items.AddRange(new object[] {
                "1200", "2400", "4800", "9600", "14400", "19200",
                "38400", "57600", "115200", "230400", "460800", "921600"
            });
            cmbBaudRate.SelectedIndex = 3; // 默认9600

            // 数据位
            cmbDataBits.Items.Clear();
            cmbDataBits.Items.AddRange(new object[] { "5", "6", "7", "8" });
            cmbDataBits.SelectedIndex = 3; // 默认8

            // 校验位
            cmbParity.Items.Clear();
            cmbParity.Items.AddRange(new object[] { "无", "奇校验", "偶校验", "标记", "空格" });
            cmbParity.SelectedIndex = 0; // 默认无

            // 停止位
            cmbStopBits.Items.Clear();
            cmbStopBits.Items.AddRange(new object[] { "1", "1.5", "2" });
            cmbStopBits.SelectedIndex = 0; // 默认1

            // 握手协议
            cmbHandshake.Items.Clear();
            cmbHandshake.Items.AddRange(new object[] { "无", "XOn/XOff", "RTS", "RTS/XOn/XOff" });
            cmbHandshake.SelectedIndex = 0; // 默认无

            // 数据格式
            cmbDataFormat.Items.Clear();
            cmbDataFormat.Items.AddRange(new object[] { "文本", "十六进制", "Base64", "JSON" });
            cmbDataFormat.SelectedIndex = 0;

            // 编码类型
            cmbEncoding.Items.Clear();
            cmbEncoding.Items.AddRange(new object[] { "UTF-8", "ASCII", "GB2312", "Unicode" });
            cmbEncoding.SelectedIndex = 0;

            // 换行类型
            cmbNewLineType.Items.Clear();
            cmbNewLineType.Items.AddRange(new object[] { "CRLF (\\r\\n)", "LF (\\n)", "CR (\\r)" });
            cmbNewLineType.SelectedIndex = 0;
        }

        /// <summary>
        /// 刷新串口列表
        /// </summary>
        private void RefreshSerialPorts()
        {
            try
            {
                string currentPort = cmbPortName.Text;
                cmbPortName.Items.Clear();

                // 获取可用串口
                string[] ports = SerialPortService.GetAvailablePorts();
                if (ports != null && ports.Length > 0)
                {
                    cmbPortName.Items.AddRange(ports);

                    // 恢复之前选中的串口
                    if (!string.IsNullOrEmpty(currentPort) && cmbPortName.Items.Contains(currentPort))
                    {
                        cmbPortName.Text = currentPort;
                    }
                    else
                    {
                        cmbPortName.SelectedIndex = 0;
                    }
                }
                else
                {
                    cmbPortName.Items.Add("(无可用串口)");
                    cmbPortName.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "刷新串口列表失败");
                cmbPortName.Items.Clear();
                cmbPortName.Items.Add("(获取失败)");
                cmbPortName.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 加载变量列表
        /// </summary>
        private void LoadVariables()
        {
            try
            {
                cmbResponseVariable.Items.Clear();

                var variables = _variableManager.GetAllVariables();
                if (variables != null)
                {
                    // 筛选字符串类型变量用于响应存储
                    var stringVars = variables
                        .Where(v => v.VarType?.ToLower() == "string")
                        .Select(v => v.VarName)
                        .ToArray();

                    cmbResponseVariable.Items.AddRange(stringVars);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "加载变量列表失败");
            }
        }

        /// <summary>
        /// 绑定事件
        /// </summary>
        private void BindEvents()
        {
            // 按钮事件
            btnRefreshPorts.Click += BtnRefreshPorts_Click;
            btnTestPort.Click += BtnTestPort_Click;
            btnTestSend.Click += BtnTestSend_Click;
            btnInsertVariable.Click += BtnInsertVariable_Click;
            btnCreateVariable.Click += BtnCreateVariable_Click;
            btnConditionHelper.Click += BtnConditionHelper_Click;
            btnOK.Click += BtnOK_Click;
            btnCancel.Click += BtnCancel_Click;

            // 复选框事件
            chkWaitResponse.CheckedChanged += ChkWaitResponse_CheckedChanged;
            chkAppendNewLine.CheckedChanged += ChkAppendNewLine_CheckedChanged;
        }

        /// <summary>
        /// 设置默认值
        /// </summary>
        private void SetDefaultValues()
        {
            _parameter = new Parameter_SerialPortSend
            {
                PortName = cmbPortName.Items.Count > 0 ? cmbPortName.Items[0].ToString() : "COM1",
                BaudRate = 9600,
                DataBits = 8,
                Parity = SerialPortParity.None,
                StopBits = SerialPortStopBits.One,
                Handshake = SerialPortHandshake.None,
                ReadTimeout = 1000,
                WriteTimeout = 1000,
                DataFormat = Parameter_EthernetSend.DataFormatType.Text,
                Encoding = Parameter_EthernetSend.EncodingType.UTF8,
                SendContent = "",
                AppendNewLine = false,
                NewLineType = NewLineType.CRLF,
                WaitResponse = false,
                ResponseTimeout = 10,
                ResponseVariableName = "",
                CloseAfterSend = true,
                Condition = "",
                Description = $"串口发送步骤 {_workflowState?.StepNum + 1}",
                IsEnabled = true
            };

            LoadParameterToForm();
        }

        #endregion

        #region 参数接口实现

        /// <summary>
        /// 获取参数
        /// </summary>
        public Parameter_SerialPortSend GetParameter()
        {
            SaveFormToParameter();
            return _parameter;
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        public void SetParameter(Parameter_SerialPortSend parameter)
        {
            _parameter = parameter ?? new Parameter_SerialPortSend();
            LoadParameterToForm();
        }

        /// <summary>
        /// 加载参数
        /// </summary>
        public void LoadParameter(object parameter)
        {
            if (parameter == null)
            {
                SetDefaultValues();
                return;
            }

            try
            {
                if (parameter is Parameter_SerialPortSend serialParam)
                {
                    _parameter = serialParam;
                }
                else
                {
                    var json = parameter.ToString();
                    _parameter = JsonConvert.DeserializeObject<Parameter_SerialPortSend>(json)
                        ?? new Parameter_SerialPortSend();
                }

                LoadParameterToForm();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "加载参数失败");
                SetDefaultValues();
            }
        }

        /// <summary>
        /// 验证参数
        /// </summary>
        public bool ValidateParameter()
        {
            // 验证串口名称
            if (string.IsNullOrWhiteSpace(cmbPortName.Text) ||
                cmbPortName.Text == "(无可用串口)" ||
                cmbPortName.Text == "(获取失败)")
            {
                UIMessageTip.ShowError("请选择有效的串口");
                cmbPortName.Focus();
                return false;
            }

            // 验证波特率
            if (!int.TryParse(cmbBaudRate.Text, out int baudRate) || baudRate <= 0)
            {
                UIMessageTip.ShowError("请选择有效的波特率");
                cmbBaudRate.Focus();
                return false;
            }

            // 验证发送内容
            if (string.IsNullOrWhiteSpace(txtSendContent.Text))
            {
                UIMessageTip.ShowError("请输入发送内容");
                txtSendContent.Focus();
                return false;
            }

            // 如果等待响应，需要指定变量名
            if (chkWaitResponse.Checked && string.IsNullOrWhiteSpace(cmbResponseVariable.Text))
            {
                UIMessageTip.ShowError("等待响应时需要指定保存变量");
                cmbResponseVariable.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region 参数加载与保存

        /// <summary>
        /// 从参数加载到表单
        /// </summary>
        private void LoadParameterToForm()
        {
            try
            {
                _isLoading = true;

                // 串口设置
                if (!string.IsNullOrEmpty(_parameter.PortName) && cmbPortName.Items.Contains(_parameter.PortName))
                {
                    cmbPortName.Text = _parameter.PortName;
                }
                else if (cmbPortName.Items.Count > 0)
                {
                    cmbPortName.SelectedIndex = 0;
                }

                cmbBaudRate.Text = _parameter.BaudRate.ToString();
                cmbDataBits.Text = _parameter.DataBits.ToString();
                cmbParity.SelectedIndex = (int)_parameter.Parity;
                cmbStopBits.SelectedIndex = GetStopBitsIndex(_parameter.StopBits);
                cmbHandshake.SelectedIndex = (int)_parameter.Handshake;

                // 超时设置
                numReadTimeout.Value = _parameter.ReadTimeout;
                numWriteTimeout.Value = _parameter.WriteTimeout;

                // 数据设置
                cmbDataFormat.SelectedIndex = (int)_parameter.DataFormat;
                cmbEncoding.SelectedIndex = (int)_parameter.Encoding;
                txtSendContent.Text = _parameter.SendContent ?? "";
                chkAppendNewLine.Checked = _parameter.AppendNewLine;
                cmbNewLineType.SelectedIndex = (int)_parameter.NewLineType;
                cmbNewLineType.Enabled = _parameter.AppendNewLine;

                // 响应设置
                chkWaitResponse.Checked = _parameter.WaitResponse;
                numResponseTimeout.Value = _parameter.ResponseTimeout;
                numResponseTimeout.Enabled = _parameter.WaitResponse;
                cmbResponseVariable.Text = _parameter.ResponseVariableName ?? "";
                cmbResponseVariable.Enabled = _parameter.WaitResponse;
                btnCreateVariable.Enabled = _parameter.WaitResponse;

                // 其他设置
                chkCloseAfterSend.Checked = _parameter.CloseAfterSend;
                txtCondition.Text = _parameter.Condition ?? "";
                txtDescription.Text = _parameter.Description ?? "";
                chkEnabled.Checked = _parameter.IsEnabled;

                _isLoading = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "加载参数到表单失败");
                _isLoading = false;
            }
        }

        /// <summary>
        /// 从表单保存到参数
        /// </summary>
        private void SaveFormToParameter()
        {
            try
            {
                // 串口设置
                _parameter.PortName = cmbPortName.Text;
                _parameter.BaudRate = int.TryParse(cmbBaudRate.Text, out int baud) ? baud : 9600;
                _parameter.DataBits = int.TryParse(cmbDataBits.Text, out int bits) ? bits : 8;
                _parameter.Parity = (SerialPortParity)cmbParity.SelectedIndex;
                _parameter.StopBits = GetStopBitsFromIndex(cmbStopBits.SelectedIndex);
                _parameter.Handshake = (SerialPortHandshake)cmbHandshake.SelectedIndex;

                // 超时设置
                _parameter.ReadTimeout = (int)numReadTimeout.Value;
                _parameter.WriteTimeout = (int)numWriteTimeout.Value;

                // 数据设置
                _parameter.DataFormat = (Parameter_EthernetSend.DataFormatType)cmbDataFormat.SelectedIndex;
                _parameter.Encoding = (Parameter_EthernetSend.EncodingType)cmbEncoding.SelectedIndex;
                _parameter.SendContent = txtSendContent.Text;
                _parameter.AppendNewLine = chkAppendNewLine.Checked;
                _parameter.NewLineType = (NewLineType)cmbNewLineType.SelectedIndex;

                // 响应设置
                _parameter.WaitResponse = chkWaitResponse.Checked;
                _parameter.ResponseTimeout = (int)numResponseTimeout.Value;
                _parameter.ResponseVariableName = cmbResponseVariable.Text;

                // 其他设置
                _parameter.CloseAfterSend = chkCloseAfterSend.Checked;
                _parameter.Condition = txtCondition.Text;
                _parameter.Description = txtDescription.Text;
                _parameter.IsEnabled = chkEnabled.Checked;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存表单到参数失败");
            }
        }

        /// <summary>
        /// 获取停止位索引
        /// </summary>
        private int GetStopBitsIndex(SerialPortStopBits stopBits)
        {
            return stopBits switch
            {
                SerialPortStopBits.One => 0,
                SerialPortStopBits.OnePointFive => 1,
                SerialPortStopBits.Two => 2,
                _ => 0
            };
        }

        /// <summary>
        /// 从索引获取停止位
        /// </summary>
        private SerialPortStopBits GetStopBitsFromIndex(int index)
        {
            return index switch
            {
                0 => SerialPortStopBits.One,
                1 => SerialPortStopBits.OnePointFive,
                2 => SerialPortStopBits.Two,
                _ => SerialPortStopBits.One
            };
        }

        #endregion

        #region 按钮事件处理

        /// <summary>
        /// 刷新串口按钮
        /// </summary>
        private void BtnRefreshPorts_Click(object sender, EventArgs e)
        {
            RefreshSerialPorts();
            UIMessageTip.ShowOk("串口列表已刷新");
        }

        /// <summary>
        /// 测试串口按钮
        /// </summary>
        private async void BtnTestPort_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbPortName.Text) ||
                cmbPortName.Text == "(无可用串口)" ||
                cmbPortName.Text == "(获取失败)")
            {
                UIMessageTip.ShowError("请选择有效的串口");
                return;
            }

            // 禁用按钮，显示测试中状态
            btnTestPort.Enabled = false;
            lblPortStatus.Text = "测试中...";
            lblPortStatus.ForeColor = Color.Orange;

            try
            {
                _testCts?.Cancel();
                _testCts = new CancellationTokenSource();

                // 构建临时参数
                SaveFormToParameter();

                // 创建串口服务测试
                using var serialService = new SerialPortService(_logger);
                serialService.Configure(_parameter);

                bool result = await serialService.TestPortAsync(_testCts.Token);

                if (result)
                {
                    lblPortStatus.Text = "串口正常";
                    lblPortStatus.ForeColor = Color.Green;
                    UIMessageTip.ShowOk($"串口 {cmbPortName.Text} 测试成功");
                }
                else
                {
                    lblPortStatus.Text = "串口异常";
                    lblPortStatus.ForeColor = Color.Red;
                    UIMessageTip.ShowError($"串口 {cmbPortName.Text} 测试失败");
                }
            }
            catch (OperationCanceledException)
            {
                lblPortStatus.Text = "测试取消";
                lblPortStatus.ForeColor = Color.Gray;
            }
            catch (Exception ex)
            {
                lblPortStatus.Text = "测试失败";
                lblPortStatus.ForeColor = Color.Red;
                _logger.LogError(ex, "测试串口失败: {Port}", cmbPortName.Text);
                UIMessageTip.ShowError($"测试失败: {ex.Message}");
            }
            finally
            {
                btnTestPort.Enabled = true;
            }
        }

        /// <summary>
        /// 测试发送按钮
        /// </summary>
        private async void BtnTestSend_Click(object sender, EventArgs e)
        {
            if (!ValidateParameter())
            {
                return;
            }

            // 禁用按钮
            btnTestSend.Enabled = false;
            btnTestSend.Text = "发送中...";

            try
            {
                _testCts?.Cancel();
                _testCts = new CancellationTokenSource();

                // 保存参数
                SaveFormToParameter();

                // 创建串口服务
                using var serialService = new SerialPortService(_logger);
                serialService.Configure(_parameter);

                // 解析变量并准备数据
                string content = ResolveVariables(txtSendContent.Text);
                byte[] data = PrepareData(content);

                // 执行发送
                var (success, response) = await serialService.SendAsync(
                    data,
                    _parameter.WaitResponse,
                    _parameter.ResponseTimeout * 1000,
                    _testCts.Token);

                if (success)
                {
                    if (_parameter.WaitResponse && !string.IsNullOrEmpty(response))
                    {
                        UIMessageTip.ShowOk($"发送成功，响应: {response}");
                        _logger.LogDebug("串口发送成功，响应: {Response}", response);
                    }
                    else
                    {
                        UIMessageTip.ShowOk("发送成功");
                    }
                }
                else
                {
                    UIMessageTip.ShowError("发送失败");
                }
            }
            catch (OperationCanceledException)
            {
                UIMessageTip.ShowWarning("发送已取消");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "测试发送失败");
                UIMessageTip.ShowError($"发送失败: {ex.Message}");
            }
            finally
            {
                btnTestSend.Enabled = true;
                btnTestSend.Text = "测试发送";
            }
        }

        /// <summary>
        /// 插入变量按钮
        /// </summary>
        private void BtnInsertVariable_Click(object sender, EventArgs e)
        {
            ShowVariableInsertMenu();
        }

        /// <summary>
        /// 创建变量按钮
        /// </summary>
        private void BtnCreateVariable_Click(object sender, EventArgs e)
        {
            try
            {
                // 打开变量创建对话框
                using var dlg = new Form_CreateVariable(_variableManager);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // 刷新变量列表
                    LoadVariables();

                    // 选中新创建的变量
                    if (!string.IsNullOrEmpty(dlg.CreatedVariableName))
                    {
                        cmbResponseVariable.Text = dlg.CreatedVariableName;
                    }

                    UIMessageTip.ShowOk("变量创建成功");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建变量失败");
                UIMessageTip.ShowError($"创建失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 条件帮助按钮
        /// </summary>
        private void BtnConditionHelper_Click(object sender, EventArgs e)
        {
            try
            {
                // 打开条件表达式帮助对话框
                using var dlg = new Form_ConditionHelper(_variableManager, txtCondition.Text);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtCondition.Text = dlg.ResultCondition;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "打开条件帮助失败");
                UIMessageTip.ShowError($"打开失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (ValidateParameter())
            {
                SaveFormToParameter();
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region 复选框事件处理

        /// <summary>
        /// 等待响应复选框
        /// </summary>
        private void ChkWaitResponse_CheckedChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;

            bool isChecked = chkWaitResponse.Checked;
            numResponseTimeout.Enabled = isChecked;
            cmbResponseVariable.Enabled = isChecked;
            btnCreateVariable.Enabled = isChecked;
        }

        /// <summary>
        /// 追加换行复选框
        /// </summary>
        private void ChkAppendNewLine_CheckedChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;
            cmbNewLineType.Enabled = chkAppendNewLine.Checked;
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 显示变量插入菜单
        /// </summary>
        private void ShowVariableInsertMenu()
        {
            try
            {
                var variables = _variableManager.GetAllVariables();
                if (variables == null || !variables.Any())
                {
                    UIMessageTip.ShowWarning("暂无可用变量");
                    return;
                }

                // 创建右键菜单
                var menu = new ContextMenuStrip();

                // 按类型分组
                var groupedVars = variables.GroupBy(v => v.VarType ?? "未知");

                foreach (var group in groupedVars)
                {
                    var typeItem = new ToolStripMenuItem(group.Key);

                    foreach (var variable in group)
                    {
                        var varItem = new ToolStripMenuItem($"{variable.VarName} ({variable.VarValue ?? "null"})");
                        varItem.Click += (s, e) =>
                        {
                            InsertVariableToContent($"{{{variable.VarName}}}");
                        };
                        typeItem.DropDownItems.Add(varItem);
                    }

                    menu.Items.Add(typeItem);
                }

                // 在按钮位置显示菜单
                menu.Show(btnInsertVariable, new Point(0, btnInsertVariable.Height));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "显示变量菜单失败");
                UIMessageTip.ShowError("获取变量列表失败");
            }
        }

        /// <summary>
        /// 插入变量到发送内容
        /// </summary>
        private void InsertVariableToContent(string variableRef)
        {
            int selStart = txtSendContent.SelectionStart;
            string text = txtSendContent.Text ?? "";

            txtSendContent.Text = text.Insert(selStart, variableRef);
            txtSendContent.SelectionStart = selStart + variableRef.Length;
            txtSendContent.Focus();
        }

        /// <summary>
        /// 解析变量
        /// </summary>
        private string ResolveVariables(string content)
        {
            if (string.IsNullOrEmpty(content)) return content;

            try
            {
                // 匹配 {变量名} 格式
                var pattern = @"\{(\w+)\}";
                return Regex.Replace(content, pattern, match =>
                {
                    var varName = match.Groups[1].Value;
                    var variable = _variableManager.GetVariable(varName);
                    if (variable != null)
                    {
                        return variable.VarValue?.ToString() ?? "";
                    }
                    return match.Value; // 保持原样
                });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "解析变量失败");
                return content;
            }
        }

        /// <summary>
        /// 准备发送数据
        /// </summary>
        private byte[] PrepareData(string content)
        {
            // 处理换行
            if (chkAppendNewLine.Checked)
            {
                string newLine = cmbNewLineType.SelectedIndex switch
                {
                    0 => "\r\n",
                    1 => "\n",
                    2 => "\r",
                    _ => "\r\n"
                };
                content += newLine;
            }

            // 获取编码
            Encoding encoding = cmbEncoding.SelectedIndex switch
            {
                0 => Encoding.UTF8,
                1 => Encoding.ASCII,
                2 => Encoding.GetEncoding("GB2312"),
                3 => Encoding.Unicode,
                _ => Encoding.UTF8
            };

            // 根据数据格式转换
            return cmbDataFormat.SelectedIndex switch
            {
                0 => encoding.GetBytes(content), // 文本
                1 => HexStringToBytes(content),  // 十六进制
                2 => Convert.FromBase64String(content), // Base64
                3 => encoding.GetBytes(content), // JSON (作为文本处理)
                _ => encoding.GetBytes(content)
            };
        }

        /// <summary>
        /// 十六进制字符串转字节数组
        /// </summary>
        private byte[] HexStringToBytes(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex))
                return Array.Empty<byte>();

            // 移除空格和分隔符
            hex = hex.Replace(" ", "").Replace("-", "").Replace("0x", "").Replace("0X", "");

            // 确保偶数长度
            if (hex.Length % 2 != 0)
                hex = "0" + hex;

            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }

            return bytes;
        }

        #endregion

        #region 窗体事件

        /// <summary>
        /// 窗体关闭
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _testCts?.Cancel();
            _testCts?.Dispose();
            base.OnFormClosing(e);
        }

        #endregion
    }
}