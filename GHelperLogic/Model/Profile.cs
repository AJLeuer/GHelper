using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GHelperLogic.Model
{
	public class Profile : GHubRecord
    {
	    public Profile() {}
	    
	    public Profile(Profile profile)
	    {
		    Application = profile.Application;
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
	    public Application? Application { get; set; }

	    [JsonProperty("activeForApplication", NullValueHandling=NullValueHandling.Ignore)]
	    public bool? ActiveForApplication { get; set; }

	    [JsonProperty("assignments")]
	    public Assignment[]? Assignments { get; set; }
	    
	    [JsonProperty("categoryColors", NullValueHandling=NullValueHandling.Ignore)]
	    public CategoryColor[]? CategoryColors { get; set; }
	    
	    [JsonProperty("commands", NullValueHandling=NullValueHandling.Ignore)]
	    public Command[]? Commands { get; set; }

	    [JsonProperty("id")]
	    public Guid? ID { get; set; }

	    [JsonProperty("lightingCard", NullValueHandling=NullValueHandling.Ignore)]
	    public Guid? LightingCard { get; set; }

	    [JsonProperty("syncLightingCard", NullValueHandling=NullValueHandling.Ignore)]
	    public Guid? SyncLightingCard { get; set; }

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