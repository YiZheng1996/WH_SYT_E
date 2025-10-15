using RW.Modules;
using System.ComponentModel;

namespace MainUI.Modules
{
    /// <summary>
    /// 控制下位机动作点位
    /// </summary>
    public partial class TestConGrp : BaseModule
    {
        public TestConGrp()
        {
            this.Driver = OPCHelper.opcTestConGroup;
            InitializeComponent();
        }
        public TestConGrp(IContainer container)
            : base(container)
        {
            this.Driver = OPCHelper.opcTestConGroup;
            InitializeComponent();
        }
        /// <summary>
        /// 对象索引器，电磁阀数组
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public object this[int index]
        {
            get
            {
                return pubList[index];
            }
            set
            {
                try
                {
                    string tag = index.ToString().PadLeft(3, '0');
                    Write("TestCon.Test" + tag, value);
                }
                catch (Exception ex)
                {
                    NlogHelper.Default.Error($"写入 TestCon[{index}] = {value} 失败", ex);
                    // 不抛出异常，避免程序崩溃
                }
            }
        }
        public void Fresh()
        {
            for (int i = 0; i < pubList.Length; i++)
            {
                TestConGroupChanged?.Invoke(this, i, pubList[i]);
            }
        }
        private object[] pubList = new object[PUBcnt];

        private const int PUBcnt = 128;
        public event ValueGroupHandler<object> TestConGroupChanged;

        public override void Init()
        {
            for (int i = 0; i < PUBcnt; i++)
            {
                int idx = i; // 循环中的i需要用临时变量存储。
                string opcTag = "TestCon.Test" + i.ToString().PadLeft(3, '0');
                AddListening(opcTag, delegate (object value)
                {
                    pubList[idx] = value;
                    TestConGroupChanged?.Invoke(this, idx, value);
                });
            }
            base.Init();
        }

    }
}
