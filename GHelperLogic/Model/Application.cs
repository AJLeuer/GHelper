using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	public class Application : GHubRecord
	{
		public Application() { }

		public Application(Application application)
		{
			Profiles = application.Profiles;
			ApplicationFolder = application.ApplicationFolder;
			ApplicationID = application.ApplicationID;
			ApplicationPath = application.ApplicationPath;
			CategoryColors = application.CategoryColors;
			Commands = application.Commands;
			DatabaseID = application.DatabaseID;
			IsInstalled = application.IsInstalled;
			LastRunTime = application.LastRunTime;
			// ReSharper disable once VirtualMemberCallInConstructor
			Name = application.Name;
			PosterURL = application.PosterURL;
			ProfileURL = application.ProfileURL;
			Version = application.Version;
			AdditionalData = application.AdditionalData;
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
		public Collection<Profile> Profiles { get; } = new ();

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