using MainUI.LogicalConfiguration.Instrument.Models;
using Sunny.UI;

namespace MainUI.LogicalConfiguration.Instrument.Forms
{
    /// <summary>
    /// 命令参数定义编辑窗体
    /// </summary>
    public partial class FrmCommandParameterEditor : UIForm
    {
        #region 属性

        /// <summary>
        /// 编辑完成后的参数对象
        /// </summary>
        public CommandParameter Parameter { get; private set; }

        #endregion

        #region 构造函数

        public FrmCommandParameterEditor(CommandParameter parameter = null)
        {
            InitializeComponent();
            InitFormData();

            if (parameter != null)
            {
                Parameter = CloneParameter(parameter);
                LoadToForm(parameter);
                this.Text = "编辑参数";
            }
            else
            {
                Parameter = new CommandParameter();
                this.Text = "添加参数";
            }

            btnOk.Click += BtnOk_Click;
            btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region 初始化

        private void InitFormData()
        {
            cboDataType.DataSource = EnumExtensions.GetEnumItems<DataType>();
            cboDataType.DisplayMember = "DisplayName";
            cboDataType.ValueMember = "Value";
            cboDataType.SelectedValue = DataType.Double;
        }

        #endregion

        #region 数据加载

        private void LoadToForm(CommandParameter p)
        {
            txtName.Text = p.Name;
            txtDisplayName.Text = p.DisplayName;
            txtDefaultValue.Text = p.DefaultValue;
            txtDescription.Text = p.Description;
            chkRequired.Checked = p.Required;

            if (cboDataType.Items.Count > 0)
                cboDataType.SelectedValue = p.DataType;

            bool hasRange = p.MinValue.HasValue || p.MaxValue.HasValue;
            chkHasRange.Checked = hasRange;
            if (hasRange)
            {
                numMin.Value = (p.MinValue ?? 0);
                numMax.Value = (p.MaxValue ?? 9999);
            }

            UpdateRangeVisibility();
        }

        #endregion

        #region 事件处理

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageHelper.MessageOK(this, $"参数名称不能为空{txtName}");
                txtName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDisplayName.Text))
            {
                MessageHelper.MessageOK(this, $"显示名称不能为空{txtDisplayName}");
                txtDisplayName.Focus();
                return;
            }

            Parameter.Name = txtName.Text.Trim();
            Parameter.DisplayName = txtDisplayName.Text.Trim();
            Parameter.DataType = (DataType)(cboDataType.SelectedValue ?? DataType.String);
            Parameter.DefaultValue = txtDefaultValue.Text;
            Parameter.Required = chkRequired.Checked;
            Parameter.Description = txtDescription.Text.Trim();

            if (chkHasRange.Checked)
            {
                Parameter.MinValue = (double)numMin.Value;
                Parameter.MaxValue = (double)numMax.Value;

                if (Parameter.MinValue > Parameter.MaxValue)
                {
                    MessageHelper.MessageOK(this, "最小值不能大于最大值");
                    return;
                }
            }
            else
            {
                Parameter.MinValue = null;
                Parameter.MaxValue = null;
            }

            DialogResult = DialogResult.OK;
        }

        private void ChkHasRange_CheckedChanged(object sender, EventArgs e)
            => UpdateRangeVisibility();

        #endregion

        #region 辅助方法

        private void UpdateRangeVisibility()
        {
            panelRange.Visible = chkHasRange.Checked;
        }

        private static CommandParameter CloneParameter(CommandParameter p) => new CommandParameter
        {
            Name = p.Name,
            DisplayName = p.DisplayName,
            DataType = p.DataType,
            DefaultValue = p.DefaultValue,
            Required = p.Required,
            Description = p.Description,
            MinValue = p.MinValue,
            MaxValue = p.MaxValue,
            Options = p.Options != null ? new List<string>(p.Options) : null
        };

        #endregion
    }
}