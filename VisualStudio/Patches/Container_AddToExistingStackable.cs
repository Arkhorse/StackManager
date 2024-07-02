namespace StackManager.Patches
{
	/* Logic:
		UNKOWN
	*/
	[HarmonyPatch(typeof(Container))]
	[HarmonyPatch(nameof(Container.AddToExistingStackable))]
	[HarmonyPatch([typeof(GearItem), typeof(float), typeof(int)])]
	public class Container_AddToExistingStackable
	{
		public static bool Prefix(ref GearItem gearToAdd, float normalizedCondition, int numUnits)
		{
			if (gearToAdd.gameObject.GetComponent<StackableItem>() != null) return true;
			return StackingUtilities.Do(gearToAdd, normalizedCondition, numUnits);
		}
	}
}
