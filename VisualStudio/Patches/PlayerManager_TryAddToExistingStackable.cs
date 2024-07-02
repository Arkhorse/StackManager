namespace StackManager.Patches
{
	/* Logic:
		UNKNOWN
	*/
	[HarmonyPatch(typeof(PlayerManager))]
	[HarmonyPatch(nameof(PlayerManager.TryAddToExistingStackable))]
	[HarmonyPatch([typeof(GearItem), typeof(float), typeof(int), typeof(GearItem)], [ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Ref])]
	public class PlayerManager_TryAddToExistingStackable
	{
		public static bool Prefix(ref GearItem gearToAdd, float normalizedCondition, int numUnits, ref GearItem existingGearItem)
		{
			if (gearToAdd.gameObject.GetComponent<StackableItem>() != null) return true;
			return StackingUtilities.Do(gearToAdd, normalizedCondition, numUnits, ref existingGearItem);
		}
	}
}
