namespace MainUI.Procedure.Mask
{
    public partial class LayerForm : Form
    {
        private Form _onLayerForm;
        public LayerForm(Form LayeredForm, Form onLayerForm)
        {
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.Manual;
            BackColor = Color.Black;
            Opacity = 0.4;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            //Location = LayeredForm.Location;
            Size = LayeredForm.Size;
            _onLayerForm = onLayerForm;
            Shown += LayerForm_Shown;
        }

        private void LayerForm_Shown(object sender, EventArgs e)
        {
            _onLayerForm.ShowInTaskbar = false;
            _onLayerForm.StartPosition = FormStartPosition.CenterParent;
            _onLayerForm.FormClosed += OnLayerForm_FormClosed;
            _onLayerForm.ShowDialog();
        }

        private void OnLayerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }
    }
}