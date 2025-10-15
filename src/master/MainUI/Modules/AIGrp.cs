using RW.Modules;
using System.ComponentModel;

namespace MainUI.Modules
{
    public partial class AIGrp : BaseModule
    {
        public const int AIcount = 8;
        public Hardware[] hardwares = new Hardware[AIcount];
        private readonly double[] _AiList = new double[AIcount];
        public double[] AIList => _AiList;
        public double this[int index] => AIList[index];
        public event ValueListHandler<double> AIvalueListChanged;
        public event ValueGroupHandler<double> AIvalueGrpChanged;
        public AIGrp()
        {
            Driver = OPCHelper.opcAIGroup;
            InitializeComponent();
            for (int i = 0; i < hardwares.Length; i++)
            {
                hardwares[i] = new Hardware();
            }
        }
        public AIGrp(IContainer container)
            : base(container)
        {
            Driver = OPCHelper.opcAIGroup;
        }
        public void Fresh()
        {
            for (int i = 0; i < _AiList.Length; i++)
            {
                AIvalueGrpChanged?.Invoke(this, i, _AiList[i]);
            }
        }
        public override void Init()
        {
            for (int i = 0; i < AIcount; i++)
            {
                int idx = i; // 循环中的i需要用临时变量存储。
                string opcTag = "AI.MAI" + i.ToString().PadLeft(3, '0');

                AddListening(opcTag, delegate (double value)
                {
                    _AiList[idx] = value;
                    AIvalueListChanged?.Invoke(this, _AiList);
                    AIvalueGrpChanged?.Invoke(this, idx, value);
                });
            }
            base.Init();
        }

    }
}
