using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using GHelperLogic.IO;
using GHelperLogic.Utility.JSONConverter;
using NDepend.Path;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NodaTime;
using SixLabors.ImageSharp;

namespace GHelperLogic.Model 
{
	/// <summary>
	/// Represents the concept referred to as "Game" within the GHUB app, and as an "application"
	/// within its JSON configuration/state file. A single Application can be associated with one or more Profiles.
	/// </summary>
	[SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
	public class Application : GHubRecord, IEquatable<Application>
	{
		public Application() { }

		public Application(Application application)
		{
			CopyStateFrom(application);
		}

		public override GHubRecord Clone()
		{
			return new Application(this);
		}

		public sealed override void CopyStateFrom(GHubRecord otherRecord)
		{
			if (otherRecord is Application otherApplication)
			{
				base.CopyStateFrom(otherRecord);
				CategoryColors = otherApplication.CategoryColors;
				Commands = otherApplication.Commands;
				LastRunTime = otherApplication.LastRunTime;
				IsCustom = otherApplication.IsCustom;
				Poster = otherApplication.Poster;
				PosterPath = otherApplication.PosterPath;
				AdditionalData = otherApplication.AdditionalData;
				Profiles = otherApplication.Profiles;
			}
		}

		[JsonProperty("categoryColors", NullValueHandling=NullValueHandling.Ignore)]
		public CategoryColor[]? CategoryColors { get; set; }
		
		[JsonProperty("commands", NullValueHandling=NullValueHandling.Ignore)]
		public Command[]? Commands { get; set; }

		[JsonConverter(typeof(DateTimeJSONConverter))]
		[JsonProperty("lastRunTime", NullValueHandling=NullValueHandling.Ignore)]
		public LocalDateTime? LastRunTime { get; set; }
		
		[JsonProperty("isCustom", NullValueHandling=NullValueHandling.Ignore)]
		public virtual bool? IsCustom { get; set; }

		[JsonConverter(typeof(PosterImageJSONConverter))]
		[JsonProperty("poster", NullValueHandling=NullValueHandling.Ignore)]
		public Image? Poster { get; set; }
		
		[JsonConverter(typeof(PathJSONConverter))]
		[JsonProperty("posterPath", NullValueHandling=NullValueHandling.Ignore)]
		public IPath? PosterPath { get; set; }

		[JsonExtensionData]
		public IDictionary<string, JToken>? AdditionalData { get; set; }

		[JsonIgnore] 
		public Collection<Profile> Profiles { get; private set; } = new ();

		[JsonIgnore]
		public Guid? ID
		{
			get => this.ApplicationID;
		}

		[JsonIgnore]
		public bool HasPoster
		{
			get { return ((PosterURL != null) || (PosterPath != null) || (Poster != null)); }
		}
		
		public bool ShouldSerializePoster()
		{
			// Newer versions of G Hub store poster images for custom applications in a cache
			// directory, and if the user of G Helper set a custom image for a custom application we'll have already stored 
			// it in that directory, so no need to serialize it into JSON too. If a user happens to be using an older version
			// of G Hub, once they update it'll find the image in the cache by reading the PosterPath property, which we've also
			// already set. The only downside is they won't be able to see their new custom image until they upgrade G Hub.
			return false;
		}

		public static void LoadApplicationPosterImage(Application application)
		{
			// If this is a application with a custom poster (and the poster bitmap was, therefore, serialized into the JSON)
			// ... then it will have already been deserialized and stored into the 'poster' field
			// However if this is not a custom application and it has a posterURL available
			// ... then on the first call to get Poster we initialize it by grabbing the image file from the URL
			// So basically Poster is used to store poster images that can be retrieved in two very different ways.
			// Update 25 Feb 2021: A new GHub update has made this situation even more complicated. Now there are 3 different
			// ways a poster could potentially be stored. The new method is a field "posterPath" which directs to a cached file
			// in the GHub AppData directory.

			if (application.HasPoster)
			{
				if (application.IsCustom == true)
				{
					if (application.PosterPath != null)
					{
						application.Poster = ImageIOHelper.LoadFromFilePath(application.PosterPath);
					}
				}
				else if (application.PosterURL != null)
				{
					application.Poster = ImageIOHelper.LoadFromHTTPURL(application.PosterURL!);
				}
			}
		}

		#region EqualityMembers
		public bool Equals(Application? other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}
			
			return base.Equals(other) && Equals(CategoryColors, other.CategoryColors) && Equals(Commands, other.Commands) && Nullable.Equals(LastRunTime, other.LastRunTime) && IsCustom == other.IsCustom && Equals(Poster, other.Poster) && Equals(AdditionalData, other.AdditionalData) && Profiles.Equals(other.Profiles) && Nullable.Equals(PosterPath, other.PosterPath);
		}

		public override bool Equals(object? obj)
		{
			if (obj is not Application)
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			return Equals((Application) obj);
		}

		public override int GetHashCode()
		{
			var hashCode = new HashCode();
			hashCode.Add(base.GetHashCode());
			hashCode.Add(CategoryColors);
			hashCode.Add(Commands);
			hashCode.Add(LastRunTime);
			hashCode.Add(IsCustom);
			hashCode.Add(Poster);
			hashCode.Add(PosterPath);
			hashCode.Add(AdditionalData);
			hashCode.Add(Profiles);
			hashCode.Add(ID);
			hashCode.Add(HasPoster);
			return hashCode.ToHashCode();
		}

		public static bool operator == (Application? left, Application? right)
		{
			return Equals(left, right);
		}

		public static bool operator != (Application? left, Application? right)
		{
			return !Equals(left, right);
		}
		
		#endregion
	}

	public class DesktopApplication : Application
	{
		public static readonly string DesktopApplicationDefaultName = "APPLICATION_NAME_DESKTOP";
		public static readonly string DesktopApplicationFriendlyName = "Desktop";

		[JsonIgnore]
		public override string DisplayName { get { return DesktopApplicationFriendlyName; } }

		public DesktopApplication(Application desktopApplication) :
			base(desktopApplication)
		{
		}
	}

	public class CustomApplication : Application
	{
		[JsonProperty("isCustom", NullValueHandling = NullValueHandling.Ignore)]
		public override bool? IsCustom
		{
			get { return true;} 
			set {}
		}
		
		public CustomApplication(Application customApplication) :
			base(customApplication)
		{
		}
	}

	public static class ApplicationExtensions
	{
		public static Application? GetByID(this IEnumerable<Application> applications, Guid? id)
		{
			foreach (Application application in applications)
			{
				if ((application.ID is { } applicationID) && (applicationID == id))
				{
					return application;
				}
			}

			return null;
		}
	} 

}