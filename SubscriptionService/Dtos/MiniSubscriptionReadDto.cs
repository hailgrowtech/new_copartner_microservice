namespace SubscriptionService.Dtos
{
    public class MiniSubscriptionReadDto
    {
        public Guid Id { get; set; }
        public Guid? ExpertId { get; set; }
        public Guid? SubscriptionId { get; set; }
        public string? MiniSubscriptionLink { get; set; }
    }
}
