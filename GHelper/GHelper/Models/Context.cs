using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json.Serialization;
using NDepend.Path;
using NodaTime;

namespace GHelper.Models
{
	/// <summary>
	/// Represents the concept referred to as "Game" within the GHUB app, and as an "application"
	/// within its JSON configuration. A single Context can be associated with one or more Profiles.
	/// </summary>
	public class Context
	{
		[JsonIgnore]
		public Collection<Profile> Profiles { get; }

		[JsonPropertyName("applicationFolder")]
		public IDirectoryPath? ApplicationFolder { get; set; }
		[JsonPropertyName("applicationId")]
		public Guid ApplicationID { get; set; }
		[JsonPropertyName("applicationPath")]
		public IFilePath? ApplicationPath { get; set; }
		[JsonPropertyName("categoryColors")]
		public Collection<Object>? CategoryColors { get; set; }
		[JsonPropertyName("commands")]
		public Collection<Object>? Commands { get; set; }
		[JsonPropertyName("databaseId")]
		public Guid DatabaseID { get; set; }
		[JsonPropertyName("isInstalled")]
		public Boolean? IsInstalled { get; set; }
		[JsonPropertyName("lastRunTime")]
		public Instant? LastRunTime { get; set; }
		[JsonPropertyName("name")]
		public String Name { get; set; }
		[JsonPropertyName("posterUrl")]
		public Uri? PosterURL { get; set; }
		[JsonPropertyName("profileUrl")]
		public Uri? ProfileURL { get; set; }
		[JsonPropertyName("version")]
		public UInt16? Version { get; set; }
	}
}