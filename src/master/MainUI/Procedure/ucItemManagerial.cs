using AntdUI;
using MainUI.Service;

namespace MainUI.Procedure
{
    public partial class ucItemManagerial : ucBaseManagerUI
    {
        int RowCount = 0;
        private ModelTypeBLL bModelType = new();
        private readonly TestProcessBLL ProcessBll = new();
        private TestProcessModel _processModel = new();

        public ucItemManagerial() => InitializeComponent();

        // 初始化表单数据
        private void LoadData()
        {
            TableTestProcess.Columns = [
                new Column("ID","ID"){ Align = ColumnAlign.Center , Visible = false },
                new Column("ProcessName","项点名称"){ Align = ColumnAlign.Center},
                new Column("EntityClassName","关联逻辑类名称"){ Align = ColumnAlign.Center},
                new ColumnSwitch("Enable","启用",ColumnAlign.Center).SetAutoCheck(false),
            ];
            var data = TestProcessBLL.GetTestProcess(cboModelType.SelectedValue.ToInt32());
            RowCount = data.Count;
            data.ForEach(x => x.Enable = x.IsVisible);
            TableTestProcess.DataSource = data;
            TableTestProcessIndex(TableTestProcess.SelectedIndex);
        }

        // 选中行
        void TableTestProcessIndex(int index)
        {
            if (index > RowCount) index -= 1;
            if (index < 0) index = RowCount;
            TableTestProcess.ScrollLine(index);
            TableTestProcess.SelectedIndex = index;
        }

        private void LoadData(TestProcessModel model, int type = 0)
        {
            using FrmTestProcess edit = new(model, type);
            edit.ShowDialog();
            if (edit.DialogResult == DialogResult.OK)
            {
                DataChangedEventManager.NotifyDataChanged(
                    DataChangeType.TestProcess,
                    DataChangeType.TestStep
                    );
            }
        }

        // 添加
        private void btnAdd_Click(object sender, EventArgs e)
        {
            LoadData(null, cboModelType.SelectedValue.ToInt32());
        }

        // 删除
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var DialogResult = MessageHelper.MessageYes("是否删除选中记录？", TType.Warn);
            if (DialogResult == DialogResult.OK)
            {
                if (ProcessBll.DelTestProcess(_processModel.ID))
                {
                    MessageHelper.MessageOK("删除成功！");
                    DataChangedEventManager.NotifyDataChanged(
                    DataChangeType.TestProcess,
                    DataChangeType.TestStep
                    );
                }
                else
                {
                    MessageHelper.MessageOK("删除失败！");
                }
            }
        }

        // 修改
        private void btnEdit_Click(object sender, EventArgs e)
        {
            LoadData(_processModel);
        }

        private void ucItemManagerial_Load(object sender, EventArgs e)
        {
            GetModelType();
            LoadData();
        }

        // 获取型号类别
        public void GetModelType()
        {
            cboModelType.DisplayMember = "ModelTypeName";
            cboModelType.ValueMember = "ID";
            cboModelType.DataSource = bModelType.GetModelsByTestBench(TestBenchService.CurrentTestBenchID);
            cboModelType.SelectedValueChanged += CboModelType_SelectedValueChanged;
        }

        // 型号类别选择变化,加载对应试验项点数据
        private void CboModelType_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void TableTestProcess_CellClick(object sender, TableClickEventArgs e)
        {
            if (e.Record is TestProcessModel model)
            {
                _processModel = model;
            }
        }

        private void TableTestProcess_CellDoubleClick(object sender, TableClickEventArgs e)
        {
            LoadData(_processModel);
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
                Debug.WriteLine("ucItemManagerial 项点管理 数据已刷新");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("ucItemManagerial 项点管理 重新加载数据失败", ex);
            }
        }

        /// <summary>
        /// 重写带类型的数据变更处理
        /// 只在用户数据变更时才刷新，提高性能
        /// </summary>
        protected override void OnDataChangedWithType(DataChangeType changeType)
        {
            // 只处理用户相关的数据变更
            if (changeType == DataChangeType.TestProcess || changeType == DataChangeType.All)
            {
                base.OnDataChangedWithType(changeType);
            }
        }
        #endregion
    }
}