using System.Text.Json.Serialization;

namespace StackManager.config
{
	public class Config
	{
		[JsonInclude]
		public Version ConfigurationVersion { get; set; } = new();
		[JsonInclude]
		public List<string> STACK_MERGE { get; set; } = new();
		[JsonInclude]
		public List<string> Advanced { get; set; } = new();
		[JsonInclude]
		public List<string> AddStackableComponent { get; set; } = new();
	}
}
