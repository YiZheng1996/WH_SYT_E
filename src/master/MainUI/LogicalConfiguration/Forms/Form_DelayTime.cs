using AntdUI;
using MainUI.LogicalConfiguration.Forms;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.Logging;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    /// <summary>
    /// 延时参数配置表单
    /// </summary>
    public partial class Form_DelayTime : BaseParameterForm
    {
        #region 属性
        private const double DEFAULT_DELAY_TIME = 1000.0;

        private Parameter_DelayTime _parameter;
        /// <summary>
        /// 参数对象 - 基类通过反射访问此属性
        /// </summary>
        public Parameter_DelayTime Parameter
        {
            get => _parameter;
            set
            {
                _parameter = value ?? new Parameter_DelayTime();
                if (!DesignMode && !IsLoading && IsHandleCreated)
                {
                    LoadParameterToForm();
                }
            }
        }
        #endregion

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

        private void InitializeForm()
        {
            if (DesignMode) return;

            BindEvents();
            LoadParameterToForm();
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
        /// <summary>
        /// 保存参数
        /// </summary>
        protected override void SaveFormToParameter()
        {
            try
            {
                _parameter ??= new Parameter_DelayTime();

                // 保存基本信息
                _parameter.T = txtTime.Text.ToDouble();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "保存界面数据到参数对象失败");
                throw;
            }
        }

        /// <summary>
        /// 加载参数到界面
        /// </summary>
        protected override void LoadParameterToForm()
        {
            try
            {
                Parameter ??= new Parameter_DelayTime();

                txtTime.Text = Parameter.T.ToString();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "加载参数到界面失败");
            }
        }

        /// <summary>
        /// 验证输入数据的有效性
        /// </summary>
        protected override bool ValidateInput()
        {
            try
            {
                // 延时时间
                if (string.IsNullOrWhiteSpace(txtTime.Text))
                {
                    MessageHelper.MessageOK("请输入需要延时的时间！", TType.Warn);
                    txtTime.Focus();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "验证输入失败");
                return false;
            }
        }

        // 设置默认值
        protected override void SetDefaultValues()
        {
            Parameter = new Parameter_DelayTime { T = DEFAULT_DELAY_TIME };
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

        #endregion
    }
}