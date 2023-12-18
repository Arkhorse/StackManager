namespace StackManager
{
    public class Settings : JsonModSettings
    {
        internal static Settings Instance { get; } = new();

        [Name("Add Stackable Behaviour")]
        [Description("If enabled, the mod will add stackable behaviour to items defined in the config (this is not something you can add to)")]
        public bool AddStack = true;

        // This is used to load the settings
        internal static void OnLoad()
        {
            Instance.AddToModSettings(BuildInfo.GUIName);
            Instance.RefreshGUI();
        }
    }
}