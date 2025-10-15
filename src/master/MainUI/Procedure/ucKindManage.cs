using AntdUI;
namespace MainUI.Procedure
{
    public partial class ucKindManage : ucBaseManagerUI
    {
        private ModelsType modelsType = new();
        private readonly ModelTypeBLL modelBLL = new();
        public ucKindManage() => InitializeComponent();

        private void ucModelManage_Load(object sender, EventArgs e) => LoadData();

        private void LoadData()
        {
            Tables.Columns = [
                new Column("ID","ID"){ Align = ColumnAlign.Center , Visible = false },
                new Column("ModelTypeName","类型名称"){ Align = ColumnAlign.Center },
                new Column("Mark","类型备注"){ Align = ColumnAlign.Center },
           ];
            Tables.DataSource = modelBLL.GetAllModelType();
        }

        private void LoadData(ModelsType model)
        {
            using frmModelTypeEdit edit = new(model);
            edit.ShowDialog();
            if (edit.DialogResult == DialogResult.OK)
            {
                // 通知所有用户管理相关的控件刷新数据
                DataChangedEventManager.NotifyDataChanged(DataChangeType.Model);
                DataChangedEventManager.NotifyDataChanged(DataChangeType.ModelType);
                DataChangedEventManager.NotifyDataChanged(DataChangeType.TestProcess);
                DataChangedEventManager.NotifyDataChanged(DataChangeType.TestStep);
            }
        }

        private void Tables_CellClick(object sender, TableClickEventArgs e)
        {
            if (e.Record is ModelsType model)
            {
                modelsType = model;
            }
        }

        private void Tables_CellDoubleClick(object sender, TableClickEventArgs e)
        {
            LoadData(modelsType);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            LoadData(null);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            LoadData(modelsType);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var DialogResult = MessageHelper.MessageYes("是否删除选中记录？", TType.Warn);
            if (DialogResult == DialogResult.OK)
            {
                if (modelBLL.Delete(modelsType.ID))
                {
                    MessageHelper.MessageOK("删除成功！");
                    // 删除后通知刷新
                    DataChangedEventManager.NotifyDataChanged(DataChangeType.ModelType);
                    DataChangedEventManager.NotifyDataChanged(DataChangeType.Model);
                    DataChangedEventManager.NotifyDataChanged(DataChangeType.TestProcess);
                    DataChangedEventManager.NotifyDataChanged(DataChangeType.TestStep);
                }
                else
                {
                    MessageHelper.MessageOK("删除失败！");
                }
            }
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
                Debug.WriteLine("ucKindManage 产品类型列表 数据已刷新");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("ucKindManage 产品类型列表 重新加载数据失败", ex);
            }
        }

        /// <summary>
        /// 重写带类型的数据变更处理
        /// 只在用户数据变更时才刷新，提高性能
        /// </summary>
        protected override void OnDataChangedWithType(DataChangeType changeType)
        {
            // 只处理用户相关的数据变更
            if (changeType == DataChangeType.ModelType || changeType == DataChangeType.All)
            {
                base.OnDataChangedWithType(changeType);
            }
        }
        #endregion
    }
}
