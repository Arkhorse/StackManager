namespace StackManager.Patches
{
	/*
	[HarmonyPatch]
	public class PlayerManager_TryAddToExistingStackable_Reflection
	{
		public static MethodBase? TargetMethod()
		{
			MethodInfo? targetMethod = typeof(PlayerManager).GetMethods().FirstOrDefault(m => m.Name == nameof(PlayerManager.TryAddToExistingStackable) && m.ReturnType == typeof(bool) && !m.IsGenericMethod && m.GetParameters().Length == 4);

			if (targetMethod is null)
			{
				throw new BadMemeException("PlayerManager.TryAddToExistingStackable(GearItem gearToAdd, float normalizedCondition, int numUnits, ref GearItem? existingGearItem) not found for patch.");
			}
			else if (targetMethod != null)
			{
				Logging.Log($"targetMethod::Found");
			}

			return targetMethod;
		}

		public static void Prefix()
		{
			Logging.Log($"PlayerManager_TryAddToExistingStackable_Reflection::Found");
		}
	}
	*/
}
