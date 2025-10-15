using AntdUI;

namespace MainUI
{
    public partial class frmDeviceInspect : UIForm
    {
        private DeviceInspectBLL deviceBLL = new();
        private DeviceInspectModel deviceModel = new();
        public frmDeviceInspect() => InitializeComponent();

        private void frmMeteringRemind_Load(object sender, EventArgs e) => LoadData();

        private void LoadData()
        {
            Tables.Columns = [
                new Column("ID","ID"){ Align = ColumnAlign.Center , Visible=false},
                new Column("PartType","部件类型"){ Align = ColumnAlign.Center},
                new Column("PartName","部件名称"){ Align = ColumnAlign.Center},
                new Column("ActionNumber","动作次数"){ Align = ColumnAlign.Center},
                new Column("UseDuration","使用时长(天)"){ Align = ColumnAlign.Center},
                new Column("Buttns","操作",ColumnAlign.Center){/* Width = "130"*/}
           ];
            Tables.DataSource = deviceBLL.GetDeviceInspects();
        }

        private void LoadData(DeviceInspectModel model)
        {
            using frmDeviceInspectEdit edit = new(model);
            VarHelper.ShowDialogWithOverlay(this, edit);
            LoadData();
        }


        private void Tables_CellButtonClick(object sender, TableButtonEventArgs e)
        {
            if (e.Record is DeviceInspectModel model)
            {
                string message = string.Empty;
                Action<DeviceInspectModel> clearAction = null;

                switch (e.Btn.Id)
                {
                    case "NumberClearing":
                        message = $"是否将部件[{model.PartName}]次数清零？清零后将无法恢复！！！";
                        clearAction = m => m.ActionNumber = 0;
                        break;
                    case "TimeClearing":
                        message = $"是否将部件[{model.PartName}]时间清零？清零后将无法恢复！！！";
                        clearAction = m => m.UseDuration = 0;
                        break;
                }

                if (clearAction != null && MessageHelper.MessageYes(this, message, TType.Error) == DialogResult.OK)
                {
                    clearAction(model);
                    deviceBLL.UpdateDATA(model);
                    LoadData();
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
                if (deviceBLL.DelDeviceInspects(deviceModel.ID))
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
            LoadData(deviceModel);
        }

        private void Tables_CellClick(object sender, TableClickEventArgs e)
        {
            if (e.Record is DeviceInspectModel model)
            {
                deviceModel = model;
            }
        }

        private void Tables_CellDoubleClick(object sender, TableClickEventArgs e)
        {
            LoadData(deviceModel);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
