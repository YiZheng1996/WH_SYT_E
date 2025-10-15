using System.ComponentModel;

namespace MainUI.Procedure.Controls
{
    public partial class UcSwitch : UserControl
    {
        public UcSwitch()
        {
            InitializeComponent();
            label1.Parent = Parent;
            label1.AutoSize = true;
        }

        private UISwitch _uiSwitch;
        public UISwitch UISwitch
        {
            get => _uiSwitch;
            set
            {
                _uiSwitch = value;
            }
        }

        private int index;
        [DefaultValue(0)]
        [Description("获取或设置相关的Index值")]
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        [Browsable(true)]
        [Bindable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("获取或设置标题文字")]
        public override string Text
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        private int borderWidth = 2;
        [DefaultValue(2)]
        [Description("文字距离控件的宽度/高度")]
        public int BorderWidth
        {
            get { return borderWidth; }
            set { borderWidth = value; Refresh(); }
        }

        private TextLayout textLayout = TextLayout.None;

        [DefaultValue(TextLayout.None)]
        [Description("获取或设置文字显示位置")]
        public TextLayout TextLayout
        {
            get { return textLayout; }
            set
            {
                textLayout = value;
                if (value == TextLayout.None)
                    label1.Visible = false;
                else
                {
                    label1.Visible = true;
                    label1.BringToFront();
                    BringToFront();
                }
                Refresh();
            }
        }

        [Browsable(true)]
        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                label1.Font = value;
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            //如果有父容器，设置label标签的位置
            if (Parent != null)
            {
                float x = 0, y = 0;
                switch (TextLayout)
                {
                    case TextLayout.Top:
                        //TOP
                        x = Location.X + (Width - label1.Width) / 2;
                        y = Location.Y - label1.Height - borderWidth;
                        break;
                    case TextLayout.Left:
                        //Left
                        x = Location.X - label1.Width - borderWidth;
                        y = Location.Y + (Height - label1.Height) / 2;
                        break;
                    case TextLayout.Bottom:
                        x = Location.X + (Width - label1.Width) / 2;
                        y = Location.Y + Height + borderWidth;
                        break;
                    case TextLayout.Right:
                        x = Location.X + Width + borderWidth;
                        y = Location.Y + (Height - label1.Height) / 2;
                        break;
                    case TextLayout.None:
                    default:
                        break;
                }
                if (TextLayout != TextLayout.None)
                {
                    label1.Location = new Point((int)x, (int)y);
                }
            }
            //档Enabled为false时，划斜线，表示禁用
            if (!Enabled)
            {
                pe.Graphics.DrawLine(Pens.Red, 0, 0, Width, Height);
            }
            //鼠标悬停样式
            if (Enabled)
            {
                pe.Graphics.DrawRectangle(Pens.Gray, 0, 0, Width - 1, Height - 1);
            }
            base.OnPaint(pe);
        }

    }

}
