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

        // 加载参数
        private void InitForm()
        {
            try
            {
                var steps = _workflowStateService.GetSteps();
                int idx = _workflowStateService.StepNum;

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

        // 加载参数到界面
        private void LoadParameter(Parameter_SystemPrompt param)
        {
            txtPromptContent.Text = param.Message ?? string.Empty;
            chkWaitResponse.Checked = param.WaitForResponse;

            // 设置对话框类型
            cmbDialogType.SelectedIndex = param.DialogType switch
            {
                DialogType.Message => 0,
                DialogType.YesNo => 1,
                DialogType.YesNoCancel => 2,
                DialogType.OKCancel => 3,
                DialogType.OK => 4,
                _ => 0
            };
        }

        // 保存按钮点击事件
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

                // 创建参数对象
                var param = new Parameter_SystemPrompt
                {
                    Message = txtPromptContent.Text.Trim(),
                    WaitForResponse = chkWaitResponse.Checked,
                    DialogType = cmbDialogType.SelectedIndex switch
                    {
                        0 => DialogType.Message,
                        1 => DialogType.YesNo,
                        2 => DialogType.YesNoCancel,
                        3 => DialogType.OKCancel,
                        4 => DialogType.OK,
                        _ => DialogType.Message
                    }
                };

                // 保存到工作流状态
                var steps = _workflowStateService.GetSteps();
                int idx = _workflowStateService.StepNum;

                if (steps != null && idx >= 0 && idx < steps.Count)
                {
                    steps[idx].StepParameter = param;
                    _logger.LogInformation("系统提示参数已保存");
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
    }
}