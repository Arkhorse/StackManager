using System.Runtime.CompilerServices;

namespace StackManager.Patches
{
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
    internal class DisableDecay
    {
        public static void Prefix(GearItem __instance)
        {
            if (__instance == null) return;
            if (string.IsNullOrWhiteSpace(__instance.name)) return;
            if (__instance.m_StackableItem != null) return;
            if (__instance.gameObject.GetComponent<StackableItem>() != null) return;
            if (Main.Config == null) return;

            string name = CommonUtilities.NormalizeName(__instance.name);

            if (Settings.Instance.AddStack)
            {
                if (Main.Config.AddStackableComponent.Contains(name))
                {
                    Main.Logger.Log($"AddStack: {name}", FlaggedLoggingLevel.Debug);

                    StackableItem stack = __instance.gameObject.AddComponent<StackableItem>();

                    stack.m_DefaultUnitsInItem                  = 1;
                    //stack.m_LocalizedSingleUnitText             = Extensions.CreateLocalizedString(__instance.GetDisplayNameWithoutConditionForInventoryInterfaces());
                    //stack.m_LocalizedMultipleUnitText           = Extensions.CreateLocalizedString(__instance.GetDisplayNameWithoutConditionForInventoryInterfaces());
                    stack.m_StackConditionDifferenceConstraint  = 100f;
                    stack.m_StackSpriteName                     = string.Empty;
                    stack.m_ShareStackWithGear                  = Array.Empty<StackableItem>();

                    if (stack.m_Units == 0) stack.m_Units = 1;

                    __instance.m_StackableItem = stack;
                    return;
                }
            }
        }

        public static void Postfix(GearItem __instance)
        {
            if (__instance == null) return;
            if (string.IsNullOrWhiteSpace(__instance.name)) return;
            //if (__instance.gameObject.GetComponent<StackableItem>() != null) return;
            if (Main.Config == null) return;

            string name = CommonUtilities.NormalizeName(__instance.name);
            StackableItem? item = __instance.gameObject.GetComponent<StackableItem>();

      //      if (!Settings.Instance.AddStack)
      //      {
      //          if (Main.Config.AddStackableComponent.Contains(name))
      //          {
      //              if (item == null)
      //              {
      //                  __instance.gameObject.AddComponent<StackableItem>();
      //              }
      ////              if (item != null)
      ////              {
						////Main.Logger.Log($"AddStack REMOVE: {name}", FlaggedLoggingLevel.Debug);
      ////                  __instance.m_StackableItem = null;

      ////                  try
      ////                  {
      ////                      UnityEngine.Object.Destroy(item, 0.1f);
      ////                  }
      ////                  catch
      ////                  {
						////	Main.Logger.Log($"Attempting to destroy the StackableItem component for item \"{name}\" failed", FlaggedLoggingLevel.Debug);
      ////                      throw;
      ////                  }
      ////              }
      //          }
      //      }

            if (Main.Config.STACK_MERGE.Contains(name))
            {
                if (item != null)
                {
                    item.m_StackConditionDifferenceConstraint = 100f;
                }
            }

            if (name == "GEAR_CoffeeTin")
            {
                __instance.SetHaltDecay(true);
                __instance.CurrentHP = 1000f;
            }

            if (name == "GEAR_GreenTeaPackage")
            {
                __instance.SetHaltDecay(true);
                __instance.CurrentHP = 1500f;
            }

            if (name == "GEAR_Carrot")
            {
                __instance.SetHaltDecay(true);
                __instance.CurrentHP = 50f;
            }

            if (name == "GEAR_Potato")
            {
                __instance.SetHaltDecay(true);
                __instance.CurrentHP = 100f;
            }
        }
    }

    public static class Extensions
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static LocalizedString CreateLocalizedString(string name)
        {
            return new LocalizedString()
            {
                m_LocalizationID = name
            };
        }
    }
}
