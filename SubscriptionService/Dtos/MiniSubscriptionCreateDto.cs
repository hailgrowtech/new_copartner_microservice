namespace SubscriptionService.Dtos
{
    public class MiniSubscriptionCreateDto
    {
        public Guid? ExpertId { get; set; }
        public Guid? SubscriptionId { get; set; }
        public string? MiniSubscriptionLink { get; set; }
    }
}
