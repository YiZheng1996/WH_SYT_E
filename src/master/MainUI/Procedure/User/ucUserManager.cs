using AntdUI;

namespace MainUI.Procedure.User
{
    public partial class ucUserManager : ucBaseManagerUI
    {
        private OperateUserBLL bLL = new();
        private OperateUserNewModel OperateUserModel = new();
        public ucUserManager() => InitializeComponent();

        private void ucUserManager_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        //数据绑定
        private void LoadData()
        {
            Tables.Columns =
           [
               new Column("ID","ID"){ Align = ColumnAlign.Center , Visible = false },
               new Column("Role_ID","权限ID"){ Align = ColumnAlign.Center },
               new Column("Username","用户名"){ Align = ColumnAlign.Center },
               new Column("RoleName","权限名称"){ Align = ColumnAlign.Center },
           ];

            // 获取所有用户并过滤掉admin（ID=1）
            var allUsers = bLL.GetUsers();
            var filteredUsers = allUsers.Where(u => u.ID != 1).ToList();
            Tables.DataSource = filteredUsers;
        }


        private void LoadData(OperateUserNewModel model)
        {
            using frmUserEdit edit = new(model);
            edit.ShowDialog();

            if (edit.DialogResult == DialogResult.OK)
            {
                // 通知所有用户管理相关的控件刷新数据
                DataChangedEventManager.NotifyDataChanged(DataChangeType.User);
            }
        }

        //添加用户按钮
        private void btnAdd_Click(object sender, EventArgs e)
        {
            LoadData(null);
        }

        //点击编辑用户按钮
        private void btnEdit_Click(object sender, EventArgs e)
        {
            LoadData(OperateUserModel);
        }


        //删除用户按钮
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (OperateUserModel.ID == 1)
            {
                MessageHelper.MessageOK("超级管理员不能删除！", TType.Error);
                return;
            }

            var DialogResult = MessageHelper.MessageYes("是否删除选中记录？", TType.Warn);
            if (DialogResult == DialogResult.OK)
            {
                if (bLL.RemoveByUserJob(OperateUserModel) > 0)
                {
                    MessageHelper.MessageOK("删除成功！");

                    // 删除后通知刷新
                    DataChangedEventManager.NotifyDataChanged(DataChangeType.User);
                }
                else
                {
                    MessageHelper.MessageOK("删除失败！");
                }
            }
        }

        private void Tables_CellClick(object sender, TableClickEventArgs e)
        {
            if (e.Record is OperateUserNewModel model)
            {
                OperateUserModel = model;
            }
        }

        private void Tables_CellDoubleClick(object sender, TableClickEventArgs e)
        {
            LoadData(OperateUserModel);
        }

        #region 数据自动刷新
        /// <summary>
        /// 重写Reload方法，实现数据刷新逻辑
        /// 当数据变更事件触发时，会自动调用此方法
        /// </summary>
        public override void Reload()
        {
            try
            {
                LoadData();
                Debug.WriteLine("ucUserManager 数据已刷新");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("ucUserManager 重新加载数据失败", ex);
            }
        }

        /// <summary>
        /// 重写带类型的数据变更处理
        /// 只在用户数据变更时才刷新，提高性能
        /// </summary>
        protected override void OnDataChangedWithType(DataChangeType changeType)
        {
            // 只处理用户相关的数据变更
            if (changeType == DataChangeType.User || changeType == DataChangeType.All)
            {
                base.OnDataChangedWithType(changeType);
            }
        }
        #endregion
    }
}
