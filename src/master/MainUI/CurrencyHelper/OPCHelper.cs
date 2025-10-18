using MainUI.Modules;
using RW.Driver;

namespace MainUI.CurrencyHelper
{
    public class OPCHelper
    {
        #region OPCDriver
        public static OPCDriver opcAIGroup = new();
        public static OPCDriver opcAOGroup = new();
        public static OPCDriver opcDIGroup = new();
        public static OPCDriver opcDOGroup = new();
        public static OPCDriver opcStatusGroup = new();
        public static OPCDriver opcTestConGroup = new();
        public static OPCDriver opcWSD = new();
        #endregion

        #region opcGroup
        public static OpcStatusGrp opcStatus;
        public static AIGrp AIgrp;
        public static AOGrp AOgrp;
        public static DIGrp DIgrp;
        public static DOGrp DOgrp;
        public static TestConGrp TestCongrp;
        public static PLCCalibration plcc;
        public static WSDGrp WSDgrp;
        #endregion

        static OPCHelper()
        {
            string kepServerName = "KEPware.KEPServerEx.V4";
            opcDOGroup.ServerName = kepServerName;
            opcDOGroup.Prefix = "SMART.PLC.";
            opcDIGroup.ServerName = kepServerName;
            opcDIGroup.Prefix = "SMART.PLC.";
            opcAIGroup.ServerName = kepServerName;
            opcAIGroup.Prefix = "SMART.PLC.";
            opcAOGroup.ServerName = kepServerName;
            opcAOGroup.Prefix = "SMART.PLC.";
            opcStatusGroup.ServerName = kepServerName;
            opcStatusGroup.Prefix = "SMART.PLC.";
            opcTestConGroup.ServerName = kepServerName;
            opcTestConGroup.Prefix = "SMART.PLC.";
            opcWSD.ServerName = kepServerName;
            opcWSD.Prefix = "Modbus.WSD.";
        }

        /// <summary>
        /// OPC打开
        /// </summary>
        public static void Connect()
        {
            opcDOGroup.Connect();
            opcDIGroup.Connect();
            opcAIGroup.Connect();
            opcAOGroup.Connect();
            opcTestConGroup.Connect();
            opcStatusGroup.Connect();
            opcWSD.Connect();
        }

        /// <summary>
        /// OPC关闭
        /// </summary>
        public static void Close()
        {
            opcDOGroup.Close();
            opcDIGroup.Close();
            opcAIGroup.Close();
            opcAOGroup.Close();
            opcTestConGroup.Close();
            opcStatusGroup.Close();
            opcWSD.Close();
        }


        public static void Init()
        {
            AIgrp = new AIGrp();
            AOgrp = new AOGrp();
            DIgrp = new DIGrp();
            DOgrp = new DOGrp();
            TestCongrp = new TestConGrp();
            plcc = new PLCCalibration();
            opcStatus = new OpcStatusGrp();
            WSDgrp = new WSDGrp();

            opcStatus.Init();
            AIgrp.Init();
            AOgrp.Init();
            DIgrp.Init();
            DOgrp.Init();
            TestCongrp.Init();
            plcc.Init();
            WSDgrp.Init();
        }

    }
}
