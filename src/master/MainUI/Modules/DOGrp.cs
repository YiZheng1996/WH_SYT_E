using RW.Modules;
using System.ComponentModel;

namespace MainUI.Modules
{
    public partial class DOGrp : BaseModule
    {
        public DOGrp()
        {
            this.Driver = OPCHelper.opcDOGroup;
            InitializeComponent();
        }

        public DOGrp(IContainer container)
            : base(container)
        {
            this.Driver = OPCHelper.opcDOGroup;
        }
        public const int DOCount = 42;

        private bool[] _doList = new bool[DOCount];
        public bool[] DOlist
        {
            get { return _doList; }
        }
        // 对象索引器，电磁阀数组
        public bool this[int index]
        {
            get
            {
                return DOlist[index];
            }
            set
            {
                try
                {
                    string tag = index.ToString().PadLeft(3, '0');
                    this.Write("DO.MDO" + tag, value);
                }
                catch (Exception ex)
                {
                    NlogHelper.Default.Error($"写入 DO[{index}] = {value} 失败", ex);
                }
            }
        }
        public void Fresh()
        {
            for (int i = 0; i < _doList.Length; i++)
            {
                DOgrpChanged?.Invoke(this, i, _doList[i]);
            }
        }
        public event ValueGroupHandler<bool> DOgrpChanged;
        public override void Init()
        {
            for (int i = 0; i < DOCount; i++)
            {
                int idx = i;
                string tag = "DO.MDO" + i.ToString().PadLeft(3, '0');
                AddListening(tag, delegate (bool DOvalue)
                {
                    _doList[idx] = DOvalue;
                    DOgrpChanged?.Invoke(this, idx, DOvalue);
                });
            }
            base.Init();
        }
    }
}
