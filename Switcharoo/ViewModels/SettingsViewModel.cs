using Switcharoo.Models;

namespace Switcharoo.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public static MainSettingsModel Settings => MainSettingsModel.Instance;
    }
}