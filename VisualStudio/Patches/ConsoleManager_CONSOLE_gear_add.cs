using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackManager.Patches
{
    [HarmonyPatch(typeof(ConsoleManager), nameof(ConsoleManager.CONSOLE_gear_add))]
    internal class ConsoleManager_CONSOLE_gear_add
    {

        private static void Postfix()
        {
            // are we tracking a postfix patch ?
            if (Main.PostFixTrack)
            {
                if (Main.PostfixStack != null)
                {
                    // correct the stack(s) conditions
                    Main.PostfixStack.CurrentHP = Main.PostfixCondition * Main.PostfixStack.m_GearItemData.m_MaxHP;
                    // reset the items constraints
                    Main.PostfixStack.m_StackableItem.m_StackConditionDifferenceConstraint = Main.PostfixConstraint;
                }
                if (Main.PostfixGearToAdd != null)
                {
                    // correct the stack(s) conditions
                    Main.PostfixGearToAdd.CurrentHP = Main.PostfixCondition * Main.PostfixGearToAdd.m_GearItemData.m_MaxHP;
                    // reset the items constraints
                    Main.PostfixGearToAdd.m_StackableItem.m_StackConditionDifferenceConstraint = Main.PostfixConstraint;
                }
            }

            // reset the static values to avoid any conflicts
            StackingUtilities.ResetPostfixParams();
        }

    }
}
