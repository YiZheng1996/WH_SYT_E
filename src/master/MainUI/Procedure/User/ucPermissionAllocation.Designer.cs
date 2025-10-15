using Padding = System.Windows.Forms.Padding;

namespace MainUI.Procedure.User
{
    partial class ucPermissionAllocation
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            grpUser = new UIGroupBox();
            PanelPermissions = new FlowLayoutPanel();
            uiLine1 = new UILine();
            uiLabel2 = new UILabel();
            cboRole = new UIComboBox();
            btnEdit = new UIButton();
            grpUser.SuspendLayout();
            SuspendLayout();
            // 
            // grpUser
            // 
            grpUser.BackColor = Color.FromArgb(236, 236, 237);
            grpUser.Controls.Add(PanelPermissions);
            grpUser.Controls.Add(uiLine1);
            grpUser.Controls.Add(uiLabel2);
            grpUser.Controls.Add(cboRole);
            grpUser.Controls.Add(btnEdit);
            grpUser.FillColor = Color.FromArgb(236, 236, 237);
            grpUser.FillColor2 = Color.FromArgb(236, 236, 237);
            grpUser.FillDisableColor = Color.FromArgb(42, 47, 55);
            grpUser.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            grpUser.ForeColor = Color.Black;
            grpUser.ForeDisableColor = Color.Black;
            grpUser.Location = new Point(0, 2);
            grpUser.Margin = new Padding(4, 5, 4, 5);
            grpUser.MinimumSize = new Size(1, 1);
            grpUser.Name = "grpUser";
            grpUser.Padding = new Padding(0, 32, 0, 0);
            grpUser.RectColor = Color.FromArgb(236, 236, 237);
            grpUser.RectDisableColor = Color.FromArgb(236, 236, 237);
            grpUser.Size = new Size(792, 787);
            grpUser.TabIndex = 399;
            grpUser.Text = "权限分配";
            grpUser.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // PanelPermissions
            // 
            PanelPermissions.BackColor = Color.White;
            PanelPermissions.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            PanelPermissions.Location = new Point(0, 98);
            PanelPermissions.Margin = new Padding(4, 5, 4, 5);
            PanelPermissions.MinimumSize = new Size(1, 1);
            PanelPermissions.Name = "PanelPermissions";
            PanelPermissions.Padding = new Padding(2);
            PanelPermissions.Size = new Size(792, 639);
            PanelPermissions.TabIndex = 15;
            PanelPermissions.Text = "uiFlowLayoutPanel1";
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Transparent;
            uiLine1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiLine1.ForeColor = Color.White;
            uiLine1.LineColor = Color.White;
            uiLine1.LineColor2 = Color.White;
            uiLine1.Location = new Point(5, 25);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(787, 29);
            uiLine1.StartCap = UILineCap.Circle;
            uiLine1.TabIndex = 410;
            // 
            // uiLabel2
            // 
            uiLabel2.AutoSize = true;
            uiLabel2.Font = new Font("微软雅黑", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 134);
            uiLabel2.ForeColor = Color.Black;
            uiLabel2.Location = new Point(264, 58);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(44, 24);
            uiLabel2.TabIndex = 397;
            uiLabel2.Text = "角色";
            uiLabel2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboRole
            // 
            cboRole.BackColor = Color.Transparent;
            cboRole.DataSource = null;
            cboRole.DropDownStyle = UIDropDownStyle.DropDownList;
            cboRole.FillColor = Color.White;
            cboRole.FillColor2 = Color.White;
            cboRole.FillDisableColor = Color.White;
            cboRole.FilterMaxCount = 50;
            cboRole.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            cboRole.ForeColor = Color.Black;
            cboRole.ForeDisableColor = Color.Black;
            cboRole.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboRole.Items.AddRange(new object[] { "系统管理员" });
            cboRole.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboRole.Location = new Point(315, 56);
            cboRole.Margin = new Padding(4, 5, 4, 5);
            cboRole.MinimumSize = new Size(63, 0);
            cboRole.Name = "cboRole";
            cboRole.Padding = new Padding(0, 0, 30, 2);
            cboRole.Radius = 10;
            cboRole.RectColor = Color.Gray;
            cboRole.RectDisableColor = Color.Gray;
            cboRole.RectSides = ToolStripStatusLabelBorderSides.None;
            cboRole.Size = new Size(213, 29);
            cboRole.SymbolSize = 24;
            cboRole.TabIndex = 396;
            cboRole.TextAlignment = ContentAlignment.MiddleLeft;
            cboRole.Watermark = "请选择";
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.Transparent;
            btnEdit.Cursor = Cursors.Hand;
            btnEdit.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            btnEdit.Location = new Point(642, 745);
            btnEdit.MinimumSize = new Size(1, 1);
            btnEdit.Name = "btnEdit";
            btnEdit.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnEdit.Size = new Size(147, 37);
            btnEdit.TabIndex = 395;
            btnEdit.Text = "保存";
            btnEdit.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnEdit.TipsText = "1";
            btnEdit.Click += btnEdit_Click;
            // 
            // ucPermissionAllocation
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 236, 237);
            Controls.Add(grpUser);
            Name = "ucPermissionAllocation";
            Size = new Size(792, 787);
            Load += ucPermissionAllocation_Load;
            grpUser.ResumeLayout(false);
            grpUser.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private UIGroupBox grpUser;
        private UIButton btnEdit;
        private UIComboBox cboRole;
        private UILabel uiLabel2;
        private UILine uiLine1;
        private FlowLayoutPanel PanelPermissions;
    }
}
