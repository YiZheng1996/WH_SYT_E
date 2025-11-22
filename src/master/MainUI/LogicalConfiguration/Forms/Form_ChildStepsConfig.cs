using AntdUI;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Parameter;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MainUI.LogicalConfiguration.Forms
{
    /// <summary>
    /// 循环体子步骤配置窗体 - 继承自基类
    /// </summary>
    public partial class Form_ChildStepsConfig : BaseStepConfigForm
    {
        #region 私有字段

        /// <summary>
        /// 子步骤列表（深拷贝）
        /// </summary>
        private List<ChildModel> _childSteps;

        /// <summary>
        /// 原始步骤列表引用（用于保存）
        /// </summary>
        private readonly List<ChildModel> _originalSteps;

        /// <summary>
        /// 底部按钮面板
        /// </summary>
        private UIPanel panelButtons;

        /// <summary>
        /// 保存按钮
        /// </summary>
        private UISymbolButton btnSave;

        /// <summary>
        /// 取消按钮
        /// </summary>
        private UISymbolButton btnCancel;


        #endregion

        #region 重写基类属性

        /// <summary>
        /// 不允许循环控制（避免嵌套循环）
        /// </summary>
        protected override bool AllowLoopControl => false;

        /// <summary>
        /// 窗体标题
        /// </summary>
        protected override string FormTitle => "📋 循环体子步骤配置";

        /// <summary>
        /// 获取步骤列表
        /// </summary>
        protected override List<ChildModel> GetStepsList() => _childSteps;

        /// <summary>
        /// 设置步骤列表
        /// </summary>
        protected override void SetStepsList(List<ChildModel> steps) => _childSteps = steps;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="childSteps">要编辑的子步骤列表</param>
        /// <param name="logger">日志服务(可选)</param>
        public Form_ChildStepsConfig(
            List<ChildModel> childSteps,
            ILogger<Form_ChildStepsConfig> logger = null)
        {
            _logger = logger;
            _originalSteps = childSteps;

            // 深拷贝子步骤列表，避免直接修改原始数据
            _childSteps = childSteps != null
                ? JsonConvert.DeserializeObject<List<ChildModel>>(
                    JsonConvert.SerializeObject(childSteps)): [];

            InitializeComponent();
            InitializeCustomUI();

            // 调用基类的初始化方法
            InitializeToolBox();
            LoadStepsToGrid();
        }

        #endregion

        #region 初始化组件

        /// <summary>
        /// 初始化自定义UI
        /// </summary>
        private void InitializeCustomUI()
        {
            // 创建保存按钮
            btnSave = new UISymbolButton
            {
                Size = new Size(100, 35),
                Location = new Point(this.Width - 240, 12),
                Text = "保存",
                Symbol = 361445, // ✓ 符号
                SymbolColor = Color.White,
                FillColor = Color.FromArgb(65, 100, 204),
                RectColor = Color.FromArgb(65, 100, 204),
                FillHoverColor = Color.FromArgb(85, 120, 224),
                Cursor = Cursors.Hand,
                Font = new Font("微软雅黑", 10F)
            };
            btnSave.Click += BtnSave_Click;

            // 创建取消按钮
            btnCancel = new UISymbolButton
            {
                Size = new Size(100, 35),
                Location = new Point(this.Width - 130, 12),
                Text = "取消",
                Symbol = 361453, // ✕ 符号
                SymbolColor = Color.FromArgb(80, 80, 80),
                FillColor = Color.FromArgb(220, 53, 69),
                RectColor = Color.FromArgb(200, 200, 200),
                FillHoverColor = Color.FromArgb(220, 53, 69),
                Cursor = Cursors.Hand,
                Font = new Font("微软雅黑", 10F)
            };
            btnCancel.Click += BtnCancel_Click;

            pnlButtons.Controls.AddRange([btnSave, btnCancel]);
            
            // 设置右键菜单
            dgvSteps.ContextMenuStrip = CreateContextMenu();
        }

        #endregion

        #region 重写基类方法

        /// <summary>
        /// 重写工具箱初始化（移除循环控制）
        /// </summary>
        protected override void InitializeToolBox()
        {
            base.InitializeToolBox();

            // 可以在这里添加子步骤特有的工具项
            // 或者移除不需要的工具项
            _logger?.LogDebug("子步骤工具箱初始化完成，已禁用循环控制");
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        protected override void SaveConfiguration()
        {
            try
            {
                // 清空原始列表
                _originalSteps?.Clear();

                // 复制编辑后的步骤到原始列表
                if (_originalSteps != null && _childSteps != null)
                {
                    _originalSteps.AddRange(_childSteps);
                }

                _hasUnsavedChanges = false;
                _logger?.LogInformation("子步骤配置已保存，共 {Count} 个步骤", _childSteps?.Count ?? 0);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "保存子步骤配置失败");
                throw;
            }
        }

        #endregion

        #region 按钮事件

        /// <summary>
        /// 保存按钮点击
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveConfiguration();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"保存失败：{ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 取消按钮点击
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 获取配置好的子步骤列表
        /// </summary>
        public List<ChildModel> GetChildSteps()
        {
            return _childSteps;
        }

        #endregion

        #region 扩展功能

        /// <summary>
        /// 验证子步骤配置
        /// </summary>
        private bool ValidateChildSteps()
        {
            if (_childSteps == null || _childSteps.Count == 0)
            {
                MessageHelper.MessageOK("循环体至少需要配置一个步骤", TType.Warn);
                return false;
            }

            // 检查是否有嵌套循环
            foreach (var step in _childSteps)
            {
                if (step.StepName == "LoopControlStart" || step.StepName == "Loop")
                {
                    MessageHelper.MessageOK(
                        "子步骤中不能包含循环控制，避免嵌套循环的复杂性",
                        TType.Warn);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 导入步骤模板
        /// </summary>
        private void ImportTemplate()
        {
            // 可以实现从模板导入常用步骤组合
            _logger?.LogInformation("导入步骤模板功能");
        }

        /// <summary>
        /// 导出为模板
        /// </summary>
        private void ExportAsTemplate()
        {
            // 可以实现将当前配置导出为模板
            _logger?.LogInformation("导出为模板功能");
        }

        #endregion
    }
}