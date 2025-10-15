using AntdUI;
using Sunny.UI;

namespace MainUI.Procedure.Edit
{
    public partial class frmPermissionEdit : UIForm
    {
        public frmPermissionEdit() => InitializeComponent();

        private PermissionModel _model;
        private readonly PermissionBLL _permissionBLL = new();
        private readonly bool _isEditMode; // 是否为编辑模式

        public frmPermissionEdit(PermissionModel model)
        {
            InitializeComponent();

            // 如果model为null，表示新增模式
            _isEditMode = model != null && model.ID > 0;
            _model = model ?? new PermissionModel();

            // 设置窗体标题
            this.Text = _isEditMode ? "编辑权限" : "新增权限";
        }

        /// <summary>
        /// 保存按钮点击事件
        /// </summary>
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // 基本验证
                if (!ValidateInput())
                {
                    return;
                }

                // 准备保存的数据
                _model.PermissionName = txtPermissionName.Text.Trim();
                _model.PermissionCode = txtPermissionCode.Text.Trim();
                _model.ControlName = txtControlName.Text.Trim();
                _model.PermissionNotes = txtPermissionNotes.Text.Trim();

                // 保存数据
                bool result;
                if (_isEditMode)
                {
                    result = _permissionBLL.UpdatePermission(_model);
                }
                else
                {
                    result = _permissionBLL.AddPermission(_model);
                }

                if (result)
                {
                    string message = _isEditMode ? "更新成功！" : "添加成功！";
                    MessageHelper.MessageOK(message, TType.Success);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageHelper.MessageOK("保存失败，请重试！", TType.Error);
                }
            }
            catch (Exception ex)
            {
                // 捕获BLL层抛出的异常（包括重复检查的异常）
                NlogHelper.Default.Error($"保存权限失败：{ex.Message}", ex);
                MessageHelper.MessageOK(ex.Message, TType.Error);
            }
        }

        /// <summary>
        /// 验证输入
        /// </summary>
        private bool ValidateInput()
        {
            // 验证权限名称
            if (string.IsNullOrWhiteSpace(txtPermissionName.Text))
            {
                MessageHelper.MessageOK("请输入权限名称！", TType.Warn);
                txtPermissionName.Focus();
                return false;
            }

            // 验证权限代码
            if (string.IsNullOrWhiteSpace(txtPermissionCode.Text))
            {
                MessageHelper.MessageOK("请输入权限代码！", TType.Warn);
                txtPermissionCode.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 加载数据到界面
        /// </summary>
        private void LoadData()
        {
            if (_isEditMode)
            {
                txtPermissionName.Text = _model.PermissionName;
                txtPermissionCode.Text = _model.PermissionCode;
                txtControlName.Text = _model.ControlName;
                txtPermissionNotes.Text = _model.PermissionNotes;
            }
        }

        private void frmMeteringEdit_Load(object sender, EventArgs e)
        {
            LoadData();
        }

    }
}