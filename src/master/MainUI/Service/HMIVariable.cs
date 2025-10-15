namespace MainUI.Service
{
    /// <summary>
    /// 注册OPC组事件处理程序
    /// </summary>
    /// <param name="form">窗体控件</param>
    sealed class OPCEventRegistration(UcHMI form)
    {
        /// <summary>
        /// 注册所有OPC组事件处理程序
        /// </summary>
        public void RegisterAll()
        {
            RegisterAIGroup();
            RegisterDIGroup();
            RegisterAOGroup();
            RegisterDOGroup();
            RegisterCurrentGroup();
            RegisterTestConGroup();
        }

        private void RegisterAIGroup()
        {
            SafeRegister(() =>
            {
                OPCHelper.AIgrp.AIvalueGrpChanged += form.AIgrp_AIvalueGrpChanged;
                OPCHelper.AIgrp.Fresh();
            }, "AI组");
        }

        private void RegisterDIGroup()
        {
            SafeRegister(() =>
            {
                OPCHelper.DIgrp.DIGroupChanged += form.DIgrp_DIGroupChanged;
                OPCHelper.DIgrp.Fresh();
            }, "DI组");
        }

        private void RegisterAOGroup()
        {
            SafeRegister(() =>
            {
                OPCHelper.AOgrp.AOvalueGrpChaned += form.AOgrp_AOvalueGrpChanged;
                OPCHelper.AOgrp.Fresh();
            }, "AO组");
        }

        private void RegisterDOGroup()
        {
            SafeRegister(() =>
            {
                OPCHelper.DOgrp.DOgrpChanged += form.DOgrp_DOgrpChanged;
                OPCHelper.DOgrp.Fresh();
            }, "DO组");
        }

        private void RegisterCurrentGroup()
        {
            SafeRegister(() =>
            {
                OPCHelper.Currentgrp.CurrentvalueGrpChaned += form.Currentgrp_CurrentvalueGrpChaned;
                OPCHelper.Currentgrp.Fresh();
            }, "Current组");
        }

        private void RegisterTestConGroup()
        {
            SafeRegister(() =>
            {
                OPCHelper.TestCongrp.TestConGroupChanged += form.TestCongrp_TestConGroupChanged;
                OPCHelper.TestCongrp.Fresh();
            }, "TestCon组");
        }

        private static void SafeRegister(Action registration, string groupName)
        {
            try
            {
                registration();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"注册{groupName}事件失败:", ex);
            }
        }
    }

    /// <summary>
    /// 字典映射控件
    /// </summary>
   public sealed class ControlMappings
    {
        // 模拟量输入、温度点、数字量输出、数字量输入等控件的映射
        public Dictionary<int, Label> AnalogInputs { get; } = [];
        public Dictionary<int, Label> TemperaturePoints { get; } = [];
        public Dictionary<int, UISwitch> DigitalOutputs { get; } = [];
        public Dictionary<int, UIButton> DigitalOutputButtons { get; } = [];
        public Dictionary<int, Procedure.Controls.SwitchPictureBox> DigitalInputs { get; } = [];
        public Dictionary<int, AntdUI.Button> NavigationButtons { get; } = [];

        public void Clear()
        {
            AnalogInputs.Clear();
            TemperaturePoints.Clear();
            DigitalOutputs.Clear();
            DigitalOutputButtons.Clear();
            DigitalInputs.Clear();
            NavigationButtons.Clear();
        }
    }

}
