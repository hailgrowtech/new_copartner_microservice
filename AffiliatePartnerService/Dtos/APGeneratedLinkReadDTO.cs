﻿namespace AffiliatePartnerService.Dtos
{
    public class APGeneratedLinkReadDTO
    {
        public Guid Id { get; set; }
        public Guid? APId { get; set; }
        public string? GeneratedLink { get; set; }
        public string? APReferralLink { get; set; }
        public string? Tag { get; set; }
        public bool? isArchive { get; set; }

    }
}
