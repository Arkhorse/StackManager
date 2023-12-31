using MelonLoader.Utils;
using StackManager.config;
using StackManager.Utilities.JSON;

namespace StackManager
{
    public class Main : MelonMod
    {
        /// <summary>
        /// This never gets saved
        /// </summary>
        public static Config Config { get; set; } = new();
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
                JsonFile.Save<Config>(ConfigFile, Config);
            }

            Config = JsonFile.Load<Config>(ConfigFile);
        }
    }
}
