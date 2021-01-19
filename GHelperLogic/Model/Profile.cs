using System;
using System.Collections.Generic;
using GHelperLogic.Utility.JSONConverter;
using NDepend.Path;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GHelperLogic.Model
{
	public class Profile 
    {
	    public Profile() {}
	    
	    public Profile(Profile profile)
	    {
		    Context = profile.Context;
		    ActiveForApplication = profile.ActiveForApplication;
		    ApplicationFolder = profile.ApplicationFolder;
		    ApplicationID = profile.ApplicationID;
		    ApplicationPath = profile.ApplicationPath;
		    Assignments = profile.Assignments;
		    CategoryColors = profile.CategoryColors;
		    Commands = profile.Commands;
		    DatabaseID = profile.DatabaseID;
		    IsInstalled = profile.IsInstalled;
		    ID = profile.ID;
		    LightingCard = profile.LightingCard;
		    Name = profile.Name;
		    PosterURL = profile.PosterURL;
		    ProfileURL = profile.ProfileURL;
		    SyncLightingCard = profile.SyncLightingCard;
		    Version = profile.Version;
		    AdditionalData = profile.AdditionalData;
	    }

	    [JsonIgnore]
	    public Context? Context { get; set; }
	    
	    [JsonIgnore]
	    public virtual string? DisplayName
	    {
		    get { return this.Name; }
	    }
	    
	    [JsonProperty("activeForApplication")]
	    public bool? ActiveForApplication { get; set; }
	    
	    [JsonConverter(typeof(PathJSONConverter))]
	    [JsonProperty("applicationFolder")]
	    public IPath? ApplicationFolder { get; set; }
	    
	    [JsonProperty("applicationId")]
	    public Guid? ApplicationID { get; set; }
	    
	    [JsonConverter(typeof(PathJSONConverter))]
	    [JsonProperty("applicationPath")]
	    public IPath? ApplicationPath { get; set; }

	    [JsonProperty("assignments")]
	    public Assignment[]? Assignments { get; set; }
	    
	    [JsonProperty("categoryColors")]
	    public CategoryColor[]? CategoryColors { get; set; }
	    
	    [JsonProperty("commands")]
	    public Command[]? Commands { get; set; }
	    
	    [JsonProperty("databaseId")]
	    public Guid? DatabaseID { get; set; }
	    
	    [JsonProperty("isInstalled")]
	    public Boolean? IsInstalled { get; set; }

	    [JsonProperty("id")]
	    public Guid? ID { get; set; }

	    [JsonProperty("lightingCard")]
	    public Guid? LightingCard { get; set; }
	    
	    [JsonProperty("name")]
	    public string? Name { get; set; }
	    
	    [JsonProperty("posterUrl")]
	    public Uri? PosterURL { get; set; }
	    
	    [JsonProperty("profileUrl")]
	    public Uri? ProfileURL { get; set; }
	    
	    [JsonProperty("syncLightingCard")]
	    public Guid? SyncLightingCard { get; set; }
	    
	    [JsonProperty("version")]
	    public UInt16? Version { get; set; }
	    
	    [JsonExtensionData]
	    public IDictionary<string, JToken>? AdditionalData { get; set; }
    }

	public class DefaultProfile : Profile
	{
		public static readonly string DefaultProfileDefaultName = "PROFILE_NAME_DEFAULT";
		public static readonly string DefaultProfileFriendlyName = "Default";

		public DefaultProfile(Profile profile) : base(profile)
		{
		}

		[JsonIgnore]
		public override string DisplayName
		{
			get { return DefaultProfileFriendlyName; }
		}
	}

    public class Assignment
    {
	    [JsonProperty("cardId")]
	    public Guid? CardID { get; set; }
	    
	    [JsonProperty("slotId")]
	    public string? SlotID { get; set; }
    }
}