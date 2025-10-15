using MainUI.LogicalConfiguration.Parameter;

namespace MainUI.LogicalConfiguration.Forms
{
    public partial class Form_Condition : UIForm
    {
        public Parameter_Condition Condition { get; private set; }

        public Form_Condition(Parameter_Condition condition = null)
        {
            InitializeComponent();
            Condition = condition ?? new Parameter_Condition();
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
