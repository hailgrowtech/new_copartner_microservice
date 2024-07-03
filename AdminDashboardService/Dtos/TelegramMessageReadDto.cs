namespace AdminDashboardService.Dtos
{
    public class TelegramMessageReadDto
    {
        public Guid Id { get; set; }
        public string? ChannelName { get; set; }
        public string? ChatId { get; set; }
        public string? JoinMessage { get; set; }
        public string? LeaveMessage { get; set; }
        public string? MarketingMessage { get; set; }
        public string? AssignedTo { get; set; }
        public string? ExpertsName { get; set; }
        public Guid? ExpertsId { get; set; }
        public string? AffiliatePartnersName { get; set; }
        public Guid? AffiliatePartnersId { get; set; }
    }
}
