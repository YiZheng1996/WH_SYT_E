using AntdUI;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ContextMenuStrip = System.Windows.Forms.ContextMenuStrip;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MainUI.LogicalConfiguration.LogicalManager
{
    /// <summary>
    /// 步骤右键菜单管理器 - 负责所有右键菜单相关的功能(支持右键菜单和按钮调用)
    /// </summary>
    public class StepContextMenuManager : IDisposable
    {
        #region 字段

        private readonly DataGridView _grid;
        private readonly IWorkflowStateService _workflowState;
        private readonly DataGridViewManager _gridManager;
        private readonly ILogger _logger;
        private readonly Form _ownerForm;

        // 右键菜单控件
        private ContextMenuStrip _contextMenu;
        private ToolStripMenuItem _menuItemInsertBefore;
        private ToolStripMenuItem _menuItemInsertAfter;
        private ToolStripMenuItem _menuItemDelete;
        private ToolStripMenuItem _menuItemMoveUp;
        private ToolStripMenuItem _menuItemMoveDown;
        private ToolStripMenuItem _menuItemCopy;
        private ToolStripMenuItem _menuItemCut;
        private ToolStripMenuItem _menuItemPaste;
        private ToolStripMenuItem _menuItemSelectAll;
        private ToolStripMenuItem _menuItemClearAll;

        // 复制/剪切的步骤 - 改为支持多个
        private List<ChildModel> _copiedSteps;
        private bool _isCut;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StepContextMenuManager(
            DataGridView grid,
            IWorkflowStateService workflowState,
            DataGridViewManager gridManager,
            ILogger logger,
            Form ownerForm)
        {
            _grid = grid ?? throw new ArgumentNullException(nameof(grid));
            _workflowState = workflowState ?? throw new ArgumentNullException(nameof(workflowState));
            _gridManager = gridManager ?? throw new ArgumentNullException(nameof(gridManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ownerForm = ownerForm ?? throw new ArgumentNullException(nameof(ownerForm));

            InitializeContextMenu();
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化右键菜单
        /// </summary>
        private void InitializeContextMenu()
        {
            try
            {
                _contextMenu = new ContextMenuStrip();
                _contextMenu.Opening += OnContextMenuOpening;

                // 创建菜单项
                _menuItemInsertBefore = CreateMenuItem("在此之前插入 (&B)", OnInsertBefore, Keys.Control | Keys.I);
                _menuItemInsertAfter = CreateMenuItem("在此之后插入 (&A)", OnInsertAfter, Keys.Control | Keys.U);
                _menuItemDelete = CreateMenuItem("删除步骤 (&D)", OnDelete, Keys.Delete);
                _menuItemMoveUp = CreateMenuItem("上移 (&U)", OnMoveUp, Keys.Control | Keys.Up);
                _menuItemMoveDown = CreateMenuItem("下移 (&N)", OnMoveDown, Keys.Control | Keys.Down);
                _menuItemCopy = CreateMenuItem("复制 (&C)", OnCopy, Keys.Control | Keys.C);
                _menuItemCut = CreateMenuItem("剪切 (&X)", OnCut, Keys.Control | Keys.X);
                _menuItemPaste = CreateMenuItem("粘贴 (&V)", OnPaste, Keys.Control | Keys.V);
                _menuItemSelectAll = CreateMenuItem("全选 (&L)", OnSelectAll, Keys.Control | Keys.A);
                _menuItemClearAll = CreateMenuItem("清空所有步骤 (&R)", OnClearAll);

                // 添加到菜单
                _contextMenu.Items.AddRange(
                [
                    _menuItemInsertBefore,
                    _menuItemInsertAfter,
                    new ToolStripSeparator(),
                    _menuItemDelete,
                    new ToolStripSeparator(),
                    _menuItemMoveUp,
                    _menuItemMoveDown,
                    new ToolStripSeparator(),
                    _menuItemCopy,
                    _menuItemCut,
                    _menuItemPaste,
                    new ToolStripSeparator(),
                    _menuItemSelectAll,
                    _menuItemClearAll
                ]);

                // 设置样式
                _contextMenu.Font = new Font("微软雅黑", 12F);
                _contextMenu.RenderMode = ToolStripRenderMode.Professional;

                // 绑定到 DataGridView
                _grid.ContextMenuStrip = _contextMenu;

                _logger.LogDebug("步骤右键菜单初始化完成");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "初始化右键菜单时发生错误");
                throw;
            }
        }

        /// <summary>
        /// 创建菜单项
        /// </summary>
        private ToolStripMenuItem CreateMenuItem(string text, EventHandler onClick, Keys shortcutKeys = Keys.None)
        {
            var item = new ToolStripMenuItem(text, null, onClick);
            if (shortcutKeys != Keys.None)
            {
                item.ShortcutKeys = shortcutKeys;
            }
            return item;
        }

        #endregion

        #region 菜单事件处理

        /// <summary>
        /// 菜单打开时动态设置可用性
        /// </summary>
        private void OnContextMenuOpening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                int selectedCount = _gridManager.GetSelectedRowCount();
                int selectedIndex = _gridManager.GetSelectedRowIndex();
                int totalRows = _grid.Rows.Count;
                bool hasSelection = selectedCount > 0;
                bool hasCopiedData = HasCopiedData();

                // 多选时只允许部分操作
                bool isSingleSelection = selectedCount == 1;

                // 插入操作仅单选时可用
                _menuItemInsertBefore.Enabled = isSingleSelection;
                _menuItemInsertAfter.Enabled = isSingleSelection;

                // 删除/复制/剪切支持多选
                _menuItemDelete.Enabled = hasSelection;
                _menuItemCopy.Enabled = hasSelection;
                _menuItemCut.Enabled = hasSelection;
                _menuItemPaste.Enabled = hasCopiedData;

                // 上移/下移仅单选时可用
                _menuItemMoveUp.Enabled = isSingleSelection && selectedIndex > 0;
                _menuItemMoveDown.Enabled = isSingleSelection && selectedIndex < totalRows - 1;

                // 全选/清空
                _menuItemSelectAll.Enabled = totalRows > 0;
                _menuItemClearAll.Enabled = totalRows > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理菜单打开事件时发生错误");
            }
        }

        #endregion

        #region 私有辅助方法

        /// <summary>
        /// 深拷贝步骤参数对象 - 避免引用复制导致的数据关联问题
        /// </summary>
        /// <param name="parameter">原始参数对象</param>
        /// <returns>深拷贝后的新对象</returns>
        private object DeepCloneParameter(object parameter)
        {
            if (parameter == null) return null;

            try
            {
                // 使用JSON序列化实现深拷贝
                string json = JsonConvert.SerializeObject(parameter);
                return JsonConvert.DeserializeObject(json, parameter.GetType());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "参数深拷贝失败,返回null");
                return null;
            }
        }

        /// <summary>
        /// 深拷贝步骤对象
        /// </summary>
        /// <param name="step">原始步骤</param>
        /// <returns>深拷贝后的新步骤</returns>
        private ChildModel DeepCloneStep(ChildModel step)
        {
            if (step == null) return null;

            return new ChildModel
            {
                StepName = step.StepName,
                Status = 0,
                StepNum = 0,
                StepParameter = DeepCloneParameter(step.StepParameter),
                Remark = step.Remark,
                ErrorMessage = null  // 新步骤清空错误信息
            };
        }

        #endregion

        #region 公共方法 - 供按钮和快捷键调用

        /// <summary>
        /// 检查是否有复制的数据
        /// </summary>
        public bool HasCopiedData() =>
            _copiedSteps != null && _copiedSteps.Count > 0;

        /// <summary>
        /// 在当前选中行之前插入步骤
        /// </summary>
        public void InsertBefore()
        {
            InsertStep(InsertPosition.Before);
        }

        /// <summary>
        /// 在当前选中行之后插入步骤
        /// </summary>
        public void InsertAfter()
        {
            InsertStep(InsertPosition.After);
        }

        /// <summary>
        /// 删除当前选中的步骤
        /// </summary>
        public void Delete()
        {
            DeleteStep();
        }

        /// <summary>
        /// 上移当前选中的步骤
        /// </summary>
        public void MoveUp()
        {
            MoveStep(MoveDirection.Up);
        }

        /// <summary>
        /// 下移当前选中的步骤
        /// </summary>
        public void MoveDown()
        {
            MoveStep(MoveDirection.Down);
        }

        /// <summary>
        /// 复制当前选中的步骤
        /// </summary>
        public void Copy()
        {
            CopyStep();
        }

        /// <summary>
        /// 剪切当前选中的步骤
        /// </summary>
        public void Cut()
        {
            CutStep();
        }

        /// <summary>
        /// 粘贴步骤
        /// </summary>
        public void Paste()
        {
            PasteStep();
        }

        /// <summary>
        /// 全选所有步骤
        /// </summary>
        public void SelectAll()
        {
            _grid.SelectAll();
        }

        /// <summary>
        /// 清空所有步骤
        /// </summary>
        public void ClearAll()
        {
            ClearAllSteps();
        }

        #endregion

        #region 私有方法 - 右键菜单事件处理

        private void OnInsertBefore(object sender, EventArgs e)
        {
            InsertBefore();
        }

        private void OnInsertAfter(object sender, EventArgs e)
        {
            InsertAfter();
        }

        private void OnDelete(object sender, EventArgs e)
        {
            Delete();
        }

        private void OnMoveUp(object sender, EventArgs e)
        {
            MoveUp();
        }

        private void OnMoveDown(object sender, EventArgs e)
        {
            MoveDown();
        }

        private void OnCopy(object sender, EventArgs e)
        {
            Copy();
        }

        private void OnCut(object sender, EventArgs e)
        {
            Cut();
        }

        private void OnPaste(object sender, EventArgs e)
        {
            Paste();
        }

        private void OnSelectAll(object sender, EventArgs e)
        {
            SelectAll();
        }

        private void OnClearAll(object sender, EventArgs e)
        {
            ClearAll();
        }

        #endregion

        #region 插入功能

        private enum InsertPosition { Before, After }

        private void InsertStep(InsertPosition position)
        {
            try
            {
                int selectedIndex = _gridManager.GetSelectedRowIndex();
                if (selectedIndex < 0)
                {
                    MessageHelper.MessageOK("请先选择一个步骤！", TType.Warn);
                    return;
                }

                var stepName = ShowStepSelectionDialog();
                if (string.IsNullOrEmpty(stepName)) return;

                int insertIndex = position == InsertPosition.Before ?
                    selectedIndex : selectedIndex + 1;
                var steps = _workflowState.GetSteps();

                var newStep = new ChildModel
                {
                    StepName = stepName,
                    Status = 0,
                    StepNum = insertIndex + 1,
                    StepParameter = 0
                };

                steps.Insert(insertIndex, newStep);

                // 重新编号
                for (int i = 0; i < steps.Count; i++)
                {
                    steps[i].StepNum = i + 1;
                }

                // 刷新数据
                _workflowState.ClearSteps();
                foreach (var step in steps)
                {
                    _workflowState.AddStep(step);
                }

                // 选中新行
                if (insertIndex < _grid.Rows.Count)
                {
                    _grid.ClearSelection();
                    _grid.Rows[insertIndex].Selected = true;
                }

                _logger.LogInformation("步骤插入成功: {StepName}", stepName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "插入步骤时发生错误");
                MessageHelper.MessageOK($"插入步骤错误：{ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 删除功能

        /// <summary>
        /// 删除选中的步骤(支持多选)
        /// </summary>
        private void DeleteStep()
        {
            try
            {
                var selectedIndices = _gridManager.GetSelectedRowIndices();
                if (selectedIndices.Length == 0)
                {
                    MessageHelper.MessageOK("请先选择要删除的步骤!", TType.Warn);
                    return;
                }

                // 构建确认消息
                string confirmMessage = selectedIndices.Length == 1
                    ? "确定要删除选中的步骤吗?"
                    : $"确定要删除选中的 {selectedIndices.Length} 个步骤吗?";

                if (MessageHelper.MessageYes(_ownerForm, confirmMessage) != DialogResult.OK)
                    return;

                // 从大到小删除,避免索引变化
                var sortedIndices = selectedIndices.OrderByDescending(i => i).ToArray();

                foreach (int index in sortedIndices)
                {
                    _workflowState.RemoveStepAt(index);
                }

                _logger.LogInformation("已删除 {Count} 个步骤", selectedIndices.Length);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除步骤时发生错误");
                MessageHelper.MessageOK($"删除失败: {ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 移动功能

        private enum MoveDirection { Up, Down }

        private void MoveStep(MoveDirection direction)
        {
            try
            {
                int selectedIndex = _gridManager.GetSelectedRowIndex();
                if (selectedIndex < 0)
                {
                    MessageHelper.MessageOK("请先选择要移动的步骤！", TType.Warn);
                    return;
                }

                var steps = _workflowState.GetSteps();
                int targetIndex;

                if (direction == MoveDirection.Up)
                {
                    if (selectedIndex == 0)
                    {
                        MessageHelper.MessageOK("已经是第一个步骤！", TType.Warn);
                        return;
                    }
                    targetIndex = selectedIndex - 1;
                }
                else
                {
                    if (selectedIndex >= steps.Count - 1)
                    {
                        MessageHelper.MessageOK("已经是最后一个步骤！", TType.Warn);
                        return;
                    }
                    targetIndex = selectedIndex + 1;
                }

                // 交换
                (steps[targetIndex], steps[selectedIndex]) = (steps[selectedIndex], steps[targetIndex]);

                // 重新编号
                for (int i = 0; i < steps.Count; i++)
                {
                    steps[i].StepNum = i + 1;
                }

                // 刷新
                _workflowState.ClearSteps();
                foreach (var step in steps)
                {
                    _workflowState.AddStep(step);
                }

                // 保持选中
                if (targetIndex < _grid.Rows.Count)
                {
                    _grid.ClearSelection();
                    _grid.Rows[targetIndex].Selected = true;
                }

                _logger.LogInformation("步骤移动成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "移动步骤时发生错误");
                MessageHelper.MessageOK($"移动步骤错误：{ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 复制/剪切/粘贴功能

        /// <summary>
        /// 复制选中的步骤(支持多选)
        /// </summary>
        private void CopyStep()
        {
            try
            {
                var selectedIndices = _gridManager.GetSelectedRowIndices();
                if (selectedIndices.Length == 0)
                {
                    MessageHelper.MessageOK("请先选择要复制的步骤!", TType.Warn);
                    return;
                }

                _copiedSteps = [];

                foreach (int index in selectedIndices)
                {
                    var step = _workflowState.GetStep(index);
                    if (step != null)
                    {
                        _copiedSteps.Add(DeepCloneStep(step));
                    }
                }

                _isCut = false;

                string message = selectedIndices.Length == 1
                    ? $"已复制步骤: {_copiedSteps[0].StepName}"
                    : $"已复制 {selectedIndices.Length} 个步骤";

                _logger.LogDebug(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "复制步骤时发生错误");
                MessageHelper.MessageOK($"复制失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 剪切选中的步骤(支持多选)
        /// </summary>
        private void CutStep()
        {
            try
            {
                var selectedIndices = _gridManager.GetSelectedRowIndices();
                if (selectedIndices.Length == 0)
                {
                    MessageHelper.MessageOK("请先选择要剪切的步骤!", TType.Warn);
                    return;
                }

                _copiedSteps = new List<ChildModel>();

                // 先复制所有选中的步骤
                foreach (int index in selectedIndices)
                {
                    var step = _workflowState.GetStep(index);
                    if (step != null)
                    {
                        _copiedSteps.Add(DeepCloneStep(step));
                    }
                }

                // 从大到小删除,避免索引变化
                var sortedIndices = selectedIndices.OrderByDescending(i => i).ToArray();
                foreach (int index in sortedIndices)
                {
                    _workflowState.RemoveStepAt(index);
                }

                _isCut = true;

                string message = selectedIndices.Length == 1
                    ? $"已剪切步骤: {_copiedSteps[0].StepName}"
                    : $"已剪切 {selectedIndices.Length} 个步骤";

                _logger.LogDebug(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "剪切步骤时发生错误");
                MessageHelper.MessageOK($"剪切失败: {ex.Message}", TType.Error);
            }
        }

        /// <summary>
        /// 粘贴步骤(支持多个步骤)
        /// </summary>
        private void PasteStep()
        {
            try
            {
                if (_copiedSteps == null || _copiedSteps.Count == 0)
                {
                    MessageHelper.MessageOK("没有可粘贴的步骤!", TType.Warn);
                    return;
                }

                int selectedIndex = _gridManager.GetSelectedRowIndex();

                // 获取当前所有步骤
                var allSteps = _workflowState.GetSteps();

                // 确定插入位置
                int insertIndex = selectedIndex >= 0
                    ? selectedIndex + 1
                    : allSteps.Count;

                // 在指定位置插入所有复制的步骤(再次深拷贝,确保多次粘贴互不影响)
                foreach (var copiedStep in _copiedSteps)
                {
                    var newStep = DeepCloneStep(copiedStep);
                    allSteps.Insert(insertIndex++, newStep);
                }

                // 清空现有步骤并重新添加
                _workflowState.ClearSteps();
                foreach (var step in allSteps)
                {
                    // 重新分配步骤号
                    step.StepNum = allSteps.IndexOf(step) + 1;
                    _workflowState.AddStep(step);
                }

                // 如果是剪切操作,粘贴后清空复制内容
                if (_isCut)
                {
                    _copiedSteps = null;
                    _isCut = false;
                }

                string message = _copiedSteps?.Count == 1
                    ? $"已粘贴步骤"
                    : $"已粘贴 {_copiedSteps?.Count ?? 0} 个步骤";

                _logger.LogDebug(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "粘贴步骤时发生错误");
                MessageHelper.MessageOK($"粘贴失败: {ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 全选和清空

        private void ClearAllSteps()
        {
            try
            {
                if (_grid.Rows.Count == 0) return;

                if (MessageHelper.MessageYes(_ownerForm,
                    $"确定要清空所有 {_grid.Rows.Count} 个步骤吗？\n此操作不可撤销！") != DialogResult.OK)
                {
                    return;
                }

                // 只操作数据层，UI刷新由事件自动触发
                _workflowState.ClearSteps();

                _logger.LogInformation("所有步骤已清空");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "清空步骤时发生错误");
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 显示步骤选择对话框
        /// 修改内容:
        /// 1. 按钮不填充,左右摆放
        /// 2. 支持双击列表项确认
        /// 3. 支持ESC键退出
        /// 4. 工具名称与ToolTreeViewControl保持一致
        /// </summary>
        private string ShowStepSelectionDialog()
        {
            var form = new UIForm
            {
                Text = "选择步骤类型",
                Font = new Font("微软雅黑", 12F, FontStyle.Bold),
                Width = 400,
                Height = 500,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ControlBox = false,
                ShowIcon = false,
                TitleColor = Color.FromArgb(65, 100, 204),
                RectColor = Color.FromArgb(65, 100, 204),
                KeyPreview = true  // 启用键盘预览,用于捕获ESC键
            };

            var listBox = new UIListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("微软雅黑", 12F),
                Height = 35,
            };

            // 加载所有步骤类型 - 与ToolTreeViewControl中的工具名称保持一致
            var stepTypes = new[]
            {
                "延时等待",
                "条件判断",
                "等待稳定",
                "实时监控",
                "循环工具",
                "变量赋值",
                "消息通知",
                "读取PLC",
                "写入PLC",
                "读取单元格",
                "写入单元格"
            };

            listBox.Items.AddRange(stepTypes);

            // 创建底部按钮面板
            var buttonPanel = new System.Windows.Forms.Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                Padding = new Padding(10, 5, 10, 5)
            };

            // 确定按钮
            var btnOK = new UIButton
            {
                Text = "确定",
                Width = 100,
                Height = 40,
                Location = new Point(buttonPanel.Width - 220, 5),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Font = new Font("微软雅黑", 11F)
            };

            // 取消按钮
            var btnCancel = new UIButton
            {
                Text = "取消",
                Width = 100,
                Height = 40,
                Location = new Point(buttonPanel.Width - 110, 5),
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Font = new Font("微软雅黑", 11F)
            };

            // 确定按钮点击事件
            btnOK.Click += (s, e) =>
            {
                if (listBox.SelectedItem != null)
                {
                    form.DialogResult = DialogResult.OK;
                    form.Close();
                }
            };

            // 取消按钮点击事件
            btnCancel.Click += (s, e) =>
            {
                form.DialogResult = DialogResult.Cancel;
                form.Close();
            };

            // 双击列表项确认
            listBox.DoubleClick += (s, e) =>
            {
                if (listBox.SelectedItem != null)
                {
                    form.DialogResult = DialogResult.OK;
                    form.Close();
                }
            };

            // ESC键退出
            form.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    form.DialogResult = DialogResult.Cancel;
                    form.Close();
                }
            };

            // 组装控件
            buttonPanel.Controls.Add(btnOK);
            buttonPanel.Controls.Add(btnCancel);

            form.Controls.Add(listBox);
            form.Controls.Add(buttonPanel);

            return VarHelper.ShowDialogWithOverlayEx(_ownerForm, form) == DialogResult.OK
                ? listBox.SelectedItem?.ToString()
                : null;
        }

        /// <summary>
        /// 处理快捷键 - 从外部调用
        /// </summary>
        public bool HandleKeyDown(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Delete:
                    Delete();
                    return true;
                case Keys.Control | Keys.C:
                    Copy();
                    return true;
                case Keys.Control | Keys.X:
                    Cut();
                    return true;
                case Keys.Control | Keys.V:
                    Paste();
                    return true;
                case Keys.Control | Keys.Up:
                    MoveUp();
                    return true;
                case Keys.Control | Keys.Down:
                    MoveDown();
                    return true;
                case Keys.Control | Keys.I:
                    InsertBefore();
                    return true;
                case Keys.Control | Keys.Shift | Keys.I:
                    InsertAfter();
                    return true;
                case Keys.Control | Keys.A:
                    SelectAll();
                    return true;
            }
            return false;
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            _contextMenu?.Dispose();
        }

        #endregion
    }
}