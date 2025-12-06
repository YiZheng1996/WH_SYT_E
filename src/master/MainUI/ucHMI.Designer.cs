using AntdUI;
using MainUI.Procedure.Controls;
using Label = System.Windows.Forms.Label;

namespace MainUI
{
    partial class UcHMI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcHMI));
            Tabs.StyleLine styleLine1 = new Tabs.StyleLine();
            uiTitlePanel3 = new UITitlePanel();
            TableItemPoint = new Table();
            btnProductSelection = new UIButton();
            uiTextBox1 = new UITextBox();
            uiTextBox2 = new UITextBox();
            uiTextBox3 = new UITextBox();
            uiTextBox4 = new UITextBox();
            uiTextBox8 = new UITextBox();
            uiTextBox10 = new UITextBox();
            uiTextBox12 = new UITextBox();
            uiTextBox13 = new UITextBox();
            uiTextBox14 = new UITextBox();
            uiTextBox15 = new UITextBox();
            uiTextBox11 = new UITextBox();
            uiTextBox19 = new UITextBox();
            uiLabel3 = new UILabel();
            uiLabel2 = new UILabel();
            btnStartTest = new UIButton();
            uiTitlePanel8 = new UITitlePanel();
            LabTestTime = new UIPanel();
            btnStopTest = new UIButton();
            btnWorkmanshipForms = new AntdUI.Button();
            btnReportForms = new AntdUI.Button();
            uiTitlePanel4 = new UITitlePanel();
            uiTextBox6 = new UITextBox();
            uiLabel5 = new UILabel();
            uiTextBox5 = new UITextBox();
            uiLabel1 = new UILabel();
            txtType = new UITextBox();
            txtModel = new UITextBox();
            uiLabel7 = new UILabel();
            txtDrawingNo = new UITextBox();
            RadioHand = new UIRadioButton();
            RadioAuto = new UIRadioButton();
            panelHand = new UIPanel();
            tabPage1 = new AntdUI.TabPage();
            uiPanel6 = new UIPanel();
            inputNumber = new InputNumber();
            btnPageDown = new UISymbolButton();
            btnPageUp = new UISymbolButton();
            btnPrintReport = new UISymbolButton();
            btnSaveReport = new UISymbolButton();
            panelReport = new UIPanel();
            tabPage3 = new AntdUI.TabPage();
            grpRainy = new UIPanel();
            tabs1 = new Tabs();
            tabPageTestDetails = new AntdUI.TabPage();
            btnTestDetails = new AntdUI.Button();
            uiTitlePanel3.SuspendLayout();
            uiTitlePanel8.SuspendLayout();
            uiTitlePanel4.SuspendLayout();
            panelHand.SuspendLayout();
            tabPage1.SuspendLayout();
            uiPanel6.SuspendLayout();
            tabPage3.SuspendLayout();
            tabs1.SuspendLayout();
            SuspendLayout();
            // 
            // uiTitlePanel3
            // 
            uiTitlePanel3.BackColor = Color.FromArgb(236, 236, 236);
            uiTitlePanel3.Controls.Add(TableItemPoint);
            uiTitlePanel3.FillColor = Color.FromArgb(236, 236, 236);
            uiTitlePanel3.FillColor2 = Color.FromArgb(236, 236, 236);
            uiTitlePanel3.FillDisableColor = Color.FromArgb(49, 54, 64);
            uiTitlePanel3.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            uiTitlePanel3.Location = new Point(0, 309);
            uiTitlePanel3.Margin = new Padding(4, 5, 4, 5);
            uiTitlePanel3.MinimumSize = new Size(1, 1);
            uiTitlePanel3.Name = "uiTitlePanel3";
            uiTitlePanel3.Padding = new Padding(1, 29, 1, 1);
            uiTitlePanel3.Radius = 0;
            uiTitlePanel3.RectColor = Color.FromArgb(236, 236, 236);
            uiTitlePanel3.RectDisableColor = Color.FromArgb(236, 236, 236);
            uiTitlePanel3.ShowText = false;
            uiTitlePanel3.Size = new Size(253, 373);
            uiTitlePanel3.TabIndex = 398;
            uiTitlePanel3.Text = "试验项点";
            uiTitlePanel3.TextAlignment = ContentAlignment.MiddleCenter;
            uiTitlePanel3.TitleColor = Color.FromArgb(65, 100, 204);
            uiTitlePanel3.TitleHeight = 29;
            // 
            // TableItemPoint
            // 
            TableItemPoint.BackColor = Color.White;
            TableItemPoint.CheckSize = 18;
            TableItemPoint.Dock = DockStyle.Bottom;
            TableItemPoint.Font = new Font("微软雅黑", 11F);
            TableItemPoint.ForeColor = Color.Black;
            TableItemPoint.Gap = 12;
            TableItemPoint.Location = new Point(1, 32);
            TableItemPoint.Name = "TableItemPoint";
            TableItemPoint.RowSelectedBg = Color.Transparent;
            TableItemPoint.Size = new Size(251, 340);
            TableItemPoint.TabIndex = 53;
            TableItemPoint.CheckedChanged += TableItemPoint_CheckedChanged;
            // 
            // btnProductSelection
            // 
            btnProductSelection.BackColor = Color.Transparent;
            btnProductSelection.Cursor = Cursors.Hand;
            btnProductSelection.FillDisableColor = Color.FromArgb(80, 160, 255);
            btnProductSelection.Font = new Font("思源黑体 CN Bold", 12F, FontStyle.Bold);
            btnProductSelection.ForeDisableColor = Color.White;
            btnProductSelection.Location = new Point(61, 43);
            btnProductSelection.MinimumSize = new Size(1, 1);
            btnProductSelection.Name = "btnProductSelection";
            btnProductSelection.Radius = 10;
            btnProductSelection.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnProductSelection.ShowFocusLine = true;
            btnProductSelection.Size = new Size(135, 29);
            btnProductSelection.TabIndex = 60;
            btnProductSelection.Text = "产品选择";
            btnProductSelection.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnProductSelection.TipsText = "1";
            btnProductSelection.Click += btnProductSelection_Click;
            // 
            // uiTextBox1
            // 
            uiTextBox1.Cursor = Cursors.IBeam;
            uiTextBox1.FillColor = Color.FromArgb(243, 249, 255);
            uiTextBox1.FillDisableColor = Color.FromArgb(243, 249, 255);
            uiTextBox1.FillReadOnlyColor = Color.FromArgb(243, 249, 255);
            uiTextBox1.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiTextBox1.Location = new Point(890, 30);
            uiTextBox1.Margin = new Padding(4, 5, 4, 5);
            uiTextBox1.MinimumSize = new Size(1, 16);
            uiTextBox1.Name = "uiTextBox1";
            uiTextBox1.Padding = new Padding(5);
            uiTextBox1.ReadOnly = true;
            uiTextBox1.RectDisableColor = Color.FromArgb(80, 160, 255);
            uiTextBox1.RectReadOnlyColor = Color.FromArgb(80, 160, 255);
            uiTextBox1.ShowText = false;
            uiTextBox1.Size = new Size(95, 30);
            uiTextBox1.TabIndex = 942;
            uiTextBox1.Tag = "1";
            uiTextBox1.Text = "0";
            uiTextBox1.TextAlignment = ContentAlignment.MiddleCenter;
            uiTextBox1.Watermark = "";
            // 
            // uiTextBox2
            // 
            uiTextBox2.Cursor = Cursors.IBeam;
            uiTextBox2.FillColor = Color.FromArgb(243, 249, 255);
            uiTextBox2.FillDisableColor = Color.FromArgb(243, 249, 255);
            uiTextBox2.FillReadOnlyColor = Color.FromArgb(243, 249, 255);
            uiTextBox2.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiTextBox2.Location = new Point(555, 92);
            uiTextBox2.Margin = new Padding(4, 5, 4, 5);
            uiTextBox2.MinimumSize = new Size(1, 16);
            uiTextBox2.Name = "uiTextBox2";
            uiTextBox2.Padding = new Padding(5);
            uiTextBox2.ReadOnly = true;
            uiTextBox2.RectDisableColor = Color.FromArgb(80, 160, 255);
            uiTextBox2.RectReadOnlyColor = Color.FromArgb(80, 160, 255);
            uiTextBox2.ShowText = false;
            uiTextBox2.Size = new Size(95, 30);
            uiTextBox2.TabIndex = 942;
            uiTextBox2.Tag = "2";
            uiTextBox2.Text = "0";
            uiTextBox2.TextAlignment = ContentAlignment.MiddleCenter;
            uiTextBox2.Watermark = "";
            // 
            // uiTextBox3
            // 
            uiTextBox3.Cursor = Cursors.IBeam;
            uiTextBox3.FillColor = Color.FromArgb(243, 249, 255);
            uiTextBox3.FillDisableColor = Color.FromArgb(243, 249, 255);
            uiTextBox3.FillReadOnlyColor = Color.FromArgb(243, 249, 255);
            uiTextBox3.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiTextBox3.Location = new Point(890, 136);
            uiTextBox3.Margin = new Padding(4, 5, 4, 5);
            uiTextBox3.MinimumSize = new Size(1, 16);
            uiTextBox3.Name = "uiTextBox3";
            uiTextBox3.Padding = new Padding(5);
            uiTextBox3.ReadOnly = true;
            uiTextBox3.RectDisableColor = Color.FromArgb(80, 160, 255);
            uiTextBox3.RectReadOnlyColor = Color.FromArgb(80, 160, 255);
            uiTextBox3.ShowText = false;
            uiTextBox3.Size = new Size(95, 30);
            uiTextBox3.TabIndex = 944;
            uiTextBox3.Tag = "3";
            uiTextBox3.Text = "0";
            uiTextBox3.TextAlignment = ContentAlignment.MiddleCenter;
            uiTextBox3.Watermark = "";
            // 
            // uiTextBox4
            // 
            uiTextBox4.Cursor = Cursors.IBeam;
            uiTextBox4.FillColor = Color.FromArgb(243, 249, 255);
            uiTextBox4.FillDisableColor = Color.FromArgb(243, 249, 255);
            uiTextBox4.FillReadOnlyColor = Color.FromArgb(243, 249, 255);
            uiTextBox4.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiTextBox4.Location = new Point(890, 284);
            uiTextBox4.Margin = new Padding(4, 5, 4, 5);
            uiTextBox4.MinimumSize = new Size(1, 16);
            uiTextBox4.Name = "uiTextBox4";
            uiTextBox4.Padding = new Padding(5);
            uiTextBox4.ReadOnly = true;
            uiTextBox4.RectDisableColor = Color.FromArgb(80, 160, 255);
            uiTextBox4.RectReadOnlyColor = Color.FromArgb(80, 160, 255);
            uiTextBox4.ShowText = false;
            uiTextBox4.Size = new Size(95, 30);
            uiTextBox4.TabIndex = 944;
            uiTextBox4.Tag = "4";
            uiTextBox4.Text = "0";
            uiTextBox4.TextAlignment = ContentAlignment.MiddleCenter;
            uiTextBox4.Watermark = "";
            // 
            // uiTextBox8
            // 
            uiTextBox8.Cursor = Cursors.IBeam;
            uiTextBox8.FillColor = Color.FromArgb(243, 249, 255);
            uiTextBox8.FillDisableColor = Color.FromArgb(243, 249, 255);
            uiTextBox8.FillReadOnlyColor = Color.FromArgb(243, 249, 255);
            uiTextBox8.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiTextBox8.Location = new Point(890, 345);
            uiTextBox8.Margin = new Padding(4, 5, 4, 5);
            uiTextBox8.MinimumSize = new Size(1, 16);
            uiTextBox8.Name = "uiTextBox8";
            uiTextBox8.Padding = new Padding(5);
            uiTextBox8.ReadOnly = true;
            uiTextBox8.RectDisableColor = Color.FromArgb(80, 160, 255);
            uiTextBox8.RectReadOnlyColor = Color.FromArgb(80, 160, 255);
            uiTextBox8.ShowText = false;
            uiTextBox8.Size = new Size(95, 30);
            uiTextBox8.TabIndex = 946;
            uiTextBox8.Tag = "6";
            uiTextBox8.Text = "0";
            uiTextBox8.TextAlignment = ContentAlignment.MiddleCenter;
            uiTextBox8.Watermark = "";
            // 
            // uiTextBox10
            // 
            uiTextBox10.Cursor = Cursors.IBeam;
            uiTextBox10.FillColor = Color.FromArgb(243, 249, 255);
            uiTextBox10.FillDisableColor = Color.FromArgb(243, 249, 255);
            uiTextBox10.FillReadOnlyColor = Color.FromArgb(243, 249, 255);
            uiTextBox10.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiTextBox10.Location = new Point(505, 337);
            uiTextBox10.Margin = new Padding(4, 5, 4, 5);
            uiTextBox10.MinimumSize = new Size(1, 16);
            uiTextBox10.Name = "uiTextBox10";
            uiTextBox10.Padding = new Padding(5);
            uiTextBox10.ReadOnly = true;
            uiTextBox10.RectDisableColor = Color.FromArgb(80, 160, 255);
            uiTextBox10.RectReadOnlyColor = Color.FromArgb(80, 160, 255);
            uiTextBox10.ShowText = false;
            uiTextBox10.Size = new Size(95, 30);
            uiTextBox10.TabIndex = 948;
            uiTextBox10.Tag = "5";
            uiTextBox10.Text = "0";
            uiTextBox10.TextAlignment = ContentAlignment.MiddleCenter;
            uiTextBox10.Watermark = "";
            // 
            // uiTextBox12
            // 
            uiTextBox12.Cursor = Cursors.IBeam;
            uiTextBox12.FillColor = Color.FromArgb(243, 249, 255);
            uiTextBox12.FillDisableColor = Color.FromArgb(243, 249, 255);
            uiTextBox12.FillReadOnlyColor = Color.FromArgb(243, 249, 255);
            uiTextBox12.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiTextBox12.Location = new Point(890, 424);
            uiTextBox12.Margin = new Padding(4, 5, 4, 5);
            uiTextBox12.MinimumSize = new Size(1, 16);
            uiTextBox12.Name = "uiTextBox12";
            uiTextBox12.Padding = new Padding(5);
            uiTextBox12.ReadOnly = true;
            uiTextBox12.RectDisableColor = Color.FromArgb(80, 160, 255);
            uiTextBox12.RectReadOnlyColor = Color.FromArgb(80, 160, 255);
            uiTextBox12.ShowText = false;
            uiTextBox12.Size = new Size(95, 30);
            uiTextBox12.TabIndex = 948;
            uiTextBox12.Tag = "8";
            uiTextBox12.Text = "0";
            uiTextBox12.TextAlignment = ContentAlignment.MiddleCenter;
            uiTextBox12.Watermark = "";
            // 
            // uiTextBox13
            // 
            uiTextBox13.Cursor = Cursors.IBeam;
            uiTextBox13.FillColor = Color.FromArgb(243, 249, 255);
            uiTextBox13.FillDisableColor = Color.FromArgb(243, 249, 255);
            uiTextBox13.FillReadOnlyColor = Color.FromArgb(243, 249, 255);
            uiTextBox13.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiTextBox13.Location = new Point(890, 487);
            uiTextBox13.Margin = new Padding(4, 5, 4, 5);
            uiTextBox13.MinimumSize = new Size(1, 16);
            uiTextBox13.Name = "uiTextBox13";
            uiTextBox13.Padding = new Padding(5);
            uiTextBox13.ReadOnly = true;
            uiTextBox13.RectDisableColor = Color.FromArgb(80, 160, 255);
            uiTextBox13.RectReadOnlyColor = Color.FromArgb(80, 160, 255);
            uiTextBox13.ShowText = false;
            uiTextBox13.Size = new Size(95, 30);
            uiTextBox13.TabIndex = 948;
            uiTextBox13.Tag = "10";
            uiTextBox13.Text = "0";
            uiTextBox13.TextAlignment = ContentAlignment.MiddleCenter;
            uiTextBox13.Watermark = "";
            // 
            // uiTextBox14
            // 
            uiTextBox14.Cursor = Cursors.IBeam;
            uiTextBox14.FillColor = Color.FromArgb(243, 249, 255);
            uiTextBox14.FillDisableColor = Color.FromArgb(243, 249, 255);
            uiTextBox14.FillReadOnlyColor = Color.FromArgb(243, 249, 255);
            uiTextBox14.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiTextBox14.Location = new Point(890, 560);
            uiTextBox14.Margin = new Padding(4, 5, 4, 5);
            uiTextBox14.MinimumSize = new Size(1, 16);
            uiTextBox14.Name = "uiTextBox14";
            uiTextBox14.Padding = new Padding(5);
            uiTextBox14.ReadOnly = true;
            uiTextBox14.RectDisableColor = Color.FromArgb(80, 160, 255);
            uiTextBox14.RectReadOnlyColor = Color.FromArgb(80, 160, 255);
            uiTextBox14.ShowText = false;
            uiTextBox14.Size = new Size(95, 30);
            uiTextBox14.TabIndex = 950;
            uiTextBox14.Tag = "12";
            uiTextBox14.Text = "0";
            uiTextBox14.TextAlignment = ContentAlignment.MiddleCenter;
            uiTextBox14.Watermark = "";
            // 
            // uiTextBox15
            // 
            uiTextBox15.Cursor = Cursors.IBeam;
            uiTextBox15.FillColor = Color.FromArgb(243, 249, 255);
            uiTextBox15.FillDisableColor = Color.FromArgb(243, 249, 255);
            uiTextBox15.FillReadOnlyColor = Color.FromArgb(243, 249, 255);
            uiTextBox15.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiTextBox15.Location = new Point(505, 491);
            uiTextBox15.Margin = new Padding(4, 5, 4, 5);
            uiTextBox15.MinimumSize = new Size(1, 16);
            uiTextBox15.Name = "uiTextBox15";
            uiTextBox15.Padding = new Padding(5);
            uiTextBox15.ReadOnly = true;
            uiTextBox15.RectDisableColor = Color.FromArgb(80, 160, 255);
            uiTextBox15.RectReadOnlyColor = Color.FromArgb(80, 160, 255);
            uiTextBox15.ShowText = false;
            uiTextBox15.Size = new Size(95, 30);
            uiTextBox15.TabIndex = 950;
            uiTextBox15.Tag = "9";
            uiTextBox15.Text = "0";
            uiTextBox15.TextAlignment = ContentAlignment.MiddleCenter;
            uiTextBox15.Watermark = "";
            // 
            // uiTextBox11
            // 
            uiTextBox11.Cursor = Cursors.IBeam;
            uiTextBox11.FillColor = Color.FromArgb(243, 249, 255);
            uiTextBox11.FillDisableColor = Color.FromArgb(243, 249, 255);
            uiTextBox11.FillReadOnlyColor = Color.FromArgb(243, 249, 255);
            uiTextBox11.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiTextBox11.Location = new Point(505, 424);
            uiTextBox11.Margin = new Padding(4, 5, 4, 5);
            uiTextBox11.MinimumSize = new Size(1, 16);
            uiTextBox11.Name = "uiTextBox11";
            uiTextBox11.Padding = new Padding(5);
            uiTextBox11.ReadOnly = true;
            uiTextBox11.RectDisableColor = Color.FromArgb(80, 160, 255);
            uiTextBox11.RectReadOnlyColor = Color.FromArgb(80, 160, 255);
            uiTextBox11.ShowText = false;
            uiTextBox11.Size = new Size(95, 30);
            uiTextBox11.TabIndex = 950;
            uiTextBox11.Tag = "7";
            uiTextBox11.Text = "0";
            uiTextBox11.TextAlignment = ContentAlignment.MiddleCenter;
            uiTextBox11.Watermark = "";
            // 
            // uiTextBox19
            // 
            uiTextBox19.Cursor = Cursors.IBeam;
            uiTextBox19.FillColor = Color.FromArgb(243, 249, 255);
            uiTextBox19.FillDisableColor = Color.FromArgb(243, 249, 255);
            uiTextBox19.FillReadOnlyColor = Color.FromArgb(243, 249, 255);
            uiTextBox19.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiTextBox19.Location = new Point(505, 560);
            uiTextBox19.Margin = new Padding(4, 5, 4, 5);
            uiTextBox19.MinimumSize = new Size(1, 16);
            uiTextBox19.Name = "uiTextBox19";
            uiTextBox19.Padding = new Padding(5);
            uiTextBox19.ReadOnly = true;
            uiTextBox19.RectDisableColor = Color.FromArgb(80, 160, 255);
            uiTextBox19.RectReadOnlyColor = Color.FromArgb(80, 160, 255);
            uiTextBox19.ShowText = false;
            uiTextBox19.Size = new Size(95, 30);
            uiTextBox19.TabIndex = 952;
            uiTextBox19.Tag = "11";
            uiTextBox19.Text = "0";
            uiTextBox19.TextAlignment = ContentAlignment.MiddleCenter;
            uiTextBox19.Watermark = "";
            // 
            // uiLabel3
            // 
            uiLabel3.AutoSize = true;
            uiLabel3.BackColor = Color.Transparent;
            uiLabel3.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            uiLabel3.ForeColor = Color.FromArgb(65, 100, 204);
            uiLabel3.Location = new Point(10, 177);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(73, 19);
            uiLabel3.TabIndex = 65;
            uiLabel3.Text = "产品图号:";
            uiLabel3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // uiLabel2
            // 
            uiLabel2.AutoSize = true;
            uiLabel2.BackColor = Color.Transparent;
            uiLabel2.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            uiLabel2.ForeColor = Color.FromArgb(65, 100, 204);
            uiLabel2.Location = new Point(10, 134);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(73, 19);
            uiLabel2.TabIndex = 63;
            uiLabel2.Text = "产品型号:";
            uiLabel2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnStartTest
            // 
            btnStartTest.Cursor = Cursors.Hand;
            btnStartTest.FillColor = Color.FromArgb(90, 124, 236);
            btnStartTest.FillColor2 = Color.FromArgb(90, 124, 236);
            btnStartTest.FillDisableColor = Color.FromArgb(153, 153, 161);
            btnStartTest.FillHoverColor = Color.FromArgb(90, 124, 236);
            btnStartTest.FillPressColor = Color.FromArgb(90, 124, 236);
            btnStartTest.FillSelectedColor = Color.FromArgb(90, 124, 236);
            btnStartTest.Font = new Font("思源黑体 CN Bold", 16F, FontStyle.Bold);
            btnStartTest.ForeColor = Color.FromArgb(235, 227, 221);
            btnStartTest.ForeDisableColor = Color.FromArgb(235, 227, 221);
            btnStartTest.LightColor = Color.FromArgb(245, 251, 241);
            btnStartTest.Location = new Point(4, 830);
            btnStartTest.MinimumSize = new Size(1, 1);
            btnStartTest.Name = "btnStartTest";
            btnStartTest.Radius = 7;
            btnStartTest.RectColor = Color.FromArgb(90, 124, 236);
            btnStartTest.RectDisableColor = Color.FromArgb(153, 153, 161);
            btnStartTest.RectHoverColor = Color.FromArgb(90, 124, 236);
            btnStartTest.RectPressColor = Color.FromArgb(90, 124, 236);
            btnStartTest.RectSelectedColor = Color.FromArgb(90, 124, 236);
            btnStartTest.Size = new Size(122, 58);
            btnStartTest.Style = UIStyle.Custom;
            btnStartTest.StyleCustomMode = true;
            btnStartTest.TabIndex = 398;
            btnStartTest.Text = "开始试验";
            btnStartTest.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnStartTest.Click += btnStartTest_Click;
            // 
            // uiTitlePanel8
            // 
            uiTitlePanel8.BackColor = Color.FromArgb(236, 236, 236);
            uiTitlePanel8.Controls.Add(LabTestTime);
            uiTitlePanel8.FillColor = Color.FromArgb(236, 236, 236);
            uiTitlePanel8.FillColor2 = Color.FromArgb(236, 236, 236);
            uiTitlePanel8.FillDisableColor = Color.FromArgb(236, 236, 236);
            uiTitlePanel8.Font = new Font("微软雅黑", 13F, FontStyle.Bold);
            uiTitlePanel8.Location = new Point(0, 729);
            uiTitlePanel8.Margin = new Padding(4, 5, 4, 5);
            uiTitlePanel8.MinimumSize = new Size(1, 1);
            uiTitlePanel8.Name = "uiTitlePanel8";
            uiTitlePanel8.Padding = new Padding(1, 29, 1, 1);
            uiTitlePanel8.Radius = 0;
            uiTitlePanel8.RectColor = Color.FromArgb(236, 236, 236);
            uiTitlePanel8.RectDisableColor = Color.FromArgb(236, 236, 236);
            uiTitlePanel8.ShowText = false;
            uiTitlePanel8.Size = new Size(254, 100);
            uiTitlePanel8.TabIndex = 400;
            uiTitlePanel8.Text = "试验时间";
            uiTitlePanel8.TextAlignment = ContentAlignment.MiddleCenter;
            uiTitlePanel8.TitleColor = Color.FromArgb(65, 100, 204);
            uiTitlePanel8.TitleHeight = 29;
            // 
            // LabTestTime
            // 
            LabTestTime.BackColor = Color.Transparent;
            LabTestTime.FillColor = Color.FromArgb(43, 46, 57);
            LabTestTime.FillColor2 = Color.FromArgb(43, 46, 57);
            LabTestTime.Font = new Font("微软雅黑", 30F);
            LabTestTime.ForeColor = Color.White;
            LabTestTime.Location = new Point(2, 33);
            LabTestTime.Margin = new Padding(4, 5, 4, 5);
            LabTestTime.MinimumSize = new Size(1, 1);
            LabTestTime.Name = "LabTestTime";
            LabTestTime.Radius = 15;
            LabTestTime.RectColor = Color.FromArgb(43, 46, 57);
            LabTestTime.RectDisableColor = Color.FromArgb(43, 46, 57);
            LabTestTime.Size = new Size(251, 63);
            LabTestTime.TabIndex = 498;
            LabTestTime.Text = "00:00:00";
            LabTestTime.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnStopTest
            // 
            btnStopTest.Cursor = Cursors.Hand;
            btnStopTest.FillColor = Color.FromArgb(230, 83, 100);
            btnStopTest.FillColor2 = Color.FromArgb(230, 83, 100);
            btnStopTest.FillDisableColor = Color.FromArgb(153, 153, 161);
            btnStopTest.FillHoverColor = Color.FromArgb(235, 115, 115);
            btnStopTest.FillPressColor = Color.FromArgb(184, 64, 64);
            btnStopTest.FillSelectedColor = Color.FromArgb(184, 64, 64);
            btnStopTest.Font = new Font("思源黑体 CN Bold", 16F, FontStyle.Bold);
            btnStopTest.ForeColor = Color.FromArgb(235, 227, 221);
            btnStopTest.ForeDisableColor = Color.FromArgb(235, 227, 221);
            btnStopTest.LightColor = Color.FromArgb(253, 243, 243);
            btnStopTest.Location = new Point(130, 830);
            btnStopTest.MinimumSize = new Size(1, 1);
            btnStopTest.Name = "btnStopTest";
            btnStopTest.Radius = 7;
            btnStopTest.RectColor = Color.FromArgb(230, 83, 100);
            btnStopTest.RectDisableColor = Color.FromArgb(153, 153, 161);
            btnStopTest.RectHoverColor = Color.FromArgb(235, 115, 115);
            btnStopTest.RectPressColor = Color.FromArgb(184, 64, 64);
            btnStopTest.RectSelectedColor = Color.FromArgb(184, 64, 64);
            btnStopTest.Size = new Size(122, 58);
            btnStopTest.Style = UIStyle.Custom;
            btnStopTest.StyleCustomMode = true;
            btnStopTest.TabIndex = 399;
            btnStopTest.Text = "结束试验";
            btnStopTest.TipsFont = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnStopTest.Click += btnStopTest_Click;
            // 
            // btnWorkmanshipForms
            // 
            btnWorkmanshipForms.BackActive = Color.FromArgb(49, 54, 64);
            btnWorkmanshipForms.BackColor = Color.White;
            btnWorkmanshipForms.BorderWidth = 1F;
            btnWorkmanshipForms.Font = new Font("微软雅黑", 12.5F, FontStyle.Bold);
            btnWorkmanshipForms.ForeColor = Color.Black;
            btnWorkmanshipForms.JoinMode = TJoinMode.Left;
            btnWorkmanshipForms.Location = new Point(257, 0);
            btnWorkmanshipForms.Name = "btnWorkmanshipForms";
            btnWorkmanshipForms.Size = new Size(124, 35);
            btnWorkmanshipForms.TabIndex = 493;
            btnWorkmanshipForms.Text = "工艺界面";
            btnWorkmanshipForms.Type = TTypeMini.Primary;
            btnWorkmanshipForms.WaveSize = 1;
            btnWorkmanshipForms.Click += btnTechnology_Click;
            // 
            // btnReportForms
            // 
            btnReportForms.BackActive = Color.FromArgb(196, 199, 204);
            btnReportForms.BackColor = Color.FromArgb(196, 199, 204);
            btnReportForms.BorderWidth = 1F;
            btnReportForms.Font = new Font("微软雅黑", 12.5F, FontStyle.Bold);
            btnReportForms.ForeColor = Color.White;
            btnReportForms.JoinMode = TJoinMode.LR;
            btnReportForms.Location = new Point(382, 0);
            btnReportForms.Name = "btnReportForms";
            btnReportForms.Size = new Size(124, 35);
            btnReportForms.TabIndex = 494;
            btnReportForms.Text = "报表界面";
            btnReportForms.Type = TTypeMini.Primary;
            btnReportForms.WaveSize = 1;
            btnReportForms.Click += btnCurve_Click;
            // 
            // uiTitlePanel4
            // 
            uiTitlePanel4.BackColor = Color.FromArgb(236, 236, 236);
            uiTitlePanel4.Controls.Add(uiTextBox6);
            uiTitlePanel4.Controls.Add(uiLabel5);
            uiTitlePanel4.Controls.Add(uiTextBox5);
            uiTitlePanel4.Controls.Add(uiLabel1);
            uiTitlePanel4.Controls.Add(txtType);
            uiTitlePanel4.Controls.Add(btnProductSelection);
            uiTitlePanel4.Controls.Add(txtModel);
            uiTitlePanel4.Controls.Add(uiLabel7);
            uiTitlePanel4.Controls.Add(txtDrawingNo);
            uiTitlePanel4.Controls.Add(uiLabel3);
            uiTitlePanel4.Controls.Add(uiLabel2);
            uiTitlePanel4.FillColor = Color.White;
            uiTitlePanel4.FillColor2 = Color.White;
            uiTitlePanel4.FillDisableColor = Color.White;
            uiTitlePanel4.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
            uiTitlePanel4.Location = new Point(0, 0);
            uiTitlePanel4.Margin = new Padding(4, 5, 4, 5);
            uiTitlePanel4.MinimumSize = new Size(1, 1);
            uiTitlePanel4.Name = "uiTitlePanel4";
            uiTitlePanel4.Padding = new Padding(1, 29, 1, 1);
            uiTitlePanel4.Radius = 10;
            uiTitlePanel4.RectColor = Color.White;
            uiTitlePanel4.RectDisableColor = Color.White;
            uiTitlePanel4.ShowText = false;
            uiTitlePanel4.Size = new Size(253, 306);
            uiTitlePanel4.TabIndex = 497;
            uiTitlePanel4.Text = "信息录入";
            uiTitlePanel4.TextAlignment = ContentAlignment.MiddleCenter;
            uiTitlePanel4.TitleColor = Color.FromArgb(65, 100, 204);
            uiTitlePanel4.TitleHeight = 29;
            // 
            // uiTextBox6
            // 
            uiTextBox6.BackColor = Color.Transparent;
            uiTextBox6.FillColor = Color.FromArgb(218, 220, 230);
            uiTextBox6.FillColor2 = Color.FromArgb(218, 220, 230);
            uiTextBox6.FillDisableColor = Color.FromArgb(218, 220, 230);
            uiTextBox6.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            uiTextBox6.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            uiTextBox6.Location = new Point(90, 258);
            uiTextBox6.Margin = new Padding(4, 5, 4, 5);
            uiTextBox6.MinimumSize = new Size(1, 16);
            uiTextBox6.Name = "uiTextBox6";
            uiTextBox6.Padding = new Padding(5);
            uiTextBox6.Radius = 15;
            uiTextBox6.RectColor = Color.FromArgb(218, 220, 230);
            uiTextBox6.RectDisableColor = Color.FromArgb(218, 220, 230);
            uiTextBox6.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            uiTextBox6.ShowText = false;
            uiTextBox6.Size = new Size(148, 28);
            uiTextBox6.TabIndex = 71;
            uiTextBox6.TextAlignment = ContentAlignment.MiddleLeft;
            uiTextBox6.Watermark = "请输入";
            // 
            // uiLabel5
            // 
            uiLabel5.AutoSize = true;
            uiLabel5.BackColor = Color.Transparent;
            uiLabel5.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            uiLabel5.ForeColor = Color.FromArgb(65, 100, 204);
            uiLabel5.Location = new Point(24, 262);
            uiLabel5.Name = "uiLabel5";
            uiLabel5.Size = new Size(59, 19);
            uiLabel5.TabIndex = 70;
            uiLabel5.Text = "备    注:";
            uiLabel5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // uiTextBox5
            // 
            uiTextBox5.BackColor = Color.Transparent;
            uiTextBox5.FillColor = Color.FromArgb(218, 220, 230);
            uiTextBox5.FillColor2 = Color.FromArgb(218, 220, 230);
            uiTextBox5.FillDisableColor = Color.FromArgb(218, 220, 230);
            uiTextBox5.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            uiTextBox5.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            uiTextBox5.Location = new Point(90, 215);
            uiTextBox5.Margin = new Padding(4, 5, 4, 5);
            uiTextBox5.MinimumSize = new Size(1, 16);
            uiTextBox5.Name = "uiTextBox5";
            uiTextBox5.Padding = new Padding(5);
            uiTextBox5.Radius = 15;
            uiTextBox5.RectColor = Color.FromArgb(218, 220, 230);
            uiTextBox5.RectDisableColor = Color.FromArgb(218, 220, 230);
            uiTextBox5.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            uiTextBox5.ShowText = false;
            uiTextBox5.Size = new Size(148, 28);
            uiTextBox5.TabIndex = 69;
            uiTextBox5.TextAlignment = ContentAlignment.MiddleLeft;
            uiTextBox5.Watermark = "请输入";
            // 
            // uiLabel1
            // 
            uiLabel1.AutoSize = true;
            uiLabel1.BackColor = Color.Transparent;
            uiLabel1.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            uiLabel1.ForeColor = Color.FromArgb(65, 100, 204);
            uiLabel1.Location = new Point(10, 220);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(73, 19);
            uiLabel1.TabIndex = 68;
            uiLabel1.Text = "产品编号:";
            uiLabel1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtType
            // 
            txtType.BackColor = Color.Transparent;
            txtType.FillColor = Color.FromArgb(218, 220, 230);
            txtType.FillColor2 = Color.FromArgb(218, 220, 230);
            txtType.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtType.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtType.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            txtType.Location = new Point(90, 86);
            txtType.Margin = new Padding(4, 5, 4, 5);
            txtType.MinimumSize = new Size(1, 16);
            txtType.Name = "txtType";
            txtType.Padding = new Padding(5);
            txtType.Radius = 15;
            txtType.RectColor = Color.FromArgb(218, 220, 230);
            txtType.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtType.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtType.ShowText = false;
            txtType.Size = new Size(148, 28);
            txtType.TabIndex = 66;
            txtType.TextAlignment = ContentAlignment.MiddleLeft;
            txtType.Watermark = "请输入";
            // 
            // txtModel
            // 
            txtModel.BackColor = Color.Transparent;
            txtModel.FillColor = Color.FromArgb(218, 220, 230);
            txtModel.FillColor2 = Color.FromArgb(218, 220, 230);
            txtModel.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtModel.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtModel.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            txtModel.Location = new Point(90, 129);
            txtModel.Margin = new Padding(4, 5, 4, 5);
            txtModel.MinimumSize = new Size(1, 16);
            txtModel.Name = "txtModel";
            txtModel.Padding = new Padding(5);
            txtModel.Radius = 15;
            txtModel.RectColor = Color.FromArgb(218, 220, 230);
            txtModel.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtModel.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtModel.ShowText = false;
            txtModel.Size = new Size(148, 28);
            txtModel.TabIndex = 3;
            txtModel.TextAlignment = ContentAlignment.MiddleLeft;
            txtModel.Watermark = "请选择";
            // 
            // uiLabel7
            // 
            uiLabel7.AutoSize = true;
            uiLabel7.BackColor = Color.Transparent;
            uiLabel7.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            uiLabel7.ForeColor = Color.FromArgb(65, 100, 204);
            uiLabel7.Location = new Point(10, 91);
            uiLabel7.Name = "uiLabel7";
            uiLabel7.Size = new Size(73, 19);
            uiLabel7.TabIndex = 67;
            uiLabel7.Text = "产品类型:";
            uiLabel7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtDrawingNo
            // 
            txtDrawingNo.BackColor = Color.Transparent;
            txtDrawingNo.FillColor = Color.FromArgb(218, 220, 230);
            txtDrawingNo.FillColor2 = Color.FromArgb(218, 220, 230);
            txtDrawingNo.FillDisableColor = Color.FromArgb(218, 220, 230);
            txtDrawingNo.FillReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtDrawingNo.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
            txtDrawingNo.Location = new Point(90, 172);
            txtDrawingNo.Margin = new Padding(4, 5, 4, 5);
            txtDrawingNo.MinimumSize = new Size(1, 16);
            txtDrawingNo.Name = "txtDrawingNo";
            txtDrawingNo.Padding = new Padding(5);
            txtDrawingNo.Radius = 15;
            txtDrawingNo.RectColor = Color.FromArgb(218, 220, 230);
            txtDrawingNo.RectDisableColor = Color.FromArgb(218, 220, 230);
            txtDrawingNo.RectReadOnlyColor = Color.FromArgb(218, 220, 230);
            txtDrawingNo.ShowText = false;
            txtDrawingNo.Size = new Size(148, 28);
            txtDrawingNo.TabIndex = 0;
            txtDrawingNo.TextAlignment = ContentAlignment.MiddleLeft;
            txtDrawingNo.Watermark = "请输入";
            // 
            // RadioHand
            // 
            RadioHand.BackColor = Color.Transparent;
            RadioHand.Font = new Font("微软雅黑", 12F);
            RadioHand.Location = new Point(13, 6);
            RadioHand.MinimumSize = new Size(1, 1);
            RadioHand.Name = "RadioHand";
            RadioHand.Size = new Size(116, 29);
            RadioHand.TabIndex = 67;
            RadioHand.Text = "手动控制";
            RadioHand.CheckedChanged += RadioAuto_CheckedChanged;
            // 
            // RadioAuto
            // 
            RadioAuto.BackColor = Color.Transparent;
            RadioAuto.Checked = true;
            RadioAuto.Font = new Font("微软雅黑", 12F);
            RadioAuto.Location = new Point(135, 6);
            RadioAuto.MinimumSize = new Size(1, 1);
            RadioAuto.Name = "RadioAuto";
            RadioAuto.Size = new Size(118, 29);
            RadioAuto.TabIndex = 498;
            RadioAuto.Text = "自动控制";
            RadioAuto.CheckedChanged += RadioAuto_CheckedChanged;
            // 
            // panelHand
            // 
            panelHand.Controls.Add(RadioHand);
            panelHand.Controls.Add(RadioAuto);
            panelHand.FillColor = Color.White;
            panelHand.FillColor2 = Color.White;
            panelHand.FillDisableColor = Color.White;
            panelHand.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panelHand.Location = new Point(1, 685);
            panelHand.Margin = new Padding(4, 5, 4, 5);
            panelHand.MinimumSize = new Size(1, 1);
            panelHand.Name = "panelHand";
            panelHand.RectColor = Color.White;
            panelHand.RectDisableColor = Color.White;
            panelHand.Size = new Size(251, 40);
            panelHand.TabIndex = 71;
            panelHand.Text = null;
            panelHand.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(uiPanel6);
            tabPage1.Controls.Add(panelReport);
            tabPage1.Dock = DockStyle.Fill;
            tabPage1.Location = new Point(0, 0);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(900, 856);
            tabPage1.TabIndex = 1;
            tabPage1.Text = "tabPage1";
            // 
            // uiPanel6
            // 
            uiPanel6.BackColor = Color.Transparent;
            uiPanel6.Controls.Add(inputNumber);
            uiPanel6.Controls.Add(btnPageDown);
            uiPanel6.Controls.Add(btnPageUp);
            uiPanel6.Controls.Add(btnPrintReport);
            uiPanel6.Controls.Add(btnSaveReport);
            uiPanel6.Dock = DockStyle.Bottom;
            uiPanel6.FillColor = Color.White;
            uiPanel6.FillColor2 = Color.White;
            uiPanel6.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiPanel6.ForeColor = Color.FromArgb(46, 46, 46);
            uiPanel6.Location = new Point(0, 797);
            uiPanel6.Margin = new Padding(4, 5, 4, 5);
            uiPanel6.MinimumSize = new Size(1, 1);
            uiPanel6.Name = "uiPanel6";
            uiPanel6.Radius = 10;
            uiPanel6.RectColor = Color.White;
            uiPanel6.RectDisableColor = Color.White;
            uiPanel6.Size = new Size(900, 59);
            uiPanel6.TabIndex = 1;
            uiPanel6.Text = null;
            uiPanel6.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // inputNumber
            // 
            inputNumber.BackColor = Color.FromArgb(218, 220, 230);
            inputNumber.Font = new Font("微软雅黑", 14F, FontStyle.Bold);
            inputNumber.Location = new Point(175, 14);
            inputNumber.Name = "inputNumber";
            inputNumber.Size = new Size(116, 37);
            inputNumber.TabIndex = 494;
            inputNumber.Text = "5";
            inputNumber.TextAlign = HorizontalAlignment.Center;
            inputNumber.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // btnPageDown
            // 
            btnPageDown.FillDisableColor = Color.FromArgb(80, 160, 255);
            btnPageDown.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnPageDown.ForeDisableColor = Color.White;
            btnPageDown.Location = new Point(297, 14);
            btnPageDown.MinimumSize = new Size(1, 1);
            btnPageDown.Name = "btnPageDown";
            btnPageDown.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnPageDown.Size = new Size(116, 37);
            btnPageDown.Symbol = 0;
            btnPageDown.TabIndex = 490;
            btnPageDown.Text = "下翻";
            btnPageDown.TipsFont = new Font("宋体", 11F);
            btnPageDown.Click += BtnPageDown_Click;
            // 
            // btnPageUp
            // 
            btnPageUp.FillDisableColor = Color.FromArgb(70, 75, 85);
            btnPageUp.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnPageUp.ForeDisableColor = Color.White;
            btnPageUp.Location = new Point(37, 14);
            btnPageUp.MinimumSize = new Size(1, 1);
            btnPageUp.Name = "btnPageUp";
            btnPageUp.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnPageUp.Size = new Size(116, 37);
            btnPageUp.Symbol = 0;
            btnPageUp.TabIndex = 489;
            btnPageUp.Text = "上翻";
            btnPageUp.TipsFont = new Font("宋体", 11F);
            btnPageUp.Click += BtnPageUp_Click;
            // 
            // btnPrintReport
            // 
            btnPrintReport.FillDisableColor = Color.FromArgb(80, 160, 255);
            btnPrintReport.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnPrintReport.ForeDisableColor = Color.White;
            btnPrintReport.Image = (Image)resources.GetObject("btnPrintReport.Image");
            btnPrintReport.Location = new Point(748, 14);
            btnPrintReport.MinimumSize = new Size(1, 1);
            btnPrintReport.Name = "btnPrintReport";
            btnPrintReport.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnPrintReport.Size = new Size(116, 37);
            btnPrintReport.Symbol = 0;
            btnPrintReport.TabIndex = 488;
            btnPrintReport.Text = "打印报表";
            btnPrintReport.TipsFont = new Font("宋体", 11F);
            btnPrintReport.Click += btnPrintReport_Click;
            // 
            // btnSaveReport
            // 
            btnSaveReport.FillDisableColor = Color.FromArgb(80, 160, 255);
            btnSaveReport.Font = new Font("思源黑体 CN Bold", 13F, FontStyle.Bold);
            btnSaveReport.ForeDisableColor = Color.White;
            btnSaveReport.Image = (Image)resources.GetObject("btnSaveReport.Image");
            btnSaveReport.Location = new Point(592, 14);
            btnSaveReport.MinimumSize = new Size(1, 1);
            btnSaveReport.Name = "btnSaveReport";
            btnSaveReport.RectDisableColor = Color.FromArgb(80, 160, 255);
            btnSaveReport.Size = new Size(116, 37);
            btnSaveReport.Symbol = 0;
            btnSaveReport.TabIndex = 487;
            btnSaveReport.Text = "保存报表";
            btnSaveReport.TipsFont = new Font("宋体", 11F);
            btnSaveReport.Click += btnSave_Click;
            // 
            // panelReport
            // 
            panelReport.BackColor = Color.FromArgb(236, 236, 236);
            panelReport.Dock = DockStyle.Top;
            panelReport.FillColor = Color.FromArgb(236, 236, 236);
            panelReport.FillColor2 = Color.FromArgb(236, 236, 236);
            panelReport.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            panelReport.Location = new Point(0, 0);
            panelReport.Margin = new Padding(4, 5, 4, 5);
            panelReport.MinimumSize = new Size(1, 1);
            panelReport.Name = "panelReport";
            panelReport.Padding = new Padding(1);
            panelReport.Radius = 0;
            panelReport.RectColor = Color.Transparent;
            panelReport.RectDisableColor = Color.FromArgb(236, 236, 236);
            panelReport.Size = new Size(900, 792);
            panelReport.TabIndex = 399;
            panelReport.Text = null;
            panelReport.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // tabPage3
            // 
            tabPage3.BackColor = Color.FromArgb(42, 47, 55);
            tabPage3.Controls.Add(grpRainy);
            tabPage3.Dock = DockStyle.Fill;
            tabPage3.Location = new Point(0, 0);
            tabPage3.Name = "tabPage3";
            tabPage3.Showed = true;
            tabPage3.Size = new Size(900, 856);
            tabPage3.TabIndex = 0;
            tabPage3.Text = "工艺界面";
            // 
            // grpRainy
            // 
            grpRainy.BackColor = Color.FromArgb(236, 236, 237);
            grpRainy.Dock = DockStyle.Fill;
            grpRainy.FillColor = Color.FromArgb(236, 236, 237);
            grpRainy.FillColor2 = Color.FromArgb(236, 236, 237);
            grpRainy.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            grpRainy.Location = new Point(0, 0);
            grpRainy.Margin = new Padding(4, 5, 4, 5);
            grpRainy.MinimumSize = new Size(1, 1);
            grpRainy.Name = "grpRainy";
            grpRainy.Radius = 0;
            grpRainy.RectColor = Color.FromArgb(236, 236, 237);
            grpRainy.RectDisableColor = Color.FromArgb(236, 236, 237);
            grpRainy.Size = new Size(900, 856);
            grpRainy.TabIndex = 521;
            grpRainy.Text = null;
            grpRainy.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // tabs1
            // 
            tabs1.BackColor = Color.FromArgb(236, 236, 237);
            tabs1.Controls.Add(tabPage3);
            tabs1.Controls.Add(tabPage1);
            tabs1.Controls.Add(tabPageTestDetails);
            tabs1.Location = new Point(257, 33);
            tabs1.Name = "tabs1";
            tabs1.Pages.Add(tabPage3);
            tabs1.Pages.Add(tabPage1);
            tabs1.Pages.Add(tabPageTestDetails);
            tabs1.ScrollForeHover = SystemColors.ActiveBorder;
            tabs1.Size = new Size(900, 856);
            tabs1.Style = styleLine1;
            tabs1.TabIndex = 405;
            tabs1.TabMenuVisible = false;
            // 
            // tabPageTestDetails
            // 
            tabPageTestDetails.Dock = DockStyle.Fill;
            tabPageTestDetails.Location = new Point(0, 0);
            tabPageTestDetails.Name = "tabPageTestDetails";
            tabPageTestDetails.Size = new Size(900, 856);
            tabPageTestDetails.TabIndex = 2;
            tabPageTestDetails.Text = "tabPage2";
            // 
            // btnTestDetails
            // 
            btnTestDetails.BackActive = Color.FromArgb(196, 199, 204);
            btnTestDetails.BackColor = Color.FromArgb(196, 199, 204);
            btnTestDetails.BorderWidth = 1F;
            btnTestDetails.Font = new Font("微软雅黑", 12.5F, FontStyle.Bold);
            btnTestDetails.ForeColor = Color.White;
            btnTestDetails.JoinMode = TJoinMode.Right;
            btnTestDetails.Location = new Point(507, 0);
            btnTestDetails.Name = "btnTestDetails";
            btnTestDetails.Size = new Size(124, 35);
            btnTestDetails.TabIndex = 495;
            btnTestDetails.Text = "试验详情";
            btnTestDetails.Type = TTypeMini.Primary;
            btnTestDetails.WaveSize = 1;
            btnTestDetails.Click += btnTestDetails_Click;
            // 
            // UcHMI
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(236, 236, 236);
            Controls.Add(btnTestDetails);
            Controls.Add(panelHand);
            Controls.Add(uiTitlePanel4);
            Controls.Add(tabs1);
            Controls.Add(btnReportForms);
            Controls.Add(uiTitlePanel8);
            Controls.Add(uiTitlePanel3);
            Controls.Add(btnStopTest);
            Controls.Add(btnStartTest);
            Controls.Add(btnWorkmanshipForms);
            Font = new Font("宋体", 11F);
            Margin = new Padding(4);
            Name = "UcHMI";
            Size = new Size(1155, 889);
            uiTitlePanel3.ResumeLayout(false);
            uiTitlePanel8.ResumeLayout(false);
            uiTitlePanel4.ResumeLayout(false);
            uiTitlePanel4.PerformLayout();
            panelHand.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            uiPanel6.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tabs1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private UITextBox uiTextBox1;
        private UITextBox uiTextBox2;
        private UITextBox uiTextBox3;
        private UITextBox uiTextBox4;
        private UITextBox uiTextBox8;
        private UITextBox uiTextBox10;
        private UITextBox uiTextBox12;
        private UITextBox uiTextBox13;
        private UITextBox uiTextBox14;
        private UITextBox uiTextBox15;
        private UITextBox uiTextBox11;
        private UITextBox uiTextBox19;
        private UIButton btnProductSelection;
        private UILabel uiLabel3;
        private UILabel uiLabel2;
        private UIButton btnStartTest;
        private UITitlePanel uiTitlePanel3;
        private UITitlePanel uiTitlePanel8;
        private UIButton btnStopTest;
        private AntdUI.Button btnWorkmanshipForms;
        private AntdUI.Button btnReportForms;
        private UITitlePanel uiTitlePanel4;
        private UITextBox txtDrawingNo;
        private UITextBox txtModel;
        private UIPanel LabTestTime;
        private UIRadioButton RadioHand;
        private UIRadioButton RadioAuto;
        private UIPanel panelHand;
        private AntdUI.Table TableItemPoint;
        private UITextBox txtType;
        private UILabel uiLabel7;
        private AntdUI.TabPage tabPage1;
        private UIPanel uiPanel6;
        private InputNumber inputNumber;
        private UISymbolButton btnPageDown;
        private UISymbolButton btnPageUp;
        private UISymbolButton btnPrintReport;
        private UISymbolButton btnSaveReport;
        private UIPanel panelReport;
        private AntdUI.TabPage tabPage3;
        private UIPanel grpRainy;
        private Tabs tabs1;
        private UILabel uiLabel5;
        private UITextBox uiTextBox5;
        private UILabel uiLabel1;
        private UITextBox uiTextBox6;
        private AntdUI.TabPage tabPageTestDetails;
        private AntdUI.Button btnTestDetails;
    }
}
