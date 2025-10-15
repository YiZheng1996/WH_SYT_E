namespace MainUI
{
    public partial class frmErrStatisticsEdit : UIForm
    {
        public frmErrStatisticsEdit() => InitializeComponent();

        private readonly ErrStatisticsBLL statisticsBLL = new();
        private readonly ErrStatisticsModel statisticsModel;
        public frmErrStatisticsEdit(ErrStatisticsModel model)
        {
            InitializeComponent();
            statisticsModel = model;
            dateRecord.Value = DateTime.Now;
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string ErrType = CboErrType.Text.Trim();
                string describe = txtDescribe.Text.Trim();
                if (string.IsNullOrEmpty(ErrType))
                {
                    MessageHelper.MessageOK(this, "未选择类型！");
                    CboErrType.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(describe))
                {
                    MessageHelper.MessageOK(this, "未输入详细内容！");
                    txtDescribe.Focus();
                    return;
                }

                bool result = false;
                if (statisticsModel == null)
                {
                    var newModel = new ErrStatisticsModel
                    {
                        ErrType = CboErrType.Text,
                        ErrContent = txtDescribe.Text,
                        ErrTime = dateRecord.Value,
                    };
                    result = statisticsBLL.AddErrStatistics(newModel);
                }
                else
                {
                    statisticsModel.ErrType = ErrType;
                    statisticsModel.ErrTime = dateRecord.Value;
                    statisticsModel.ErrContent = describe;
                    result = statisticsBLL.UpdateErrStatistics(statisticsModel);
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
            if (statisticsModel != null)
            {
                CboErrType.Text = statisticsModel.ErrType;
                dateRecord.Value = statisticsModel.ErrTime;
                txtDescribe.Text = statisticsModel.ErrContent;
            }
        }
    }
}