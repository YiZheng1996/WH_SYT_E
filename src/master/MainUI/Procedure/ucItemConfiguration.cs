using AntdUI;
using MainUI.LogicalConfiguration;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Services;
using MainUI.Service;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Windows.Forms;
using Button = System.Windows.Forms.Button;
using Label = System.Windows.Forms.Label;

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

        #region 试验项点复制功能模块

        /// <summary>
        /// 复制按钮点击事件 - 打开跨型号复制对话框
        /// </summary>
        private async void btnCopyItem_Click(object sender, EventArgs e)
        {
            try
            {
                // 获取父窗体
                var parentForm = this.FindForm();
                if (parentForm == null)
                {
                    MessageHelper.MessageOK("无法找到父窗体!", TType.Error);
                    return;
                }

                // 打开复制选择对话框
                using var copyDialog = new ItemCopyDialog();

                // 设置当前选中的源信息作为默认值
                if (cboType.SelectedValue != null && 
                    cboModel.SelectedValue != null && 
                    lstTestPoint.SelectedItem != null)
                {
                    copyDialog.SetDefaultSource(
                        (int)cboType.SelectedValue,
                        (int)cboModel.SelectedValue,
                        lstTestPoint.SelectedItem.ToString()
                    );
                }

                if (VarHelper.ShowDialogWithOverlayEx(parentForm, copyDialog) != DialogResult.OK) return;
                var copyInfo = copyDialog.GetCopyInfo();

                // 确认操作
                var confirmMessage = $"确定要将逻辑复制吗?\n\n" +
                                     $"源: {copyInfo.SourceModelType} / {copyInfo.SourceModelName} / {copyInfo.SourceItemName}\n" +
                                     $"目标: {copyInfo.TargetModelType} / {copyInfo.TargetModelName} / {copyInfo.TargetItemName}\n\n" +
                                     $"注意: 目标项点的现有逻辑配置将被覆盖!";

                if (MessageHelper.MessageYes(parentForm, confirmMessage, TType.Warn) != DialogResult.OK)
                    return;

                // 执行复制
                await CopyTestItemLogic(copyInfo);

                MessageHelper.MessageOK("测试项逻辑复制成功!", TType.Success);

                // 如果目标是当前型号,刷新列表
                if (copyInfo.TargetModelId == (int?)cboModel.SelectedValue)
                {
                    LoadConfiguaredProcess();
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("复制测试项失败", ex);
                MessageHelper.MessageOK($"复制失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 执行跨型号的测试项逻辑复制
        /// </summary>
        private async Task CopyTestItemLogic(ItemCopyInfo copyInfo)
        {
            // 构建源文件路径
            string sourceJsonPath = Path.Combine(
                Application.StartupPath,
                "Procedure",
                copyInfo.SourceModelType,
                copyInfo.SourceModelName,
                $"{copyInfo.SourceItemName}.json"
            );

            // 构建目标文件路径
            string targetJsonPath = Path.Combine(
                Application.StartupPath,
                "Procedure",
                copyInfo.TargetModelType,
                copyInfo.TargetModelName,
                $"{copyInfo.TargetItemName}.json"
            );

            // 验证源文件存在
            if (!File.Exists(sourceJsonPath))
            {
                throw new FileNotFoundException($"源测试项配置文件不存在: {copyInfo.SourceItemName}");
            }

            // 确保目标目录存在
            string targetDir = Path.GetDirectoryName(targetJsonPath);
            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            // 读取源配置
            string jsonContent = await File.ReadAllTextAsync(sourceJsonPath);
            var sourceConfig = JsonConvert.DeserializeObject<JsonManager.JsonConfig>(jsonContent);

            if (sourceConfig == null)
            {
                throw new InvalidOperationException("无法解析源配置文件");
            }

            // 读取目标配置(如果存在)或创建新配置
            JsonManager.JsonConfig targetConfig;
            if (File.Exists(targetJsonPath))
            {
                string targetJsonContent = await File.ReadAllTextAsync(targetJsonPath);
                targetConfig = JsonConvert.DeserializeObject<JsonManager.JsonConfig>(targetJsonContent);
            }
            else
            {
                // 目标文件不存在,创建新配置
                targetConfig = new JsonManager.JsonConfig
                {
                    System = new JsonManager.JsonConfig.SystemInfo
                    {
                        CreateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                        ProjectName = "软件通用平台"
                    },
                    Form = new List<Parent>
                    {
                        new Parent
                        {
                            ModelTypeName = copyInfo.TargetModelType,
                            ModelName = copyInfo.TargetModelName,
                            ItemName = copyInfo.TargetItemName,
                            ChildSteps = new List<ChildModel>()
                        }
                    },
                    Variable = new List<VarItem>()
                };
            }

            // 更新目标配置的Form信息(保持目标的型号信息)
            if (targetConfig.Form == null || targetConfig.Form.Count == 0)
            {
                targetConfig.Form = new List<Parent>
                {
                    new Parent
                    {
                        ModelTypeName = copyInfo.TargetModelType,
                        ModelName = copyInfo.TargetModelName,
                        ItemName = copyInfo.TargetItemName,
                        ChildSteps = new List<ChildModel>()
                    }
                };
            }
            else
            {
                targetConfig.Form[0].ModelTypeName = copyInfo.TargetModelType;
                targetConfig.Form[0].ModelName = copyInfo.TargetModelName;
                targetConfig.Form[0].ItemName = copyInfo.TargetItemName;
            }

            // 复制步骤逻辑(深拷贝)
            if (sourceConfig.Form != null && sourceConfig.Form.Count > 0 && sourceConfig.Form[0].ChildSteps != null)
            {
                var copiedSteps = sourceConfig.Form[0]
                    .ChildSteps.Select(step => new ChildModel
                    {
                        StepNum = step.StepNum,
                        StepName = step.StepName,
                        Status = 0, // 重置状态
                        StepParameter = DeepCloneParameter(step.StepParameter),
                        Remark = step.Remark,
                        ErrorMessage = null, // 清空错误信息
                        NestingLevel = step.NestingLevel,
                        ParentStepId = step.ParentStepId,
                        StepType = step.StepType
                    })
                    .ToList();
                targetConfig.Form[0].ChildSteps = copiedSteps;
            }

            // 复制变量(深拷贝)
            if (sourceConfig.Variable != null)
            {
                targetConfig.Variable = sourceConfig.Variable.Select(variable => new VarItem
                {
                    VarName = variable.VarName,
                    VarType = variable.VarType,
                    VarValue = variable.VarValue,
                    VarText = variable.VarText
                }).ToList();
            }

            // 更新系统信息
            if (targetConfig.System != null)
            {
                targetConfig.System.CreateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            }

            // 保存目标配置
            var SavetargetJsonContent = JsonConvert.SerializeObject(targetConfig, Formatting.Indented);
            await File.WriteAllTextAsync(targetJsonPath, SavetargetJsonContent);

            NlogHelper.Default.Info($"成功复制测试项逻辑: {copyInfo.SourceModelType}/{copyInfo.SourceModelName}/{copyInfo.SourceItemName} -> {copyInfo.TargetModelType}/{copyInfo.TargetModelName}/{copyInfo.TargetItemName}");
        }

        /// <summary>
        /// 深拷贝参数对象
        /// </summary>
        private object DeepCloneParameter(object parameter)
        {
            if (parameter == null) return null;

            try
            {
                string json = JsonConvert.SerializeObject(parameter);
                return JsonConvert.DeserializeObject(json, parameter.GetType());
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("参数深拷贝失败", ex);
                return null;
            }
        }

        #endregion

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