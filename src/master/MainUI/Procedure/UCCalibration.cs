using RW.Modules;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MainUI.Procedure
{
    public delegate void DoubleValueHandler(object sender, double value);
    [ComVisible(true)]
    [ClassInterface(1)]
    [ToolboxItemFilter("System.Windows.Forms")]
    [DefaultEvent("Submited")]
    [DebuggerDisplay("Index={Index},Text={Text},Value=[{Value},{ValueDecimalPlaces}],Gain=[{GainValue},{GainDecimalPlaces}],Zero=[{ZeroValue},{ZeroDecimalPlaces}]")]
    public partial class UCCalibration : UserControl, IIndexValue
    {
        public UCCalibration()
        {
            InitializeComponent();

            // 使用这个属性来区分设计时和运行时
            if (!DesignMode)
            {
                try
                {
                    Control.CheckForIllegalCrossThreadCalls = false;

                    //InitData();
                    //patch:由于rw.ini文件移植到config/rw.ini中，此处增加代码，用于自动移植
                    if (!Directory.Exists(Application.StartupPath + "\\config"))
                    {
                        Directory.CreateDirectory(Application.StartupPath + "\\config");
                        //自动移植文件到config目录下
                        if (File.Exists(Application.StartupPath + "\\rw.ini"))
                            File.Move(Application.StartupPath + "\\rw.ini", Application.StartupPath + "\\config\\rw.ini");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }

        private bool asyncClick = true;
        [DefaultValue(true)]
        public bool AsyncClick { get { return asyncClick; } set { asyncClick = value; } }

        //public event DoubleValueHandler CurrentValueChanged;
        //public event DoubleValueHandler ZeroValueChanged;
        //public event DoubleValueHandler GainValueChanged;

        [DefaultValue(true)]
        public override bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                base.AutoSize = value;
            }
        }

        [Browsable(true)]
        [Localizable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get { return this.lblText.Text; }
            set { this.lblText.Text = value; this.Refresh(); }
        }

        [Localizable(true)]
        [Obsolete("请使用Text属性")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Title
        {
            get { return this.lblText.Text; }
            set { this.lblText.Text = value; }
        }

        [DefaultValue(0d)]
        [Description("当前值")]
        public virtual double Value
        {
            get { return Math.Round(Convert.ToDouble(this.txtValue.Text), this.ValueDecimalPlaces); }
            set { this.txtValue.Text = Math.Round(value, this.ValueDecimalPlaces).ToString(); }
        }

        [DefaultValue(0d)]
        [Description("零点值")]
        public virtual double ZeroValue
        {
            get
            {
                string temp = "";
                if (this.nudZeroValue.Text == "" || this.nudZeroValue.Text == "-")
                    temp = "0";
                else
                    temp = this.nudZeroValue.Text;
                return Convert.ToDouble(temp);
            }
            set
            {
                this.nudZeroValue.Text = value.ToString();
                SubmitArgs.Zero = value;
            }
        }

        [DefaultValue(1d)]
        [Description("增益值")]
        public virtual double GainValue
        {
            get
            {
                string temp = "";
                if (this.nudGainValue.Text == "" || this.nudGainValue.Text == "-")
                    temp = "0";
                else
                    temp = this.nudGainValue.Text;
                return Convert.ToDouble(temp);
            }
            set
            {
                this.nudGainValue.Text = value.ToString();
                SubmitArgs.Gain = value;
            }
        }

        private int maxIndex = 1;
        [DefaultValue(1)]
        [Description("最大的索引号，从1开始，最大999")]
        public int MaxIndex
        {
            get { return maxIndex; }
            set { maxIndex = value; SetMaxIndex(value); }
        }

        void SetMaxIndex(int value)
        {
            this.cboChannel.Items.Clear();
            for (int i = 1; i <= value; i++)
                this.cboChannel.Items.Add(i);
            //this.cboChannel.Text = this.ChannelIndex.ToString();
            //Debug.WriteLine("value:" + this.ChannelIndex);
            //MessageBox.Show(this.ChannelIndex.ToString());
            //this.cboChannel.Text = this.Index.ToString();
            //this.cboChannel.SelectedItem = this.Index;
        }

        [DefaultValue(0)]
        [Description("用于多回路时的索引号，不用可不设置")]
        public virtual int Index
        {
            get
            {
                int value;
                int.TryParse(this.cboChannel.Text, out value);
                return value - 1;
            }
            set
            {
                this.cboChannel.Text = (value + 1).ToString();
                SubmitArgs.Index = value;
                OnIndexChanged(value);
            }
        }

        private bool enableChannel = false;
        [DefaultValue(false)]
        [Description("是否显示Index的选项")]
        public bool EnableChannel
        {
            get { return enableChannel; }
            set { enableChannel = value; this.cboChannel.Visible = value; }
        }



        //private BaseReadModule module;
        //[DefaultValue(null)]
        //public BaseReadModule Module
        //{
        //    get { return module; }
        //    set { module = value; }
        //}
        private string sectionName = null;
        [DefaultValue(null)]
        [Description("用于保存到Ini中的Section名称")]
        [System.ComponentModel.Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual string SectionName
        {
            get
            {
                //return Text + "_" + this.Name;
                if (sectionName == null)
                {
                    if (NameTypes == SectionNameTypes.Auto)
                        sectionName = this.GetType().Name + "_" + this.Name;
                    else if (NameTypes == SectionNameTypes.Text)
                        sectionName = this.Text;
                    else if (nameTypes == SectionNameTypes.Custom)
                        sectionName = this.CustomName;
                    if (!string.IsNullOrEmpty(FormatName))
                        sectionName = string.Format(FormatName, sectionName);
                }
                return sectionName;
            }
            set { sectionName = value; }
        }

        private SectionNameTypes nameTypes = SectionNameTypes.Auto;
        /// <summary>
        /// 配置文件的名称组合方式
        /// </summary>
        [DefaultValue(SectionNameTypes.Auto)]
        public SectionNameTypes NameTypes
        {
            get { return nameTypes; }
            set { nameTypes = value; }
        }

        private string customName = "";
        /// <summary>
        /// 自定义配置的名称
        /// </summary>
        [DefaultValue("")]
        public string CustomName
        {
            get { return customName; }
            set { customName = value; }
        }

        private string formatName = "{0}";
        [DefaultValue("{0}")]
        public string FormatName
        {
            get { return formatName; }
            set { formatName = value; }
        }

        /// <summary>
        /// 初始化零点增益值和Index值
        /// </summary>
        public virtual void InitData()
        {
            if (DesignMode) return;
            if (EnableGainConfig)
            {
                double gain = config.Config.GetDouble(SectionName, "Gain", 1);
                this.GainValue = gain;
            }
            if (EnableZeroConfig)
            {
                double zero = config.Config.GetDouble(SectionName, "Zero", 0);
                this.ZeroValue = zero;
            }
            if (EnableChannel)
            {
                int index = config.Config.GetInt(SectionName, "Index", this.Index);
                this.Index = index;
            }
        }

        private bool enableGainConfig = true;
        [DefaultValue(true)]
        [Description("是否开启Gain参数从config文件的读写。")]
        public bool EnableGainConfig
        {
            get { return enableGainConfig; }
            set { enableGainConfig = value; }
        }

        private bool enableZeroConfig = true;
        [DefaultValue(true)]
        [Description("是否开启Zero参数从config文件的读写。")]
        public bool EnableZeroConfig
        {
            get { return enableZeroConfig; }
            set { enableZeroConfig = value; }
        }

        private int valueDecimalPlaces = 4;
        /// <summary>
        /// 值的小数位数量，默认为4
        /// </summary>
        [Description("值的小数位数量，默认为4")]
        [DefaultValue(4)]
        public int ValueDecimalPlaces
        {
            get { return valueDecimalPlaces; }
            set { valueDecimalPlaces = value; }
        }

        /// <summary>
        /// 零点的小数位数量，默认为2
        /// </summary>
        [Description("零点的小数位数量，默认为2")]
        [DefaultValue(2)]
        public int ZeroDecimalPlaces
        {
            get { return this.nudZeroValue.DecimalPlaces; }
            set { this.nudZeroValue.DecimalPlaces = value; }
        }

        /// <summary>
        /// 增益的小数位数量，默认为3
        /// </summary>
        [Description("增益的小数位数量，默认为3")]
        [DefaultValue(3)]
        public int GainDecimalPlaces
        {
            get { return this.nudGainValue.DecimalPlaces; }
            set { this.nudGainValue.DecimalPlaces = value; }
        }

        private bool touchMode;
        [DefaultValue(false)]
        [Description("触摸板模式")]
        public bool TouchMode { get { return touchMode; } set { touchMode = value; } }

        private void UCCalibration_Load(object sender, EventArgs e)
        {

        }

        protected IniConfig config = new(Application.StartupPath + "\\config\\rw.ini");

        void module_ValueChanged(object sender, double value)
        {
            this.Value = value;
        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            OnValueChanged(this.Value);
        }

        private void nudGainValue_ValueChanged(object sender, EventArgs e)
        {
            SubmitArgs.Gain = this.GainValue;
            OnGainValueChanged(this.GainValue);
        }

        private void nudZeroDisplacement_ValueChanged(object sender, EventArgs e)
        {
            SubmitArgs.Zero = this.ZeroValue;
            OnZeroValueChanged(this.ZeroValue);
        }

        private void cboIndex_TextChanged(object sender, EventArgs e)
        {
            this.SubmitArgs.Index = this.Index;
            this.OnIndexChanged(this.Index);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Reset();
        }

        public virtual void Reset()
        {
            SubmitArgs.Gain = this.GainValue;
            SubmitArgs.Zero = this.ZeroValue;
            this.nudGainValue.Text = 1.0M.ToString();
            this.nudZeroValue.Text = 0M.ToString();
        }

        #region ISupportInitialize 成员

        public void BeginInit()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public void EndInit()
        {
            //this.Index = this.Index;
        }

        #endregion


        private SubmitArgs submitArgs = new();
        [Browsable(false)]
        public virtual SubmitArgs SubmitArgs { get { return submitArgs; } }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.AsyncClick)
                ControlHelper.ButtonClickAsync(this, delegate { OnSubmits(this, SubmitArgs); });
            else
                OnSubmits(this, SubmitArgs);
        }

        public virtual void Submit()
        {
            this.OnSubmits(this, SubmitArgs);
        }

        protected virtual void OnIndexChanged(int value)
        {
            IndexChanged?.Invoke(this, value);
        }

        protected virtual void OnGainValueChanged(double value)
        {
            GainValueChanged?.Invoke(this, value);
        }

        protected virtual void OnZeroValueChanged(double value)
        {
            ZeroValueChanged?.Invoke(this, value);
        }

        protected virtual void OnValueChanged(double value)
        {
            ValueChanged?.Invoke(this, value);
        }

        protected virtual void OnSubmits(object sender, SubmitArgs e)
        {
            SubmitArgs.Zero = e.Zero;
            SubmitArgs.Gain = e.Gain;
            SubmitArgs.Index = e.Index;
            if (EnableZeroConfig)
                config.Config.SetDouble(SectionName, "Zero", e.Zero);
            if (EnableGainConfig)
                config.Config.SetDouble(SectionName, "Gain", e.Gain);
            if (EnableChannel)
                config.Config.SetInt(SectionName, "Index", e.Index);
            if (e.Zero == 0)
                config.Config.RemoveKey(SectionName, "Zero");
            if (e.Gain == 1)
                config.Config.RemoveKey(SectionName, "Gain");
            if (e.Index == 0 || !EnableChannel)
                config.Config.RemoveKey(SectionName, "Index");
            Submited?.Invoke(sender, e);
        }

        public event ValueHandler<int> IndexChanged;
        public event EventHandler<SubmitArgs> Submited;

        #region IGroupIndex 成员


        public double this[int index]
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion

        #region IHardware 成员


        public event ValueHandler ValueChanged;

        public event ValueHandler GainValueChanged;

        public event ValueHandler ZeroValueChanged;

        #endregion

        private void cboChannel_Click(object sender, EventArgs e)
        {
            if (TouchMode)
                SelectorHelper.ShowDialog(sender);
        }

    }

    public enum SectionNameTypes
    {
        Auto = 0,
        Text = 1,
        Custom = 2,
    }
}
