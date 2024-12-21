global using ComplexLogger;
global using StackManager.config;
global using StackManager.Utilities;
global using StackManager.Utilities.Exceptions;
global using StackManager.Utilities.JSON;

global using MelonLoader.Utils;

namespace StackManager
{
	public class Main : MelonMod
	{
		public static ComplexLogger<Main> Logger { get; } = new();
		#region Config File
		public static Config Config { get; set; } = new();
		public static string ConfigFile { get; } = Path.Combine(MelonEnvironment.ModsDirectory, "StackManager", "config.json");
		public static Version CurrentVersion { get; } = new(1,0,1);
		#endregion
		#region MelonMod Methods
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
				if (SetupDefaultConfig()) JsonFile.Save<Config>(ConfigFile, Config);
			}

			Config ??= JsonFile.Load<Config>(ConfigFile);

			if (Config?.ConfigurationVersion != CurrentVersion) UpdateConfig();
		}
		#endregion

		public static bool UpdateConfig()
		{
			File.Delete(ConfigFile);
			Config.ConfigurationVersion = CurrentVersion;
			if (SetupDefaultConfig()) JsonFile.Save<Config>(ConfigFile, Config);

			return true;
		}

		public static bool SetupDefaultConfig()
		{
			Config = new();
			Config.ConfigurationVersion = CurrentVersion;

			Config.STACK_MERGE = new()
			{
				"GEAR_BirchSaplingDried",
				"GEAR_BearHideDried",
				"GEAR_BottleAntibiotics",
				"GEAR_BottlePainKillers",
				"GEAR_Carrot",
				"GEAR_CoffeeTin",
				"GEAR_GreenTeaPackage",
				"GEAR_GutDried",
				"GEAR_LeatherDried",
				"GEAR_LeatherHideDried",
				"GEAR_MapleSaplingDried",
				"GEAR_MooseHideDried",
				"GEAR_PackMatches",
				"GEAR_Potato",
				"GEAR_RabbitPeltDried",
				//"GEAR_RecycledCan",
				"GEAR_StumpRemover",
				"GEAR_WolfPeltDried",
				"GEAR_WoodMatches"
			};

			Config.Advanced = new()
			{
				"GEAR_CoffeeTin",
				"GEAR_GreenTeaPackage",
				//"GEAR_RecycledCan",
				"GEAR_Potato"
			};

			Config.AddStackableComponent = new()
			{
				"GEAR_Potato",
				//"GEAR_RecycledCan",
				"GEAR_StumpRemover"
			};

			return true;
		}

		/// <summary>
		/// Used to track postfix changes. Mostly for console command patching
		/// </summary>
		public class PostfixTrack
		{
			/// <summary></summary>
			/// <value></value>
			public static bool PostFixTrack { get; set; }				= false;
			public static float PostfixCondition { get; set; }			= 0;
			public static GearItem? PostfixStack { get; set; }			= null;
			public static GearItem? PostfixGearToAdd { get; set; }		= null;
			public static float PostfixConstraint { get; set; }			= 0;
		}
	}
}
