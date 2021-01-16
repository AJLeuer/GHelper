using System.Drawing;
using GHelperLogic.Utility;
using Newtonsoft.Json;

namespace GHelperLogic.Models
{
	public class Categorycolor
	{
		[JsonConverter(typeof(ColorJSONConverter))]
		[JsonProperty("hex")]
		public Color? Hex { get; set; }
	    
		[JsonProperty("tag")]
		public string? Tag { get; set; }
	}
}