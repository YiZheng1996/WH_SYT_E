using AntdUI;
using MainUI.Service;

namespace MainUI
{
    public partial class FrmDataManager : UIForm
    {
        private TestRecordModelDto RecordModel = new();
        TestRecordNewBLL _testRecordBLL = new();
        private bool _isInitializing = false; // 标记是否正在初始化，避免触发多次事件

        public FrmDataManager() => InitializeComponent();

        private void frmDataManager_Load(object sender, EventArgs e)
        {
            Init();
            InittializeWrkstations();
            LoadData();
        }

        /// <summary>
        /// 加载所有工位
        /// </summary>
        private void InittializeWrkstations()
        {
            try
            {
                TestBenchBLL _testBenchBLL = new();

                // 获取当前试验台信息
                var currentTestBench = TestBenchService.CurrentTestBench;

                if (currentTestBench == null)
                {
                    NlogHelper.Default.Warn("当前试验台信息未初始化");
                    CboWorkstations.Enabled = false;
                    return;
                }

                List<TestBenchModel> workstations;

                // 根据DataScope判断显示哪些工位
                if (currentTestBench.DataScope == 1)
                {
                    // DataScope=1: 可以查看所有工位
                    workstations = _testBenchBLL.GetActiveTestBenches();

                    // 在列表开头添加"所有工位"选项
                    workstations.Insert(0, new TestBenchModel
                    {
                        ID = 0,
                        BenchName = "所有工位"
                    });

                    NlogHelper.Default.Info($"当前试验台[{currentTestBench.BenchName}]拥有全局数据权限,可查看所有工位");
                }
                else
                {
                    // DataScope!=1: 只能查看当前工位
                    workstations = [currentTestBench];
                    NlogHelper.Default.Info($"当前试验台[{currentTestBench.BenchName}]仅可查看本工位数据");
                }

                // 绑定数据源
                CboWorkstations.DisplayMember = "BenchName";
                CboWorkstations.ValueMember = "ID";
                CboWorkstations.DataSource = workstations;

                // 默认选择"所有工位"或当前工位
                if (currentTestBench.DataScope == 1)
                {
                    CboWorkstations.SelectedValue = 0; // 选择"所有工位"
                    CboWorkstations.Enabled = true;
                }
                else
                {
                    CboWorkstations.SelectedValue = currentTestBench.ID;
                    CboWorkstations.Enabled = false; // 禁用切换
                }

                // 订阅工位切换事件（在数据源绑定完成后）
                CboWorkstations.SelectedIndexChanged += CboWorkstations_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("初始化工位列表失败", ex);
                MessageHelper.MessageOK($"初始化工位列表失败：{ex.Message}", AntdUI.TType.Error);
            }
        }

