using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services.ServicesCommunication;
using MainUI.Procedure.DSL.LogicalConfiguration.Forms;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sunny.UI;
using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 以太网发送参数配置表单
    /// 支持TCP/UDP协议数据发送，可配置超时、编码、响应等参数
    /// </summary>
    public partial class Form_EthernetSend : BaseParameterForm
    {
        #region 私有字段

        private readonly IWorkflowStateService _workflowState;
        private readonly GlobalVariableManager _variableManager;
        private readonly ILogger<Form_EthernetSend> _logger;
        private Parameter_EthernetSend _parameter;
        private bool _isLoading = false;
        private CancellationTokenSource _testCts;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数 - 依赖注入
        /// </summary>
        public Form_EthernetSend(
            IWorkflowStateService workflowState,
            GlobalVariableManager variableManager,
            ILogger<Form_EthernetSend> logger)
        {
            _workflowState = workflowState ?? throw new ArgumentNullException(nameof(workflowState));
            _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            InitializeComponent();
            InitializeForm();

            _logger.LogDebug("Form_EthernetSend 初始化完成");
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
            // 协议类型
            cmbProtocol.Items.Clear();
            cmbProtocol.Items.AddRange(new object[] { "TCP", "UDP" });
            cmbProtocol.SelectedIndex = 0;

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
            btnTestConnection.Click += BtnTestConnection_Click;
            btnTestSend.Click += BtnTestSend_Click;
            btnInsertVariable.Click += BtnInsertVariable_Click;
            btnCreateVariable.Click += BtnCreateVariable_Click;
            btnConditionHelper.Click += BtnConditionHelper_Click;
            btnOK.Click += BtnOK_Click;
            btnCancel.Click += BtnCancel_Click;

            // 复选框事件
            chkWaitResponse.CheckedChanged += ChkWaitResponse_CheckedChanged;
            chkAppendNewLine.CheckedChanged += ChkAppendNewLine_CheckedChanged;

            // 文本框验证
            txtIPAddress.Leave += TxtIPAddress_Leave;
            txtPort.Leave += TxtPort_Leave;
        }

        /// <summary>
        /// 设置默认值
        /// </summary>
        private void SetDefaultValues()
        {
            _parameter = new Parameter_EthernetSend
            {
                IPAddress = "192.168.1.100",
                Port = 8080,
                Protocol = ProtocolType.TCP,
                ConnectTimeout = 5,
                SendTimeout = 5,
                ReceiveTimeout = 5,
                DataFormat = DataFormatType.Text,
                Encoding = EncodingType.UTF8,
                SendContent = "",
                AppendNewLine = false,
                NewLineType = NewLineType.CRLF,
                WaitResponse = false,
                ResponseTimeout = 10,
                ResponseVariableName = "",
                DisconnectAfterSend = true,
                Condition = "",
                Description = $"以太网发送步骤 {_workflowState?.StepNum + 1}",
                IsEnabled = true
            };

            LoadParameterToForm();
        }

        #endregion

        #region 参数接口实现

        /// <summary>
        /// 获取参数
        /// </summary>
        public Parameter_EthernetSend GetParameter()
        {
            SaveFormToParameter();
            return _parameter;
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        public void SetParameter(Parameter_EthernetSend parameter)
        {
            _parameter = parameter ?? new Parameter_EthernetSend();
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
                if (parameter is Parameter_EthernetSend ethernetParam)
                {
                    _parameter = ethernetParam;
                }
                else
                {
                    var json = parameter.ToString();
                    _parameter = JsonConvert.DeserializeObject<Parameter_EthernetSend>(json)
                        ?? new Parameter_EthernetSend();
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
            // 验证IP地址
            if (string.IsNullOrWhiteSpace(txtIPAddress.Text))
            {
                UIMessageTip.ShowError("请输入IP地址");
                txtIPAddress.Focus();
                return false;
            }

            if (!IPAddress.TryParse(txtIPAddress.Text.Trim(), out _))
            {
                UIMessageTip.ShowError("IP地址格式不正确");
                txtIPAddress.Focus();
                return false;
            }

            // 验证端口
            if (!int.TryParse(txtPort.Text, out int port) || port < 1 || port > 65535)
            {
                UIMessageTip.ShowError("端口号必须在1-65535之间");
                txtPort.Focus();
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

                // 连接设置
                txtIPAddress.Text = _parameter.IPAddress ?? "";
                txtPort.Text = _parameter.Port.ToString();
                cmbProtocol.SelectedIndex = _parameter.Protocol == ProtocolType.TCP ? 0 : 1;
                numConnectTimeout.Value = _parameter.ConnectTimeout;
                numSendTimeout.Value = _parameter.SendTimeout;
                numReceiveTimeout.Value = _parameter.ReceiveTimeout;

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
                chkDisconnectAfterSend.Checked = _parameter.DisconnectAfterSend;
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
                // 连接设置
                _parameter.IPAddress = txtIPAddress.Text.Trim();
                _parameter.Port = int.TryParse(txtPort.Text, out int port) ? port : 8080;
                _parameter.Protocol = cmbProtocol.SelectedIndex == 0 ? ProtocolType.TCP : ProtocolType.UDP;
                _parameter.ConnectTimeout = numConnectTimeout.Value;
                _parameter.SendTimeout = numSendTimeout.Value;
                _parameter.ReceiveTimeout = numReceiveTimeout.Value;

                // 数据设置
                _parameter.DataFormat = (DataFormatType)cmbDataFormat.SelectedIndex;
                _parameter.Encoding = (EncodingType)cmbEncoding.SelectedIndex;
                _parameter.SendContent = txtSendContent.Text;
                _parameter.AppendNewLine = chkAppendNewLine.Checked;
                _parameter.NewLineType = (NewLineType)cmbNewLineType.SelectedIndex;

                // 响应设置
                _parameter.WaitResponse = chkWaitResponse.Checked;
                _parameter.ResponseTimeout = numResponseTimeout.Value;
                _parameter.ResponseVariableName = cmbResponseVariable.Text.Trim();

                // 其他设置
                _parameter.DisconnectAfterSend = chkDisconnectAfterSend.Checked;
                _parameter.Condition = txtCondition.Text.Trim();
                _parameter.Description = txtDescription.Text.Trim();
                _parameter.IsEnabled = chkEnabled.Checked;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存表单到参数失败");
            }
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// 测试连接按钮
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
                    Protocol = cmbProtocol.SelectedIndex == 0 ? ProtocolType.TCP : ProtocolType.UDP,
                    ConnectTimeout = numConnectTimeout.Value * 1000,
                    SendTimeout = numSendTimeout.Value * 1000,
                    ReceiveTimeout = numReceiveTimeout.Value * 1000
                };

                var service = new EthernetService(config);
                var result = await service.ConnectAsync(_testCts.Token);

                if (result.Success)
                {
                    lblConnectionStatus.Text = "连接成功";
                    lblConnectionStatus.ForeColor = Color.Green;
                    await service.DisconnectAsync();
                }
                else
                {
                    lblConnectionStatus.Text = "连接失败";
                    lblConnectionStatus.ForeColor = Color.Red;
                    UIMessageTip.ShowError($"连接失败: {result.Message}");
                }
            }
            catch (OperationCanceledException)
            {
                lblConnectionStatus.Text = "已取消";
                lblConnectionStatus.ForeColor = Color.Gray;
            }
            catch (Exception ex)
            {
                lblConnectionStatus.Text = "连接错误";
                lblConnectionStatus.ForeColor = Color.Red;
                _logger.LogError(ex, "测试连接失败");
                UIMessageTip.ShowError($"连接错误: {ex.Message}");
            }
            finally
            {
                btnTestConnection.Enabled = true;
            }
        }

        /// <summary>
        /// 测试发送按钮
        /// </summary>
        private async void BtnTestSend_Click(object sender, EventArgs e)
        {
            if (!ValidateParameter())
                return;

            btnTestSend.Enabled = false;

            try
            {
                _testCts?.Cancel();
                _testCts = new CancellationTokenSource();

                var config = new EthernetConfig
                {
                    IPAddress = txtIPAddress.Text.Trim(),
                    Port = int.Parse(txtPort.Text),
                    Protocol = cmbProtocol.SelectedIndex == 0 ? ProtocolType.TCP : ProtocolType.UDP,
                    ConnectTimeout = numConnectTimeout.Value * 1000,
                    SendTimeout = numSendTimeout.Value * 1000,
                    ReceiveTimeout = numReceiveTimeout.Value * 1000
                };

                var service = new EthernetService(config);

                // 准备数据
                string content = ResolveVariables(txtSendContent.Text);
                byte[] data = PrepareData(content);

                CommunicationResult result;
                if (chkWaitResponse.Checked)
                {
                    result = await service.SendAndReceiveAsync(data, numResponseTimeout.Value * 1000, _testCts.Token);
                    if (result.Success && !string.IsNullOrEmpty(result.ResponseText))
                    {
                        UIMessageTip.ShowOk($"发送成功，响应: {result.ResponseText.Substring(0, Math.Min(100, result.ResponseText.Length))}...");
                    }
                }
                else
                {
                    result = await service.SendAsync(data, _testCts.Token);
                }

                if (result.Success)
                {
                    UIMessageTip.ShowOk($"发送成功! 耗时: {result.ElapsedTime}ms");
                }
                else
                {
                    UIMessageTip.ShowError($"发送失败: {result.Message}");
                }

                if (chkDisconnectAfterSend.Checked)
                {
                    await service.DisconnectAsync();
                }
            }
            catch (OperationCanceledException)
            {
                UIMessageTip.ShowWarning("操作已取消");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "测试发送失败");
                UIMessageTip.ShowError($"发送错误: {ex.Message}");
            }
            finally
            {
                btnTestSend.Enabled = true;
            }
        }

        /// <summary>
        /// 插入变量按钮
        /// </summary>
        private void BtnInsertVariable_Click(object sender, EventArgs e)
        {
            try
            {
                var variables = _variableManager.GetAllVariables()?.Select(v => v.VarName).ToArray();
                if (variables == null || variables.Length == 0)
                {
                    UIMessageTip.ShowWarning("没有可用的变量");
                    return;
                }

                // 创建简单的变量选择菜单
                var menu = new ContextMenuStrip();
                foreach (var varName in variables)
                {
                    var item = new ToolStripMenuItem(varName);
                    item.Click += (s, args) =>
                    {
                        int selStart = txtSendContent.SelectionStart;
                        string insertText = $"{{{varName}}}";
                        txtSendContent.Text = txtSendContent.Text.Insert(selStart, insertText);
                        txtSendContent.SelectionStart = selStart + insertText.Length;
                        txtSendContent.Focus();
                    };
                    menu.Items.Add(item);
                }
                menu.Show(btnInsertVariable, new Point(0, btnInsertVariable.Height));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "显示变量列表失败");
            }
        }

        /// <summary>
        /// 新建变量按钮
        /// </summary>
        private void BtnCreateVariable_Click(object sender, EventArgs e)
        {
            try
            {
                string varName = $"EthernetResponse_{DateTime.Now:HHmmss}";
                if (UIInputDialog.InputStringDialog(ref varName, false, "请输入新变量名"))
                {
                    if (_variableManager.AddVariable(varName, "string", ""))
                    {
                        LoadVariables();
                        cmbResponseVariable.Text = varName;
                        UIMessageTip.ShowOk($"变量 '{varName}' 创建成功");
                    }
                    else
                    {
                        UIMessageTip.ShowError("变量创建失败，可能已存在同名变量");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建变量失败");
                UIMessageTip.ShowError($"创建变量失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 条件助手按钮
        /// </summary>
        private void BtnConditionHelper_Click(object sender, EventArgs e)
        {
            try
            {
                using var helper = new Form_ExpressionHelper(_variableManager);
                helper.SetExpression(txtCondition.Text);
                if (helper.ShowDialog() == DialogResult.OK)
                {
                    txtCondition.Text = helper.GetExpression();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "打开条件助手失败");
            }
        }

        /// <summary>
        /// 等待响应复选框变化
        /// </summary>
        private void ChkWaitResponse_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = chkWaitResponse.Checked;
            numResponseTimeout.Enabled = enabled;
            cmbResponseVariable.Enabled = enabled;
            btnCreateVariable.Enabled = enabled;
        }

        /// <summary>
        /// 追加换行符复选框变化
        /// </summary>
        private void ChkAppendNewLine_CheckedChanged(object sender, EventArgs e)
        {
            cmbNewLineType.Enabled = chkAppendNewLine.Checked;
        }

        /// <summary>
        /// IP地址失去焦点验证
        /// </summary>
        private void TxtIPAddress_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtIPAddress.Text))
            {
                if (!IPAddress.TryParse(txtIPAddress.Text.Trim(), out _))
                {
                    txtIPAddress.ForeColor = Color.Red;
                }
                else
                {
                    txtIPAddress.ForeColor = Color.Black;
                }
            }
        }

        /// <summary>
        /// 端口失去焦点验证
        /// </summary>
        private void TxtPort_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(txtPort.Text, out int port) && port >= 1 && port <= 65535)
            {
                txtPort.ForeColor = Color.Black;
            }
            else
            {
                txtPort.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (!ValidateParameter())
                return;

            SaveFormToParameter();
            DialogResult = DialogResult.OK;
            Close();
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

        #region 辅助方法

        /// <summary>
        /// 验证连接设置
        /// </summary>
        private bool ValidateConnectionSettings()
        {
            if (string.IsNullOrWhiteSpace(txtIPAddress.Text))
            {
                UIMessageTip.ShowError("请输入IP地址");
                return false;
            }

            if (!IPAddress.TryParse(txtIPAddress.Text.Trim(), out _))
            {
                UIMessageTip.ShowError("IP地址格式不正确");
                return false;
            }

            if (!int.TryParse(txtPort.Text, out int port) || port < 1 || port > 65535)
            {
                UIMessageTip.ShowError("端口号必须在1-65535之间");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 解析变量引用
        /// </summary>
        private string ResolveVariables(string content)
        {
            if (string.IsNullOrEmpty(content))
                return content;

            // 匹配 {变量名} 格式
            var pattern = @"\{(\w+)\}";
            return Regex.Replace(content, pattern, match =>
            {
                string varName = match.Groups[1].Value;
                var variable = _variableManager.GetVariable(varName);
                return variable?.VarValue?.ToString() ?? match.Value;
            });
        }

        /// <summary>
        /// 准备发送数据
        /// </summary>
        private byte[] PrepareData(string content)
        {
            // 追加换行符
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
            var encoding = cmbEncoding.SelectedIndex switch
            {
                0 => System.Text.Encoding.UTF8,
                1 => System.Text.Encoding.ASCII,
                2 => System.Text.Encoding.GetEncoding("GB2312"),
                3 => System.Text.Encoding.Unicode,
                _ => System.Text.Encoding.UTF8
            };

            // 根据格式转换
            return cmbDataFormat.SelectedIndex switch
            {
                0 => encoding.GetBytes(content), // 文本
                1 => HexStringToBytes(content),  // 十六进制
                2 => Convert.FromBase64String(content), // Base64
                3 => encoding.GetBytes(content), // JSON
                _ => encoding.GetBytes(content)
            };
        }

        /// <summary>
        /// 十六进制字符串转字节数组
        /// </summary>
        private byte[] HexStringToBytes(string hex)
        {
            hex = hex.Replace(" ", "").Replace("-", "");
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
        /// 窗体关闭时
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