using System.Text.Json.Serialization;

namespace GHelper.Models
{

    public class Profile
    {
	    [JsonPropertyName("activeForApplication")]
	    public bool ActiveForApplication { get; set; }
	    [JsonPropertyName("applicationId")]
	    public string ApplicationID { get; set; }
	    [JsonPropertyName("assignments")]
	    public Assignment[] Assignments { get; set; }
	    [JsonPropertyName("id")]
	    public string ID { get; set; }
	    [JsonPropertyName("lightingCard")]
	    public string LightingCard { get; set; }
	    [JsonPropertyName("name")]
	    public string Name { get; init; }
	    [JsonPropertyName("syncLightingCard")]
	    public string SyncLightingCard { get; set; }
    }

    public class Assignment
    {
	    [JsonPropertyName("cardId")]
	    public string CardID { get; set; }
	    [JsonPropertyName("slotId")]
	    public string SlotID { get; set; }
    }

}