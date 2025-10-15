using Sunny.UI;
using System.Data;

namespace MainUI
{
    public partial class frmMeteringEdit : UIForm
    {
        public frmMeteringEdit() => InitializeComponent();

        private readonly bool IsInspect;
        private readonly MeteringRemindBLL remindBLL = new();
        private readonly MeteringRemindModel remindModel;
        public frmMeteringEdit(MeteringRemindModel model, bool IsInspect = false)
        {
            InitializeComponent();
            remindModel = model;
            this.IsInspect = IsInspect;
            dateCurrent.Value = DateTime.Now;
        }


        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string inspectType = CboInspectType.Text.Trim();
                string inspectName = txtInspectName.Text.Trim();
                string describe = txtDescribe.Text.Trim();
                DateTime dateTime1 = dateCurrent.Value;
                DateTime dateTime2 = dateNext.Value;
                if (string.IsNullOrEmpty(inspectName))
                {
                    MessageHelper.MessageOK(this, "未输入名称！");
                    txtInspectName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(inspectType))
                {
                    MessageHelper.MessageOK(this, "未选择类型！");
                    CboInspectType.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(describe))
                {
                    MessageHelper.MessageOK(this, "未输入检查说明！");
                    txtDescribe.Focus();
                    return;
                }
                if (dateTime1 >= dateTime2)
                {
                    MessageHelper.MessageOK(this, "当前检查时间不能大于下次检查时间！");
                    dateCurrent.Focus();
                    return;
                }

                bool result = false;
                var difference = (dateTime2 - dateTime1).TotalDays; // 计算周期
                if (remindModel == null)
                {
                    var newModel = new MeteringRemindModel
                    {
                        Cycle = (int)difference,
                        NextTime = dateNext.Value,
                        InspectType = inspectType,
                        InspectName = inspectName,
                        CurrentTime = dateCurrent.Value,
                        InspectDescribe = describe,
                    };
                    result = remindBLL.AddMeteringReminds(newModel);
                }
                else
                {
                    remindModel.Cycle = (int)difference;
                    remindModel.NextTime = dateNext.Value;
                    remindModel.InspectType = inspectType;
                    remindModel.InspectName = inspectName;
                    remindModel.CurrentTime = dateCurrent.Value;
                    remindModel.InspectDescribe = describe;
                    result = remindBLL.UpdateMeteringReminds(remindModel);
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
            if (IsInspect)
            {
                CboInspectType.Enabled = !IsInspect;
                txtInspectName.Enabled = !IsInspect;
                dateCurrent.Enabled = !IsInspect;
                txtDescribe.Enabled = !IsInspect;
                dateCurrent.Value = DateTime.Now;
                labPrompt.Text = $"请选择下次检查日期后，点击[保存]按钮!";
            }
            if (remindModel != null)
            {
                CboInspectType.Text = remindModel.InspectType;
                txtInspectName.Text = remindModel.InspectName;
                dateCurrent.Value = remindModel.CurrentTime;
                dateNext.Value = remindModel.NextTime;
                txtDescribe.Text = remindModel.InspectDescribe;
            }
        }
    }
}