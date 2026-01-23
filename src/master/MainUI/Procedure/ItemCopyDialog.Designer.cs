namespace MainUI.Procedure
{
    partial class ItemCopyDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblSourceTitle = new UILabel();
            lblSourceType = new UILabel();
            cboSourceType = new UIComboBox();
            lblSourceModel = new UILabel();
            cboSourceModel = new UIComboBox();
            lblSourceItem = new UILabel();
            cboSourceItem = new UIComboBox();
            lblTargetTitle = new UILabel();
            lblTargetType = new UILabel();
            cboTargetType = new UIComboBox();
            lblTargetModel = new UILabel();
            cboTargetModel = new UIComboBox();
            lblTargetItem = new UILabel();
            cboTargetItem = new UIComboBox();
            btnOK = new UIButton();
            btnCancel = new UIButton();
            SuspendLayout();
            // 
            // lblSourceTitle
            // 
            lblSourceTitle.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            lblSourceTitle.ForeColor = Color.FromArgb(64, 158, 255);
            lblSourceTitle.Location = new Point(30, 80);
            lblSourceTitle.Name = "lblSourceTitle";
            lblSourceTitle.Size = new Size(300, 25);
            lblSourceTitle.TabIndex = 0;
            lblSourceTitle.Text = "【源】选择要复制的测试项";
            // 
            // lblSourceType
            // 
            lblSourceType.AutoSize = true;
            lblSourceType.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblSourceType.ForeColor = Color.FromArgb(48, 48, 48);
            lblSourceType.Location = new Point(30, 137);
            lblSourceType.Name = "lblSourceType";
            lblSourceType.Size = new Size(79, 16);
            lblSourceType.TabIndex = 1;
            lblSourceType.Text = "产品类型:";
            // 
            // cboSourceType
            // 
            cboSourceType.DataSource = null;
            cboSourceType.DropDownStyle = UIDropDownStyle.DropDownList;
            cboSourceType.FillColor = Color.White;
            cboSourceType.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cboSourceType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboSourceType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboSourceType.Location = new Point(117, 130);
            cboSourceType.Margin = new Padding(4, 5, 4, 5);
            cboSourceType.MinimumSize = new Size(63, 0);
            cboSourceType.Name = "cboSourceType";
            cboSourceType.Padding = new Padding(0, 0, 30, 2);
            cboSourceType.Size = new Size(250, 30);
            cboSourceType.SymbolSize = 24;
            cboSourceType.TabIndex = 2;
            cboSourceType.TextAlignment = ContentAlignment.MiddleLeft;
            cboSourceType.Watermark = "";
            cboSourceType.SelectedIndexChanged += CboSourceType_SelectedIndexChanged;
            // 
            // lblSourceModel
            // 
            lblSourceModel.AutoSize = true;
            lblSourceModel.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblSourceModel.ForeColor = Color.FromArgb(48, 48, 48);
            lblSourceModel.Location = new Point(30, 197);
            lblSourceModel.Name = "lblSourceModel";
            lblSourceModel.Size = new Size(79, 16);
            lblSourceModel.TabIndex = 3;
            lblSourceModel.Text = "产品型号:";
            // 
            // cboSourceModel
            // 
            cboSourceModel.DataSource = null;
            cboSourceModel.DropDownStyle = UIDropDownStyle.DropDownList;
            cboSourceModel.FillColor = Color.White;
            cboSourceModel.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cboSourceModel.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboSourceModel.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboSourceModel.Location = new Point(117, 191);
            cboSourceModel.Margin = new Padding(4, 5, 4, 5);
            cboSourceModel.MinimumSize = new Size(63, 0);
            cboSourceModel.Name = "cboSourceModel";
            cboSourceModel.Padding = new Padding(0, 0, 30, 2);
            cboSourceModel.Size = new Size(250, 30);
            cboSourceModel.SymbolSize = 24;
            cboSourceModel.TabIndex = 4;
            cboSourceModel.TextAlignment = ContentAlignment.MiddleLeft;
            cboSourceModel.Watermark = "";
            cboSourceModel.SelectedIndexChanged += CboSourceModel_SelectedIndexChanged;
            // 
            // lblSourceItem
            // 
            lblSourceItem.AutoSize = true;
            lblSourceItem.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblSourceItem.ForeColor = Color.FromArgb(48, 48, 48);
            lblSourceItem.Location = new Point(46, 261);
            lblSourceItem.Name = "lblSourceItem";
            lblSourceItem.Size = new Size(63, 16);
            lblSourceItem.TabIndex = 5;
            lblSourceItem.Text = "测试项:";
            // 
            // cboSourceItem
            // 
            cboSourceItem.DataSource = null;
            cboSourceItem.DropDownStyle = UIDropDownStyle.DropDownList;
            cboSourceItem.FillColor = Color.White;
            cboSourceItem.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cboSourceItem.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboSourceItem.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboSourceItem.Location = new Point(117, 255);
            cboSourceItem.Margin = new Padding(4, 5, 4, 5);
            cboSourceItem.MinimumSize = new Size(63, 0);
            cboSourceItem.Name = "cboSourceItem";
            cboSourceItem.Padding = new Padding(0, 0, 30, 2);
            cboSourceItem.Size = new Size(250, 30);
            cboSourceItem.SymbolSize = 24;
            cboSourceItem.TabIndex = 6;
            cboSourceItem.TextAlignment = ContentAlignment.MiddleLeft;
            cboSourceItem.Watermark = "";
            // 
            // lblTargetTitle
            // 
            lblTargetTitle.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            lblTargetTitle.ForeColor = Color.FromArgb(230, 80, 80);
            lblTargetTitle.Location = new Point(420, 80);
            lblTargetTitle.Name = "lblTargetTitle";
            lblTargetTitle.Size = new Size(300, 25);
            lblTargetTitle.TabIndex = 7;
            lblTargetTitle.Text = "【目标】选择要覆盖的测试项";
            // 
            // lblTargetType
            // 
            lblTargetType.AutoSize = true;
            lblTargetType.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblTargetType.ForeColor = Color.FromArgb(48, 48, 48);
            lblTargetType.Location = new Point(420, 137);
            lblTargetType.Name = "lblTargetType";
            lblTargetType.Size = new Size(79, 16);
            lblTargetType.TabIndex = 8;
            lblTargetType.Text = "产品类型:";
            // 
            // cboTargetType
            // 
            cboTargetType.DataSource = null;
            cboTargetType.DropDownStyle = UIDropDownStyle.DropDownList;
            cboTargetType.FillColor = Color.White;
            cboTargetType.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cboTargetType.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboTargetType.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboTargetType.Location = new Point(502, 130);
            cboTargetType.Margin = new Padding(4, 5, 4, 5);
            cboTargetType.MinimumSize = new Size(63, 0);
            cboTargetType.Name = "cboTargetType";
            cboTargetType.Padding = new Padding(0, 0, 30, 2);
            cboTargetType.Size = new Size(250, 30);
            cboTargetType.SymbolSize = 24;
            cboTargetType.TabIndex = 9;
            cboTargetType.TextAlignment = ContentAlignment.MiddleLeft;
            cboTargetType.Watermark = "";
            cboTargetType.SelectedIndexChanged += CboTargetType_SelectedIndexChanged;
            // 
            // lblTargetModel
            // 
            lblTargetModel.AutoSize = true;
            lblTargetModel.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblTargetModel.ForeColor = Color.FromArgb(48, 48, 48);
            lblTargetModel.Location = new Point(420, 197);
            lblTargetModel.Name = "lblTargetModel";
            lblTargetModel.Size = new Size(79, 16);
            lblTargetModel.TabIndex = 10;
            lblTargetModel.Text = "产品型号:";
            // 
            // cboTargetModel
            // 
            cboTargetModel.DataSource = null;
            cboTargetModel.DropDownStyle = UIDropDownStyle.DropDownList;
            cboTargetModel.FillColor = Color.White;
            cboTargetModel.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cboTargetModel.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboTargetModel.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboTargetModel.Location = new Point(502, 191);
            cboTargetModel.Margin = new Padding(4, 5, 4, 5);
            cboTargetModel.MinimumSize = new Size(63, 0);
            cboTargetModel.Name = "cboTargetModel";
            cboTargetModel.Padding = new Padding(0, 0, 30, 2);
            cboTargetModel.Size = new Size(250, 30);
            cboTargetModel.SymbolSize = 24;
            cboTargetModel.TabIndex = 11;
            cboTargetModel.TextAlignment = ContentAlignment.MiddleLeft;
            cboTargetModel.Watermark = "";
            cboTargetModel.SelectedIndexChanged += CboTargetModel_SelectedIndexChanged;
            // 
            // lblTargetItem
            // 
            lblTargetItem.AutoSize = true;
            lblTargetItem.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblTargetItem.ForeColor = Color.FromArgb(48, 48, 48);
            lblTargetItem.Location = new Point(436, 261);
            lblTargetItem.Name = "lblTargetItem";
            lblTargetItem.Size = new Size(63, 16);
            lblTargetItem.TabIndex = 12;
            lblTargetItem.Text = "测试项:";
            // 
            // cboTargetItem
            // 
            cboTargetItem.DataSource = null;
            cboTargetItem.DropDownStyle = UIDropDownStyle.DropDownList;
            cboTargetItem.FillColor = Color.White;
            cboTargetItem.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            cboTargetItem.ItemHoverColor = Color.FromArgb(155, 200, 255);
            cboTargetItem.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            cboTargetItem.Location = new Point(502, 255);
            cboTargetItem.Margin = new Padding(4, 5, 4, 5);
            cboTargetItem.MinimumSize = new Size(63, 0);
            cboTargetItem.Name = "cboTargetItem";
            cboTargetItem.Padding = new Padding(0, 0, 30, 2);
            cboTargetItem.Size = new Size(250, 30);
            cboTargetItem.SymbolSize = 24;
            cboTargetItem.TabIndex = 13;
            cboTargetItem.TextAlignment = ContentAlignment.MiddleLeft;
            cboTargetItem.Watermark = "";
            // 
            // btnOK
            // 
            btnOK.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnOK.Location = new Point(235, 325);
            btnOK.MinimumSize = new Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(110, 45);
            btnOK.TabIndex = 14;
            btnOK.Text = "确定复制";
            btnOK.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnOK.Click += BtnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnCancel.Location = new Point(441, 325);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(110, 45);
            btnCancel.TabIndex = 15;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnCancel.Click += BtnCancel_Click;
            // 
            // ItemCopyDialog
            // 
            AutoSize = true;
            ClientSize = new Size(786, 404);
            Controls.Add(cboTargetType);
            Controls.Add(cboTargetModel);
            Controls.Add(cboTargetItem);
            Controls.Add(cboSourceItem);
            Controls.Add(cboSourceModel);
            Controls.Add(cboSourceType);
            Controls.Add(lblSourceType);
            Controls.Add(lblSourceModel);
            Controls.Add(lblSourceItem);
            Controls.Add(lblTargetType);
            Controls.Add(lblTargetModel);
            Controls.Add(lblTargetItem);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Controls.Add(lblSourceTitle);
            Controls.Add(lblTargetTitle);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ItemCopyDialog";
            RectColor = Color.FromArgb(65, 100, 204);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "复制测试项逻辑";
            TitleColor = Color.FromArgb(65, 100, 204);
            TitleFont = new Font("微软雅黑", 13F, FontStyle.Bold);
            ZoomScaleRect = new Rectangle(15, 15, 800, 480);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        #region 控件字段声明

        // 源选择区域
        private Sunny.UI.UILabel lblSourceTitle;
        private Sunny.UI.UILabel lblSourceType;
        private Sunny.UI.UIComboBox cboSourceType;
        private Sunny.UI.UILabel lblSourceModel;
        private Sunny.UI.UIComboBox cboSourceModel;
        private Sunny.UI.UILabel lblSourceItem;
        private Sunny.UI.UIComboBox cboSourceItem;

        // 目标选择区域
        private Sunny.UI.UILabel lblTargetTitle;
        private Sunny.UI.UILabel lblTargetType;
        private Sunny.UI.UIComboBox cboTargetType;
        private Sunny.UI.UILabel lblTargetModel;
        private Sunny.UI.UIComboBox cboTargetModel;
        private Sunny.UI.UILabel lblTargetItem;
        private Sunny.UI.UIComboBox cboTargetItem;

        // 按钮
        private Sunny.UI.UIButton btnOK;
        private Sunny.UI.UIButton btnCancel;

        #endregion
    }
}