using AntdUI;
using MainUI.Service;

namespace MainUI.Procedure
{
    public partial class ucModelManage : ucBaseManagerUI
    {
        Models model = new();
        ModelBLL modelBLL = new();
        ModelTypeBLL bModelType = new();
        public ucModelManage() => InitializeComponent();

        private void ucModelManage_Load(object sender, EventArgs e)
        {
            GetModelType();
            LoadData();
        }

        public void GetModelType()
        {
            cboModelType.DisplayMember = "ModelTypeName";
            cboModelType.ValueMember = "ID";
            cboModelType.DataSource = bModelType.GetModelsByTestBench(TestBenchService.CurrentTestBenchID);
            cboModelType.SelectedValueChanged += CboModelType_SelectedValueChanged;
        }

        private void CboModelType_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            Tables.Columns = [
                new Column("ID","ID"){ Align = ColumnAlign.Center , Visible = false },
                new Column("TypeID","类型ID"){ Align = ColumnAlign.Center , Visible = false },
                new Column("ModelName","型号名称"){ Align = ColumnAlign.Center },
                new Column("DrawingNo","产品图号"){ Align = ColumnAlign.Center },
                new Column("ReleaseTime","发布时间"){ Align = ColumnAlign.Center },
                //new Column("Mark","型号描述"){ Align = ColumnAlign.Center },
                new Column("Buttns","操作",ColumnAlign.Center){ Width = "100"}
           ];
            Tables.DataSource = ModelBLL
                .GetNewModels(cboModelType.SelectedValue.ToInt32(), true);
        }

        private void LoadData(Models model)
        {
            using frmModelEdit edit = new(model, cboModelType.SelectedValue.ToInt32());
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            LoadData(null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var DialogResult = MessageHelper.MessageYes("是否删除选中记录？", TType.Warn);
            if (DialogResult == DialogResult.OK)
            {
                if (modelBLL.Delete(model.ID))
                {
                    MessageHelper.MessageOK("删除成功！");
                    // 通知所有用户管理相关的控件刷新数据
                    DataChangedEventManager.NotifyDataChanged(DataChangeType.Model);
                    DataChangedEventManager.NotifyDataChanged(DataChangeType.ModelType);
                    DataChangedEventManager.NotifyDataChanged(DataChangeType.TestProcess);
                    DataChangedEventManager.NotifyDataChanged(DataChangeType.TestStep);
                }
                else
                {
                    MessageHelper.MessageOK("删除失败！");
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            LoadData(model);
        }

        private void Tables_CellClick(object sender, TableClickEventArgs e)
        {
            if (e.Record is Models model)
            {
                this.model = model;
            }
        }

        private void Tables_CellDoubleClick(object sender, TableClickEventArgs e)
        {
            LoadData(model);
        }

        private void Tables_CellButtonClick(object sender, TableButtonEventArgs e)
        {
            try
            {
                if (e.Record is NewModels data)
                {
                    if (e.Btn.Id == "Release")
                    {
                        if (DialogResult.OK == MessageHelper.MessageYes($"确认发布型号{data.ModelName}吗？"))
                        {
                            if (modelBLL.IsRelease(data))
                            {
                                LoadData();
                                MessageHelper.MessageOK($"发布成功！");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK("发布错误：" + ex.Message);
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
                // 保存当前选中的值
                int? currentSelectedValue = cboModelType.SelectedValue?.ToInt32();
                LoadData();
                GetModelType();
                if (currentSelectedValue.HasValue) // 恢复之前选中的值
                {
                    cboModelType.SelectedValue = currentSelectedValue.Value;
                }
                Debug.WriteLine("ucModelManage 型号管理 数据已刷新");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("ucModelManage 型号管理 重新加载数据失败", ex);
            }
        }

        /// <summary>
        /// 重写带类型的数据变更处理
        /// 只在用户数据变更时才刷新，提高性能
        /// </summary>
        protected override void OnDataChangedWithType(DataChangeType changeType)
        {
            // 只处理用户相关的数据变更
            if (changeType == DataChangeType.Model || changeType == DataChangeType.All)
            {
                base.OnDataChangedWithType(changeType);
            }
        }
        #endregion
    }
}
