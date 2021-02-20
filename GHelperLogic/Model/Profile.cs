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
			CopyStateFrom(profile);
	    }

	    public override GHubRecord Clone()
	    {
		    return new Profile(this);
	    }

	    public sealed override void CopyStateFrom(GHubRecord otherRecord)
	    {
		    if (otherRecord is Profile otherProfile)
		    {
			    Application = otherProfile.Application;
			    ActiveForApplication = otherProfile.ActiveForApplication;
			    ApplicationFolder = otherProfile.ApplicationFolder;
			    ApplicationID = otherProfile.ApplicationID;
			    ApplicationPath = otherProfile.ApplicationPath;
			    Assignments = otherProfile.Assignments;
			    CategoryColors = otherProfile.CategoryColors;
			    Commands = otherProfile.Commands;
			    DatabaseID = otherProfile.DatabaseID;
			    IsInstalled = otherProfile.IsInstalled;
			    ID = otherProfile.ID;
			    LightingCard = otherProfile.LightingCard;
			    Name = otherProfile.Name;
			    PosterURL = otherProfile.PosterURL;
			    ProfileURL = otherProfile.ProfileURL;
			    SyncLightingCard = otherProfile.SyncLightingCard;
			    Version = otherProfile.Version;
			    AdditionalData = otherProfile.AdditionalData;
		    }
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