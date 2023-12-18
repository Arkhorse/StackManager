namespace StackManager
{
    public class StackingUtilities
    {
        internal static void ResetPostfixParams()
        {
            Main.PostFixTrack       = false;
            Main.PostfixCondition   = 0;
            Main.PostfixStack       = null;
            Main.PostfixGearToAdd   = null;
            Main.PostfixConstraint  = 0;
        }
        /*
        public static float MaybeSetCondition(GearItem gi)
        {
            return gi.m_GearItemData.MaxHP
                        * ((gi.m_StackableItem.IsAStackOfItems) ? gi.m_StackableItem.StackMultiplier : 1);
        }

        public static float SetNormalizedCondition(GearItem gi, string floatParse)
        {
            float num = gi.CurrentHP = Mathf.Clamp((Mathf.Clamp(float.Parse(floatParse), 0, 100) / 100f), 0, gi.m_GearItemData.MaxHP);
            return gi.GetNormalizedCondition();
        }

        public static float SetNormalizedCondition(GearItem gi, int units, float condition)
        {
            return units * condition + gi.m_StackableItem.m_Units * gi.GetNormalizedCondition() / (units + gi.m_StackableItem.m_Units); 
        }

        public static void MergeIntoStack(float normalizedCondition, int numUnits, GearItem targetStack, GearItem gearToAdd)
        {
            if (uConsole.IsOn())
            {
                if (uConsole.GetNumParameters() == 2)
                {
                    gearToAdd.RollGearCondition(false);
                    normalizedCondition = gearToAdd.GetNormalizedCondition();
                }

                if (uConsole.GetNumParameters() == 3)
                {
                    Main.PostFixTrack = true;

                    normalizedCondition = SetNormalizedCondition(gearToAdd, uConsole.m_Argv[3]);
                }
            }

            float TargetCondition = SetNormalizedCondition(targetStack, numUnits, normalizedCondition);

            if (Main.PostFixTrack == true)
            {
                Main.PostfixCondition   = TargetCondition;
                Main.PostfixStack       = targetStack;
                Main.PostfixGearToAdd   = gearToAdd;
                Main.PostfixConstraint  = targetStack.m_StackableItem.m_StackConditionDifferenceConstraint;
            }

            // 0% - 100%
            targetStack.m_StackableItem.m_StackConditionDifferenceConstraint    = Settings.Instance.MaxHPDifference;
            gearToAdd.m_StackableItem.m_StackConditionDifferenceConstraint      = Settings.Instance.MaxHPDifference;

            targetStack.CurrentHP = Mathf.Clamp(TargetCondition, 0, targetStack.m_GearItemData.MaxHP); // used to multiply the TargetCondition by the max HP...
        }

        public static bool CanBeMerged([NotNullWhen(true)] GearItem? target, [NotNullWhen(true)] GearItem? item)
        {
            return target != null && item != null;
        }

        public static bool UseDefaultStacking(GearItem? gearItem)
        {
            return gearItem == null || !(Main.Config.STACK_MERGE.Contains(gearItem.name));
        }
        */
    }
}
