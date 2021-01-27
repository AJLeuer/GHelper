using System;
using GHelperLogic.Utility.JSONConverter;
using NDepend.Path;
using Newtonsoft.Json;

namespace GHelperLogic.Model
{
	public abstract class GHubRecord
	{
		[JsonProperty("name")]
		public string? Name { get; set; }

		[JsonProperty("applicationId")]
		public Guid? ApplicationID { get; set; }
		
		[JsonConverter(typeof(PathJSONConverter))]
		[JsonProperty("applicationFolder")]
		public IPath? ApplicationFolder { get; set; }
		
		[JsonConverter(typeof(PathJSONConverter))]
		[JsonProperty("applicationPath")]
		public IPath? ApplicationPath { get; set; }

		[JsonProperty("databaseId")]
		public Guid? DatabaseID { get; set; }
		
		[JsonProperty("isInstalled")]
		public Boolean? IsInstalled { get; set; }

		[JsonProperty("posterUrl")]
		public Uri? PosterURL { get; set; }
		
		[JsonProperty("profileUrl")]
		public Uri? ProfileURL { get; set; }

		[JsonProperty("version")]
		public UInt16? Version { get; set; }
		
		[JsonIgnore]
		public virtual string? DisplayName
		{
			get { return this.Name; }
		}
	}
}