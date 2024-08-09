﻿namespace UserService.Dtos;

public class TempUserReadDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? UserImagePath { get; set; }
    public string? Email { get; set; }
    public string MobileNumber { get; set; }
    public bool? isKYC { get; set; }
    public string? PAN { get; set; }
    public string? Address { get; set; }
    public string? State { get; set; }
    public string? ReferralCode { get; set; } // The referral code used by the AP
    public Guid? AffiliatePartnerId { get; set; } // The ID of the AP who referred this user
    public string? ReferralMode { get; set; } // The referral Mode used by the AR,AP,CP
    public Guid? ExpertsID { get; set; }
    public Guid? AdvertisingAgencyId { get; set; }
    public Guid? ExpertsAdvertisingAgencyId { get; set; }
    public string? LandingPageUrl { get; set; }
    public DateTime CreatedOn { get; set; }
}
