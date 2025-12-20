using MainUI.Model;

namespace MainUI
{
    public partial class frmModelTypeEdit : UIForm
    {
        public frmModelTypeEdit() => InitializeComponent();

        private ModelsType modelsType = new();
        private readonly ModelTypeBLL modelBLL = new();
        public frmModelTypeEdit(ModelsType model)
        {
            InitializeComponent();
            modelsType = model;
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string ModelType = txtModelType.Text.Trim();
                string Mark = txtMark.Text.Trim();
                if (string.IsNullOrEmpty(ModelType))
                {
                    MessageHelper.MessageOK(this, "未输入类型名称！");
                    txtModelType.Focus();
                    return;
                }

                bool result = false;
                if (modelsType == null)
                {
                    var newModel = new ModelsType
                    {
                        ModelTypeName = ModelType,
                        Mark = Mark,
                    };
                    result = modelBLL.Add(newModel);
                }
                else
                {
                    modelsType.ModelTypeName = ModelType;
                    modelsType.Mark = Mark;
                    result = modelBLL.Updata(modelsType);
                }

                if (result)
                {
                    MessageHelper.MessageOK(this, "保存成功！");
                    DataChangedEventManager.NotifyDataChanged(DataChangeType.ModelType);
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
            if (modelsType != null)
            {
                txtModelType.Text = modelsType.ModelTypeName;
                txtMark.Text = modelsType.Mark;
            }
        }
    }
}