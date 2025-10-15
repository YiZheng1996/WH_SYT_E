namespace MainUI.Service
{
    /// <summary>
    /// 提供初始化和映射各种UI控件的功能，模拟输入和导航按钮，位于指定容器内
    /// </summary>
    /// <param name="controls">处理所提供容器中的控件，并将其映射到各自的类别</param>
    public class ControlInitializationService(ControlMappings controls)
    {
        /// <summary>
        /// 初始化所有控件
        /// </summary>
        public void InitializeAllControls(Control uiContainer)
        {
            InitializeDigitalInputs();
            InitializeDigitalOutputButtons(uiContainer);
            InitializeAnalogInputs(uiContainer);
            InitializeDigitalOutputs(uiContainer);
            InitializeNavigationButtons(uiContainer);
        }

        /// <summary>
        /// 通过清除任何现有条目来初始化数字输入的集合
        /// </summary>
        private void InitializeDigitalInputs()
        {
            controls.DigitalInputs.Clear();
        }

        /// <summary>
        /// 初始化数字输出按钮
        /// </summary>
        /// <param name="container"></param>
        private void InitializeDigitalOutputButtons(Control container)
        {
            controls.DigitalOutputButtons.Clear();
            var buttons = container.Controls.OfType<UIButton>()
                .Where(b => b.Tag != null);

            foreach (var btn in buttons)
            {
                controls.DigitalOutputButtons.TryAdd(btn.Tag.ToInt32(), btn);
            }
        }

        /// <summary>
        /// 初始化模拟输入
        /// </summary>
        /// <param name="container"></param>
        private void InitializeAnalogInputs(Control container)
        {
            controls.AnalogInputs.Clear();
            controls.TemperaturePoints.Clear();

            // 查找并映射模拟输入标签
            var analogLabels = container.Controls.OfType<Label>()
                .Where(l => l.Name.StartsWith("LabAI"));

            foreach (var lab in analogLabels)
            {
                controls.AnalogInputs.TryAdd(lab.Tag.ToInt32(), lab);
            }

            // 查找并绘制温度点标签
            var tempLabels = container.Controls.OfType<Label>()
                .Where(l => l.Name.StartsWith("LabTP"));

            foreach (var lab in tempLabels)
            {
                controls.TemperaturePoints.TryAdd(lab.Tag.ToInt32(), lab);
            }
        }

        /// <summary>
        /// 初始化数字输出
        /// </summary>
        /// <param name="container"></param>
        private void InitializeDigitalOutputs(Control container)
        {
            controls.DigitalOutputs.Clear();
            MapDigitalOutputsRecursively(container);
        }

        /// <summary>
        /// 初始化数字量输出控件，递归遍历容器中的所有控件
        /// </summary>
        /// <param name="container"></param>
        private void MapDigitalOutputsRecursively(Control container)
        {
            foreach (Control item in container.Controls)
            {
                if (item is UISwitch uiSwitch)
                {
                    int key = uiSwitch.Tag.ToInt32();
                    controls.DigitalOutputs.TryAdd(key, uiSwitch);
                }
                MapDigitalOutputsRecursively(item);
            }
        }

        /// <summary>
        /// 通过将导航按钮与指定容器中的控件相关联来初始化导航按钮
        /// </summary>
        /// <param name="container"></param>
        private void InitializeNavigationButtons(Control container)
        {
            controls.NavigationButtons.Clear();
            controls.NavigationButtons.Add(0, 
                container.Controls["btnWorkmanshipForms"] as AntdUI.Button);
            controls.NavigationButtons.Add(1, 
                container.Controls["btnReportForms"] as AntdUI.Button);
        }
    }
}
