using AntdUI;
using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.Logging;
using Button = System.Windows.Forms.Button;
using ContextMenuStrip = System.Windows.Forms.ContextMenuStrip;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using Panel = System.Windows.Forms.Panel;

namespace MainUI.LogicalConfiguration.LogicalManager
{
    /// <summary>
    /// 步骤右键菜单管理器 - 负责所有右键菜单相关的功能
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

        // 复制/剪切的步骤
        private ChildModel _copiedStep;
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
                _menuItemInsertAfter = CreateMenuItem("在此之后插入 (&A)", OnInsertAfter, Keys.Control | Keys.Shift | Keys.I);
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
                int selectedIndex = _gridManager.GetSelectedRowIndex();
                int totalRows = _grid.Rows.Count;
                bool hasSelection = selectedIndex >= 0;
                bool hasCopiedData = _copiedStep != null;

                _menuItemInsertBefore.Enabled = hasSelection;
                _menuItemInsertAfter.Enabled = hasSelection;
                _menuItemDelete.Enabled = hasSelection;
                _menuItemMoveUp.Enabled = hasSelection && selectedIndex > 0;
                _menuItemMoveDown.Enabled = hasSelection && selectedIndex < totalRows - 1;
                _menuItemCopy.Enabled = hasSelection;
                _menuItemCut.Enabled = hasSelection;
                _menuItemPaste.Enabled = hasCopiedData;
                _menuItemSelectAll.Enabled = totalRows > 0;
                _menuItemClearAll.Enabled = totalRows > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理菜单打开事件时发生错误");
            }
        }

        #endregion

        #region 插入功能

        private void OnInsertBefore(object sender, EventArgs e)
        {
            InsertStep(InsertPosition.Before);
        }

        private void OnInsertAfter(object sender, EventArgs e)
        {
            InsertStep(InsertPosition.After);
        }

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

                int insertIndex = position == InsertPosition.Before ? selectedIndex : selectedIndex + 1;
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

        private void OnDelete(object sender, EventArgs e)
        {
            try
            {
                int selectIndex = _gridManager.GetSelectedRowIndex();
                if (selectIndex < 0)
                {
                    MessageHelper.MessageOK("请先选择要删除的步骤！", TType.Warn);
                    return;
                }

                var selectedStep = _workflowState.GetStep(selectIndex);
                string stepName = selectedStep?.StepName ?? "未知步骤";

                if (MessageHelper.MessageYes(_ownerForm, $"确定要删除步骤 [{stepName}] 吗？") != DialogResult.OK)
                {
                    return;
                }

                // 只操作数据层，UI刷新由 OnStepRemoved 事件自动触发
                if (_workflowState.RemoveStepAt(selectIndex))
                {
                    _logger.LogInformation("步骤删除成功: {StepName}", stepName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除步骤时发生错误");
                MessageHelper.MessageOK($"删除步骤错误：{ex.Message}", TType.Error);
            }
        }

        #endregion

        #region 移动功能

        private void OnMoveUp(object sender, EventArgs e)
        {
            MoveStep(MoveDirection.Up);
        }

        private void OnMoveDown(object sender, EventArgs e)
        {
            MoveStep(MoveDirection.Down);
        }

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

        private void OnCopy(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = _gridManager.GetSelectedRowIndex();
                if (selectedIndex < 0)
                {
                    MessageHelper.MessageOK("请先选择要复制的步骤！", TType.Warn);
                    return;
                }

                var step = _workflowState.GetStep(selectedIndex);
                if (step == null) return;

                _copiedStep = new ChildModel
                {
                    StepName = step.StepName,
                    Status = 0,
                    StepNum = 0,
                    StepParameter = step.StepParameter
                };

                _isCut = false;
                _logger.LogDebug("复制步骤: {StepName}", step.StepName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "复制步骤时发生错误");
            }
        }

        private void OnCut(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = _gridManager.GetSelectedRowIndex();
                if (selectedIndex < 0)
                {
                    MessageHelper.MessageOK("请先选择要剪切的步骤！", TType.Warn);
                    return;
                }

                var step = _workflowState.GetStep(selectedIndex);
                if (step == null) return;

                _copiedStep = new ChildModel
                {
                    StepName = step.StepName,
                    Status = 0,
                    StepNum = 0,
                    StepParameter = step.StepParameter
                };

                _isCut = true;
                _workflowState.RemoveStepAt(selectedIndex);

                _logger.LogDebug("剪切步骤: {StepName}", step.StepName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "剪切步骤时发生错误");
            }
        }

        private void OnPaste(object sender, EventArgs e)
        {
            try
            {
                if (_copiedStep == null)
                {
                    MessageHelper.MessageOK("没有可粘贴的步骤！", TType.Warn);
                    return;
                }

                int selectedIndex = _gridManager.GetSelectedRowIndex();
                int insertIndex = selectedIndex >= 0 ? selectedIndex + 1 : _grid.Rows.Count;

                var steps = _workflowState.GetSteps();

                var newStep = new ChildModel
                {
                    StepName = _copiedStep.StepName,
                    Status = 0,
                    StepNum = insertIndex + 1,
                    StepParameter = _copiedStep.StepParameter
                };

                steps.Insert(insertIndex, newStep);

                // 重新编号
                for (int i = 0; i < steps.Count; i++)
                {
                    steps[i].StepNum = i + 1;
                }

                _workflowState.ClearSteps();
                foreach (var step in steps)
                {
                    _workflowState.AddStep(step);
                }

                if (_isCut)
                {
                    _copiedStep = null;
                    _isCut = false;
                }

                if (insertIndex < _grid.Rows.Count)
                {
                    _grid.ClearSelection();
                    _grid.Rows[insertIndex].Selected = true;
                }

                _logger.LogInformation("粘贴步骤成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "粘贴步骤时发生错误");
            }
        }

        #endregion

        #region 全选和清空

        private void OnSelectAll(object sender, EventArgs e)
        {
            _grid.SelectAll();
        }

        private void OnClearAll(object sender, EventArgs e)
        {
            try
            {
                if (_grid.Rows.Count == 0) return;

                if (MessageHelper.MessageYes(_ownerForm,
                    $"确定要清空所有 {_grid.Rows.Count} 个步骤吗？\n此操作不可撤销！") != DialogResult.OK)
                {
                    return;
                }

                // 只操作数据层，UI刷新由 OnStepsChanged 事件自动触发
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
        /// </summary>
        private string ShowStepSelectionDialog()
        {
            using var form = new Form
            {
                Text = "选择步骤类型",
                Width = 400,
                Height = 500,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var listBox = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("微软雅黑", 12F)
            };

            listBox.Items.AddRange(
            [
                "延时等待", "条件判断", "循环开始", "循环结束",
                "变量赋值", "数据读取", "数据计算", "消息通知",
                "读取PLC", "写入PLC"
            ]);

            var btnPanel = new Panel { Dock = DockStyle.Bottom, Height = 50 };
            var btnOK = new Button
            {
                Text = "确定",
                DialogResult = DialogResult.OK,
                Width = 80,
                Height = 35,
                Location = new Point(150, 7)
            };
            var btnCancel = new Button
            {
                Text = "取消",
                DialogResult = DialogResult.Cancel,
                Width = 80,
                Height = 35,
                Location = new Point(240, 7)
            };

            btnPanel.Controls.AddRange(new Control[] { btnOK, btnCancel });
            form.Controls.Add(listBox);
            form.Controls.Add(btnPanel);

            form.AcceptButton = btnOK;
            form.CancelButton = btnCancel;

            listBox.DoubleClick += (s, e) =>
            {
                if (listBox.SelectedItem != null)
                {
                    form.DialogResult = DialogResult.OK;
                    form.Close();
                }
            };

            return form.ShowDialog(_ownerForm) == DialogResult.OK && listBox.SelectedItem != null
                ? listBox.SelectedItem.ToString()
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
                    OnDelete(null, EventArgs.Empty);
                    return true;
                case Keys.Control | Keys.C:
                    OnCopy(null, EventArgs.Empty);
                    return true;
                case Keys.Control | Keys.X:
                    OnCut(null, EventArgs.Empty);
                    return true;
                case Keys.Control | Keys.V:
                    OnPaste(null, EventArgs.Empty);
                    return true;
                case Keys.Control | Keys.Up:
                    OnMoveUp(null, EventArgs.Empty);
                    return true;
                case Keys.Control | Keys.Down:
                    OnMoveDown(null, EventArgs.Empty);
                    return true;
                case Keys.Control | Keys.I:
                    OnInsertBefore(null, EventArgs.Empty);
                    return true;
                case Keys.Control | Keys.Shift | Keys.I:
                    OnInsertAfter(null, EventArgs.Empty);
                    return true;
                case Keys.Control | Keys.A:
                    OnSelectAll(null, EventArgs.Empty);
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