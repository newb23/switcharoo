using ff14bot.Helpers;
using System.Windows.Media;

namespace Switcharoo.Utilities
{
    internal class Logger
    {
        internal static void SwitcharooLog(string text, params object[] args)
        {
            Logging.Write(Color.FromRgb(109, 139, 225), $@"[Switcharoo] {text}", args);
        }
    }
}