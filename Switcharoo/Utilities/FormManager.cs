using Switcharoo.Models;
using Switcharoo.Views;

namespace Switcharoo.Utilities
{
    internal class FormManager
    {
        private static SwitcharooWindow form;

        public static void SaveFormInstances()
        {
            MainSettingsModel.Instance.Save();
        }

        private static SwitcharooWindow Form
        {
            get
            {
                if (form != null) return form;
                form = new SwitcharooWindow();
                form.Closed += (sender, args) => form = null;
                return form;
            }
        }

        public static void OpenForms()
        {
            if (Form.IsVisible)
            {
                Form.Activate();
                return;
            }

            Form.Show();
        }
    }
}