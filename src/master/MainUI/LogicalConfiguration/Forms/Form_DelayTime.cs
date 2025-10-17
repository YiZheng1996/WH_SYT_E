using AntdUI;
using MainUI.LogicalConfiguration.Forms;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    /// <summary>
    /// 延时参数配置表单
    /// </summary>
    public partial class Form_DelayTime : BaseParameterForm, IParameterForm<Parameter_DelayTime>
    {
        private const double DEFAULT_DELAY_TIME = 1000.0;

        /// <summary>
        /// 参数对象
        /// </summary>
        public Parameter_DelayTime Parameter { get; set; }

        #region 构造函数

        // 保留无参构造函数供设计器使用
        public Form_DelayTime()
        {
            InitializeComponent();

            // 只有在非设计时模式才进行初始化
            if (!DesignMode)
            {
                InitializeForm();
            }
        }

        // 依赖注入构造函数（推荐在运行时使用）
        public Form_DelayTime(IWorkflowStateService workflowState, ILogger<Form_DelayTime> logger)
            : base(workflowState, logger)
        {
            InitializeComponent();
            InitializeForm();
        }

        // 支持带参数的构造函数
        public Form_DelayTime(IWorkflowStateService workflowState, ILogger<Form_DelayTime> logger, Parameter_DelayTime parameter)
            : base(workflowState, logger)
        {
            InitializeComponent();
            Parameter = parameter ?? new Parameter_DelayTime();
            InitializeForm();
        }

        private void InitializeForm()
        {
            if (DesignMode) return;

            Parameter ??= new Parameter_DelayTime();
            BindEvents();
        }

        private void BindEvents()
        {
            if (BtnSave != null) BtnSave.Click += OnSaveClick;

            // 绑定输入验证事件
            if (txtTime != null)
            {
                txtTime.KeyPress += txtTime_KeyPress;
                txtTime.Leave += txtTime_Leave;
            }
        }

        #endregion

        #region 重写基类方法

        protected override void LoadParameterFromStep(object stepParameter)
        {
            if (!IsServiceAvailable) return;

            Parameter = ConvertParameter(stepParameter);
            PopulateControls(Parameter);
        }

        protected override void SetDefaultValues()
        {
            Parameter = new Parameter_DelayTime { T = DEFAULT_DELAY_TIME };
            PopulateControls(Parameter);
        }

        protected override bool ValidateParameters()
        {
            return ValidateTypedParameters();
        }

        protected override object CollectParameters()
        {
            return CollectTypedParameters();
        }

        #endregion

        #region IParameterForm<Parameter_DelayTime> 实现

        public void PopulateControls(Parameter_DelayTime parameter)
        {
            if (parameter == null || DesignMode) return;

            try
            {
                if (txtTime != null)
                {
                    txtTime.Text = parameter.T.ToString();
                }

                Logger?.LogDebug("控件填充完成: DelayTime={DelayTime}", parameter.T);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "填充控件失败");
                if (!DesignMode)
                {
                    MessageHelper.MessageOK("填充控件失败：" + ex.Message, TType.Error);
                }
            }
        }

        public bool ValidateTypedParameters()
        {
            if (DesignMode) return true;

            if (txtTime == null || string.IsNullOrWhiteSpace(txtTime.Text))
            {
                MessageHelper.MessageOK("请输入延时时间", TType.Warn);
                return false;
            }

            if (!double.TryParse(txtTime.Text, out double delayTime) || delayTime <= 0)
            {
                MessageHelper.MessageOK("延时时间必须是大于0的数字", TType.Warn);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 收集的类型参数
        /// </summary>
        /// <returns></returns>
        public Parameter_DelayTime CollectTypedParameters()
        {
            if (DesignMode) return new Parameter_DelayTime();

            try
            {
                var parameter = new Parameter_DelayTime
                {
                    T = txtTime != null ? double.Parse(txtTime.Text) : DEFAULT_DELAY_TIME
                };

                Logger?.LogDebug("参数收集完成: DelayTime={DelayTime}", parameter.T);
                return parameter;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "收集参数失败");
                return new Parameter_DelayTime { T = DEFAULT_DELAY_TIME };
            }
        }

        public Parameter_DelayTime ConvertParameter(object stepParameter)
        {
            try
            {
                if (stepParameter is Parameter_DelayTime param)
                {
                    return param;
                }
                else if (stepParameter != null)
                {
                    var jsonObject = JObject.Parse(stepParameter.ToString());
                    return jsonObject.ToObject<Parameter_DelayTime>() ?? new Parameter_DelayTime();
                }

                return new Parameter_DelayTime();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "参数转换失败");
                return new Parameter_DelayTime();
            }
        }

        #endregion

        #region 事件处理

        private void OnSaveClick(object sender, EventArgs e)
        {
            if (DesignMode) return;
            SaveParameters();
        }

        private void txtTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (DesignMode) return;

            // 只允许输入数字和小数点
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // 只允许一个小数点
            if (e.KeyChar == '.' && (sender as TextBox)?.Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtTime_Leave(object sender, EventArgs e)
        {
            if (DesignMode || IsLoading || !IsServiceAvailable) return;

            if (txtTime != null && double.TryParse(txtTime.Text, out double value))
            {
                Parameter.T = value;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {

        }

        void IParameterForm<Parameter_DelayTime>.SetDefaultValues()
        {
            SetDefaultValues();
        }

        #endregion
    }
}