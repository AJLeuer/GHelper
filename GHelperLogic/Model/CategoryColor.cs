using System.Drawing;
using GHelperLogic.Utility;
using Newtonsoft.Json;

namespace GHelperLogic.Model
{
	public class CategoryColor
	{
		[JsonConverter(typeof(ColorJSONConverter))]
		[JsonProperty("hex")]
		public Color? Hex { get; set; }
	    
		[JsonProperty("tag")]
		public string? Tag { get; set; }
	}
}