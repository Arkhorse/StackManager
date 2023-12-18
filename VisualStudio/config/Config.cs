using System.Text.Json.Serialization;

namespace StackManager.config
{
    public class Config
    {
        [JsonInclude]
        public List<string> STACK_MERGE { get; set; } = new();
        [JsonInclude]
        public List<string> CustomHandled { get; set; } = new()
        {
            "GEAR_Potato",
            "GEAR_StumpRemover",
            "GEAR_RecycledCan",
            "GEAR_CoffeeTin",
            "GEAR_GreenTeaPackage",
            "GEAR_Carrot"
        };
    }
}
