using StackManager.Utilities;
using StackManager.Utilities.Logger;
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
                if (Main.Config.CustomHandled.Contains(name))
                {
                    StackableItem stack = __instance.gameObject.AddComponent<StackableItem>();

                    stack.m_DefaultUnitsInItem                  = 1;
                    stack.m_LocalizedSingleUnitText             = Extensions.CreateLocalizedString(__instance.GetDisplayNameWithoutConditionForInventoryInterfaces());
                    stack.m_LocalizedMultipleUnitText           = Extensions.CreateLocalizedString(__instance.GetDisplayNameWithoutConditionForInventoryInterfaces());
                    stack.m_StackConditionDifferenceConstraint  = 100f;
                    stack.m_StackSpriteName                     = string.Empty;
                    stack.m_ShareStackWithGear                  = Array.Empty<StackableItem>();

                    if (stack.m_Units == 0) stack.m_Units = 1;

                    __instance.m_StackableItem = stack;
                    return;
                }
            }
            else
            {
                if (Main.Config.CustomHandled.Contains(name))
                {
                    StackableItem? item = __instance.GetComponent<StackableItem>();

                    if (item != null)
                    {
                        __instance.m_StackableItem = null;

                        try
                        {
                            UnityEngine.Object.Destroy(item, 0.1f);
                        }
                        catch
                        {
                            Logging.LogError($"Attempting to destroy the StackableItem component for item \"{name}\" failed");
                            throw;
                        }
                    }
                }
            }

            // functional items cant have stackable functions added as of 2.25
            /*
            if (CommonUtilities.NormalizeName(__instance.name) == "GEAR_FlareA")
            {
                //if (__instance.IsLitFlare()) return;
                //if (__instance.GetComponent<StackableItem>() != null) return;

                //StackableItem stack = __instance.gameObject.AddComponent<StackableItem>();

                //stack.m_DefaultUnitsInItem                      = 1;
                //stack.m_LocalizedSingleUnitText                 = Extensions.CreateLocalizedString(__instance.GetDisplayNameWithoutConditionForInventoryInterfaces());
                //stack.m_LocalizedMultipleUnitText               = Extensions.CreateLocalizedString(__instance.GetDisplayNameWithoutConditionForInventoryInterfaces());
                //stack.m_StackConditionDifferenceConstraint      = 0.01f;
                //stack.m_StackSpriteName                         = string.Empty;
                //stack.m_ShareStackWithGear                      = Array.Empty<StackableItem>();
                //stack.m_Units = 1;

                //__instance.m_StackableItem = stack;
                return;
            }

            if (CommonUtilities.NormalizeName(__instance.name) == "GEAR_SprayPaintCan")
            {
                //StackableItem stack = __instance.gameObject.AddComponent<StackableItem>();

                //stack.m_DefaultUnitsInItem                      = 1;
                //stack.m_LocalizedSingleUnitText                 = Extensions.CreateLocalizedString(__instance.GetDisplayNameWithoutConditionForInventoryInterfaces());
                //stack.m_LocalizedMultipleUnitText               = Extensions.CreateLocalizedString(__instance.GetDisplayNameWithoutConditionForInventoryInterfaces());
                //stack.m_StackConditionDifferenceConstraint      = 0.01f;
                //stack.m_StackSpriteName                         = string.Empty;
                //stack.m_ShareStackWithGear                      = Array.Empty<StackableItem>();

                //if (stack.m_Units == 0) stack.m_Units = 1;

                //__instance.m_StackableItem = stack;
                return;
            }

            if (CommonUtilities.NormalizeName(__instance.name) == "GEAR_SewingKit")
            {
                //StackableItem stack = __instance.gameObject.AddComponent<StackableItem>();

                //stack.m_DefaultUnitsInItem                      = 1;
                //stack.m_LocalizedSingleUnitText                 = Extensions.CreateLocalizedString(__instance.GetDisplayNameWithoutConditionForInventoryInterfaces());
                //stack.m_LocalizedMultipleUnitText               = Extensions.CreateLocalizedString(__instance.GetDisplayNameWithoutConditionForInventoryInterfaces());
                //stack.m_StackConditionDifferenceConstraint      = 0.01f;
                //stack.m_StackSpriteName                         = string.Empty;
                //stack.m_ShareStackWithGear                      = Array.Empty<StackableItem>();

                //if (stack.m_Units == 0) stack.m_Units = 1;

                //__instance.m_StackableItem = stack;
                return;
            }
            */
        }

        public static void Postfix(GearItem __instance)
        {
            if (__instance == null) return;
            if (string.IsNullOrWhiteSpace(__instance.name)) return;
            if (Main.Config == null) return;

            string name = CommonUtilities.NormalizeName(__instance.name);

            if (!Main.Config.CustomHandled.Contains(name))
            {
                if (__instance.gameObject.GetComponent<StackableItem>() != null)
                {
                    StackableItem item = __instance.gameObject.GetComponent<StackableItem>();
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
