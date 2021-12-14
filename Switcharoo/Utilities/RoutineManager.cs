using ff14bot;
using ff14bot.Enums;
using ff14bot.Objects;
using Switcharoo.Models;

namespace Switcharoo.Utilities
{
    internal class RoutineManager
    {
        private static LocalPlayer Me => Core.Player;
        public static ClassJobType currentClass;

        public static ClassJobType CurrentClass
        {
            get
            {
                if (currentClass == Me.CurrentJob)
                    return currentClass;

                currentClass = Me.CurrentJob;
                return currentClass;
            }
        }

        public static string GetRoutine()
        {
            switch (CurrentClass)
            {
                case ClassJobType.Arcanist:
                    return MainSettingsModel.Instance.PreferedArcanistRoutine;

                case ClassJobType.Summoner:
                    return MainSettingsModel.Instance.PreferedSummonerRoutine;

                case ClassJobType.Scholar:
                    return MainSettingsModel.Instance.PreferedScholarRoutine;

                case ClassJobType.Archer:
                case ClassJobType.Bard:
                    return MainSettingsModel.Instance.PreferedBardRoutine;

                case ClassJobType.Conjurer:
                case ClassJobType.WhiteMage:
                    return MainSettingsModel.Instance.PreferedWhiteMageRoutine;

                case ClassJobType.Gladiator:
                case ClassJobType.Paladin:
                    return MainSettingsModel.Instance.PreferedPaladinRoutine;

                case ClassJobType.Lancer:
                case ClassJobType.Dragoon:
                    return MainSettingsModel.Instance.PreferedDragoonRoutine;

                case ClassJobType.Marauder:
                case ClassJobType.Warrior:
                    return MainSettingsModel.Instance.PreferedWarriorRoutine;

                case ClassJobType.Pugilist:
                case ClassJobType.Monk:
                    return MainSettingsModel.Instance.PreferedMonkRoutine;

                case ClassJobType.Rogue:
                case ClassJobType.Ninja:
                    return MainSettingsModel.Instance.PreferedNinjaRoutine;

                case ClassJobType.Thaumaturge:
                case ClassJobType.BlackMage:
                    return MainSettingsModel.Instance.PreferedBlackMageRoutine;

                case ClassJobType.Machinist:
                    return MainSettingsModel.Instance.PreferedMachinistRoutine;

                case ClassJobType.DarkKnight:
                    return MainSettingsModel.Instance.PreferedDarkKnightRoutine;

                case ClassJobType.Astrologian:
                    return MainSettingsModel.Instance.PreferedAstrologianRoutine;

                case ClassJobType.RedMage:
                    return MainSettingsModel.Instance.PreferedRedMageRoutine;

                case ClassJobType.Samurai:
                    return MainSettingsModel.Instance.PreferedSamuraiRoutine;

                case ClassJobType.BlueMage:
                    return MainSettingsModel.Instance.PreferedBlueMageRoutine;

                case ClassJobType.Dancer:
                    return MainSettingsModel.Instance.PreferedDancerRoutine;

                case ClassJobType.Gunbreaker:
                    return MainSettingsModel.Instance.PreferedGunbreakerRoutine;

                case ClassJobType.Reaper:
                    return MainSettingsModel.Instance.PreferedReaperRoutine;

                case ClassJobType.Sage:
                    return MainSettingsModel.Instance.PreferedSageRoutine;

                case ClassJobType.Alchemist:
                case ClassJobType.Armorer:
                case ClassJobType.Blacksmith:
                case ClassJobType.Botanist:
                case ClassJobType.Carpenter:
                case ClassJobType.Culinarian:
                case ClassJobType.Fisher:
                case ClassJobType.Goldsmith:
                case ClassJobType.Leatherworker:
                case ClassJobType.Miner:
                case ClassJobType.Weaver:
                    return MainSettingsModel.Instance.PreferedDoHlRoutine;

                default:
                    return "";
            }
        }
    }
}