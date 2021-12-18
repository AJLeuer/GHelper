using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using GHelperLogic.Utility;
using GHelperLogic.Utility.JSONConverter;
using NDepend.Path;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GHelperLogic.Model
{
	[SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
	public abstract class GHubRecord : ICloneable<GHubRecord>, IEquatable<GHubRecord>
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

		[JsonProperty("categoryColors", NullValueHandling=NullValueHandling.Ignore)]
		public CategoryColor[]? CategoryColors { get; set; }
	    
		[JsonProperty("commands", NullValueHandling=NullValueHandling.Ignore)]
		public Collection<Command>? Commands { get; set; }

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

		[JsonExtensionData]
		public IDictionary<string, JToken>? AdditionalData { get; set; }
		
		[JsonIgnore]
		public virtual string? DisplayName
		{
			get { return this.Name; }
		}

		public abstract GHubRecord Clone();

		public virtual void CopyStateFrom(GHubRecord otherRecord)
		{
			Name = otherRecord.Name;
			ApplicationID = otherRecord.ApplicationID;
			ApplicationFolder = otherRecord.ApplicationFolder;
			ApplicationPath = otherRecord.ApplicationPath;
			DatabaseID = otherRecord.DatabaseID;
			IsInstalled = otherRecord.IsInstalled;
			PosterURL = otherRecord.PosterURL;
			ProfileURL = otherRecord.ProfileURL;
			Version = otherRecord.Version;
		}
		
		#region EqualityMembers
		public bool Equals(GHubRecord? other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}
			
			return Name == other.Name && Nullable.Equals(ApplicationID, other.ApplicationID) && Equals(ApplicationFolder, other.ApplicationFolder) && Equals(ApplicationPath, other.ApplicationPath) && Nullable.Equals(DatabaseID, other.DatabaseID) && IsInstalled == other.IsInstalled && Equals(PosterURL, other.PosterURL) && Equals(ProfileURL, other.ProfileURL) && Version == other.Version;
		}

		public override bool Equals(object? obj)
		{
            if (obj is not GHubRecord)
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			return Equals((GHubRecord) obj);
		}

		public override int GetHashCode()
		{
			var hashCode = new HashCode();
			hashCode.Add(Name);
			hashCode.Add(ApplicationID);
			hashCode.Add(ApplicationFolder);
			hashCode.Add(ApplicationPath);
			hashCode.Add(DatabaseID);
			hashCode.Add(IsInstalled);
			hashCode.Add(PosterURL);
			hashCode.Add(ProfileURL);
			hashCode.Add(Version);
			return hashCode.ToHashCode();
		}

		public static bool operator == (GHubRecord? left, GHubRecord? right)
		{
			return Equals(left, right);
		}

		public static bool operator != (GHubRecord? left, GHubRecord? right)
		{
			return !Equals(left, right);
		}

		#endregion
	}
}