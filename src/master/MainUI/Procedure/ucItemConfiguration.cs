using AntdUI;
using MainUI.LogicalConfiguration;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Services;
using MainUI.Procedure.Controls;
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
        private NewModels _currentModel;  // 当前选中的产品

        TestStepBLL StepBLL = new();
        TestProcessBLL TestProcessBLL = new();

        public ucItemConfiguration()
        {
            InitializeComponent();
        }

        List<TestProcessModel> lstTestProcess = [];

        void LoadProcess()
        {
            if (_currentModel == null) return;

            int typeID = _currentModel.ModelTypeID;
            lstAllPoint.Items.Clear();
            lstTestProcess = TestProcessBLL.GetTestProcess(typeID, true);
            foreach (var item in lstTestProcess)
            {
                lstAllPoint.Items.Add(item.ProcessName);
            }
        }

        /// <summary>
        /// 加载已配置的试验项点
        /// </summary>
        void LoadConfiguaredProcess()
        {
            try
            {
                lstTestPoint.Items.Clear();

                if (_currentModel.ModelTypeID == 0)
                    return;

                // 获取已配置的测试步骤，按Step字段排序
                List<TestStepModel> lstTestStep = [.. StepBLL.GetTestSteps(new TestStepModel { ModelID = (int)_currentModel.ID }).OrderBy(x => x.Step)];

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

        /// <summary>
        /// 修改对应项点自动试验逻辑
        /// 核心改动：使用 ProductPathHelper 构建路径
        /// </summary>
        private void EnditTest(UIListBox lstbox)
        {
            try
            {
                if (_currentModel == null)
                {
                    MessageHelper.MessageOK("请先选择产品型号！", TType.Warn);
                    return;
                }

                if (lstbox.Items.Count <= 0 || lstbox.SelectedItem == null)
                {
                    MessageHelper.MessageOK("请先选择试验项点！", TType.Warn);
                    return;
                }

                string modelType = _currentModel.ModelTypeName;
                string modelName = _currentModel.ModelName;
                string lstName = lstbox.SelectedItem.ToString();

                // 使用 ProductPathHelper 构建路径，保证唯一性
                string testPath = ProductPathHelper.BuildJsonPath(
                    modelType, _currentModel.ID, modelName, lstName);

                Debug.WriteLine($"产品：{_currentModel.ProductSummary}，" +
                    $"项点：{lstName}，路径：{testPath}");

                var formFactory = Program.ServiceProvider.GetRequiredService<IFormService>();
                var form = formFactory.CreateLogicalConfigurationForm(
                    testPath, modelType, _currentModel.ID, modelName, lstName);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                MessageHelper.MessageOK($"获取自动试验逻辑失败：{err}");
            }
        }

        #region 试验项点复制功能模块

        /// <summary>
        /// 复制按钮点击事件 - 打开跨型号复制对话框
        /// 路径构建改用 ProductPathHelper
        /// </summary>
        private async void btnCopyItem_Click(object sender, EventArgs e)
        {
            try
            {
                var parentForm = this.FindForm();
                if (parentForm == null)
                {
                    MessageHelper.MessageOK("无法找到父窗体!", TType.Error);
                    return;
                }

                using var copyDialog = new ItemCopyDialog();

                if (_currentModel != null && lstTestPoint.SelectedItem != null)
                {
                    copyDialog.SetDefaultSource(
                        (int)_currentModel.ModelTypeID,
                        (int)_currentModel.ID,
                        lstTestPoint.SelectedItem.ToString()
                    );
                }

                if (VarHelper.ShowDialogWithOverlayEx(parentForm, copyDialog) != DialogResult.OK) return;
                var copyInfo = copyDialog.GetCopyInfo();

                var confirmMessage = $"确定要将逻辑复制吗?\n\n" +
                                     $"源: {copyInfo.SourceModelType} / {copyInfo.SourceModelName} / {copyInfo.SourceItemName}\n" +
                                     $"目标: {copyInfo.TargetModelType} / {copyInfo.TargetModelName} / {copyInfo.TargetItemName}\n\n" +
                                     $"注意: 目标项点的现有逻辑配置将被覆盖!";

                if (MessageHelper.MessageYes(parentForm, confirmMessage, TType.Warn) != DialogResult.OK)
                    return;

                await CopyTestItemLogic(copyInfo);

                MessageHelper.MessageOK("测试项逻辑复制成功!", TType.Success);

                if (copyInfo.TargetModelId == _currentModel?.ID)
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
        /// 路径构建改用 ProductPathHelper
        /// </summary>
        private async Task CopyTestItemLogic(ItemCopyInfo copyInfo)
        {
            // 使用 ProductPathHelper 构建源和目标路径
            string sourceJsonPath = ProductPathHelper.BuildJsonPath(
                copyInfo.SourceModelType, copyInfo.SourceModelId,
                copyInfo.SourceModelName, copyInfo.SourceItemName);

            string targetJsonPath = ProductPathHelper.BuildJsonPath(
                copyInfo.TargetModelType, copyInfo.TargetModelId,
                copyInfo.TargetModelName, copyInfo.TargetItemName);

            if (!File.Exists(sourceJsonPath))
            {
                throw new FileNotFoundException($"源测试项配置文件不存在: {copyInfo.SourceItemName}");
            }

            string targetDir = Path.GetDirectoryName(targetJsonPath);
            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            string jsonContent = await File.ReadAllTextAsync(sourceJsonPath);
            var sourceConfig = JsonConvert.DeserializeObject<JsonManager.JsonConfig>(jsonContent);

            if (sourceConfig == null)
            {
                throw new InvalidOperationException("无法解析源配置文件");
            }

            // 更新目标配置中的产品信息
            if (sourceConfig.Form != null && sourceConfig.Form.Count > 0)
            {
                sourceConfig.Form[0].ModelTypeName = copyInfo.TargetModelType;
                sourceConfig.Form[0].ModelName = copyInfo.TargetModelName;
                sourceConfig.Form[0].ItemName = copyInfo.TargetItemName;
            }

            string targetJson = JsonConvert.SerializeObject(sourceConfig, Formatting.Indented);
            await File.WriteAllTextAsync(targetJsonPath, targetJson);

            NlogHelper.Default.Info($"测试项逻辑复制完成: " +
                $"{copyInfo.SourceModelName}/{copyInfo.SourceItemName} -> " +
                $"{copyInfo.TargetModelName}/{copyInfo.TargetItemName}");
        }

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

        public override void Reload()
        {
            try
            {
                if (_currentModel == null) return;

                // 只清理已失效的项点，不重建整个列表
                // 先刷新 lstTestProcess（项点定义可能被外部改过）
                lstTestProcess = TestProcessBLL.GetTestProcess(
                    (int)_currentModel.ModelTypeID, true);

                CleanInvalidTestPoints();

                Debug.WriteLine("ucItemConfiguration 项点配置 数据已刷新");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("ucItemConfiguration 项点配置 重新加载数据失败", ex);
            }
        }

        private void CleanInvalidTestPoints()
        {
            try
            {
                if (lstTestPoint.Items.Count == 0 || lstTestProcess == null || lstTestProcess.Count == 0)
                    return;

                var invalidItems = new List<object>();
                foreach (var item in lstTestPoint.Items)
                {
                    string processName = item.ToString();
                    if (!lstTestProcess.Exists(x => x.ProcessName == processName))
                    {
                        invalidItems.Add(item);
                    }
                }

                if (invalidItems.Count <= 0) return;

                foreach (var item in invalidItems)
                {
                    lstTestPoint.Items.Remove(item);
                }

                NlogHelper.Default.Warn($"已自动移除 {invalidItems.Count} 个无效的测试项点");
                MessageHelper.MessageOK($"检测到 {invalidItems.Count} 个项点已被删除,已自动移除", TType.Info);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("清理无效测试点失败", ex);
            }
        }

        protected override void OnDataChangedWithType(DataChangeType changeType)
        {
            // TestProcess：项点定义变了，需要清理已失效项
            // TestStep：步骤配置变了（本控件自己发出，不需要响应自己）
            if (changeType == DataChangeType.TestProcess || changeType == DataChangeType.All)
            {
                base.OnDataChangedWithType(changeType);
            }
            // TestStep 不触发 Reload，避免自己保存后刷新覆盖列表
        }

        #endregion

        #region 上下移动功能（保持不变）

        private void MoveUpTestPoint()
        {
            try
            {
                if (lstTestPoint.SelectedIndex == -1)
                {
                    MessageHelper.MessageOK("请先选择要上移的测试点!", TType.Warn);
                    return;
                }

                int selectedIndex = lstTestPoint.SelectedIndex;
                if (selectedIndex == 0)
                {
                    MessageHelper.MessageOK("已经是第一项,无法继续上移!", TType.Info);
                    return;
                }

                object currentItem = lstTestPoint.Items[selectedIndex];
                object previousItem = lstTestPoint.Items[selectedIndex - 1];
                lstTestPoint.Items[selectedIndex - 1] = currentItem;
                lstTestPoint.Items[selectedIndex] = previousItem;
                lstTestPoint.SelectedIndex = selectedIndex - 1;
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"上移测试点失败: {ex.Message}", TType.Error);
                NlogHelper.Default.Error("上移测试点失败", ex);
            }
        }

        private void MoveDownTestPoint()
        {
            try
            {
                if (lstTestPoint.SelectedIndex == -1)
                {
                    MessageHelper.MessageOK("请先选择要下移的测试点!", TType.Warn);
                    return;
                }

                int selectedIndex = lstTestPoint.SelectedIndex;
                if (selectedIndex == lstTestPoint.Items.Count - 1)
                {
                    MessageHelper.MessageOK("已经是最后一项,无法继续下移!", TType.Info);
                    return;
                }

                object currentItem = lstTestPoint.Items[selectedIndex];
                object nextItem = lstTestPoint.Items[selectedIndex + 1];
                lstTestPoint.Items[selectedIndex + 1] = currentItem;
                lstTestPoint.Items[selectedIndex] = nextItem;
                lstTestPoint.SelectedIndex = selectedIndex + 1;
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"下移测试点失败: {ex.Message}", TType.Error);
                NlogHelper.Default.Error("下移测试点失败", ex);
            }
        }

        private void btnUp_Click(object sender, EventArgs e) => MoveUpTestPoint();

        private void btnDown_Click(object sender, EventArgs e) => MoveDownTestPoint();

        #endregion

        private void lstTestPoint_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EnditTest(sender as UIListBox);
        }

        private void productSelectButton1_ProductSelected(object sender, NewModels e)
        {
            _currentModel = e;

            // 加载该产品类型下的项点
            LoadProcess();
            // 加载该产品已配置的项点
            LoadConfiguaredProcess();
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
                        testStep.ModelID = (int)_currentModel.ID;
                        testStep.ProcessName = $"{lstTestPoint.Items[i]}";
                        testStep.TestProcessID = lstTestProcess.Find(x => x.ProcessName == testStep.ProcessName).ID;
                    }
                    lstTestStep.Add(testStep);
                }
                if (_currentModel.ID == 0)
                {
                    MessageHelper.MessageOK("型号未选择，保存失败！");
                    return;
                }
                StepBLL.InsertTestStep(lstTestStep, _currentModel.ID);
                LoadConfiguaredProcess();
                MessageHelper.MessageOK("保存成功！");
                DataChangedEventManager.NotifyDataChanged(DataChangeType.TestStep);
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"保存错误：{ex.Message}");
            }

        }
    }
}