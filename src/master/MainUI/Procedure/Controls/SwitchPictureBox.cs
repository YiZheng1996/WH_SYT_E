using System.ComponentModel;

namespace MainUI.Procedure.Controls
{
    /// <summary>
    /// 可自定义两种图片的控件，支持Switch切换
    /// </summary>
    public partial class SwitchPictureBox : PictureBox, ISupportInitialize
    {
        public SwitchPictureBox()
        {
            ParentChanged += new EventHandler(SwitchPictureBox_ParentChanged);
            SwitchChanged += new SwitchHandler(SwitchPictureBox_SwitchChanged);
            label1 = new()
            {
                AutoSize = true,
                Visible = false,
                Parent = Parent
            };
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            label1.Visible = Visible;
            base.OnVisibleChanged(e);
        }
        void SwitchPictureBox_ParentChanged(object sender, EventArgs e)
        {
            label1.Parent = Parent;
            label1.BringToFront();
            BringToFront();
            //throw new Exception("The method or operation is not implemented.");
        }

        private Label label1;

        private bool canClick = true;
        /// <summary>
        /// 是否可点击
        /// </summary>
        [Description("获取或设置是否可以由用户点击")]
        public virtual bool CanClick
        {
            get { return canClick; }
            set
            {
                canClick = value;
                if (value)
                    Cursor = Cursors.Hand;
                else
                    Cursor = Cursors.Default;
            }
        }

        private bool clickSwitch = true;
        /// <summary>
        /// 获取或设置点击时是否触发开关，默认可触发
        /// </summary>
        [DefaultValue(true)]
        [Description(" 获取或设置点击时是否触发开关，默认可触发")]
        public virtual bool ClickSwitch
        {
            get { return clickSwitch; }
            set { clickSwitch = value; }
        }

        void SwitchPictureBox_SwitchChanged(object sender, bool obj)
        {
            Image = obj ? TrueImage : FalseImage;
        }

        private bool _switch;
        /// <summary>
        /// 打开或关闭开关
        /// </summary>
        [Description("打开或关闭开关")]
        public virtual bool Switch
        {
            get { return _switch; }
            set
            {
                _switch = value;
                OnSwitchChanged(value);
            }
        }

        /// <summary>
        /// 打开开关
        /// </summary>
        public virtual void On()
        {
            Switch = true;
        }

        /// <summary>
        /// 关闭开关
        /// </summary>
        public virtual void Off()
        {
            Switch = false;
        }

        private Image trueImage;
        /// <summary>
        /// 开关打开时的图片
        /// </summary>
        [DefaultValue(null)]
        [Description("开关打开时的图片")]
        [Localizable(false)]
        public virtual Image TrueImage
        {
            get { return trueImage; }
            set
            {
                trueImage = value;
                if (Switch) Image = value;
            }
        }

        private Image falseImage;
        /// <summary>
        /// 开关关闭时的图片
        /// </summary>
        [DefaultValue(null)]
        [Description("开关关闭时的图片")]
        [Localizable(false)]
        public virtual Image FalseImage
        {
            get { return falseImage; }
            set
            {
                falseImage = value;
                if (!Switch) Image = value;
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

        private int borderWidth = 2;
        [DefaultValue(2)]
        [Description("文字距离控件的宽度/高度")]
        public int BorderWidth
        {
            get { return borderWidth; }
            set { borderWidth = value; Refresh(); }
        }

        [Browsable(false)]
        public override Color ForeColor => label1.ForeColor = Color.FromArgb(64, 64, 64);
        //{
        //    get { return label1.ForeColor; }
        //    set { label1.ForeColor = value; }
        //}

        [DefaultValue(typeof(Color), "Control")]
        public Color TextBackColor
        {
            get { return label1.BackColor; }
            set { label1.BackColor = value; }
        }

        private bool asyncClick = true;

        [DefaultValue(true)]
        [Description("是否支持异步点击")]
        public bool AsyncClick
        {
            get { return asyncClick; }
            set { asyncClick = value; }
        }

        private bool enableStatus;
        [DefaultValue(false)]
        [Description("打开或关闭设置Enabled时的状态，档Enabled为false控件用斜线表示")]
        public bool EnableStatus
        {
            get { return enableStatus; }
            set { enableStatus = value; }
        }

        delegate void D(EventArgs e);
        protected override void OnClick(EventArgs e)
        {
            if (!CanClick)
                return;
            if (clickSwitch)
                Switch = !Switch;

            D d = base.OnClick;
            if (AsyncClick)
                ControlHelper.ButtonClickAsync(this, delegate { d(e); });
            else
                d(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
        }

        bool isEnter = false;
        protected override void OnMouseEnter(EventArgs e)
        {
            isEnter = true;
            Refresh();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            isEnter = false;
            Refresh();
            base.OnMouseLeave(e);
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
            if (EnableStatus && !Enabled)
            {
                pe.Graphics.DrawLine(Pens.Red, 0, 0, Width, Height);
            }
            //鼠标悬停样式
            if (isEnter && Enabled && CanClick)
            {
                pe.Graphics.DrawRectangle(Pens.Gray, 0, 0, Width - 1, Height - 1);
            }
            base.OnPaint(pe);
        }

        protected virtual void OnSwitchChanged(bool value)
        {
            SwitchChanged?.Invoke(this, value);
        }

        public event SwitchHandler SwitchChanged;

        #region ISupportInitialize 成员

        void ISupportInitialize.BeginInit()
        {
        }

        void ISupportInitialize.EndInit()
        {
        }

        #endregion

        #region ITagSwitch 成员

        [DefaultValue(null)]
        public string InputDriverName { get; set; }

        [DefaultValue(null)]
        public string InputTagName { get; set; }

        [DefaultValue(null)]
        public string OutputDriverName { get; set; }

        [DefaultValue(null)]
        public string OutputTagName { get; set; }

        #endregion

        #region IClickable 成员

        private int clickType;
        /// <summary>
        /// 用于描述点击类型，0表示同步写，1表示脉冲写
        /// </summary>
        [DefaultValue(0)]
        public int ClickType
        {
            get { return clickType; }
            set { clickType = value; }
        }


        #endregion
    }

    /// <summary>
    /// 用于描述文字的显示位置
    /// </summary>
    public enum TextLayout
    {
        None,
        Top,
        Left,
        Bottom,
        Right
    }
}
