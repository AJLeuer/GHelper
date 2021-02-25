using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using GHelperLogic.Utility.JSONConverter;
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
		public bool? IsCustom { get; set; }

		[JsonConverter(typeof(PosterImageJSONConverter))]
		[JsonProperty("poster", NullValueHandling=NullValueHandling.Ignore)]
		public Image? Poster { get; set; }

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
			get { return ((PosterURL != null) || (IsCustom == true)); }
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
			return base.Equals(other) && Equals(CategoryColors, other.CategoryColors) && Equals(Commands, other.Commands) && Nullable.Equals(LastRunTime, other.LastRunTime) && IsCustom == other.IsCustom && Equals(Poster, other.Poster) && Equals(AdditionalData, other.AdditionalData) && Profiles.Equals(other.Profiles);
		}

		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			if (obj.GetType() != this.GetType())
			{
				return false;
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