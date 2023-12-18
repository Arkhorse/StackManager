using Il2CppSystem.Text.RegularExpressions;

namespace StackManager.Utilities
{
    internal class CommonUtilities
    {
        // NOTE: These "Get" methods are volitile. Ensure it is always up to date as otherwise it can break anything tied to GearItem
        // This is used to load prefab info of a GearItem
        [return: NotNullIfNotNull(nameof(name))]
        public static GearItem GetGearItemPrefab(string name) => GearItem.LoadGearItemPrefab(name);
        [return: NotNullIfNotNull(nameof(name))]
        public static ToolsItem GetToolItemPrefab(string name) => GearItem.LoadGearItemPrefab(name).GetComponent<ToolsItem>();
        [return: NotNullIfNotNull(nameof(name))]
        public static ClothingItem GetClothingItemPrefab(string name) => GearItem.LoadGearItemPrefab(name).GetComponent<ClothingItem>();

        /// <summary>
        /// Normalizes the name given to remove extra bits using regex for most of the changes
        /// </summary>
        /// <param name="name">The name of the thing to normalize</param>
        /// <returns>Normalized name without <c>(Clone)</c> or any numbers appended</returns>
        [return: NotNullIfNotNull(nameof(name))]
        public static string? NormalizeName(string name)
        {
            string name0 = Regex.Replace(name, @"(?:\(\d{0,}\))", string.Empty);
            string name1 = Regex.Replace(name0, @"(?:\s\d{0,})", string.Empty);
            string name2 = name1.Replace("(Clone)", string.Empty, StringComparison.InvariantCultureIgnoreCase);
            string name3 = name2.Replace("\0", string.Empty);
            return name3.Trim();
        }

        /// <summary>
        /// Checks if the player is currently involved in anything that would make modded actions unwanted
        /// </summary>
        /// <param name="PlayerManagerComponent">The current instance of the PlayerManager component, use <see cref="GameManager.GetPlayerManagerComponent()"/></param>
        /// <returns><c>true</c> if the player isnt dead, in a conversation, locked or in a cinematic</returns>
        public static bool IsPlayerAvailable(PlayerManager PlayerManagerComponent)
        {
            if (PlayerManagerComponent == null) return false;

            bool first      = PlayerManagerComponent.m_ControlMode == PlayerControlMode.Dead;
            bool second     = PlayerManagerComponent.m_ControlMode == PlayerControlMode.InConversation;
            bool third      = PlayerManagerComponent.m_ControlMode == PlayerControlMode.Locked;
            bool fourth     = PlayerManagerComponent.m_ControlMode == PlayerControlMode.InFPCinematic;

            return first && second && third && fourth;
        }
    }
}
