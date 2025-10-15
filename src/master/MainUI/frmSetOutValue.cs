using System.ComponentModel;

namespace MainUI
{
    public partial class frmSetOutValue : UIForm
    {
        private double _OutValue = 0.0;
        /// <summary>
        /// 输出返回值
        /// </summary>
        [Description("返回设定值")]
        [DefaultValue(0)]
        public double OutValue
        {
            get { return _OutValue; }
            set { _OutValue = value; }
        }
        public frmSetOutValue()
        {
            InitializeComponent();

        }
        /// <summary>
        /// 初始化模拟量输出窗体
        /// <para>origValue 原始值</para>
        /// <para>title 显示文本</para>
        /// <para>maxValue 最大值</para>
        /// </summary>
        public frmSetOutValue(double origValue, string title, double maxValue)
        {
            InitializeComponent();
            nudOutputValue.Maximum = maxValue;
            if (origValue > maxValue)
                origValue = maxValue;
            nudOutputValue.Text = origValue.ToString("f2");
            this.Text = title + "[最大值 " + maxValue + "]";
            OutValue = origValue;
            nudOutputValue.Focus();
            nudOutputValue.SelectAll();
            MaxValue = maxValue;
        }
        double MaxValue = 0;
        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            OutValue = Convert.ToDouble(nudOutputValue.Text);
            if (OutValue > MaxValue)
                OutValue = MaxValue;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void nudOutputValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Btn_Ok_Click(null, null);

            }
        }

    }
}
