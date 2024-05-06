﻿using CommonLibrary.CommonModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationDB.Models;
[Table("User")]
public class User : BaseModel
{
    public string Name { get; set; }
    public string? UserImagePath { get; set; }
    public string? Email { get; set; }
    public string? MobileNumber { get; set; }
    public string SubscriptionType { get; set; }
    public string SubscriptionId { get; set; }
    public bool? isKYC { get; set; }
    public string? AadharCardImagePath { get; set; }
    public string? ReferralCode { get; set; } // The referral code used by the AP
    public Guid? AffiliatePartnerId { get; set; } // The ID of the AP who referred this user
    public string? ReferralMode { get; set; } // The referral Mode used by the AR,AP,CP
    public Guid? ExpertsID { get; set; }
    public Guid? AdvertisingAgencyId { get; set; }
    public Guid? ExpertsAdvertisingAgencyId { get; set; }
}
