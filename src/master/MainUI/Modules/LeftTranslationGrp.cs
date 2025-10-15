using RW.Modules;
using System.ComponentModel;

namespace MainUI.Modules
{
    public partial class LeftTranslationGrp : BaseModule
    {
        private const int Count = 15;
        public event ValueGroupHandler<double> LeftTranslationvalueGrpChaned;
        public LeftTranslationGrp()
        {
            Driver = OPCHelper.opcLeftTranslation;
            InitializeComponent();
        }

        public LeftTranslationGrp(IContainer container)
            : base(container)
        {
            Driver = OPCHelper.opcLeftTranslation;
        }
        private double[] _TestList = new double[Count];
        public double[] TestList
        {
            get { return _TestList; }
        }
        public void Fresh()
        {
            for (int i = 0; i < _TestList.Length; i++)
            {
                LeftTranslationvalueGrpChaned?.Invoke(this, i, _TestList[i]);
            }
        }
        /// <summary>
        /// 0:温度; 1:湿度;
        /// </summary>
        public double this[int index]
        {
            get { return TestList[index]; }
            set { Write("CH" + index.ToString().PadLeft(2, '0'), value); }
        }

        public override void Init()
        {
            for (int i = 0; i < Count; i++)
            {
                int idx = i; // 循环中的i需要用临时变量存储。
                string opcTag = "CH" + i.ToString().PadLeft(2, '0');
                AddListening(opcTag, delegate (double value)
                {
                    _TestList[idx] = value;
                    LeftTranslationvalueGrpChaned?.Invoke(this, idx, value);
                });
            }
            base.Init();
        }
    }
}
