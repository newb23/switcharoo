using ff14bot;
using ff14bot.Managers;
using ff14bot.Objects;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;

namespace Switcharoo.Models
{
    public class MainSettingsModel : BaseModel
    {
        private static LocalPlayer Me => Core.Player;
        private static MainSettingsModel _instance;
        public static MainSettingsModel Instance => _instance ?? (_instance = new MainSettingsModel());

        private MainSettingsModel() : base(@"Settings/" + Me.Name + "/Switcharoo/Main_Settings.json")
        {
        }

        private string _preferedArcanistRoutine, _preferedSummonerRoutine, _preferedScholarRoutine, _preferedBardRoutine, _preferedWhiteMageRoutine, _preferedPaladinRoutine,
            _preferedDragoonRoutine, _preferedWarriorRoutine, _preferedMonkRoutine, _preferedNinjaRoutine, _preferedBlankMageRoutine, _preferedMachinistRoutine,
            _preferedDarkKnightRoutine, _preferedAstrologianRoutine, _preferedDoHlRoutine, _preferedRedMageRoutine, _preferedSamuraiRoutine, _preferedBlueMageRoutine,
            _preferedDancerRoutine, _preferedGunbreakerRoutine, _preferedReaperRoutine, _preferedSageRoutine;

        #region Prefered Routines

        [Setting]
        public string PreferedArcanistRoutine
        { get { return _preferedArcanistRoutine; } set { _preferedArcanistRoutine = value; OnPropertyChanged(); } }

        [Setting]
        public string PreferedSummonerRoutine
        { get { return _preferedSummonerRoutine; } set { _preferedSummonerRoutine = value; OnPropertyChanged(); } }

        [Setting]
        public string PreferedScholarRoutine
        { get { return _preferedScholarRoutine; } set { _preferedScholarRoutine = value; OnPropertyChanged(); } }

        [Setting]
        public string PreferedBardRoutine
        { get { return _preferedBardRoutine; } set { _preferedBardRoutine = value; OnPropertyChanged(); } }

        [Setting]
        public string PreferedWhiteMageRoutine
        { get { return _preferedWhiteMageRoutine; } set { _preferedWhiteMageRoutine = value; OnPropertyChanged(); } }

        [Setting]
        public string PreferedPaladinRoutine
        { get { return _preferedPaladinRoutine; } set { _preferedPaladinRoutine = value; OnPropertyChanged(); } }

        [Setting]
        public string PreferedDragoonRoutine
        { get { return _preferedDragoonRoutine; } set { _preferedDragoonRoutine = value; OnPropertyChanged(); } }

        [Setting]
        public string PreferedWarriorRoutine
        { get { return _preferedWarriorRoutine; } set { _preferedWarriorRoutine = value; OnPropertyChanged(); } }

        [Setting]
        public string PreferedMonkRoutine
        { get { return _preferedMonkRoutine; } set { _preferedMonkRoutine = value; OnPropertyChanged(); } }

        [Setting]
        public string PreferedNinjaRoutine
        { get { return _preferedNinjaRoutine; } set { _preferedNinjaRoutine = value; OnPropertyChanged(); } }

        [Setting]
        public string PreferedBlackMageRoutine
        { get { return _preferedBlankMageRoutine; } set { _preferedBlankMageRoutine = value; OnPropertyChanged(); } }

        [Setting]
        public string PreferedMachinistRoutine
        { get { return _preferedMachinistRoutine; } set { _preferedMachinistRoutine = value; OnPropertyChanged(); } }

        [Setting]
        public string PreferedDarkKnightRoutine
        { get { return _preferedDarkKnightRoutine; } set { _preferedDarkKnightRoutine = value; OnPropertyChanged(); } }

        [Setting]
        public string PreferedAstrologianRoutine
        { get { return _preferedAstrologianRoutine; } set { _preferedAstrologianRoutine = value; OnPropertyChanged(); } }

        [Setting]
        public string PreferedDoHlRoutine
        { get { return _preferedDoHlRoutine; } set { _preferedDoHlRoutine = value; OnPropertyChanged(); } }

        [Setting]
        public string PreferedRedMageRoutine
        { get { return _preferedRedMageRoutine; } set { _preferedRedMageRoutine = value; OnPropertyChanged(); } }

        [Setting]
        public string PreferedSamuraiRoutine
        { get { return _preferedSamuraiRoutine; } set { _preferedSamuraiRoutine = value; OnPropertyChanged(); } }

        [Setting]
        public string PreferedBlueMageRoutine
        { get { return _preferedBlueMageRoutine; } set { _preferedBlueMageRoutine = value; OnPropertyChanged(); } }

        [Setting]
        public string PreferedDancerRoutine
        { get { return _preferedDancerRoutine; } set { _preferedDancerRoutine = value; OnPropertyChanged(); } }

        [Setting]
        public string PreferedGunbreakerRoutine
        { get { return _preferedGunbreakerRoutine; } set { _preferedGunbreakerRoutine = value; OnPropertyChanged(); } }

        [Setting]
        public string PreferedReaperRoutine
        { get { return _preferedReaperRoutine; } set { _preferedReaperRoutine = value; OnPropertyChanged(); } }

        [Setting]
        public string PreferedSageRoutine
        { get { return _preferedSageRoutine; } set { _preferedSageRoutine = value; OnPropertyChanged(); } }

        #endregion Prefered Routines

        [JsonIgnore]
        private List<string> _combatRoutineList;

        [JsonIgnore]
        public List<string> CombatRoutineList => _combatRoutineList ?? (_combatRoutineList = GetCombatRoutines().ToList());

        private static IEnumerable<string> GetCombatRoutines()
        {
            var retval = new HashSet<string>();
            foreach (var routine in RoutineManager.AllRoutines.Select(x => x.Name).Where(x => x != "InvalidRoutineWrapper" && x != " "))
            {
                var index = routine.IndexOf('[');
                retval.Add(index > 0 ? routine.Substring(0, index) : routine);
            }

            retval.Add(string.Empty);

            return retval;
        }

        private SelectedTheme _selectedTheme;

        [Setting]
        [DefaultValue(SelectedTheme.Pink)]
        public SelectedTheme Theme
        { get { return _selectedTheme; } set { _selectedTheme = value; OnPropertyChanged(); } }
    }

    public enum SelectedTheme
    {
        Blue,
        Pink,
        Green,
        Red,
        Yellow
    }
}