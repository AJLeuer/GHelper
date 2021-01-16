using System;
using Newtonsoft.Json;

namespace GHelperLogic.Model
{
	public class Command
	{
		[JsonProperty("cardId")]
		public Guid? CardID { get; set; }
		
		[JsonProperty("category")]
		public string? Category { get; set; }
		
		[JsonProperty("name")]
		public string? Name { get; set; }
	}
}