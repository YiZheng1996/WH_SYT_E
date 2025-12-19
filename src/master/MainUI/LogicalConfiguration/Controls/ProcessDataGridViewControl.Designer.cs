using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Controls
{
    partial class ProcessDataGridViewControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源,为 true;否则为 false。</param>
        /// <summary>
        /// 释放资源
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing && (components != null))
                {
                    try
                    {
                        // 取消事件订阅
                        UnsubscribeFromWorkflowEvents();

                        // 释放菜单管理器
                        _menuManager?.Dispose();
                        _menuManager = null;

                        // 释放DataGridView事件
                        if (_dataGridView != null)
                        {
                            _dataGridView.CellDoubleClick -= DataGridView_CellDoubleClick;
                            _dataGridView.DragEnter -= DataGridView_DragEnter;
                            _dataGridView.DragDrop -= DataGridView_DragDrop;
                            _dataGridView.SelectionChanged -= DataGridView_SelectionChanged;
                            _dataGridView.SelectionChanged -= UpdateButtonStates;
                            _dataGridView.CellBeginEdit -= DataGridView_CellBeginEdit;
                            _dataGridView.CellEndEdit -= DataGridView_CellEndEdit;
                            _dataGridView.KeyDown -= DataGridView_KeyDown;
                        }

                        _logger?.LogDebug("流程配置表格控件资源已释放");
                    }
                    catch (Exception ex)
                    {
                        _logger?.LogError(ex, "释放资源时发生错误");
                    }
                    finally
                    {
                        components.Dispose();
                    }
                }

                _isDisposed = true;
            }

            base.Dispose(disposing);
        }


        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            panelToolbar = new Panel();
            btnClearAll = new AntdUI.Button();
            btnSelectAll = new AntdUI.Button();
            btnPaste = new AntdUI.Button();
            btnCut = new AntdUI.Button();
            btnCopy = new AntdUI.Button();
            btnMoveDown = new AntdUI.Button();
            btnMoveUp = new AntdUI.Button();
            btnDelete = new AntdUI.Button();
            btnInsertAfter = new AntdUI.Button();
            btnInsertBefore = new AntdUI.Button();
            panelToolbar.SuspendLayout();
            SuspendLayout();
            // 
            // panelToolbar
            // 
            panelToolbar.BackColor = Color.White;
            panelToolbar.Controls.Add(btnClearAll);
            panelToolbar.Controls.Add(btnSelectAll);
            panelToolbar.Controls.Add(btnPaste);
            panelToolbar.Controls.Add(btnCut);
            panelToolbar.Controls.Add(btnCopy);
            panelToolbar.Controls.Add(btnMoveDown);
            panelToolbar.Controls.Add(btnMoveUp);
            panelToolbar.Controls.Add(btnDelete);
            panelToolbar.Controls.Add(btnInsertAfter);
            panelToolbar.Controls.Add(btnInsertBefore);
            panelToolbar.Dock = DockStyle.Bottom;
            panelToolbar.Location = new Point(0, 550);
            panelToolbar.Name = "panelToolbar";
            panelToolbar.Padding = new Padding(8);
            panelToolbar.Size = new Size(1000, 50);
            panelToolbar.TabIndex = 0;
            // 
            // btnClearAll
            // 
            btnClearAll.Font = new Font("微软雅黑", 9F);
            btnClearAll.Location = new Point(890, 8);
            btnClearAll.Name = "btnClearAll";
            btnClearAll.Size = new Size(100, 34);
            btnClearAll.TabIndex = 9;
            btnClearAll.Text = "清空所有";
            btnClearAll.Type = AntdUI.TTypeMini.Warn;
            btnClearAll.Click += BtnClearAll_Click;
            // 
            // btnSelectAll
            // 
            btnSelectAll.Font = new Font("微软雅黑", 9F);
            btnSelectAll.Location = new Point(792, 8);
            btnSelectAll.Name = "btnSelectAll";
            btnSelectAll.Size = new Size(90, 34);
            btnSelectAll.TabIndex = 8;
            btnSelectAll.Text = "全选 (Ctrl+A)";
            btnSelectAll.Type = AntdUI.TTypeMini.Success;
            btnSelectAll.Click += BtnSelectAll_Click;
            // 
            // btnPaste
            // 
            btnPaste.Font = new Font("微软雅黑", 9F);
            btnPaste.Location = new Point(694, 8);
            btnPaste.Name = "btnPaste";
            btnPaste.Size = new Size(90, 34);
            btnPaste.TabIndex = 7;
            btnPaste.Text = "粘贴 (Ctrl+V)";
            btnPaste.Type = AntdUI.TTypeMini.Success;
            btnPaste.Click += BtnPaste_Click;
            // 
            // btnCut
            // 
            btnCut.Font = new Font("微软雅黑", 9F);
            btnCut.Location = new Point(596, 8);
            btnCut.Name = "btnCut";
            btnCut.Size = new Size(90, 34);
            btnCut.TabIndex = 6;
            btnCut.Text = "剪切 (Ctrl+X)";
            btnCut.Type = AntdUI.TTypeMini.Success;
            btnCut.Click += BtnCut_Click;
            // 
            // btnCopy
            // 
            btnCopy.Font = new Font("微软雅黑", 9F);
            btnCopy.Location = new Point(498, 8);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(90, 34);
            btnCopy.TabIndex = 5;
            btnCopy.Text = "复制 (Ctrl+C)";
            btnCopy.Type = AntdUI.TTypeMini.Success;
            btnCopy.Click += BtnCopy_Click;
            // 
            // btnMoveDown
            // 
            btnMoveDown.Font = new Font("微软雅黑", 9F);
            btnMoveDown.Location = new Point(400, 8);
            btnMoveDown.Name = "btnMoveDown";
            btnMoveDown.Size = new Size(90, 34);
            btnMoveDown.TabIndex = 4;
            btnMoveDown.Text = "下移 ( (Ctrl+↓)";
            btnMoveDown.Type = AntdUI.TTypeMini.Success;
            btnMoveDown.Click += BtnMoveDown_Click;
            // 
            // btnMoveUp
            // 
            btnMoveUp.Font = new Font("微软雅黑", 9F);
            btnMoveUp.Location = new Point(302, 8);
            btnMoveUp.Name = "btnMoveUp";
            btnMoveUp.Size = new Size(90, 34);
            btnMoveUp.TabIndex = 3;
            btnMoveUp.Text = "上移 (Ctrl+↑)";
            btnMoveUp.Type = AntdUI.TTypeMini.Success;
            btnMoveUp.Click += BtnMoveUp_Click;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("微软雅黑", 9F);
            btnDelete.Location = new Point(204, 8);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(90, 34);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "删除 (Del)";
            btnDelete.Type = AntdUI.TTypeMini.Error;
            btnDelete.Click += BtnDelete_Click;
            // 
            // btnInsertAfter
            // 
            btnInsertAfter.Font = new Font("微软雅黑", 9F);
            btnInsertAfter.Location = new Point(106, 8);
            btnInsertAfter.Name = "btnInsertAfter";
            btnInsertAfter.Size = new Size(90, 34);
            btnInsertAfter.TabIndex = 1;
            btnInsertAfter.Text = "后插 (Ctrl+U)";
            btnInsertAfter.Type = AntdUI.TTypeMini.Primary;
            btnInsertAfter.Click += BtnInsertAfter_Click;
            // 
            // btnInsertBefore
            // 
            btnInsertBefore.Font = new Font("微软雅黑", 9F);
            btnInsertBefore.Location = new Point(8, 8);
            btnInsertBefore.Name = "btnInsertBefore";
            btnInsertBefore.Size = new Size(90, 34);
            btnInsertBefore.TabIndex = 0;
            btnInsertBefore.Text = "前插 (Ctrl+I)";
            btnInsertBefore.Type = AntdUI.TTypeMini.Primary;
            btnInsertBefore.Click += BtnInsertBefore_Click;
            // 
            // ProcessDataGridViewControl
            // 
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(panelToolbar);
            Name = "ProcessDataGridViewControl";
            Size = new Size(1000, 600);
            panelToolbar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelToolbar;
        private AntdUI.Button btnInsertBefore;
        private AntdUI.Button btnInsertAfter;
        private AntdUI.Button btnDelete;
        private AntdUI.Button btnMoveUp;
        private AntdUI.Button btnMoveDown;
        private AntdUI.Button btnCopy;
        private AntdUI.Button btnCut;
        private AntdUI.Button btnPaste;
        private AntdUI.Button btnSelectAll;
        private AntdUI.Button btnClearAll;
    }
}