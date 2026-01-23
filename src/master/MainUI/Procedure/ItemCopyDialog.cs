using AntdUI;
using MainUI.Model;

namespace MainUI.Procedure
{
    /// <summary>
    /// 跨型号项点逻辑复制对话框
    /// </summary>
    public partial class ItemCopyDialog : Sunny.UI.UIForm
    {
        #region 私有字段

        // 数据访问层
        private readonly ModelTypeBLL _modelTypeBLL = new();
        private readonly ModelBLL _modelBLL = new();
        private readonly TestStepBLL _testStepBLL = new();

        #endregion

        #region 构造函数

        public ItemCopyDialog()
        {
            InitializeComponent();
            InitializeData();
        }

        #endregion

        #region 初始化方法

        /// <summary>
        /// 初始化数据 - 加载产品类型列表
        /// </summary>
        private void InitializeData()
        {
            try
            {
                // 加载源和目标的产品类型
                var modelTypes = _modelTypeBLL.GetModelsByTestBench(Service.TestBenchService.CurrentTestBenchID);

                cboSourceType.DisplayMember = "ModelTypeName";
                cboSourceType.ValueMember = "ID";
                cboSourceType.DataSource = new List<ModelsType>(modelTypes);

                cboTargetType.DisplayMember = "ModelTypeName";
                cboTargetType.ValueMember = "ID";
                cboTargetType.DataSource = new List<ModelsType>(modelTypes);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("初始化复制对话框数据失败", ex);
                MessageHelper.MessageOK($"初始化失败: {ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 源选择事件

        /// <summary>
        /// 源产品类型选择改变事件 - 加载对应的产品型号
        /// </summary>
        private void CboSourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSourceType.SelectedValue == null) return;

            try
            {
                int typeId = (int)cboSourceType.SelectedValue;
                var models = ModelBLL.GetNewModels(typeId);

                cboSourceModel.DisplayMember = "ModelName";
                cboSourceModel.ValueMember = "ID";
                cboSourceModel.DataSource = models;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载源型号失败", ex);
            }
        }

        /// <summary>
        /// 源产品型号选择改变事件 - 加载对应的测试项
        /// </summary>
        private void CboSourceModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSourceModel.SelectedValue == null) return;

            try
            {
                int modelId = (int)cboSourceModel.SelectedValue;
                var testSteps = _testStepBLL.GetTestSteps(new TestStepModel { ModelID = modelId })
                    .OrderBy(x => x.Step)
                    .Select(x => x.ProcessName)
                    .Distinct()
                    .ToList();

                cboSourceItem.DataSource = testSteps;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载源测试项失败", ex);
            }
        }

        #endregion

        #region 目标选择事件

        /// <summary>
        /// 目标产品类型选择改变事件 - 加载对应的产品型号
        /// </summary>
        private void CboTargetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTargetType.SelectedValue == null) return;

            try
            {
                int typeId = (int)cboTargetType.SelectedValue;
                var models = ModelBLL.GetNewModels(typeId);

                cboTargetModel.DisplayMember = "ModelName";
                cboTargetModel.ValueMember = "ID";
                cboTargetModel.DataSource = models;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载目标型号失败", ex);
            }
        }

        /// <summary>
        /// 目标产品型号选择改变事件 - 加载对应的测试项
        /// </summary>
        private void CboTargetModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTargetModel.SelectedValue == null) return;

            try
            {
                int modelId = (int)cboTargetModel.SelectedValue;
                var testSteps = _testStepBLL.GetTestSteps(new TestStepModel { ModelID = modelId })
                    .OrderBy(x => x.Step)
                    .Select(x => x.ProcessName)
                    .Distinct()
                    .ToList();

                cboTargetItem.DataSource = testSteps;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("加载目标测试项失败", ex);
            }
        }

        #endregion

        #region 按钮事件

        /// <summary>
        /// 确定按钮点击事件 - 验证选择并关闭对话框
        /// </summary>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            // 验证源选择是否完整
            if (cboSourceType.SelectedValue == null || cboSourceModel.SelectedValue == null ||
                cboSourceItem.SelectedItem == null)
            {
                MessageHelper.MessageOK("请选择完整的源测试项信息!", TType.Warn);
                return;
            }

            // 验证目标选择是否完整
            if (cboTargetType.SelectedValue == null || cboTargetModel.SelectedValue == null ||
                cboTargetItem.SelectedItem == null)
            {
                MessageHelper.MessageOK("请选择完整的目标测试项信息!", TType.Warn);
                return;
            }

            // 检查是否选择了相同的项
            if (cboSourceType.SelectedValue.Equals(cboTargetType.SelectedValue) &&
                cboSourceModel.SelectedValue.Equals(cboTargetModel.SelectedValue) &&
                cboSourceItem.SelectedItem.ToString() == cboTargetItem.SelectedItem.ToString())
            {
                MessageHelper.MessageOK("源和目标不能是同一个测试项!", TType.Warn);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// 取消按钮点击事件 - 关闭对话框
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 设置默认的源信息
        /// </summary>
        /// <param name="modelTypeId">产品类型ID</param>
        /// <param name="modelId">产品型号ID</param>
        /// <param name="itemName">测试项名称</param>
        public void SetDefaultSource(int modelTypeId, int modelId, string itemName)
        {
            try
            {
                cboSourceType.SelectedValue = modelTypeId;
                cboSourceModel.SelectedValue = modelId;

                // 等待数据加载
                Application.DoEvents();

                if (cboSourceItem.Items.Contains(itemName))
                {
                    cboSourceItem.SelectedItem = itemName;
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("设置默认源信息失败", ex);
            }
        }

        /// <summary>
        /// 获取复制信息
        /// </summary>
        /// <returns>包含源和目标信息的复制对象</returns>
        public ItemCopyInfo GetCopyInfo()
        {
            return new ItemCopyInfo
            {
                SourceModelTypeId = (int)cboSourceType.SelectedValue,
                SourceModelType = cboSourceType.SelectedText,
                SourceModelId = (int)cboSourceModel.SelectedValue,
                SourceModelName = cboSourceModel.SelectedText,
                SourceItemName = cboSourceItem.SelectedItem?.ToString(),

                TargetModelTypeId = (int)cboTargetType.SelectedValue,
                TargetModelType = cboTargetType.SelectedText,
                TargetModelId = (int)cboTargetModel.SelectedValue,
                TargetModelName = cboTargetModel.SelectedText,
                TargetItemName = cboTargetItem.SelectedItem?.ToString()
            };
        }

        #endregion
    }

    /// <summary>
    /// 项点复制信息类 - 存储源和目标的完整信息
    /// </summary>
    public class ItemCopyInfo
    {
        /// <summary>源产品类型ID</summary>
        public int SourceModelTypeId { get; set; }

        /// <summary>源产品类型名称</summary>
        public string SourceModelType { get; set; }

        /// <summary>源产品型号ID</summary>
        public int SourceModelId { get; set; }

        /// <summary>源产品型号名称</summary>
        public string SourceModelName { get; set; }

        /// <summary>源测试项名称</summary>
        public string SourceItemName { get; set; }

        /// <summary>目标产品类型ID</summary>
        public int TargetModelTypeId { get; set; }

        /// <summary>目标产品类型名称</summary>
        public string TargetModelType { get; set; }

        /// <summary>目标产品型号ID</summary>
        public int TargetModelId { get; set; }

        /// <summary>目标产品型号名称</summary>
        public string TargetModelName { get; set; }

        /// <summary>目标测试项名称</summary>
        public string TargetItemName { get; set; }
    }
}