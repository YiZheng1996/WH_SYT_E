using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using AntdUI;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using MainUI.LogicalConfiguration.Services.ServicesCommunication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 以太网发送参数配置表单
    /// 支持TCP/UDP协议数据发送,可配置超时、编码、响应等参数
    /// </summary>
    public partial class Form_EthernetSend : BaseParameterForm
    {
        #region 属性

        /// <summary>
        ///     参数对象 - 基类通过反射访问此属性
        /// </summary>
        public Parameter_EthernetSend Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value ?? new Parameter_EthernetSend();
                if (!DesignMode && !IsLoading && IsHandleCreated) LoadParameterToForm();
            }
        }

        #endregion

        #region 私有字段

        private Parameter_EthernetSend _parameter;
        private CancellationTokenSource _testCts;
        private bool _isInitializing = true;

        #endregion

        #region 构造函数

        /// <summary>
        ///     无参构造函数 - 供设计器和运行时使用
        /// </summary>
        public Form_EthernetSend()
        {
            InitializeComponent();

            if (DesignMode) return;

            InitializeForm();
        }

        /// <summary>
        ///     依赖注入构造函数
        /// </summary>
        public Form_EthernetSend(
            IWorkflowStateService workflowState,
            ILogger<Form_EthernetSend> logger)
            : base(workflowState, logger)
        {
            InitializeComponent();
            InitializeForm();
        }

        #endregion

        #region 初始化方法

        /// <summary>
        ///     初始化表单
        /// </summary>
        private void InitializeForm()
        {
            if (DesignMode) return;

            try
            {
                _isInitializing = true;

                // 初始化控件
                InitializeFormControls();

                // 绑定事件
                BindEvents();

                // 设置默认值
                SetDefaultValues();

                Logger?.LogDebug("Form_EthernetSend 初始化完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "初始化窗体时发生错误");
                MessageHelper.MessageOK(this, $"初始化失败: {ex.Message}", TType.Error);
            }
            finally
            {
                _isInitializing = false;
            }
        }

        /// <summary>
        ///     初始化表单控件
        /// </summary>
        private void InitializeFormControls()
        {
            // 初始化协议类型下拉框
            cmbProtocol.Items.Clear();
            cmbProtocol.Items.AddRange(["TCP", "UDP"]);
            cmbProtocol.SelectedIndex = 0;

            // 初始化数据格式下拉框
            cmbDataFormat.Items.Clear();
            cmbDataFormat.Items.AddRange(["文本", "十六进制", "Base64"]);
            cmbDataFormat.SelectedIndex = 0;

            // 初始化编码下拉框
            cmbEncoding.Items.Clear();
            cmbEncoding.Items.AddRange(["UTF-8", "ASCII", "GB2312", "Unicode"]);
            cmbEncoding.SelectedIndex = 0;

            // 初始化换行符类型下拉框
            cmbNewLineType.Items.Clear();
            cmbNewLineType.Items.AddRange(["CRLF (\\r\\n)", "LF (\\n)", "CR (\\r)"]);
            cmbNewLineType.SelectedIndex = 0;
            cmbNewLineType.Enabled = false;

            // 初始化响应变量下拉框
            LoadVariableList();
        }

        /// <summary>
        ///     绑定事件
        /// </summary>
        private void BindEvents()
        {
            // 按钮事件
            btnTestConnection.Click += BtnTestConnection_Click;
            btnInsertVariable.Click += BtnInsertVariable_Click;
            btnCreateVariable.Click += BtnCreateVariable_Click;
            btnTestSend.Click += BtnTestSend_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            btnHelp.Click += BtnHelp_Click;

            // 复选框事件
            chkWaitResponse.CheckedChanged += ChkWaitResponse_CheckedChanged;
            chkAppendNewLine.CheckedChanged += ChkAppendNewLine_CheckedChanged;

            // 文本框验证事件
            txtIPAddress.Leave += TxtIPAddress_Leave;
            txtPort.Leave += TxtPort_Leave;

            // 表单关闭事件
            FormClosing += Form_EthernetSend_FormClosing;
        }

        /// <summary>
        ///     加载变量列表
        /// </summary>
        private void LoadVariableList()
        {
            try
            {
                cmbResponseVariable.Items.Clear();

                var variableManager = _globalVariable ??
                                      Program.ServiceProvider?.GetService<GlobalVariableManager>();

                if (variableManager != null)
                {
                    var variables = variableManager.GetAllUserVariables();
                    foreach (var variable in variables) cmbResponseVariable.Items.Add(variable.VarName);
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载变量列表失败");
            }
        }

        #endregion

        #region 重写基类方法

        /// <summary>
        ///     设置默认值
        /// </summary>
        protected override void SetDefaultValues()
        {
            try
            {
                _parameter = new Parameter_EthernetSend
                {
                    Description = $"以太网发送步骤 {(_workflowState?.StepNum ?? 0) + 1}",
                    IsEnabled = true,
                    Condition = "",

                    // 连接设置
                    IPAddress = "192.168.1.100",
                    Port = 8080,
                    Protocol = ProtocolType.Tcp,
                    ConnectTimeout = 5000,
                    SendTimeout = 3000,

                    // 数据设置
                    DataFormat = Parameter_EthernetSend.DataFormatType.Text,
                    Encoding = Parameter_EthernetSend.EncodingType.UTF8,
                    SendContent = "",
                    AppendNewLine = false,
                    NewLineType = "\r\n",  // 直接赋值字符串

                    // 响应设置
                    WaitResponse = false,
                    ResponseTimeout = 5000,
                    ResponseVariableName = "",

                    // 其他设置
                    DisconnectAfterSend = true
                };

                LoadParameterToForm();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "设置默认值失败");
            }
        }

        /// <summary>
        ///     加载参数到界面
        /// </summary>
        protected override void LoadParameterToForm()
        {
            if (_parameter == null || _isInitializing) return;

            try
            {
                _isInitializing = true;

                // 基本信息
                txtDescription.Text = _parameter.Description ?? "";
                chkEnabled.Checked = _parameter.IsEnabled;
                txtCondition.Text = _parameter.Condition ?? "";

                // 连接设置
                txtIPAddress.Text = _parameter.IPAddress ?? "192.168.1.100";
                txtPort.Text = _parameter.Port.ToString();
                cmbProtocol.SelectedIndex = _parameter.Protocol == ProtocolType.Tcp ? 0 : 1;
                numConnectTimeout.Value = _parameter.ConnectTimeout / 1000;
                numSendTimeout.Value = _parameter.SendTimeout / 1000;

                // 数据设置
                cmbDataFormat.SelectedIndex = (int)_parameter.DataFormat;
                cmbEncoding.SelectedIndex = (int)_parameter.Encoding;
                txtSendContent.Text = _parameter.SendContent ?? "";
                chkAppendNewLine.Checked = _parameter.AppendNewLine;

                // 换行符类型
                cmbNewLineType.SelectedIndex = _parameter.NewLineType switch
                {
                    "\r\n" => 0,
                    "\n" => 1,
                    "\r" => 2,
                    _ => 0
                };

                // 响应设置
                chkWaitResponse.Checked = _parameter.WaitResponse;
                numResponseTimeout.Value = _parameter.ResponseTimeout / 1000;
                cmbResponseVariable.Text = _parameter.ResponseVariableName ?? "";

                // 其他设置
                chkDisconnectAfterSend.Checked = _parameter.DisconnectAfterSend;

                Logger?.LogDebug("参数已加载到界面");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载参数到界面失败");
                MessageHelper.MessageOK(this, $"加载参数失败：{ex.Message}", TType.Error);
            }
            finally
            {
                _isInitializing = false;
            }
        }

        /// <summary>
        ///     保存界面数据到参数对象
        /// </summary>
        protected override void SaveFormToParameter()
        {
            try
            {
                _parameter ??= new Parameter_EthernetSend();

                // 基本信息
                _parameter.Description = txtDescription.Text.Trim();
                _parameter.IsEnabled = chkEnabled.Checked;
                _parameter.Condition = txtCondition.Text.Trim();

                // 连接设置
                _parameter.IPAddress = txtIPAddress.Text.Trim();
                _parameter.Port = int.TryParse(txtPort.Text, out var port) ? port : 8080;
                _parameter.Protocol = cmbProtocol.SelectedIndex == 0 ? ProtocolType.Tcp : ProtocolType.Udp;
                _parameter.ConnectTimeout = numConnectTimeout.Value * 1000;
                _parameter.SendTimeout = numSendTimeout.Value * 1000;

                // 数据设置
                _parameter.DataFormat = (Parameter_EthernetSend.DataFormatType)cmbDataFormat.SelectedIndex;
                _parameter.Encoding = (Parameter_EthernetSend.EncodingType)cmbEncoding.SelectedIndex;
                _parameter.SendContent = txtSendContent.Text;
                _parameter.AppendNewLine = chkAppendNewLine.Checked;

                // 换行符类型
                _parameter.NewLineType = cmbNewLineType.SelectedIndex switch
                {
                    0 => "\r\n",
                    1 => "\n",
                    2 => "\r",
                    _ => "\r\n"
                };

                // 响应设置
                _parameter.WaitResponse = chkWaitResponse.Checked;
                _parameter.ResponseTimeout = numResponseTimeout.Value * 1000;
                _parameter.ResponseVariableName = cmbResponseVariable.Text.Trim();

                // 其他设置
                _parameter.DisconnectAfterSend = chkDisconnectAfterSend.Checked;

                Logger?.LogDebug("界面数据已保存到参数对象");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存界面数据到参数对象失败");
                throw;
            }
        }

        /// <summary>
        ///     验证输入
        /// </summary>
        protected override bool ValidateInput()
        {
            try
            {
                // 验证IP地址
                if (string.IsNullOrWhiteSpace(txtIPAddress.Text))
                {
                    MessageHelper.MessageOK(this, "请输入IP地址", TType.Warn);
                    txtIPAddress.Focus();
                    return false;
                }

                if (!IPAddress.TryParse(txtIPAddress.Text.Trim(), out _))
                {
                    MessageHelper.MessageOK(this, "IP地址格式无效", TType.Warn);
                    txtIPAddress.Focus();
                    return false;
                }

                // 验证端口
                if (!int.TryParse(txtPort.Text, out var port) || port < 1 || port > 65535)
                {
                    MessageHelper.MessageOK(this, "端口号必须在1-65535之间", TType.Warn);
                    txtPort.Focus();
                    return false;
                }

                // 验证发送内容
                if (string.IsNullOrWhiteSpace(txtSendContent.Text))
                {
                    MessageHelper.MessageOK(this, "请输入发送内容", TType.Warn);
                    txtSendContent.Focus();
                    return false;
                }

                // 如果选择十六进制格式,验证十六进制字符串
                if (cmbDataFormat.SelectedIndex == 1) // 十六进制
                {
                    var hex = txtSendContent.Text.Replace(" ", "").Replace("-", "");
                    if (!Regex.IsMatch(hex, @"^[0-9A-Fa-f]+$"))
                    {
                        MessageHelper.MessageOK(this, "十六进制格式无效,只能包含0-9和A-F字符", TType.Warn);
                        txtSendContent.Focus();
                        return false;
                    }

                    if (hex.Length % 2 != 0)
                    {
                        MessageHelper.MessageOK(this, "十六进制字符串长度必须是偶数", TType.Warn);
                        txtSendContent.Focus();
                        return false;
                    }
                }

                // 验证响应变量
                if (!chkWaitResponse.Checked || !string.IsNullOrWhiteSpace(cmbResponseVariable.Text)) return true;
                MessageHelper.MessageOK(this, "等待响应时必须指定响应变量名", TType.Warn);
                cmbResponseVariable.Focus();
                return false;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "验证输入时发生错误");
                return false;
            }
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// 帮助按钮点击
        /// </summary>
        private void BtnHelp_Click(object sender, EventArgs e)
        {
            try
            {
                string helpText = @"以太网发送配置说明：

1. 连接设置
   - IP地址：目标设备的IP地址（如 192.168.1.100）
   - 端口：目标设备的端口号（1-65535）
   - 协议：TCP（可靠连接）或 UDP（快速传输）

2. 超时设置
   - 连接超时：建立连接的最长等待时间
   - 发送超时：发送数据的最长等待时间
   - 接收超时：等待响应的最长等待时间

3. 数据格式
   - 文本：直接发送文本内容
   - 十六进制：发送十六进制数据（如：FF AA BB CC）
   - Base64：发送Base64编码数据

4. 编码方式
   - UTF-8：支持中文和多语言（推荐）
   - ASCII：仅支持英文字符
   - GB2312：中文简体编码
   - Unicode：通用Unicode编码

5. 变量引用
   - 使用 {变量名} 格式引用全局变量
   - 例如：Send data: {Temperature} °C
   - 发送前会自动替换为变量的实际值

6. 响应处理
   - 勾选【等待响应】可接收服务器返回数据
   - 响应数据可保存到指定的全局变量
   - 可用于后续步骤的条件判断或数据处理
        
7.执行条件
   - 可选设置，为空时总是执行
   - 支持表达式，如：{ Status} == 'Ready'
   - 条件为 true 时才执行发送操作
        
8.测试功能
   - 测试连接：验证能否连接到目标设备
   - 测试发送：实际发送一次数据进行验证

 使用建议：
   - TCP适用于需要可靠传输的场景
   - UDP适用于对速度要求高、允许少量丢包的场景
   - 建议先测试连接，确认网络通畅后再配置完整参数";
        
                MessageHelper.MessageOK(this, helpText, TType.Info);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "显示帮助时发生错误");
            }
        }

        /// <summary>
        /// 保存按钮点击
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 调用基类的 SaveParameters 方法
                // 它会自动调用 ValidateInput、SaveFormToParameter 等
                SaveParameters();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存参数时发生错误");
                MessageHelper.MessageOK(this, $"保存失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 取消按钮点击
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        ///     测试连接按钮
        /// </summary>
        private async void BtnTestConnection_Click(object sender, EventArgs e)
        {
            if (!ValidateConnectionSettings())
                return;

            btnTestConnection.Enabled = false;
            lblConnectionStatus.Text = "测试中...";
            lblConnectionStatus.ForeColor = Color.Orange;

            try
            {
                _testCts?.Cancel();
                _testCts = new CancellationTokenSource();

                var config = new EthernetConfig
                {
                    IPAddress = txtIPAddress.Text.Trim(),
                    Port = int.Parse(txtPort.Text),
                    Protocol = cmbProtocol.SelectedIndex == 0 ? ProtocolType.Tcp : ProtocolType.Udp,
                    ConnectTimeout = numConnectTimeout.Value * 1000,
                    SendTimeout = numSendTimeout.Value * 1000
                };

                using var service = new EthernetService(config);
                var result = await service.ConnectAsync(_testCts.Token);

                if (result.Success)
                {
                    lblConnectionStatus.Text = "连接成功";
                    lblConnectionStatus.ForeColor = Color.Green;
                    MessageHelper.MessageOK(this, "连接测试成功");
                }
                else
                {
                    lblConnectionStatus.Text = "连接失败";
                    lblConnectionStatus.ForeColor = Color.Red;
                    MessageHelper.MessageOK(this, $"连接失败: {result.Message}", TType.Error);
                }
            }
            catch (OperationCanceledException)
            {
                lblConnectionStatus.Text = "测试取消";
                lblConnectionStatus.ForeColor = Color.Gray;
            }
            catch (Exception ex)
            {
                lblConnectionStatus.Text = "连接异常";
                lblConnectionStatus.ForeColor = Color.Red;
                Logger?.LogError(ex, "测试连接失败");
                MessageHelper.MessageOK(this, $"测试失败: {ex.Message}", TType.Error);
            }
            finally
            {
                btnTestConnection.Enabled = true;
            }
        }

        /// <summary>
        ///     测试发送按钮
        /// </summary>
        private async void BtnTestSend_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            btnTestSend.Enabled = false;

            try
            {
                _testCts?.Cancel();
                _testCts = new CancellationTokenSource();

                SaveFormToParameter();

                var content = ResolveVariables(txtSendContent.Text);
                var data = PrepareData(content);

                var config = new EthernetConfig
                {
                    IPAddress = _parameter.IPAddress,
                    Port = _parameter.Port,
                    Protocol = _parameter.Protocol,
                    ConnectTimeout = _parameter.ConnectTimeout,
                    SendTimeout = _parameter.SendTimeout,
                    ReceiveTimeout = _parameter.ResponseTimeout
                };

                using var service = new EthernetService(config);

                // 连接
                var connectResult = await service.ConnectAsync(_testCts.Token);
                if (!connectResult.Success)
                {
                    MessageHelper.MessageOK(this, $"连接失败: {connectResult.Message}", TType.Error);
                    return;
                }

                // 发送
                CommunicationResult result;
                if (_parameter.WaitResponse)
                {
                    result = await service.SendAndReceiveAsync(data, _parameter.ResponseTimeout, _testCts.Token);
                    if (result.Success)
                        MessageHelper.MessageOK(this,
                            $"发送成功,收到响应:\n{result.ResponseText}");
                    else
                        MessageHelper.MessageOK(this, $"发送失败: {result.Message}", TType.Error);
                }
                else
                {
                    result = await service.SendAsync(data, _testCts.Token);
                    if (result.Success)
                        MessageHelper.MessageOK(this,
                            $"发送成功,共 {data.Length} 字节");
                    else
                        MessageHelper.MessageOK(this, $"发送失败: {result.Message}", TType.Error);
                }
            }
            catch (OperationCanceledException)
            {
                MessageHelper.MessageOK(this, "测试已取消", TType.Info);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "测试发送失败");
                MessageHelper.MessageOK(this, $"测试失败: {ex.Message}", TType.Error);
            }
            finally
            {
                btnTestSend.Enabled = true;
            }
        }

        /// <summary>
        ///     插入变量按钮
        /// </summary>
        private void BtnInsertVariable_Click(object sender, EventArgs e)
        {
            try
            {
                var variableManager = _globalVariable ??
                                      Program.ServiceProvider?.GetService<GlobalVariableManager>();

                if (variableManager == null)
                {
                    MessageHelper.MessageOK(this, "变量管理器未初始化", TType.Warn);
                    return;
                }

                var variables = variableManager.GetAllUserVariables();
                if (variables == null || variables.Count == 0)
                {
                    MessageHelper.MessageOK(this, "没有可用的变量", TType.Info);
                    return;
                }

                // 显示变量选择对话框
                var varNames = variables.Select(v => v.VarName).ToList();
                var selectedVar = ShowVariableSelectDialog(varNames);

                if (string.IsNullOrEmpty(selectedVar)) return;
                // 在光标位置插入变量引用
                var selectionStart = txtSendContent.SelectionStart;
                var newText = txtSendContent.Text.Insert(selectionStart, $"{{{selectedVar}}}");
                txtSendContent.Text = newText;
                txtSendContent.SelectionStart = selectionStart + selectedVar.Length + 2;
                txtSendContent.Focus();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "插入变量失败");
                MessageHelper.MessageOK(this, $"插入变量失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        ///     创建变量按钮
        /// </summary>
        private void BtnCreateVariable_Click(object sender, EventArgs e)
        {
            try
            {
                // TODO: 打开变量创建对话框
                MessageHelper.MessageOK(this, "变量创建功能开发中", TType.Info);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "创建变量失败");
            }
        }

        /// <summary>
        ///     等待响应复选框变化
        /// </summary>
        private void ChkWaitResponse_CheckedChanged(object sender, EventArgs e)
        {
            var enabled = chkWaitResponse.Checked;
            numResponseTimeout.Enabled = enabled;
            cmbResponseVariable.Enabled = enabled;
            btnCreateVariable.Enabled = enabled;
        }

        /// <summary>
        ///     追加换行符复选框变化
        /// </summary>
        private void ChkAppendNewLine_CheckedChanged(object sender, EventArgs e)
        {
            cmbNewLineType.Enabled = chkAppendNewLine.Checked;
        }

        /// <summary>
        ///     IP地址失去焦点验证
        /// </summary>
        private void TxtIPAddress_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtIPAddress.Text))
            {
                if (!IPAddress.TryParse(txtIPAddress.Text.Trim(), out _))
                    txtIPAddress.ForeColor = Color.Red;
                else
                    txtIPAddress.ForeColor = Color.Black;
            }
        }

        /// <summary>
        ///     端口失去焦点验证
        /// </summary>
        private void TxtPort_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(txtPort.Text, out var port) && port >= 1 && port <= 65535)
                txtPort.ForeColor = Color.Black;
            else
                txtPort.ForeColor = Color.Red;
        }

        /// <summary>
        ///     表单关闭事件
        /// </summary>
        private void Form_EthernetSend_FormClosing(object sender, FormClosingEventArgs e)
        {
            _testCts?.Cancel();
            _testCts?.Dispose();
        }

        #endregion

        #region 辅助方法

        /// <summary>
        ///     验证连接设置
        /// </summary>
        private bool ValidateConnectionSettings()
        {
            // 验证IP地址
            if (string.IsNullOrWhiteSpace(txtIPAddress.Text))
            {
                MessageHelper.MessageOK(this, "请输入IP地址", TType.Warn);
                return false;
            }

            if (!IPAddress.TryParse(txtIPAddress.Text.Trim(), out _))
            {
                MessageHelper.MessageOK(this, "IP地址格式无效", TType.Warn);
                return false;
            }

            // 验证端口
            if (!int.TryParse(txtPort.Text, out var port) || port < 1 || port > 65535)
            {
                MessageHelper.MessageOK(this, "端口号必须在1-65535之间", TType.Warn);
                return false;
            }

            return true;
        }

        /// <summary>
        ///     解析变量引用
        /// </summary>
        private string ResolveVariables(string content)
        {
            if (string.IsNullOrEmpty(content))
                return content;

            try
            {
                var variableManager = _globalVariable ??
                                      Program.ServiceProvider?.GetService<GlobalVariableManager>();

                if (variableManager == null)
                    return content;

                var regex = new Regex(@"\{(\w+)\}");
                return regex.Replace(content, match =>
                {
                    var varName = match.Groups[1].Value;
                    var variable = variableManager.FindVariableByName(varName);
                    return variable?.VarValue?.ToString() ?? match.Value;
                });
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "解析变量引用失败");
                return content;
            }
        }

        /// <summary>
        ///     准备发送数据
        /// </summary>
        private byte[] PrepareData(string content)
        {
            if (_parameter.AppendNewLine)
                content += _parameter.NewLineType;

            var encoding = GetEncoding(_parameter.Encoding);

            return _parameter.DataFormat switch
            {
                Parameter_EthernetSend.DataFormatType.Hex => HexStringToBytes(content),
                Parameter_EthernetSend.DataFormatType.Base64 => Convert.FromBase64String(content),
                _ => encoding.GetBytes(content)
            };
        }

        /// <summary>
        ///     获取编码
        /// </summary>
        private Encoding GetEncoding(Parameter_EthernetSend.EncodingType encodingType)
        {
            return encodingType switch
            {
                Parameter_EthernetSend.EncodingType.UTF8 => Encoding.UTF8,
                Parameter_EthernetSend.EncodingType.ASCII => Encoding.ASCII,
                Parameter_EthernetSend.EncodingType.GB2312 => Encoding.GetEncoding("GB2312"),
                Parameter_EthernetSend.EncodingType.Unicode => Encoding.Unicode,
                _ => Encoding.UTF8
            };
        }

        /// <summary>
        ///     十六进制字符串转字节数组
        /// </summary>
        private byte[] HexStringToBytes(string hex)
        {
            hex = hex.Replace(" ", "").Replace("-", "");
            var bytes = new byte[hex.Length / 2];

            for (var i = 0; i < bytes.Length; i++) bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);

            return bytes;
        }

        /// <summary>
        ///     显示变量选择对话框
        /// </summary>
        private string ShowVariableSelectDialog(List<string> variables)
        {
            using var dialog = new Form
            {
                Text = "选择变量",
                Width = 300,
                Height = 400,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var listBox = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("微软雅黑", 10F)
            };

            foreach (var variable in variables) listBox.Items.Add(variable);

            var btnPanel = new UIPanel
            {
                Dock = DockStyle.Bottom,
                Height = 50
            };

            var btnOK = new UIButton
            {
                Text = "确定",
                DialogResult = DialogResult.OK,
                Location = new Point(80, 10),
                Width = 60
            };

            var btnCancel = new UIButton
            {
                Text = "取消",
                DialogResult = DialogResult.Cancel,
                Location = new Point(160, 10),
                Width = 60
            };

            btnPanel.Controls.Add(btnOK);
            btnPanel.Controls.Add(btnCancel);

            dialog.Controls.Add(listBox);
            dialog.Controls.Add(btnPanel);

            dialog.AcceptButton = btnOK;
            dialog.CancelButton = btnCancel;

            if (dialog.ShowDialog(this) == DialogResult.OK && listBox.SelectedItem != null)
                return listBox.SelectedItem.ToString();

            return null;
        }

        #endregion
    }
}
