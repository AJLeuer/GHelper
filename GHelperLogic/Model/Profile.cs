﻿using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace GHelperLogic.Model
{
	[SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
	public class Profile : GHubRecord, IEquatable<Profile>
	{
		public static readonly string DefaultProfileDefaultName  = "PROFILE_NAME_DEFAULT";
		public static readonly string DefaultProfileFriendlyName = "Default";
		
	    public Profile() {}
	    
	    public Profile(Profile profile)
	    {
			CopyStateFrom(profile);
	    }

	    [JsonProperty("activeForApplication", NullValueHandling=NullValueHandling.Ignore)]
	    public bool? ActiveForApplication { get; set; }

	    [JsonProperty("assignments")]
	    public Assignment[]? Assignments { get; set; }

	    [JsonProperty("id")]
	    public Guid? ID { get; set; }

	    [JsonProperty("lightingCard", NullValueHandling=NullValueHandling.Ignore)]
	    public Guid? LightingCard { get; set; }

	    [JsonProperty("syncLightingCard", NullValueHandling=NullValueHandling.Ignore)]
	    public Guid? SyncLightingCard { get; set; }

	    [JsonIgnore]
	    public Application? Application { get; set; }
	    
	    [JsonIgnore]
	    public override string? DisplayName
	    {
		    get
		    {
			    if (this.Name == DefaultProfileDefaultName)
			    {
				    return DefaultProfileFriendlyName;
			    }
			    else
			    {
				    return base.DisplayName;
			    }
		    }
	    }

	    public override GHubRecord Clone()
	    {
		    return new Profile(this);
	    }

	    public sealed override void CopyStateFrom(GHubRecord otherRecord)
	    {
		    if (otherRecord is Profile otherProfile)
		    {
			    base.CopyStateFrom(otherRecord);
			    Application = otherProfile.Application;
			    ActiveForApplication = otherProfile.ActiveForApplication;
			    Assignments = otherProfile.Assignments;
			    CategoryColors = otherProfile.CategoryColors;
			    Commands = otherProfile.Commands;
			    ID = otherProfile.ID;
			    LightingCard = otherProfile.LightingCard;
			    SyncLightingCard = otherProfile.SyncLightingCard;
			    AdditionalData = otherProfile.AdditionalData;
		    }
	    }

	    #region EqualityMembers
	    public bool Equals(Profile? other)
	    {
		    if (ReferenceEquals(null, other))
		    {
			    return false;
		    }

		    if (ReferenceEquals(this, other))
		    {
			    return true;
		    }
		    
		    return base.Equals(other) && Equals(Application, other.Application) && ActiveForApplication == other.ActiveForApplication && Equals(Assignments, other.Assignments) && Equals(CategoryColors, other.CategoryColors) && Equals(Commands, other.Commands) && Nullable.Equals(ID, other.ID) && Nullable.Equals(LightingCard, other.LightingCard) && Nullable.Equals(SyncLightingCard, other.SyncLightingCard) && Equals(AdditionalData, other.AdditionalData);
	    }

	    public override bool Equals(object? obj)
	    {
		    if (obj is not Profile)
		    {
			    return false;
		    }

		    if (ReferenceEquals(this, obj))
		    {
			    return true;
		    }

		    return Equals((Profile) obj);
	    }

	    public override int GetHashCode()
	    {
		    var hashCode = new HashCode();
		    hashCode.Add(base.GetHashCode());
		    hashCode.Add(Application);
		    hashCode.Add(ActiveForApplication);
		    hashCode.Add(Assignments);
		    hashCode.Add(CategoryColors);
		    hashCode.Add(Commands);
		    hashCode.Add(ID);
		    hashCode.Add(LightingCard);
		    hashCode.Add(SyncLightingCard);
		    hashCode.Add(AdditionalData);
		    return hashCode.ToHashCode();
	    }

	    public static bool operator == (Profile? left, Profile? right)
	    {
		    return Equals(left, right);
	    }

	    public static bool operator != (Profile? left, Profile? right)
	    {
		    return !Equals(left, right);
	    }

	    #endregion
    }

	public class Assignment
    {
	    [JsonProperty("cardId")]
	    public Guid? CardID { get; set; }
	    
	    [JsonProperty("slotId")]
	    public string? SlotID { get; set; }
    }
}