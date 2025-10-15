using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainUI.CurrencyHelper;
using RW.Modules;

namespace MainUI.Modules
{
    public partial class OpcStatusGrp : BaseModule
    {
        public OpcStatusGrp()
        {
            this.Driver = OPCHelper.opcStatusGroup;
            InitializeComponent();
        }
        public OpcStatusGrp(IContainer container)
              : base(container)
        {
            this.Driver = OPCHelper.opcStatusGroup;
        }
        [DefaultValue(false)]
        public bool NoError { get; set; }
        public bool Simulated { get; set; }
        public override void Init()
        {
            AddListening("_System._NoError", delegate (bool value) { NoError = value; });
            AddListening("_System._Simulated", delegate (bool value) { Simulated = value; });
            base.Init();
        }
    }
}
