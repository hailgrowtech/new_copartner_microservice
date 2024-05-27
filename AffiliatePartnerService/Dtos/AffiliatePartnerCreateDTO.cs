using System.ComponentModel.DataAnnotations;

namespace AffiliatePartnerService.Dtos
{
    public class AffiliatePartnerCreateDTO
    {
        public string Name { get; set; }
        public string? LegalName { get; set; }
        public string? AffiliatePartnerImagePath { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public string? GST { get; set; }
        public string? PAN { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }
        public string ReferralCode { get; set; }
        public string? ReferralLink { get; set; } // Link
        public int? FixCommission1 { get; set; }
        public int? FixCommission2 { get; set; }
        public Guid RelationshipManagerId { get; set; }
        public bool isActive { get; set; }


    }
}
