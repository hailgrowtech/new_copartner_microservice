namespace AdminDashboardService.Dtos
{
    public class UserFirstTimePaymentListingDto
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public decimal Payment { get; set; }
        public string MobileNumber { get; set; }
        public Guid? APId {  get; set; }
        public Guid? RAId { get; set; }
        public Guid? PaymentRAId { get; set; }
        public string PaymentRAName { get; set; }
        public string? ReferralMode { get; set; }
        public Guid? RASubscriber { get; set; }
        public bool? IsSpecialSubscription { get; set; }
        //public string? AffiliatePartnerName { get; set; }
        //public string? ExpertName { get; set; }
    }
}
