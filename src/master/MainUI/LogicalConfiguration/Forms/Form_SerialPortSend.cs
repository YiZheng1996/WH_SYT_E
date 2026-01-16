using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services.ServicesCommunication;
using MainUI.Procedure.DSL.LogicalConfiguration.Forms;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sunny.UI;
using System;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 串口发送参数配置表单
    /// 支持串口数据发送，可配置串口参数、超时、编码、响应等
    /// </summary>
    public partial class Form_SerialPortSend : BaseParameterForm
    {
        #region 私有字段

        private Parameter_SerialPortSend _parameter;
        private CancellationTokenSource _testCts;

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
                if (!DesignMode && !IsLoading)
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

            // 初始化控件
            InitializeFormControls();
            // 绑定事件
            BindEvents();
            // 设置默认值
            SetDefaultValues();
        }

        #endregion

        #region 初始化方法

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
            cmbDataBits.Items.AddRange(new object[] { "5", "6", "7", "8" });
            cmbDataBits.SelectedItem = "8";

            // 初始化校验位下拉框
            cmbParity.Items.Clear();
            cmbParity.Items.AddRange(new object[] { "None", "Odd", "Even", "Mark", "Space" });
            cmbParity.SelectedIndex = 0;

            // 初始化停止位下拉框
            cmbStopBits.Items.Clear();
            cmbStopBits.Items.AddRange(new object[] { "One", "OnePointFive", "Two" });
            cmbStopBits.SelectedIndex = 0;

            // 初始化流控制下拉框
            cmbHandshake.Items.Clear();
            cmbHandshake.Items.AddRange(new object[] { "None", "XOnXOff", "RequestToSend", "RequestToSendXOnXOff" });
            cmbHandshake.SelectedIndex = 0;

            // 初始化数据格式下拉框
            cmbDataFormat.Items.Clear();
            cmbDataFormat.Items.AddRange(new object[] { "文本", "十六进制" });
            cmbDataFormat.SelectedIndex = 0;

            // 初始化编码下拉框
            cmbEncoding.Items.Clear();
            cmbEncoding.Items.AddRange(new object[] { "UTF-8", "ASCII", "GB2312", "Unicode" });
            cmbEncoding.SelectedIndex = 0;

            // 初始化换行符类型下拉框
            cmbNewLineType.Items.Clear();
            cmbNewLineType.Items.AddRange(new object[] { "CRLF (\\r\\n)", "LF (\\n)", "CR (\\r)" });
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
            btnTestSend.Click += BtnTestSend_Click;
            btnCreateVariable.Click += BtnCreateVariable_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;

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
            cmbPortName.Items.Clear();

            try
            {
                var ports = SerialPort.GetPortNames();
                if (ports.Length > 0)
                {
                    cmbPortName.Items.AddRange(ports.Cast<object>().ToArray());
                    cmbPortName.SelectedIndex = 0;
                }
                else
                {
                    cmbPortName.Items.Add("(无可用串口)");
                    cmbPortName.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "获取串口列表失败");
                cmbPortName.Items.Add("(获取失败)");
                cmbPortName.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 加载变量列表
        /// </summary>
        private void LoadVariableList()
        {
            cmbResponseVariable.Items.Clear();
            cmbResponseVariable.Items.Add("");

            if (_globalVariable != null)
            {
                var variables = _globalVariable.GetAllVariables();
                foreach (var v in variables.Where(v => v.VarType == "string"))
                {
                    cmbResponseVariable.Items.Add(v.VarName);
                }
            }
        }

        /// <summary>
        /// 设置默认值
        /// </summary>
        protected override void SetDefaultValues()
        {
            _parameter = new Parameter_SerialPortSend
            {
                PortName = cmbPortName.Items.Count > 0 &&
                           cmbPortName.Items[0].ToString() != "(无可用串口)" &&
                           cmbPortName.Items[0].ToString() != "(获取失败)"
                    ? cmbPortName.Items[0].ToString() : "COM1",
                BaudRate = 9600,
                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One,
                Handshake = Handshake.None,
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
                Description = $"串口发送步骤 {WorkflowState?.StepNum + 1}",
                IsEnabled = true
            };

            LoadParameterToForm();
        }

        #endregion

        #region BaseParameterForm 重写方法

        /// <summary>
        /// 加载参数到表单 - BaseParameterForm 要求实现
        /// </summary>
        protected override void LoadParameterToForm()
        {
            if (_parameter == null) return;

            try
            {
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
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载参数到表单失败");
            }
        }

        /// <summary>
        /// 保存表单到参数 - BaseParameterForm 要求实现
        /// </summary>
        protected override void SaveFormToParameter()
        {
            try
            {
                // 串口设置
                _parameter.PortName = cmbPortName.Text;
                _parameter.BaudRate = int.TryParse(cmbBaudRate.Text, out int baud) ? baud : 9600;
                _parameter.DataBits = int.TryParse(cmbDataBits.Text, out int bits) ? bits : 8;
                _parameter.Parity = (Parity)cmbParity.SelectedIndex;
                _parameter.StopBits = GetStopBitsFromIndex(cmbStopBits.SelectedIndex);
                _parameter.Handshake = (Handshake)cmbHandshake.SelectedIndex;

                // 超时设置
                _parameter.ReadTimeout = (int)numReadTimeout.Value;
                _parameter.WriteTimeout = (int)numWriteTimeout.Value;

                // 数据设置
                _parameter.DataFormat = (Parameter_EthernetSend.DataFormatType)cmbDataFormat.SelectedIndex;
                _parameter.Encoding = (Parameter_EthernetSend.EncodingType)cmbEncoding.SelectedIndex;
                _parameter.SendContent = txtSendContent.Text.Trim();
                _parameter.AppendNewLine = chkAppendNewLine.Checked;
                _parameter.NewLineType = (NewLineType)cmbNewLineType.SelectedIndex;

                // 响应设置
                _parameter.WaitResponse = chkWaitResponse.Checked;
                _parameter.ResponseTimeout = (int)numResponseTimeout.Value;
                _parameter.ResponseVariableName = cmbResponseVariable.Text.Trim();

                // 其他设置
                _parameter.CloseAfterSend = chkCloseAfterSend.Checked;
                _parameter.Condition = txtCondition.Text.Trim();
                _parameter.Description = txtDescription.Text.Trim();
                _parameter.IsEnabled = chkEnabled.Checked;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存表单到参数失败");
            }
        }

        /// <summary>
        /// 验证输入 - BaseParameterForm 要求实现
        /// </summary>
        protected override bool ValidateInput()
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

        /// <summary>
        /// 收集参数（基类方法重写）
        /// </summary>
        protected override object CollectParameters()
        {
            SaveFormToParameter();
            return _parameter;
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 从停止位枚举获取索引
        /// </summary>
        private int GetStopBitsIndex(StopBits stopBits)
        {
            return stopBits switch
            {
                StopBits.One => 0,
                StopBits.OnePointFive => 1,
                StopBits.Two => 2,
                _ => 0
            };
        }

        /// <summary>
        /// 从索引获取停止位枚举
        /// </summary>
        private StopBits GetStopBitsFromIndex(int index)
        {
            return index switch
            {
                0 => StopBits.One,
                1 => StopBits.OnePointFive,
                2 => StopBits.Two,
                _ => StopBits.One
            };
        }

        /// <summary>
        /// 解析变量引用
        /// </summary>
        private string ResolveVariables(string content)
        {
            if (string.IsNullOrEmpty(content) || _globalVariable == null)
                return content;

            // 匹配 {变量名} 格式
            var pattern = @"\{([^{}]+)\}";
            return Regex.Replace(content, pattern, match =>
            {
                var varName = match.Groups[1].Value;
                var variable = _globalVariable.GetAllVariables()
                    .FirstOrDefault(v => v.VarName == varName);

                return variable?.VarValue?.ToString() ?? match.Value;
            });
        }

        /// <summary>
        /// 准备发送数据
        /// </summary>
        private byte[] PrepareData(string content)
        {
            if (string.IsNullOrEmpty(content))
                return Array.Empty<byte>();

            // 追加换行符
            if (_parameter.AppendNewLine)
            {
                content += _parameter.NewLineType switch
                {
                    NewLineType.CRLF => "\r\n",
                    NewLineType.LF => "\n",
                    NewLineType.CR => "\r",
                    _ => "\r\n"
                };
            }

            // 根据数据格式转换
            if (_parameter.DataFormat == Parameter_EthernetSend.DataFormatType.Hex)
            {
                return HexStringToBytes(content);
            }

            // 根据编码转换
            Encoding encoding = _parameter.Encoding switch
            {
                Parameter_EthernetSend.EncodingType.ASCII => Encoding.ASCII,
                Parameter_EthernetSend.EncodingType.GB2312 => Encoding.GetEncoding("GB2312"),
                Parameter_EthernetSend.EncodingType.Unicode => Encoding.Unicode,
                _ => Encoding.UTF8
            };

            return encoding.GetBytes(content);
        }

        /// <summary>
        /// 十六进制字符串转字节数组
        /// </summary>
        private byte[] HexStringToBytes(string hex)
        {
            hex = hex.Replace(" ", "").Replace("-", "");
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

            btnTestPort.Enabled = false;
            lblPortStatus.Text = "测试中...";
            lblPortStatus.ForeColor = Color.Orange;

            try
            {
                _testCts?.Cancel();
                _testCts = new CancellationTokenSource();

                SaveFormToParameter();

                // 简单测试：尝试打开和关闭串口
                using var port = new SerialPort
                {
                    PortName = _parameter.PortName,
                    BaudRate = _parameter.BaudRate,
                    Parity = _parameter.Parity,
                    DataBits = _parameter.DataBits,
                    StopBits = _parameter.StopBits,
                    Handshake = _parameter.Handshake
                };

                await Task.Run(() =>
                {
                    port.Open();
                    Thread.Sleep(100);
                    port.Close();
                }, _testCts.Token);

                lblPortStatus.Text = "串口正常";
                lblPortStatus.ForeColor = Color.Green;
                UIMessageTip.ShowOk($"串口 {cmbPortName.Text} 测试成功");
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
            if (!ValidateInput())
                return;

            btnTestSend.Enabled = false;
            btnTestSend.Text = "发送中...";

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
                    ReadTimeout = _parameter.ResponseTimeout * 1000,
                    WriteTimeout = _parameter.WriteTimeout
                };

                await Task.Run(async () =>
                {
                    port.Open();

                    port.Write(data, 0, data.Length);

                    if (_parameter.WaitResponse)
                    {
                        await Task.Delay(_parameter.ResponseTimeout * 1000, _testCts.Token);
                        if (port.BytesToRead > 0)
                        {
                            string response = port.ReadExisting();
                            this.Invoke(() =>
                            {
                                UIMessageTip.ShowOk($"发送成功，响应: {response}");
                            });
                        }
                        else
                        {
                            this.Invoke(() =>
                            {
                                UIMessageTip.ShowWarning("发送成功，但未收到响应");
                            });
                        }
                    }
                    else
                    {
                        this.Invoke(() =>
                        {
                            UIMessageTip.ShowOk("发送成功");
                        });
                    }

                    if (_parameter.CloseAfterSend)
                    {
                        port.Close();
                    }
                }, _testCts.Token);
            }
            catch (OperationCanceledException)
            {
                UIMessageTip.ShowWarning("发送已取消");
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "测试发送失败");
                UIMessageTip.ShowError($"发送失败: {ex.Message}");
            }
            finally
            {
                btnTestSend.Enabled = true;
                btnTestSend.Text = "测试发送";
            }
        }

        /// <summary>
        /// 创建变量按钮
        /// </summary>
        private void BtnCreateVariable_Click(object sender, EventArgs e)
        {
            // 弹出创建变量对话框
            using var inputForm = new UIInputForm();
            inputForm.Text = "创建响应变量";

            if (inputForm.ShowDialog(this) == DialogResult.OK)
            {
                string varName = inputForm.Value?.Trim();
                if (!string.IsNullOrEmpty(varName))
                {
                    _globalVariable?.CreateVariable(varName, "string", "");
                    LoadVariableList();
                    cmbResponseVariable.Text = varName;
                    UIMessageTip.ShowOk($"变量 '{varName}' 创建成功");
                }
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            OnSaveClick();
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            OnCancelClick();
        }

        #endregion

        #region 复选框事件处理

        /// <summary>
        /// 等待响应复选框
        /// </summary>
        private void ChkWaitResponse_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = chkWaitResponse.Checked;
            numResponseTimeout.Enabled = enabled;
            cmbResponseVariable.Enabled = enabled;
            btnCreateVariable.Enabled = enabled;
        }

        /// <summary>
        /// 追加换行符复选框
        /// </summary>
        private void ChkAppendNewLine_CheckedChanged(object sender, EventArgs e)
        {
            cmbNewLineType.Enabled = chkAppendNewLine.Checked;
        }

        #endregion

        #region 表单事件

        /// <summary>
        /// 表单关闭事件
        /// </summary>
        private void Form_SerialPortSend_FormClosing(object sender, FormClosingEventArgs e)
        {
            _testCts?.Cancel();
            _testCts?.Dispose();
        }

        #endregion
    }
}