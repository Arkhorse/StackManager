using MelonLoader.Utils;
using StackManager.config;
using StackManager.Utilities.JSON;

namespace StackManager
{
    public class Main : MelonMod
    {
        public static List<string> STACK_MERGE { get; } = new()
        {
            "GEAR_BirchSaplingDried",
            "GEAR_BearHideDried",
            "GEAR_BottleAntibiotics",
            "GEAR_BottlePainKillers",
            "GEAR_CoffeeTin",
            "GEAR_GreenTeaPackage",
            "GEAR_GutDried",
            "GEAR_LeatherDried",
            "GEAR_LeatherHideDried",
            "GEAR_MapleSaplingDried",
            "GEAR_MooseHideDried",
            "GEAR_PackMatches",
            "GEAR_RabbitPeltDried",
            "GEAR_WolfPeltDried",
            "GEAR_WoodMatches",
        };

        public static List<string> CustomHandled { get; } = new()
        {
            "GEAR_Potato",
            "GEAR_StumpRemover",
            "GEAR_RecycledCan",
            "GEAR_CoffeeTin",
            "GEAR_GreenTeaPackage",
            "GEAR_Carrot"
        };

        /// <summary>
        /// This never gets saved
        /// </summary>
        public static Config? Config { get; set; } = new();
        public static string ConfigFile { get; } = Path.Combine(MelonEnvironment.ModsDirectory, "StackManager", "config.json");
        public static bool PostFixTrack { get; set; } = false;
        public static float PostfixCondition { get; set; } = 0;
        public static GearItem? PostfixStack { get; set; } = null;
        public static GearItem? PostfixGearToAdd { get; set; } = null;
        public static float PostfixConstraint { get; set; } = 0;

        public override void OnInitializeMelon()
        {
            Settings.OnLoad();

            if (!Directory.Exists(Path.Combine(MelonEnvironment.ModsDirectory, "StackManager")))
            {
                Directory.CreateDirectory(Path.Combine(MelonEnvironment.ModsDirectory, "StackManager"));
                return;
            }
            if (!File.Exists(ConfigFile))
            {
                return;
            }

            Config = JsonFile.Load<Config>(ConfigFile);
        }
    }
}
