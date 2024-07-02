namespace StackManager
{
	public class StackingUtilities
	{
		#region Console Stuff
		[Obsolete("Use StackingUtilities.SetPostFix(GearItem, GearItem, float, float, bool) instead")]
		internal static void ResetPostfixParams() { }

		[Obsolete]
		public static bool SetPostFix(GearItem? PostfixStack, GearItem? PostfixGearToAdd, float PostfixCondition, float PostfixConstraint, bool reset)
		{
			Main.PostfixTrack.PostFixTrack = !reset;

			if (!string.IsNullOrWhiteSpace(Main.PostfixTrack.PostfixGearToAdd?.name) && !string.IsNullOrWhiteSpace(Main.PostfixTrack.PostfixStack?.name))
			{
				Main.Logger.Log($"SetPostFix({Main.PostfixTrack.PostFixTrack}, {Main.PostfixTrack.PostfixGearToAdd.name}, {Main.PostfixTrack.PostfixCondition}, {Main.PostfixTrack.PostfixStack.name}, {Main.PostfixTrack.PostfixConstraint})", FlaggedLoggingLevel.Debug);
			}
			else if (!string.IsNullOrWhiteSpace(Main.PostfixTrack.PostfixGearToAdd?.name))
			{
				Main.Logger.Log($"SetPostFix({Main.PostfixTrack.PostFixTrack}, {Main.PostfixTrack.PostfixGearToAdd.name}, {Main.PostfixTrack.PostfixCondition}, null, {Main.PostfixTrack.PostfixConstraint})", FlaggedLoggingLevel.Debug);
			}
			else if (!string.IsNullOrWhiteSpace(Main.PostfixTrack.PostfixStack?.name))
			{
				Main.Logger.Log($"SetPostFix({Main.PostfixTrack.PostFixTrack}, null, {Main.PostfixTrack.PostfixCondition}, {Main.PostfixTrack.PostfixStack.name}, {Main.PostfixTrack.PostfixConstraint})", FlaggedLoggingLevel.Debug);
			}

			switch (reset)
			{
				case true:
					Main.PostfixTrack.PostfixStack			= null;
					Main.PostfixTrack.PostfixGearToAdd		= null;
					Main.PostfixTrack.PostfixCondition		= 0;
					Main.PostfixTrack.PostfixConstraint		= 0;
					break;
				default:
					Main.PostfixTrack.PostfixStack			= PostfixStack;
					Main.PostfixTrack.PostfixGearToAdd		= PostfixGearToAdd;
					Main.PostfixTrack.PostfixCondition		= PostfixCondition;
					Main.PostfixTrack.PostfixConstraint		= PostfixConstraint;
					break;
			}

			return true;
		}
		#endregion

		[Obsolete("Use StackingUtilities.SetNormalizedCondition instead")]
		public static float MaybeSetCondition(GearItem gi)
		{
			return gi.m_GearItemData.MaxHP
						* ((gi.m_StackableItem.IsAStackOfItems) ? gi.m_StackableItem.StackMultiplier : 1);
		}

		public static float SetNormalizedCondition(GearItem gi, GearItem target)
		{
			return Math.Max(gi.GetNormalizedCondition(), target.GetNormalizedCondition()); 
		}

		public static void MergeIntoStack(GearItem targetStack, GearItem gearToAdd, int units)
		{
			if (gearToAdd == null) return;
			if (targetStack == null) return;
			
			float TargetCondition = SetNormalizedCondition(gearToAdd, targetStack);

			Main.Logger.Log($"TargetCondition:{TargetCondition}", FlaggedLoggingLevel.Debug);

			if (Main.PostfixTrack.PostFixTrack == true)
			{
				SetPostFix(null, null, 0, 0, true);
			}

			if (Main.Config.Advanced.Contains(gearToAdd.name))
			{
				
				targetStack.CurrentHP = targetStack.m_GearItemData.MaxHP;
			}

			targetStack.CurrentHP = Mathf.Clamp(TargetCondition * targetStack.m_GearItemData.MaxHP, 0, targetStack.m_GearItemData.MaxHP);
			targetStack.m_StackableItem.m_Units += units;
		}

		public static bool CanBeMerged(GearItem? target, GearItem? item)
		{
			return target != null && item != null;
		}

		public static bool UseDefaultStacking(GearItem gearItem)
		{
			return gearItem == null || !(Main.Config.STACK_MERGE.Contains(gearItem.name));
		}

		public static bool CanOperate(GearItem? gi, float condition)
		{
			return !(gi == null) && condition > 0 && !(UseDefaultStacking(gi));
		}

		public static bool Do(GearItem gearToAdd, float normalizedCondition, int numUnits)
		{
			if (!CanOperate(gearToAdd, normalizedCondition)) return true;

			//GearItem? targetStack = null;
			GearItem? targetStack = StackableItem.GetClosestMatchStackable(GameManager.GetInventoryComponent().m_Items, gearToAdd, normalizedCondition);

			if (targetStack == null)
			{
				Main.Logger.Log($"targetStack:null", FlaggedLoggingLevel.Debug);
				return true;
			}

			if (!CanBeMerged(targetStack, gearToAdd))
			{
				Main.Logger.Log("GearItem cant be stacked", FlaggedLoggingLevel.Debug);

				if (!string.IsNullOrWhiteSpace(gearToAdd.name))
				{
					if (!string.IsNullOrWhiteSpace(targetStack.name)) Main.Logger.Log($"CanBeMerged({targetStack.name}, {gearToAdd.name})", FlaggedLoggingLevel.Debug);
					else Main.Logger.Log($"CanBeMerged(null, {gearToAdd.name})", FlaggedLoggingLevel.Debug);
				}
				else Main.Logger.Log("gearToAdd.name:null", FlaggedLoggingLevel.Debug);

				return true;
			}

			if (targetStack.m_StackableItem != null)
			{
				if (Settings.Instance.UseMaxHP) normalizedCondition = targetStack.m_GearItemData.MaxHP;

				MergeIntoStack(targetStack, gearToAdd, numUnits);

				//LogEverthing(gearToAdd, targetStack, normalizedCondition, numUnits);
				return true;
			}

			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gearToAdd"></param>
		/// <param name="normalizedCondition"></param>
		/// <param name="numUnits"></param>
		/// <param name="existingGearItem"></param>
		/// <returns></returns>
		public static bool Do(GearItem gearToAdd, float normalizedCondition, int numUnits, ref GearItem? existingGearItem)
		{
			if (gearToAdd == null) return true;
			if (!CanOperate(gearToAdd, normalizedCondition)) return true;

			//GearItem? targetStack = null;
			GearItem? targetStack = StackableItem.GetClosestMatchStackable(GameManager.GetInventoryComponent().m_Items, gearToAdd, normalizedCondition);

			if (targetStack == null)
			{
				Main.Logger.Log($"{gearToAdd.name}::targetStack:null", FlaggedLoggingLevel.Debug);
				existingGearItem = null;

				return true;
			}

			if (!CanBeMerged(targetStack, gearToAdd))
			{
				Main.Logger.Log("GearItem cant be stacked", FlaggedLoggingLevel.Debug);

				if (!string.IsNullOrWhiteSpace(gearToAdd.name))
				{
					if (!string.IsNullOrWhiteSpace(targetStack.name)) Main.Logger.Log($"CanBeMerged({targetStack.name}, {gearToAdd.name})", FlaggedLoggingLevel.Debug);
					else Main.Logger.Log($"CanBeMerged(null, {gearToAdd.name})", FlaggedLoggingLevel.Debug);
				}
				else Main.Logger.Log("gearToAdd.name:null", FlaggedLoggingLevel.Debug);

				existingGearItem = null;
				return true;
			}

			// A lit match should never be stackable
			if (gearToAdd.IsLitMatch())
			{
				existingGearItem = null;
				return false;
			}
			if (targetStack.IsLitMatch())
			{
				existingGearItem = null;
				return false;
			}

			if (targetStack.m_StackableItem != null)
			{
				if (Settings.Instance.UseMaxHP) normalizedCondition = targetStack.m_GearItemData.MaxHP;

				MergeIntoStack(targetStack, gearToAdd, numUnits);
				existingGearItem = targetStack;

				//LogEverthing(gearToAdd, targetStack, normalizedCondition, numUnits);
				return false;
			}

			return true;
		}

		public static void LogEverthing(GearItem? gearToAdd, GearItem? targetStack, float normalizedCondition, int units)
		{
			if (gearToAdd == null && targetStack == null)
			{
				Main.Logger.Log("gearToAdd:null", FlaggedLoggingLevel.Verbose);
				Main.Logger.Log("targetStack:null", FlaggedLoggingLevel.Verbose);
				return;
			}
			else if (gearToAdd == null)
			{
				Main.Logger.Log("gearToAdd:null", FlaggedLoggingLevel.Verbose);
				return;
			}
			else if (targetStack == null)
			{
				Main.Logger.Log("targetStack:null", FlaggedLoggingLevel.Verbose);
				return;
			}

			float difference			= Math.Abs(gearToAdd.CurrentHP - targetStack.CurrentHP);
			string gearToAddName		= "NULL";
			string targetStackName		= "NULL";
			object? gearToAddUID		= CommonUtilities.GetGearItemPDID(gearToAdd);
			object? targetStackUID		= CommonUtilities.GetGearItemPDID(targetStack);

			try
			{
				if (!string.IsNullOrWhiteSpace(gearToAdd.name)) gearToAddName = gearToAdd.name;
			}
			catch { }

			try
			{
				if (!string.IsNullOrWhiteSpace(targetStack.name)) targetStackName = targetStack.name;
			}
			catch { }

			Main.Logger.Log("Begin Detailed Log", FlaggedLoggingLevel.Verbose, LoggingSubType.IntraSeparator);
			Main.Logger.Log($"LogEverthing({gearToAddName}, {targetStackName}, {normalizedCondition}, {units}) Difference in HP:{difference}", FlaggedLoggingLevel.Verbose);

			Main.Logger.Log($"{gearToAddName}:{gearToAddUID}", FlaggedLoggingLevel.Verbose);
			Main.Logger.Log($"\tCurrentHP:{gearToAdd.CurrentHP}, Normalized:{gearToAdd.GetNormalizedCondition()}", FlaggedLoggingLevel.Verbose);

			Main.Logger.Log($"{targetStackName}:{targetStackUID}", FlaggedLoggingLevel.Verbose);
			Main.Logger.Log($"\tCurrentHP:{targetStack.CurrentHP}, Normalized:{targetStack.GetNormalizedCondition()}, m_StackableItem.m_Units:{targetStack.m_StackableItem.m_Units}", FlaggedLoggingLevel.Verbose);

			Main.Logger.Log("End Detailed Log", FlaggedLoggingLevel.Verbose, LoggingSubType.IntraSeparator);
		}
	}
}
