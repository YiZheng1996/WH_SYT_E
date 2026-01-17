using AntdUI;
using MainUI.LogicalConfiguration.Controls;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using MainUI.LogicalConfiguration.Services.ServicesCommunication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Panel = System.Windows.Forms.Panel;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 串口发送参数配置表单
    /// 支持串口数据发送,可配置串口参数、超时、编码、响应等
    /// </summary>
    public partial class Form_SerialPortSend : BaseParameterForm
    {
        #region 私有字段

        private Parameter_SerialPortSend _parameter;
        private CancellationTokenSource _testCts;
        private bool _isInitializing = true;

        #endregion

        #region 属性

        /// <summary>
        /// 参数对象 - 基类通过反射访问此属性
        /// </summary>
        public Parameter_SerialPortSend Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value ?? new Parameter_SerialPortSend();
                if (!DesignMode && !IsLoading && IsHandleCreated)
                {
                    LoadParameterToForm();
                }
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 无参构造函数 - 供设计器和运行时使用
        /// </summary>
        public Form_SerialPortSend()
        {
            InitializeComponent();

            if (DesignMode) return;

            InitializeForm();
        }

        /// <summary>
        /// 依赖注入构造函数
        /// </summary>
        public Form_SerialPortSend(
            IWorkflowStateService workflowState,
            ILogger<Form_SerialPortSend> logger)
            : base(workflowState, logger)
        {
            InitializeComponent();
            InitializeForm();
        }

        #endregion

        #region 初始化方法

        /// <summary>
        /// 初始化表单
        /// </summary>
        private void InitializeForm()
        {
            if (DesignMode) return;

            try
            {
                _isInitializing = true;

                // 初始化控件
                InitializeFormControls();

                SetupExpressionPanels();

                // 绑定事件
                BindEvents();

                // 设置默认值
                SetDefaultValues();

                Logger?.LogDebug("Form_SerialPortSend 初始化完成");
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
        /// 设置表达式输入面板
        /// </summary>
        private void SetupExpressionPanels()
        {
            try
            {
                // 发送内容输入框 - 支持变量、表达式、常量
                ExpressionInputPanel.AttachTo(txtSendContent, new InputPanelOptions
                {
                    Mode = InputMode.Expression,
                    EnabledModules = InputModules.Variable | InputModules.Expression | InputModules.Constant,
                    Title = "发送内容",
                    ShowValidation = true,
                    ShowPreview = true,
                    CloseOnSubmit = true
                });
                txtSendContent.Watermark = "点击输入发送内容，支持 {变量名} 引用变量 (按F2打开面板)";

                // 执行条件输入框 - 支持条件表达式
                ExpressionInputPanel.AttachTo(txtCondition, new InputPanelOptions
                {
                    Mode = InputMode.Condition,
                    EnabledModules = InputModules.Variable | InputModules.PLC |
                                     InputModules.Expression | InputModules.Constant,
                    Title = "执行条件表达式",
                    ShowValidation = true,
                    ShowPreview = true,
                    CloseOnSubmit = true
                });
                txtCondition.Watermark = "可选，如：{Status} == 'Ready' (按F2打开面板)";

                // 响应变量名输入框 - 仅支持变量选择
                //ExpressionInputPanel.AttachTo(cmbResponseVariable, new InputPanelOptions
                //{
                //    Mode = InputMode.VariableOnly,
                //    EnabledModules = InputModules.Variable,
                //    Title = "选择响应保存变量",
                //    ShowValidation = false,
                //    CloseOnSubmit = true
                //});

                Logger?.LogDebug("串口发送表达式输入面板设置完成");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "设置表达式输入面板失败");
            }
        }


        /// <summary>
        /// 初始化表单控件
        /// </summary>
        private void InitializeFormControls()
        {
            // 初始化串口名称下拉框
            RefreshSerialPorts();

            // 初始化波特率下拉框
            cmbBaudRate.Items.Clear();
            foreach (var rate in Parameter_SerialPortSend.GetCommonBaudRates())
            {
                cmbBaudRate.Items.Add(rate.ToString());
            }
            cmbBaudRate.SelectedItem = "9600";

            // 初始化数据位下拉框
            cmbDataBits.Items.Clear();
            cmbDataBits.Items.AddRange(["5", "6", "7", "8"]);
            cmbDataBits.SelectedItem = "8";

            // 初始化校验位下拉框
            cmbParity.Items.Clear();
            cmbParity.Items.AddRange(["None", "Odd", "Even", "Mark", "Space"]);
            cmbParity.SelectedIndex = 0;

            // 初始化停止位下拉框
            cmbStopBits.Items.Clear();
            cmbStopBits.Items.AddRange(["One", "OnePointFive", "Two"]);
            cmbStopBits.SelectedIndex = 0;

            // 初始化流控制下拉框
            cmbHandshake.Items.Clear();
            cmbHandshake.Items.AddRange(["None", "XOnXOff", "RequestToSend", "RequestToSendXOnXOff"]);
            cmbHandshake.SelectedIndex = 0;

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
        /// 绑定事件
        /// </summary>
        private void BindEvents()
        {
            // 按钮事件
            btnRefreshPorts.Click += BtnRefreshPorts_Click;
            btnTestPort.Click += BtnTestPort_Click;
            btnInsertVariable.Click += BtnInsertVariable_Click;
            btnCreateVariable.Click += BtnCreateVariable_Click;
            btnTestSend.Click += BtnTestSend_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            btnHelp.Click += BtnHelp_Click;

            // 复选框事件
            chkWaitResponse.CheckedChanged += ChkWaitResponse_CheckedChanged;
            chkAppendNewLine.CheckedChanged += ChkAppendNewLine_CheckedChanged;

            // 表单关闭事件
            FormClosing += Form_SerialPortSend_FormClosing;
        }

        /// <summary>
        /// 刷新串口列表
        /// </summary>
        private void RefreshSerialPorts()
        {
            try
            {
                cmbPortName.Items.Clear();
                var ports = SerialPort.GetPortNames();

                if (ports.Length > 0)
                {
                    foreach (var port in ports)
                    {
                        cmbPortName.Items.Add(port);
                    }
                    cmbPortName.SelectedIndex = 0;
                }
                else
                {
                    cmbPortName.Text = "无可用串口";
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "刷新串口列表失败");
            }
        }

        /// <summary>
        /// 加载变量列表
        /// </summary>
        private void LoadVariableList()
        {
            try
            {
                cmbResponseVariable.Items.Clear();

                var variableManager = _globalVariable ??
                    Program.ServiceProvider?.GetService<GlobalVariableManager>();

                if (variableManager == null) return;
                var variables = variableManager.GetAllUserVariables();
                foreach (var variable in variables)
                {
                    cmbResponseVariable.Items.Add(variable.VarName);
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
        /// 设置默认值
        /// </summary>
        protected override void SetDefaultValues()
        {
            try
            {
                _parameter = new Parameter_SerialPortSend
                {
                    Description = $"串口发送步骤 {(_workflowState?.StepNum ?? 0) + 1}",
                    IsEnabled = true,
                    Condition = "",

                    // 串口设置
                    PortName = "COM1",
                    BaudRate = 9600,
                    Parity = Parity.None,
                    DataBits = 8,
                    StopBits = StopBits.One,
                    Handshake = Handshake.None,

                    // 数据设置
                    DataFormat = Parameter_EthernetSend.DataFormatType.Text,
                    Encoding = Parameter_EthernetSend.EncodingType.UTF8,
                    SendContent = "",
                    AppendNewLine = false,
                    NewLineType = "\r\n",

                    // 响应设置
                    WaitResponse = false,
                    ResponseTimeout = 3000,
                    ResponseVariableName = "",

                    // 其他设置
                    CloseAfterSend = true,
                    WriteTimeout = 3000
                };

                LoadParameterToForm();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "设置默认值失败");
            }
        }

        /// <summary>
        /// 加载参数到界面
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

                // 串口设置
                cmbPortName.Text = _parameter.PortName;
                cmbBaudRate.Text = _parameter.BaudRate.ToString();
                cmbDataBits.Text = _parameter.DataBits.ToString();
                cmbParity.SelectedIndex = (int)_parameter.Parity;
                cmbStopBits.SelectedIndex = (int)_parameter.StopBits;
                cmbHandshake.SelectedIndex = (int)_parameter.Handshake;

                // 超时设置
                numReadTimeout.Value = _parameter.ResponseTimeout / 1000;
                numWriteTimeout.Value = _parameter.WriteTimeout / 1000;

                // 数据设置
                cmbDataFormat.SelectedIndex = (int)_parameter.DataFormat;
                cmbEncoding.SelectedIndex = (int)_parameter.Encoding;
                txtSendContent.Text = _parameter.SendContent ?? "";
                chkAppendNewLine.Checked = _parameter.AppendNewLine;

                // 换行符类型
                switch (_parameter.NewLineType)
                {
                    case "\r\n": cmbNewLineType.SelectedIndex = 0; break;
                    case "\n": cmbNewLineType.SelectedIndex = 1; break;
                    case "\r": cmbNewLineType.SelectedIndex = 2; break;
                    default: cmbNewLineType.SelectedIndex = 0; break;
                }

                // 响应设置
                chkWaitResponse.Checked = _parameter.WaitResponse;
                numResponseTimeout.Value = _parameter.ResponseTimeout / 1000;
                cmbResponseVariable.Text = _parameter.ResponseVariableName ?? "";

                // 其他设置
                chkCloseAfterSend.Checked = _parameter.CloseAfterSend;

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
        /// 保存界面数据到参数对象
        /// </summary>
        protected override void SaveFormToParameter()
        {
            try
            {
                _parameter ??= new Parameter_SerialPortSend();

                // 基本信息
                _parameter.Description = txtDescription.Text.Trim();
                _parameter.IsEnabled = chkEnabled.Checked;
                _parameter.Condition = txtCondition.Text.Trim();

                // 串口设置
                _parameter.PortName = cmbPortName.Text;
                _parameter.BaudRate = int.TryParse(cmbBaudRate.Text, out var baud) ? baud : 9600;
                _parameter.DataBits = int.TryParse(cmbDataBits.Text, out var bits) ? bits : 8;
                _parameter.Parity = (Parity)cmbParity.SelectedIndex;
                _parameter.StopBits = (StopBits)cmbStopBits.SelectedIndex;
                _parameter.Handshake = (Handshake)cmbHandshake.SelectedIndex;

                // 超时设置
                _parameter.ResponseTimeout = numReadTimeout.Value * 1000;
                _parameter.WriteTimeout = numWriteTimeout.Value * 1000;

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
                _parameter.CloseAfterSend = chkCloseAfterSend.Checked;

                Logger?.LogDebug("界面数据已保存到参数对象");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存界面数据到参数对象失败");
                throw;
            }
        }

        /// <summary>
        /// 验证输入
        /// </summary>
        protected override bool ValidateInput()
        {
            try
            {
                // 验证串口名称
                if (string.IsNullOrWhiteSpace(cmbPortName.Text))
                {
                    MessageHelper.MessageOK(this, "请选择串口号", TType.Warn);
                    cmbPortName.Focus();
                    return false;
                }

                // 验证波特率
                if (!int.TryParse(cmbBaudRate.Text, out int baudRate) || baudRate <= 0)
                {
                    MessageHelper.MessageOK(this, "波特率必须是正整数", TType.Warn);
                    cmbBaudRate.Focus();
                    return false;
                }

                // 验证数据位
                if (!int.TryParse(cmbDataBits.Text, out int dataBits) || dataBits < 5 || dataBits > 8)
                {
                    MessageHelper.MessageOK(this, "数据位必须在5-8之间", TType.Warn);
                    cmbDataBits.Focus();
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
                    string hex = txtSendContent.Text.Replace(" ", "").Replace("-", "");
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
                if (chkWaitResponse.Checked && string.IsNullOrWhiteSpace(cmbResponseVariable.Text))
                {
                    MessageHelper.MessageOK(this, "等待响应时必须指定响应变量名", TType.Warn);
                    cmbResponseVariable.Focus();
                    return false;
                }

                return true;
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
                var helpText = @"📖 串口发送工具使用说明

串口设置
   - 串口：选择正确的串口号（如 COM1）
   - 波特率：常用 9600、115200 等
   - 数据位：通常为 8 位
   - 校验位：None/Odd/Even/Mark/Space
   - 停止位：One/OnePointFive/Two

数据格式
   - 文本：直接发送文本内容
   - 十六进制：发送十六进制数据（如：FF AA BB）
   - Base64：发送Base64编码数据

变量引用
   - 使用 {变量名} 格式引用全局变量
   - 例如：CMD:{CommandID}
   - 发送前会自动替换为变量的实际值
   - 点击发送内容输入框可打开表达式面板
   - 按 F2 快速打开输入面板

响应处理
   - 勾选【等待响应】可接收设备返回数据
   - 响应数据可保存到指定的全局变量
   - 可用于后续步骤的条件判断

执行条件
   - 可选设置，为空时总是执行
   - 支持表达式，如：{DeviceReady} == true
   - 条件为 true 时才执行发送操作

换行符
   - CRLF (\r\n)：Windows 标准
   - LF (\n)：Linux/Unix 标准
   - CR (\r)：旧版 Mac 标准

使用建议
   - 确保串口参数与设备配置一致
   - 可先使用刷新按钮检测可用串口
   - 测试按钮可验证串口是否正常";

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
        /// 刷新串口按钮
        /// </summary>
        private void BtnRefreshPorts_Click(object sender, EventArgs e)
        {
            RefreshSerialPorts();
            MessageHelper.MessageOK(this, "串口列表已刷新", TType.Success);
        }

        /// <summary>
        /// 测试串口按钮
        /// </summary>
        private async void BtnTestPort_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbPortName.Text))
            {
                MessageHelper.MessageOK(this, "请先选择串口", TType.Warn);
                return;
            }

            btnTestPort.Enabled = false;
            lblPortStatus.Text = "测试中...";
            lblPortStatus.ForeColor = Color.Orange;

            try
            {
                _testCts?.Cancel();
                _testCts = new CancellationTokenSource();

                await Task.Run(() =>
                {
                    using var port = new SerialPort
                    {
                        PortName = cmbPortName.Text,
                        BaudRate = int.TryParse(cmbBaudRate.Text, out int baud) ? baud : 9600,
                        Parity = (Parity)cmbParity.SelectedIndex,
                        DataBits = int.TryParse(cmbDataBits.Text, out int bits) ? bits : 8,
                        StopBits = (StopBits)cmbStopBits.SelectedIndex
                    };

                    port.Open();
                    Thread.Sleep(500); // 短暂延时确保端口打开
                    port.Close();
                }, _testCts.Token);

                lblPortStatus.Text = "串口可用";
                lblPortStatus.ForeColor = Color.Green;
                MessageHelper.MessageOK(this, "串口测试成功", TType.Success);
            }
            catch (OperationCanceledException)
            {
                lblPortStatus.Text = "测试取消";
                lblPortStatus.ForeColor = Color.Gray;
            }
            catch (Exception ex)
            {
                lblPortStatus.Text = "串口异常";
                lblPortStatus.ForeColor = Color.Red;
                Logger?.LogError(ex, "测试串口失败: {Port}", cmbPortName.Text);
                MessageHelper.MessageOK(this, $"测试失败: {ex.Message}", TType.Error);
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
            if (!ValidateInput())
                return;

            btnTestSend.Enabled = false;

            try
            {
                _testCts?.Cancel();
                _testCts = new CancellationTokenSource();

                SaveFormToParameter();

                string content = ResolveVariables(txtSendContent.Text);
                byte[] data = PrepareData(content);

                using var port = new SerialPort
                {
                    PortName = _parameter.PortName,
                    BaudRate = _parameter.BaudRate,
                    Parity = _parameter.Parity,
                    DataBits = _parameter.DataBits,
                    StopBits = _parameter.StopBits,
                    Handshake = _parameter.Handshake,
                    ReadTimeout = _parameter.ResponseTimeout,
                    WriteTimeout = _parameter.WriteTimeout
                };

                await Task.Run(async () =>
                {
                    port.Open();

                    port.Write(data, 0, data.Length);

                    if (_parameter.WaitResponse)
                    {
                        // 等待响应
                        var buffer = new byte[4096];
                        var bytesRead = port.Read(buffer, 0, buffer.Length);
                        var response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                        this.Invoke(new Action(() =>
                        {
                            MessageHelper.MessageOK(this,
                                $"发送成功,收到响应:\n{response}",
                                TType.Success);
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            MessageHelper.MessageOK(this,
                                $"发送成功,共 {data.Length} 字节",
                                TType.Success);
                        }));
                    }

                    await Task.Delay(100); // 短暂延时确保数据发送完成
                    port.Close();
                }, _testCts.Token);
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
        /// 插入变量按钮
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

                if (!string.IsNullOrEmpty(selectedVar))
                {
                    // 在光标位置插入变量引用
                    int selectionStart = txtSendContent.SelectionStart;
                    string newText = txtSendContent.Text.Insert(selectionStart, $"{{{selectedVar}}}");
                    txtSendContent.Text = newText;
                    txtSendContent.SelectionStart = selectionStart + selectedVar.Length + 2;
                    txtSendContent.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "插入变量失败");
                MessageHelper.MessageOK(this, $"插入变量失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 创建变量按钮
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
        /// 表单关闭事件
        /// </summary>
        private void Form_SerialPortSend_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 关闭活动的表达式面板
            ExpressionInputPanel.CloseActivePanel();

            _testCts?.Cancel();
            _testCts?.Dispose();
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 解析变量引用
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
        /// 准备发送数据
        /// </summary>
        private byte[] PrepareData(string content)
        {
            if (_parameter.AppendNewLine)
            {
                content += _parameter.NewLineType;
            }

            var encoding = GetEncoding(_parameter.Encoding);

            return _parameter.DataFormat switch
            {
                Parameter_EthernetSend.DataFormatType.Hex => HexStringToBytes(content),
                Parameter_EthernetSend.DataFormatType.Base64 => Convert.FromBase64String(content),
                _ => encoding.GetBytes(content)
            };
        }

        /// <summary>
        /// 获取编码
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

        /// <summary>
        /// 显示变量选择对话框
        /// </summary>
        private string ShowVariableSelectDialog(List<string> variables)
        {
            // 简单实现,可以后续改进为更友好的对话框
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

            foreach (var variable in variables)
            {
                listBox.Items.Add(variable);
            }

            var btnPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50
            };

            var btnOK = new Sunny.UI.UIButton
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
            {
                return listBox.SelectedItem.ToString();
            }

            return null;
        }

        #endregion
    }
}