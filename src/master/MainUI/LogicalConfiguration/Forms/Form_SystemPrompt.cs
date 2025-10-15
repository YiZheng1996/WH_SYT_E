using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
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

        // 加载参数（只读内存临时步骤）
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
                        txtPromptContent.Text = param.Message;
                    }
                    else if (paramObj is not null)
                    {
                        try
                        {
                            var jsonObject = JObject.Parse(paramObj.ToString());
                            var p = jsonObject.ToObject<Parameter_SystemPrompt>();
                            txtPromptContent.Text = p?.Message ?? "请输入";
                        }
                        catch
                        {
                            txtPromptContent.Text = "请输入";
                        }
                    }
                    else
                    {
                        txtPromptContent.Text = "请输入";
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载参数错误", ex);
                MessageHelper.MessageOK("加载参数错误。" + ex.Message, AntdUI.TType.Error);
            }
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var steps = _workflowStateService.GetSteps();
                int idx = _workflowStateService.StepNum;
                if (steps != null && idx >= 0 && idx < steps.Count)
                {
                    steps[idx].StepParameter = new Parameter_SystemPrompt { Message = txtPromptContent.Text };
                    MessageHelper.MessageOK("参数已暂存，主界面点击保存后才会写入文件。", AntdUI.TType.Info);
                    Close();
                }
                else
                {
                    MessageHelper.MessageOK("步骤索引无效，无法保存参数。", AntdUI.TType.Error);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("保存参数错误", ex);
                MessageHelper.MessageOK("保存参数错误。" + ex.Message, AntdUI.TType.Error);
            }
        }
    }
}
