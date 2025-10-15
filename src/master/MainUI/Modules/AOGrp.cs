using RW.Modules;
using System.ComponentModel;

namespace MainUI.Modules
{
    public partial class AOGrp : BaseModule
    {
        private const int AOcount = 3;
        private readonly double[] _AOList = new double[AOcount];
        public double[] AOList => _AOList;
        public double this[int index] => AOList[index];
        public event ValueGroupHandler<double> AOvalueGrpChaned;
        public event ValueListHandler<double> AOvalueListChanged;
        public AOGrp()
        {
            Driver = OPCHelper.opcAOGroup;
            InitializeComponent();
        }

        public AOGrp(IContainer container)
            : base(container)
        {
            Driver = OPCHelper.opcAOGroup;
        }

        public void Fresh()
        {
            for (int i = 0; i < _AOList.Length; i++)
            {
                AOvalueGrpChaned?.Invoke(this, i, _AOList[i]);
            }
        }

        private double _ca00 = 0;
        public double CA00 { get { return _ca00; } set { _ca00 = value; Write("AO.CA00", value); } }

        private double _ca01 = 0;
        public double CA01 { get { return _ca01; } set { _ca01 = value; Write("AO.CA01", value); } }

        private double _ca02 = 0;
        public double CA02 { get { return _ca02; } set { _ca02 = value; Write("AO.CA02", value); } }

        public override void Init()
        {
            for (int i = 0; i < AOcount; i++)
            {
                int idx = i;
                string opcTag = "AO.MAQ" + i.ToString().PadLeft(3, '0');
                AddListening(opcTag, delegate (double value)
                {
                    _AOList[idx] = value;
                    AOvalueListChanged?.Invoke(this, _AOList);
                    AOvalueGrpChaned?.Invoke(this, idx, value);
                });
            }
            base.Init();
        }
    }
}
