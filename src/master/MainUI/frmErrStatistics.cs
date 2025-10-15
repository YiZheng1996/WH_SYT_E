using AntdUI;

namespace MainUI
{
    public partial class frmErrStatistics : UIForm
    {
        public frmErrStatistics() => InitializeComponent();

        private ErrStatisticsModel errStatisticsModel = new();
        private readonly ErrStatisticsBLL errStatisticsBLL = new();
        private void frmMeteringRemind_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            Tables.Columns = [
               new Column("ID","ID"){ Align = ColumnAlign.Center , Visible = false },
               new Column("ErrType","问题类型"){ Align = ColumnAlign.Center},
               new Column("ErrContent","详细内容"){ Align = ColumnAlign.Center},
               new Column("ErrDate","当前检查日期"){ Align = ColumnAlign.Center},
           ];
            Tables.DataSource = errStatisticsBLL.GetErrStatistics();
        }

        private void LoadData(ErrStatisticsModel model)
        {
            using frmErrStatisticsEdit edit = new(model);
            VarHelper.ShowDialogWithOverlay(this, edit);
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            LoadData(null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var DialogResult = MessageHelper.MessageYes(this, "是否删除选中记录？", TType.Warn);
            if (DialogResult == DialogResult.OK)
            {
                if (errStatisticsBLL.DelErrStatistics(errStatisticsModel.ID))
                {
                    MessageHelper.MessageOK(this, "删除成功！");
                }
                else
                {
                    MessageHelper.MessageOK(this, "删除失败！");
                }
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            LoadData(errStatisticsModel);
        }

        private void Tables_CellClick(object sender, TableClickEventArgs e)
        {
            if (e.Record is ErrStatisticsModel model)
            {
                errStatisticsModel = model;
            }
        }

        private void Tables_CellDoubleClick(object sender, TableClickEventArgs e)
        {
            LoadData(errStatisticsModel);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
