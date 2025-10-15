using AntdUI;

namespace MainUI
{
    public partial class frmMeteringRemind : UIForm
    {
        private MeteringRemindModel RemindModel = new();
        private MeteringRemindBLL RemindBLL = new();
        public frmMeteringRemind() => InitializeComponent();

        private void frmMeteringRemind_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            Tables.Columns = [
                new Column("ID","ID"){ Align = ColumnAlign.Center , Visible=false},
                new Column("InspectType","类型"){ Align = ColumnAlign.Center},
                new Column("InspectName","名称"){ Align = ColumnAlign.Center},
                new Column("CurrenDate","当前检查日期"){ Align = ColumnAlign.Center},
                new Column("NextDate","下次检查日期"){ Align = ColumnAlign.Center},
                new Column("InspectDescribe","检查说明"){ Align = ColumnAlign.Center},
                new Column("Cycle","周期/天"){ Align = ColumnAlign.Center},
                new Column("Buttns","操作",ColumnAlign.Center){}
           ];
            Tables.DataSource = RemindBLL.GetMeteringReminds();
        }

        private void Tables_CellButtonClick(object sender, TableButtonEventArgs e)
        {
            if (e.Record is MeteringRemindModel model)
            {
                if (MessageHelper.MessageYes(this, $"确认[{model.InspectName}]是否计量完成？", TType.Warn) == DialogResult.OK)
                {
                    LoadData(RemindModel, true);
                }
            }
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
                if (RemindBLL.DelMeteringReminds(RemindModel.ID))
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
            LoadData(RemindModel);
        }

        private void LoadData(MeteringRemindModel model, bool IsInspect = false)
        {
            using frmMeteringEdit edit = new(model, IsInspect);
            VarHelper.ShowDialogWithOverlay(this, edit);
            LoadData();
        }

        private void Tables_CellClick(object sender, TableClickEventArgs e)
        {
            if (e.Record is MeteringRemindModel model)
            {
                RemindModel = model;
            }
        }

        private void Tables_CellDoubleClick(object sender, TableClickEventArgs e)
        {
            LoadData(RemindModel);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
