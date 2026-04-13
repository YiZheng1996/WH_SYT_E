using MainUI.LogicalConfiguration.Controls;
using MainUI.LogicalConfiguration.Helpers;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace MainUI.LogicalConfiguration.Forms
{
    public partial class Form_SystemPrompt : UIForm
    {
        private readonly IWorkflowStateService _workflowStateService;
        private readonly ILogger<Form_SystemPrompt> _logger;

        public Form_SystemPrompt(IWorkflowStateService workflowStateService, ILogger<Form_SystemPrompt> logger)
        {
            _workflowStateService = workflowStateService;
            _logger = logger;

            InitializeComponent();
            InitForm();
        }

        /// <summary>
        /// 初始化表单，加载现有参数
        /// </summary>
        private void InitForm()
        {
            try
            {
                // 设置默认值
                cmbDialogType.SelectedIndex = 0;
                cmbMessageLevel.SelectedIndex = 0;
                UpdateResultVariableVisibility();

                var steps = _workflowStateService.GetSteps();
                int idx = _workflowStateService.StepNum;

                AttachExpressionPanels();

                if (steps != null && idx >= 0 && idx < steps.Count)
                {
                    var paramObj = steps[idx].StepParameter;
                    if (paramObj is Parameter_SystemPrompt param)
                    {
                        LoadParameter(param);
                    }
                    else if (paramObj is not null)
                    {
                        try
                        {
                            var jsonObject = JObject.Parse(paramObj.ToString());
                            var p = jsonObject.ToObject<Parameter_SystemPrompt>();
                            if (p != null)
                            {
                                LoadParameter(p);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "解析参数失败");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "初始化表单失败");
            }
        }

        /// <summary>
        /// 附加表达式输入面板
        /// </summary>
        private void AttachExpressionPanels()
        {
            try
            {
                // 消息内容框 — 支持混合文本 + 变量引用
                ExpressionInputPanel.AttachTo(txtPromptContent, new InputPanelOptions
                {
                    Mode = InputMode.VariableOnly,   // 文本混合变量模式
                    EnabledModules = InputModules.Variable,
                    Title = "编辑提示内容",
                    ShowValidation = false,
                    ShowPreview = true,
                    CloseOnSubmit = false,                // 不自动关闭，方便继续编辑
                });
                txtPromptContent.Watermark = "输入提示内容，用 {变量名} 引用变量，如：请将压力调整到{范围上限}kPa";

                // 为条件表达式文本框附加ExpressionInputPanel
                ExpressionInputPanel.AttachTo(txtResultVariable, new InputPanelOptions
                {
                    Mode = InputMode.VariableOnly,
                    EnabledModules = InputModules.Variable,
                    Title = "配置条件表达式",
                    ShowValidation = true,
                    ShowPreview = true,
                    CloseOnSubmit = true,
                    ExpectedReturnType = typeof(bool)
                });

                // 设置水印提示
                txtResultVariable.Watermark = "点击选择变量，如：{开始气压} (按F2打开面板)";
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "附加表达式输入面板失败");
            }
        }

        /// <summary>
        /// 加载参数到界面
        /// </summary>
        private void LoadParameter(Parameter_SystemPrompt param)
        {
            txtPromptContent.Text = param.Message ?? string.Empty;

            // 设置对话框类型
            cmbDialogType.SelectedIndex = param.DialogType switch
            {
                DialogType.OK => 0,
                DialogType.YesNo => 1,
                DialogType.OKCancel => 2,
                _ => 0
            };

            // 设置提示等级
            cmbMessageLevel.SelectedIndex = param.MessageLevel switch
            {
                MessageLevel.Info => 0,
                MessageLevel.Warning => 1,
                MessageLevel.Error => 2,
                MessageLevel.Question => 3,
                _ => 0
            };

            // 设置返回值变量
            txtResultVariable.Text = param.ResultVariable ?? string.Empty;

            // 更新返回值变量面板的可见性
            UpdateResultVariableVisibility();
        }

        /// <summary>
        /// 对话框类型改变事件
        /// </summary>
        private void CmbDialogType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateResultVariableVisibility();
        }

        /// <summary>
        /// 根据对话框类型更新返回值变量面板的可见性
        /// </summary>
        private void UpdateResultVariableVisibility()
        {
            // 只有 是/否 和 确认/取消 类型需要返回值变量
            bool needsResultVariable = cmbDialogType.SelectedIndex > 0;
            pnlResultVariable.Visible = needsResultVariable;

            // 动态调整保存按钮位置
            if (needsResultVariable)   // 是否
            {
                BtnSave.Location = new Point(70, 420);
                btnCancel.Location = new Point(230, 420);
                this.ClientSize = new Size(427, 472);
            }
            else                      // 确认
            {
                BtnSave.Location = new Point(70, 330);
                btnCancel.Location = new Point(230, 330);
                this.ClientSize = new Size(427, 380);
            }
        }

        /// <summary>
        /// 保存按钮点击事件
        /// </summary>
        /// <summary>
        /// 保存按钮点击事件 - 添加变量名验证
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 验证输入
                if (string.IsNullOrWhiteSpace(txtPromptContent.Text))
                {
                    MessageBox.Show("请输入提示内容", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 如果是需要返回值的类型，验证变量名
                var dialogType = GetSelectedDialogType();
                if (dialogType != DialogType.OK)
                {
                    var varNameInput = txtResultVariable.Text?.Trim();

                    if (string.IsNullOrWhiteSpace(varNameInput))
                    {
                        MessageBox.Show("请输入用于保存结果的变量名", "提示",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 使用 VariableNameHelper 验证变量名格式
                    var normalizedVarName = VariableNameHelper.NormalizeVariableName(varNameInput);

                    if (normalizedVarName == null)
                    {
                        MessageBox.Show(
                            $"变量名格式无效: {varNameInput}\n\n" +
                            "变量名规则：\n" +
                            "1. 只能包含字母、数字、下划线、中文\n" +
                            "2. 不能以数字开头\n" +
                            "3. 可以带花括号 {变量名} 或不带",
                            "提示",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    // 如果用户输入的是带花括号的格式，给出友好提示
                    //if (varNameInput != normalizedVarName)
                    //{
                    //    var result = MessageBox.Show(
                    //        $"检测到您输入的变量名包含花括号：\n\n" +
                    //        $"原始输入: {varNameInput}\n" +
                    //        $"规范化后: {normalizedVarName}\n\n" +
                    //        $"系统会自动使用规范化后的变量名。是否继续？",
                    //        "变量名规范化",
                    //        MessageBoxButtons.YesNo,
                    //        MessageBoxIcon.Information);

                    //    if (result != DialogResult.Yes)
                    //    {
                    //        return;
                    //    }
                    //}
                }

                // 创建参数对象（保存原始输入，在SystemMethods中再规范化）
                var param = new Parameter_SystemPrompt
                {
                    Message = txtPromptContent.Text.Trim(),
                    DialogType = dialogType,
                    MessageLevel = GetSelectedMessageLevel(),
                    ResultVariable = dialogType != DialogType.OK
                        ? txtResultVariable.Text.Trim()
                        : null,
                    WaitForResponse = true
                };

                // 保存到工作流状态
                var steps = _workflowStateService.GetSteps();
                int idx = _workflowStateService.StepNum;

                if (steps != null && idx >= 0 && idx < steps.Count)
                {
                    steps[idx].StepParameter = param;
                    _logger.LogInformation(
                        "系统提示参数已保存: DialogType={DialogType}, MessageLevel={MessageLevel}, ResultVariable={ResultVariable}",
                        param.DialogType, param.MessageLevel, param.ResultVariable);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存参数失败");
                MessageBox.Show($"保存失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 获取选中的对话框类型
        /// </summary>
        private DialogType GetSelectedDialogType()
        {
            return cmbDialogType.SelectedIndex switch
            {
                0 => DialogType.OK,
                1 => DialogType.YesNo,
                2 => DialogType.OKCancel,
                _ => DialogType.OK
            };
        }

        /// <summary>
        /// 获取选中的提示等级
        /// </summary>
        private MessageLevel GetSelectedMessageLevel()
        {
            return cmbMessageLevel.SelectedIndex switch
            {
                0 => MessageLevel.Info,
                1 => MessageLevel.Warning,
                2 => MessageLevel.Error,
                3 => MessageLevel.Question,
                _ => MessageLevel.Info
            };
        }

        /// <summary>
        /// 验证变量名格式
        /// </summary>
        private static bool IsValidVariableName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            // 不能以数字开头
            if (char.IsDigit(name[0]))
                return false;

            // 只能包含字母、数字、下划线和中文
            foreach (char c in name)
            {
                if (!char.IsLetterOrDigit(c) && c != '_')
                    return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Form_SystemPrompt_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 关闭活动的表达式面板
            ExpressionInputPanel.CloseActivePanel();
        }
    }
}