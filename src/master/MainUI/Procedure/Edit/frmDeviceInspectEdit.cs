namespace MainUI
{
    public partial class frmDeviceInspectEdit : UIForm
    {
        public frmDeviceInspectEdit() => InitializeComponent();

        private readonly DeviceInspectBLL DeviceBLL = new();
        private readonly DeviceInspectModel DeviceModel;
        public frmDeviceInspectEdit(DeviceInspectModel model)
        {
            InitializeComponent();
            DeviceModel = model;
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string PartType = CboPartType.Text.Trim();
                string PartName = txtPartName.Text.Trim();
                if (string.IsNullOrEmpty(PartType))
                {
                    MessageHelper.MessageOK(this, "未选择类型！");
                    CboPartType.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(PartName))
                {
                    MessageHelper.MessageOK(this, "未输入详细内容！");
                    txtPartName.Focus();
                    return;
                }

                bool result = false;
                if (DeviceModel == null)
                {
                    var newModel = new DeviceInspectModel
                    {
                        PartType = CboPartType.Text,
                        PartName = txtPartName.Text,
                    };
                    result = DeviceBLL.AddDeviceInspect(newModel);
                }
                else
                {
                    DeviceModel.PartType = PartType;
                    DeviceModel.PartName = PartName;
                    result = DeviceBLL.UpdateDeviceInspect(DeviceModel);
                }

                if (result)
                {
                    MessageHelper.MessageOK(this, "保存成功！");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageHelper.MessageOK(this, "保存失败！");
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("保存失败：", ex);
                MessageHelper.MessageOK(this, "保存失败：" + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmMeteringEdit_Load(object sender, EventArgs e)
        {
            if (DeviceModel != null)
            {
                CboPartType.Text = DeviceModel.PartType;
                txtPartName.Text = DeviceModel.PartName;
            }
        }
    }
}