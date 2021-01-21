using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	/// within its JSON configuration/state file. A single Context can be associated with one or more Profiles.
	/// </summary>
	public class Context
	{
		public Context() { }

		public Context(Context context)
		{
			Profiles = context.Profiles;
			ApplicationFolder = context.ApplicationFolder;
			ApplicationID = context.ApplicationID;
			ApplicationPath = context.ApplicationPath;
			CategoryColors = context.CategoryColors;
			Commands = context.Commands;
			DatabaseID = context.DatabaseID;
			IsInstalled = context.IsInstalled;
			LastRunTime = context.LastRunTime;
			// ReSharper disable once VirtualMemberCallInConstructor
			Name = context.Name;
			PosterURL = context.PosterURL;
			ProfileURL = context.ProfileURL;
			Version = context.Version;
			AdditionalData = context.AdditionalData;
		}

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
		public Image? Poster { get; set; }
		
		[JsonExtensionData]
		public IDictionary<string, JToken>? AdditionalData { get; set; }

		public bool HasPoster()
		{
			return ((PosterURL != null) || (IsCustom == true));
		}
	}

	public class DesktopContext : Context
	{
		public static readonly string DesktopContextDefaultName = "APPLICATION_NAME_DESKTOP";
		public static readonly string DesktopContextFriendlyName = "Desktop";

		[JsonIgnore]
		public override string DisplayName { get { return DesktopContextFriendlyName; } }

		public DesktopContext(Context desktopContext) :
			base(desktopContext)
		{
		}
	}

	public static class ContextExtensions
	{
		public static Context? GetByID(this IEnumerable<Context> contexts, Guid? id)
		{
			foreach (Context context in contexts)
			{
				if ((context.ID is { } contextID) && (contextID == id))
				{
					return context;
				}
			}

			return null;
		}
	} 

}