        /// <summary>
        /// 初始化产品类型和型号下拉框
        /// </summary>
        private new void Init()
        {
            try
            {
                _isInitializing = true;

                ModelTypeBLL bModelType = new();
                dtpStartBig.Value = DateTime.Now.AddDays(-3);
                dtpStartEnd.Value = DateTime.Now;

                // 初始化产品类型下拉框
                InitializeModelTypeComboBox();

                // 订阅产品类型选择变化事件
                cboType.SelectedIndexChanged += CboType_SelectedIndexChanged;

                _isInitializing = false;
            }
            catch (Exception ex)
            {
                _isInitializing = false;
                MessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 初始化产品类型下拉框
        /// </summary>
        private void InitializeModelTypeComboBox()
        {
            ModelTypeBLL bModelType = new();

            // 根据DataScope决定是否过滤：DataScope==1时查看所有工位，否则只查看当前工位
            bool shouldFilter = !TestBenchService.CanViewAllBenchData();
            var modelTypes = bModelType.GetModels(filterByTestBench: shouldFilter);

            // 在开头添加"请选择"选项
            modelTypes.Insert(0, new ModelsType
            {
                ID = -1,
                ModelTypeName = "请选择"
            });

            cboType.DisplayMember = "ModelTypeName";
            cboType.ValueMember = "ID";
            cboType.DataSource = modelTypes;
            cboType.SelectedValue = -1; // 默认选中"请选择"
        }

        /// <summary>
        /// 初始化产品型号下拉框
        /// </summary>
        /// <param name="typeID">产品类型ID，-1表示显示"请选择"</param>
        private void InitializeModelComboBox(int typeID = -1)
        {
            List<NewModels> models;

            if (typeID == -1)
            {
                // 显示"请选择"
                models =
                [
                    new NewModels
            {
                ID = -1,
                ModelName = "请选择"
            }
                ];
            }
            else
            {
                // 根据DataScope决定是否过滤：DataScope==1时查看所有工位，否则只查看当前工位
                bool shouldFilter = !TestBenchService.CanViewAllBenchData();
                models = ModelBLL.GetNewModels(typeID, IsRelease: false, filterByTestBench: shouldFilter);

                // 在开头添加"请选择"选项
                models.Insert(0, new NewModels
                {
                    ID = -1,
                    ModelName = "请选择"
                });
            }

            cboModel.ValueMember = "ID";
            cboModel.DisplayMember = "ModelName";
            cboModel.DataSource = models;
            cboModel.SelectedValue = -1; // 默认选中"请选择"
        }

        /// <summary>
        /// 选择类别时事件
        /// </summary>
        private void CboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

            try
            {
                int typeID = cboType.SelectedValue.ToInt32();
                InitializeModelComboBox(typeID);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("产品类型选择变化处理失败", ex);
            }
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        private void LoadData()
        {
            try
            {
                string TestId = txtNumber.Text;
                int TypeID = cboType.SelectedValue.ToInt32();
                int Model = cboModel.SelectedValue.ToInt32();
                DateTime dateFrom = dtpStartBig.Value;
                DateTime dateTo = dtpStartEnd.Value;

                // 获取选中的工位ID
                int selectedWorkstationID = CboWorkstations.SelectedValue?.ToInt32() ?? 0;

                // 构建查询条件
                var searchModel = new TestRecordModel
                {
                    KindID = TypeID,
                    ModelID = Model,
                    TestID = TestId,
                    TestTime = dateFrom,
                    TestBenchID = selectedWorkstationID // 传入选中的工位ID，0表示查询所有
                };

                var data = _testRecordBLL.GetTestRecord(searchModel, dateTo.AddDays(1));

                Tables.Columns =
                [
                    new Column("ID","ID"){ Align = ColumnAlign.Center , Visible = false },
                    new Column("KindID","类型ID"){ Align = ColumnAlign.Center , Visible = false },
                    new Column("ModelTypeName","类型名称"){ Align = ColumnAlign.Center },
                    new Column("ModelID","型号ID"){ Align = ColumnAlign.Center , Visible = false },
                    new Column("ModelName","型号名称"){ Align = ColumnAlign.Center  },
                    new Column("TestID","产品图号"){ Align = ColumnAlign.Center },
                    new Column("Tester","操作员"){ Align = ColumnAlign.Center },
                    new Column("TestTime","保存时间"){ Align = ColumnAlign.Center },
                    new Column("TestBenchName","试验台"){ Align = ColumnAlign.Center }, // 新增列
                    new Column("ReportPath","保存路径"){ Align = ColumnAlign.Center , Visible = false },
                ];
                Tables.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"加载数据出现错误：{ex.Message}");
            }
        }

        /// <summary>
        /// 查看报表方法 - 支持跨工位访问
        /// </summary>
        private void View()
        {
            try
            {
                if (RecordModel.ReportPath == null)
                {
                    MessageBox.Show("请先选择一条记录。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string originalPath = RecordModel.ReportPath.ToString();

                // 获取当前工位信息
                var currentTestBench = TestBenchService.CurrentTestBench;
                if (currentTestBench == null)
                {
                    MessageBox.Show("当前工位信息未初始化", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 通过 TestBenchID 查询目标工位信息
                TestBenchModel targetTestBench = null;
                if (RecordModel.TestBenchID > 0)
                {
                    TestBenchBLL testBenchBLL = new();
                    targetTestBench = testBenchBLL.GetTestBench(RecordModel.TestBenchID);
                }

                // 如果无法获取目标工位，使用当前工位
                if (targetTestBench == null)
                {
                    targetTestBench = currentTestBench;
                    NlogHelper.Default.Warn($"无法获取TestBenchID={RecordModel.TestBenchID}的工位信息，使用当前工位");
                }

                // 转换为可访问的路径（本地或网络UNC路径）
                string accessiblePath = NetworkPathHelper.ConvertToAccessiblePath(
                    originalPath,
                    targetTestBench,
                    currentTestBench
                );

                // 记录路径转换信息
                if (accessiblePath != originalPath)
                {
                    NlogHelper.Default.Info($"跨工位访问报表:");
                    NlogHelper.Default.Info($"  源工位: {targetTestBench.BenchName} (ID:{targetTestBench.ID}, IP:{targetTestBench.BenchIP})");
                    NlogHelper.Default.Info($"  当前工位: {currentTestBench.BenchName} (ID:{currentTestBench.ID})");
                    NlogHelper.Default.Info($"  原始路径: {originalPath}");
                    NlogHelper.Default.Info($"  访问路径: {accessiblePath}");
                }

                // 尝试访问文件
                var (success, message) = NetworkPathHelper.TryAccessFile(accessiblePath);

                if (!success)
                {
                    MessageBox.Show(message, "无法访问报表", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 打开报表
                frmDispReport report = new(accessiblePath);
                VarHelper.ShowDialogWithOverlay(this, report);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("查看报表失败", ex);
                MessageBox.Show($"查看报表失败: {ex.Message}", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 查看报表
        /// </summary>
        private void btnView_Click(object sender, EventArgs e)
        {
            View();
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (RecordModel.ReportPath == null)
            {
                MessageBox.Show("请先选择一条记录。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageHelper.MessageYes(this, "删除后无法恢复，确定要删除该条记录吗？") == DialogResult.OK)
            {
                _testRecordBLL.DeleteTestRecord(RecordModel.ID);
                LoadData();
            }
        }

        private void Tables_CellClick(object sender, TableClickEventArgs e)
        {
            if (e.Record is TestRecordModelDto model)
            {
                RecordModel = model;
            }
        }

        private void Tables_CellDoubleClick(object sender, TableClickEventArgs e)
        {
            View();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 工位选择改变事件
        /// </summary>
        private void CboWorkstations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

            try
            {
                var currentTestBench = TestBenchService.CurrentTestBench;

                if (currentTestBench == null)
                    return;

                int selectedWorkstationID = CboWorkstations.SelectedValue?.ToInt32() ?? 0;

                // 如果DataScope!=1且选择的不是当前工位,阻止切换
                if (currentTestBench.DataScope != 1 && selectedWorkstationID != currentTestBench.ID)
                {
                    MessageHelper.MessageOK("您没有权限查看其他工位的数据!", AntdUI.TType.Warn);

                    // 恢复为当前工位
                    CboWorkstations.SelectedValue = currentTestBench.ID;
                    return;
                }

                // 切换工位后重置产品类型和型号选择
                _isInitializing = true;

                // 重新加载产品类型（重置为"请选择"）
                InitializeModelTypeComboBox();

                // 重置产品型号为"请选择"
                InitializeModelComboBox(-1);

                _isInitializing = false;

                // 切换工位后重新加载数据
                LoadData();

                NlogHelper.Default.Info($"切换到工位: {(selectedWorkstationID == 0 ? "所有工位" : CboWorkstations.Text)}");
            }
            catch (Exception ex)
            {
                _isInitializing = false;
                NlogHelper.Default.Error("切换工位失败", ex);
            }
        }
    }
}


