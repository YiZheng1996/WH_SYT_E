namespace MainUI.CurrencyHelper;

public static class FormManager
{
    public static readonly Dictionary<Type, Form> openForms = [];

    public static Form ShowForm<T>(bool showIfAlreadyOpen = true) where T : Form, new()
    {
        if (openForms.TryGetValue(typeof(T), out Form form))
        {
            if (showIfAlreadyOpen)
            {
                form.Show();
                form.BringToFront();
            }
        }
        else
        {
            form = new T();
            openForms[typeof(T)] = form;
            form.FormClosing += Form_FormClosing;
            form.Show();
        }

        return form;
    }

    private static void Form_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (sender is Form closingForm)
        {
            openForms.Remove(closingForm.GetType());
        }
    }
}