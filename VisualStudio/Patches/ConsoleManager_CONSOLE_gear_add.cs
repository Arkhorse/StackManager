namespace StackManager.Patches
{
    /*
    [HarmonyPatch(typeof(ConsoleManager), nameof(ConsoleManager.CONSOLE_gear_add))]
    public class ConsoleManager_CONSOLE_gear_add
    {
        public static void Postfix()
        {
            // are we tracking a postfix patch ?
            if (Main.PostfixTrack.PostFixTrack)
            {
                if (Main.PostfixTrack.PostfixStack != null)
                {
                    // correct the stack(s) conditions
                    Main.PostfixTrack.PostfixStack.CurrentHP = Main.PostfixTrack.PostfixCondition * Main.PostfixTrack.PostfixStack.m_GearItemData.m_MaxHP;
                    // reset the items constraints
                    Main.PostfixTrack.PostfixStack.m_StackableItem.m_StackConditionDifferenceConstraint = Main.PostfixTrack.PostfixConstraint;
                }
                if (Main.PostfixTrack.PostfixGearToAdd != null)
                {
                    // correct the stack(s) conditions
                    Main.PostfixTrack.PostfixGearToAdd.CurrentHP = Main.PostfixTrack.PostfixCondition * Main.PostfixTrack.PostfixGearToAdd.m_GearItemData.m_MaxHP;
                    // reset the items constraints
                    Main.PostfixTrack.PostfixGearToAdd.m_StackableItem.m_StackConditionDifferenceConstraint = Main.PostfixTrack.PostfixConstraint;
                }
            }

            // reset the static values to avoid any conflicts
            StackingUtilities.SetPostFix(null, null, 0, 0, true);
        }
    }
    */
}
