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
		[JsonProperty("applicationFolder", NullValueHandling=NullValueHandling.Ignore)]
		public IPath? ApplicationFolder { get; set; }
		
		[JsonConverter(typeof(PathJSONConverter))]
		[JsonProperty("applicationPath", NullValueHandling=NullValueHandling.Ignore)]
		public IPath? ApplicationPath { get; set; }

		[JsonProperty("databaseId", NullValueHandling=NullValueHandling.Ignore)]
		public Guid? DatabaseID { get; set; }
		
		[JsonProperty("isInstalled", NullValueHandling=NullValueHandling.Ignore)]
		public Boolean? IsInstalled { get; set; }

		[JsonProperty("posterUrl", NullValueHandling=NullValueHandling.Ignore)]
		public Uri? PosterURL { get; set; }
		
		[JsonProperty("profileUrl", NullValueHandling=NullValueHandling.Ignore)]
		public Uri? ProfileURL { get; set; }

		[JsonProperty("version", NullValueHandling=NullValueHandling.Ignore)]
		public UInt16? Version { get; set; }
		
		[JsonIgnore]
		public virtual string? DisplayName
		{
			get { return this.Name; }
		}
	}
}