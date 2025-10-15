using RW.Modules;
using System.ComponentModel;

namespace MainUI.Modules
{
    public partial class DIGrp : BaseModule
    {
        public DIGrp()
        {
            this.Driver = OPCHelper.opcDIGroup;
            InitializeComponent();
        }

        public DIGrp(IContainer container)
                        : base(container)
        {
            this.Driver = OPCHelper.opcDIGroup;
        }
        private const int DIcnt = 46;
        private bool[] _diList = new bool[DIcnt];
        public bool this[int index]
        {
            get { return _diList[index]; }
            set
            {
                string tag = index.ToString().PadLeft(2, '0');
                Write("DI.MDI" + tag, value);
            }
        }

        public bool[] DiList
        {
            get { return _diList; }
        }

        public void Fresh()
        {
            for (int i = 0; i < _diList.Length; i++)
            {
                DIGroupChanged?.Invoke(this, i, _diList[i]);
            }
        }
        public event ValueGroupHandler<bool> DIGroupChanged;
        public override void Init()
        {
            for (int i = 0; i < DIcnt; i++)
            {
                int idx = i; // 循环中的i需要用临时变量存储。
                string opcTag = "DI.MDI" + i.ToString().PadLeft(3, '0');
                AddListening(opcTag, delegate (bool value)
                {
                    //数组赋值
                    _diList[idx] = value;
                    DIGroupChanged?.Invoke(this, idx, value);
                });
            }
            base.Init();
        }
    }
}
