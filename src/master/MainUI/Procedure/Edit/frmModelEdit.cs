using MainUI.Service;

namespace MainUI
{
    public partial class frmModelEdit : UIForm
    {
        public frmModelEdit() => InitializeComponent();

        Models model = new();
        ModelBLL modelBLL = new();
        public frmModelEdit(Models model, int typeID)
        {
            InitializeComponent();
            this.model = model;
            GetModelType(typeID);
        }

        public void GetModelType(int typeID)
        {
            ModelTypeBLL bModelType = new();
            cboModelType.DisplayMember = "ModelTypeName";
            cboModelType.ValueMember = "ID";
            cboModelType.DataSource = bModelType.GetModelsByTestBench(TestBenchService.CurrentTestBenchID);
            cboModelType.SelectedValue = typeID;
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string ModelName = txtModelType.Text.Trim();
                string Mark = txtMark.Text.Trim();
                string DrawingNo = txtDrawingNo.Text.Trim();
                int TypeID = cboModelType.SelectedValue.ToInt32();
                if (string.IsNullOrEmpty(ModelName))
                {
                    MessageHelper.MessageOK(this, "未输入型号名称！");
                    txtModelType.Focus();
                    return;
                }

                bool result = false;
                if (model == null)
                {
                    var newModel = new Models
                    {
                        TypeID = TypeID,
                        ModelName = ModelName,
                        Mark = Mark,
                        DrawingNo = DrawingNo,
                    };
                    result = ModelBLL.Add(newModel);
                }
                else
                {
                    model.TypeID = TypeID;
                    model.ModelName = ModelName;
                    model.Mark = Mark;
                    model.DrawingNo = DrawingNo;
                    result = modelBLL.Update(model);
                }

                if (result)
                {
                    MessageHelper.MessageOK(this, "保存成功！");
                    DataChangedEventManager.NotifyDataChanged(DataChangeType.Model);
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
            if (model != null)
            {
                cboModelType.SelectedValue = model.TypeID;
                txtModelType.Text = model.ModelName;
                txtMark.Text = model.Mark;
                txtDrawingNo.Text = model.DrawingNo;
            }
        }
    }
}