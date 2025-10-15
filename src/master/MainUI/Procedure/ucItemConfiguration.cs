using MainUI.LogicalConfiguration.Services;
using MainUI.Service;
using Microsoft.Extensions.DependencyInjection;

namespace MainUI.Procedure
{
    public partial class ucItemConfiguration : ucBaseManagerUI
    {
        ModelTypeBLL _modelBLL = new();
        TestStepBLL StepBLL = new();
        TestProcessBLL TestProcessBLL = new();
        public ucItemConfiguration()
        {
            InitializeComponent();
            LoadCboModelType();
            cboModel.SelectedIndexChanged += CboModel_SelectedIndexChanged;
        }


        private void CboModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProcess();
            LoadConfiguaredProcess();
        }

        void LoadCboModelType()
        {
            cboType.DisplayMember = "ModelTypeName";
            cboType.ValueMember = "ID";
            cboType.DataSource = _modelBLL.GetModelsByTestBench(TestBenchService.CurrentTestBenchID);
            LoadProcess();
            LoadConfiguaredProcess();
        }

        void LoadCboModel()
        {
            ModelTypeBLL bModelType = new();
            cboModel.ValueMember = "ID";
            cboModel.DisplayMember = "ModelName";
            cboModel.DataSource = ModelBLL.GetNewModels(cboType.SelectedValue.ToInt32());
        }

        List<TestProcessModel> lstTestProcess = [];
        void LoadProcess()
        {
            int typeID = cboType.SelectedValue.ToInt32();
            lstAllPoint.Items.Clear();
            lstTestProcess = TestProcessBLL.GetTestProcess(typeID, true);
            foreach (var item in lstTestProcess)
            {
                lstAllPoint.Items.Add(item.ProcessName);
            }
        }

        private void LoadConfiguaredProcess()
        {
            try
            {
                lstTestPoint.Items.Clear();

                if (cboModel?.SelectedValue == null)
                    return;

                // 获取已配置的测试步骤，按Step字段排序
                List<TestStepModel> lstTestStep = StepBLL.GetTestSteps(new TestStepModel { ModelID = (int)cboModel.SelectedValue })
                    .OrderBy(x => x.Step) // 按Step字段排序
                    .ToList();

                // 按正确顺序添加到右侧列表
                foreach (TestStepModel step in lstTestStep)
                {
                    lstTestPoint.Items.Add(step.ProcessName);
                }

                // 重新加载左侧列表，排除已配置的项目
                lstAllPoint.Items.Clear();
                var configuredNames = lstTestStep.Select(s => s.ProcessName).ToHashSet();
                foreach (var item in lstTestProcess)
                {
                    if (!configuredNames.Contains(item.ProcessName))
                    {
                        lstAllPoint.Items.Add(item.ProcessName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"加载项点名称错误：{ex.Message}");
            }
        }


        private void MoveTo(UIListBox from, UIListBox to)
        {
            for (int i = 0; i < from.SelectedItems.Count; i++)
            {
                to.Items.Add(from.SelectedItems[i]);
            }
            to.ClearSelected();
            to.SelectedIndex = to.Items.Count - 1;
            int beforeIndex = -1;
            while (from.SelectedItems.Count > 0)
            {
                beforeIndex = from.SelectedIndex;
                from.Items.Remove(from.SelectedItems[0]);
            }

            if (from.Items.Count == beforeIndex)
                from.SelectedIndex = beforeIndex - 1;
            else
                from.SelectedIndex = beforeIndex;
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            MoveTo(lstAllPoint, lstTestPoint);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            MoveTo(lstTestPoint, lstAllPoint);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<TestStepModel> lstTestStep = [];
                for (int i = 0; i < lstTestPoint.Count; i++)
                {
                    TestStepModel testStep = new();
                    {
                        testStep.Step = i;
                        testStep.ModelID = (int)cboModel.SelectedValue;
                        testStep.ProcessName = $"{lstTestPoint.Items[i]}";
                        testStep.TestProcessID = lstTestProcess.Find(x => x.ProcessName == testStep.ProcessName).ID;
                    }
                    lstTestStep.Add(testStep);
                }
                if (cboModel.SelectedValue == null)
                {
                    MessageHelper.MessageOK("型号未选择，保存失败！");
                    return;
                }
                StepBLL.InsertTestStep(lstTestStep, (int)cboModel?.SelectedValue);
                LoadConfiguaredProcess();
                MessageHelper.MessageOK("保存成功！");
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"保存错误：{ex.Message}");
            }
        }

        private void lstTestPoint_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EnditTest(sender as UIListBox);
        }

        /// <summary>
        /// 修改对应项点自动试验逻辑
        /// </summary>
        /// <param name="lstbox"></param>
        private void EnditTest(UIListBox lstbox)
        {
            try
            {
                if (cboModel.Items.Count > 0 & lstbox.Items.Count > 0)
                {
                    TestProcessBLL bll = new();
                    string ModelType = cboType.SelectedText;
                    string ModelName = cboModel.SelectedText;
                    string LstName = lstbox.SelectedItem.ToString();
                    string LstIndex = lstbox.SelectedIndex.ToString();
                    string TestPath = $"{Application.StartupPath}Procedure\\{ModelType}\\{ModelName}\\{LstName}.json";
                    Debug.WriteLine($"选择型号：{ModelName},选择下标：{LstIndex},选择项点：{LstName}，路径：{TestPath}");

                    var formFactory = Program.ServiceProvider.GetRequiredService<IFormService>();
                    var form = formFactory.CreateLogicalConfigurationForm(TestPath, ModelType, ModelName, LstName);
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                MessageHelper.MessageOK($"获取自动试验逻辑失败：{err}");
            }
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCboModel();
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
                LoadCboModelType();
                LoadCboModel();
                Debug.WriteLine("ucItemConfiguration 项点配置 数据已刷新");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("ucItemConfiguration 项点配置 重新加载数据失败", ex);
            }
        }

        /// <summary>
        /// 重写带类型的数据变更处理
        /// 只在用户数据变更时才刷新，提高性能
        /// </summary>
        protected override void OnDataChangedWithType(DataChangeType changeType)
        {
            // 只处理用户相关的数据变更
            if (changeType == DataChangeType.TestStep || changeType == DataChangeType.All)
            {
                base.OnDataChangedWithType(changeType);
            }
        }
        #endregion
    }
}
