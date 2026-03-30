using MainUI.Service;
using Sunny.UI;

namespace MainUI.Procedure.Controls
{
    /// <summary>
    /// 产品选择按钮控件
    /// 点击弹出 FrmSpec 选择产品，按钮文字显示产品摘要
    /// 
    /// 用法：
    ///   1. 拖到窗体上（或代码创建）
    ///   2. 订阅 ProductSelected 事件
    ///   3. 通过 SelectedModel 获取选中的产品
    /// </summary>
    public class ProductSelectButton : UIButton
    {
        #region 私有字段

        private NewModels _selectedModel;

        #endregion

        #region 属性

        /// <summary>
        /// 当前选中的产品型号
        /// </summary>
        public NewModels SelectedModel
        {
            get => _selectedModel;
            private set
            {
                _selectedModel = value;
                UpdateButtonText();
            }
        }

        /// <summary>
        /// 是否已选择产品
        /// </summary>
        public bool HasSelection => _selectedModel != null && _selectedModel.ID > 0;

        /// <summary>
        /// 未选择时的提示文字
        /// </summary>
        public string PlaceholderText { get; set; } = "点击选择产品型号...";

        #endregion

        #region 事件

        /// <summary>
        /// 产品选择完成事件
        /// </summary>
        public event EventHandler<NewModels> ProductSelected;

        #endregion

        #region 构造函数

        public ProductSelectButton()
        {
            // 默认样式
            Cursor = Cursors.Hand;
            Font = new Font("微软雅黑", 12F);
            ForeColor = Color.Black;
            FillColor = Color.FromArgb(218, 220, 230);
            FillColor2 = Color.FromArgb(218, 220, 230);
            RectColor = Color.Gray;
            Radius = 10;
            Size = new Size(400, 35);
            TextAlign = ContentAlignment.MiddleLeft;

            UpdateButtonText();
        }

        #endregion

        #region 核心方法

        /// <summary>
        /// 点击事件 - 弹出 FrmSpec 选择产品
        /// </summary>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            ShowProductSelector();
        }

        /// <summary>
        /// 弹出产品选择窗体
        /// </summary>
        private void ShowProductSelector()
        {
            try
            {
                // 查找父窗体
                var parentForm = FindForm();

                using FrmSpec frmSpec = new();

                if (parentForm != null)
                {
                    VarHelper.ShowDialogWithOverlay(parentForm, frmSpec);
                }
                else
                {
                    frmSpec.ShowDialog();
                }

                if (frmSpec.DialogResult == DialogResult.OK && VarHelper.TestViewModel != null)
                {
                    SelectedModel = VarHelper.TestViewModel;
                    ProductSelected?.Invoke(this, SelectedModel);
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("产品选择失败", ex);
                MessageHelper.MessageOK($"产品选择失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 外部设置选中的产品（用于数据恢复/初始化）
        /// </summary>
        public void SetSelectedModel(NewModels model)
        {
            _selectedModel = model;
            UpdateButtonText();
        }

        /// <summary>
        /// 通过ID加载并设置选中的产品
        /// </summary>
        public void SetSelectedModelById(int modelId)
        {
            if (modelId <= 0)
            {
                ClearSelection();
                return;
            }

            var model = ModelBLL.GetModelById(modelId);
            if (model != null)
            {
                _selectedModel = model;
                UpdateButtonText();
            }
        }

        /// <summary>
        /// 清除选择
        /// </summary>
        public void ClearSelection()
        {
            _selectedModel = null;
            UpdateButtonText();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 更新按钮显示文字
        /// </summary>
        private void UpdateButtonText()
        {
            if (_selectedModel != null && _selectedModel.ID > 0)
            {
                // 显示产品摘要 + 类型名
                Text = $"  {_selectedModel.ModelTypeName} / {_selectedModel.ProductSummary}";
            }
            else
            {
                Text = $"  {PlaceholderText}";
            }
        }

        #endregion
    }
}