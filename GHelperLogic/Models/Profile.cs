using System;
using GHelper.Models;
using GHelperLogic.Utility;
using NDepend.Path;
using Newtonsoft.Json;

namespace GHelperLogic.Models
{
	public class Profile
    {
	    [JsonIgnore]
	    public Context? Context { get; set; }
	    
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
	    public Categorycolor[]? CategoryColors { get; set; }
	    
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
	    public String? Name { get; set; }
	    
	    [JsonProperty("posterUrl")]
	    public Uri? PosterURL { get; set; }
	    
	    [JsonProperty("profileUrl")]
	    public Uri? ProfileURL { get; set; }
	    
	    [JsonProperty("syncLightingCard")]
	    public Guid? SyncLightingCard { get; set; }
	    
	    [JsonProperty("version")]
	    public UInt16? Version { get; set; }
    }

    public class Assignment
    {
	    [JsonProperty("cardId")]
	    public Guid? CardID { get; set; }
	    
	    [JsonProperty("slotId")]
	    public String? SlotID { get; set; }
    }
    public class Categorycolor
    {
	    [JsonProperty("hex")]
	    public String? Hex { get; set; }
	    
	    [JsonProperty("tag")]
	    public string? Tag { get; set; }
    }

    public class Command
    {
	    [JsonProperty("cardId")]
	    public Guid? CardID { get; set; }
	    
	    [JsonProperty("category")]
	    public string? Category { get; set; }
	    
	    [JsonProperty("name")]
	    public string? Name { get; set; }
    }

}