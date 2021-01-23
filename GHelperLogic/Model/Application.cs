﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

		[JsonConverter(typeof(PathJSONConverter))]
		[JsonProperty("applicationFolder")]
		public IPath? ApplicationFolder { get; set; }
		
		[JsonProperty("applicationId")]
		public Guid? ApplicationID { get; set; }
		
		[JsonConverter(typeof(PathJSONConverter))]
		[JsonProperty("applicationPath")]
		public IPath? ApplicationPath { get; set; }
		
		[JsonProperty("categoryColors")]
		public CategoryColor[]? CategoryColors { get; set; }
		
		[JsonProperty("commands")]
		public Command[]? Commands { get; set; }
		
		[JsonProperty("databaseId")]
		public Guid? DatabaseID { get; set; }
		
		[JsonProperty("isInstalled")]
		public Boolean? IsInstalled { get; set; }
		
		[JsonConverter(typeof(DateTimeJSONConverter))]
		[JsonProperty("lastRunTime")]
		public LocalDateTime? LastRunTime { get; set; }
		
		[JsonProperty("name")]
		public string? Name { get; set; }
		
		[JsonProperty("posterUrl")]
		public Uri? PosterURL { get; set; }
		
		[JsonProperty("profileUrl")]
		public Uri? ProfileURL { get; set; }
		
		[JsonProperty("version")]
		public UInt16? Version { get; set; }
		
		[JsonProperty("isCustom")]
		public bool? IsCustom { get; set; }

		[JsonConverter(typeof(PosterImageJSONConverter))]
		[JsonProperty("poster")]
		public Image? Poster
		{
			get
			{
				//If this is a application with a custom poster (and the poster bitmap was, therefore, serialized into the JSON)
				//... then it will have already been deserialized and stored into the 'poster' field
				//However if this is not a custom application and it has a posterURL available
				//... then on the first call to get Poster we initialize it by grabbing the image file from the URL
				//So basically Poster is used to store poster images that can be retrieved in two very different ways.
				
				if ((poster == null) && (PosterURL != null))
				{
					poster = IOHelper.LoadFromURL(PosterURL);
				}
				return poster;
			}
			set { poster = value; }
		}

		[JsonIgnore]
		private Image? poster;

		[JsonExtensionData]
		public IDictionary<string, JToken>? AdditionalData { get; set; }

		[JsonIgnore] 
		public ObservableCollection<Profile> Profiles { get; } = new ();

		[JsonIgnore]
		public Guid? ID
		{
			get => this.ApplicationID;
		}

		[JsonIgnore]
		public virtual string? DisplayName
		{
			get { return this.Name; }
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