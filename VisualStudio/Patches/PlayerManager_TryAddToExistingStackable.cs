//using StackManager.Utilities.Logger;
//using StackManager.Utilities.Exceptions;
//using System.Runtime.InteropServices;

//namespace StackManager.Patches
//{

//    [HarmonyPatch]
//    public static class PlayerManager_TryAddToExistingStackable_Reflection
//    {
//        static MethodBase? TargetMethod()
//        {
//            MethodInfo? targetMethod = typeof(PlayerManager)
//                .GetMethods()
//                .FirstOrDefault(
//                    m => m.Name == nameof(PlayerManager.TryAddToExistingStackable)
//                    && m.ReturnType == typeof(bool)
//                    && !m.IsGenericMethod
//                    && m.GetParameters().Length == 4
//                    );
//            if (targetMethod is null)
//            {
//                throw new BadMemeException("PlayerManager.TryAddToExistingStackable(GearItem, float, int, out GearItem) not found for patch.");
//            }

//            return targetMethod;
//        }
//        public static bool Prefix(GearItem gearToAdd, float normalizedCondition, int numUnits, ref GearItem existingGearItem)
//        {
//            return !StackableHandle.Do(gearToAdd, normalizedCondition, numUnits, ref existingGearItem);
//        }
//    }
//    /*
//    [HarmonyPatch(typeof(PlayerManager),
//                nameof(PlayerManager.TryAddToExistingStackable),
//                new Type[]          { typeof(GearItem), typeof(float), typeof(int), typeof(GearItem) },
//                new ArgumentType[]  { ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Out })]
//    public class PlayerManager_TryAddToExistingStackable
//    {
//        public static bool Prefix(GearItem? gearToAdd, float normalizedCondition, int numUnits, ref GearItem? existingGearItem)
//        {
//            return !StackableHandle.Do(gearToAdd, normalizedCondition, numUnits, ref existingGearItem);
//        }
//    }
//    */

//    public class StackableHandle
//    {
//        public static bool Do(GearItem gearToAdd, float normalizedCondition, int numUnits, ref GearItem existingGearItem)
//        {
//            if (gearToAdd == null) return false;
//            if (existingGearItem == null) return false;

//            Logging.Log($"PlayerManager.TryAddToExistingStackable({gearToAdd.name}, {normalizedCondition}, {numUnits}, {existingGearItem.name})");

//            // prevent destroyed items from stacking
//            if (normalizedCondition == 0) return false;
//            if (StackingUtilities.UseDefaultStacking(gearToAdd))
//            {
//                Logging.Log($"UseDefaultStacking({gearToAdd.name})");
//                return false;
//            }

//            GearItem targetStack = StackableItem.GetClosestMatchStackable(GameManager.GetInventoryComponent().m_Items, gearToAdd, normalizedCondition);

//            if (!StackingUtilities.CanBeMerged(targetStack, gearToAdd))
//            {
//                Logging.Log($"GearItem cant be stacked, CanBeMerged({targetStack.name}, {gearToAdd.name})");
//                existingGearItem = null;
//                return true;
//            }

//            // A lit match should never be stackable
//            if (gearToAdd.IsLitMatch())
//            {
//                existingGearItem = null;
//                return true;
//            }
//            if (targetStack.IsLitMatch())
//            {
//                existingGearItem = null;
//                return true;
//            }

//            if (targetStack.m_StackableItem != null)
//            {
//                Logging.Log($"MergeIntoStack({normalizedCondition}, {numUnits}, {targetStack.name}, {gearToAdd.name})");

//                StackingUtilities.MergeIntoStack(normalizedCondition, numUnits, targetStack, gearToAdd);
//                existingGearItem = targetStack;
//                return true;
//            }

//            return false;
//        }
//    }
//}
