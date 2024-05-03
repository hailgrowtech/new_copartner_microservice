namespace AdminDashboardService.Dtos
{
    public class MarketingContentReadDto
    {
        public Guid Id { get; set; }
        public string BannerName { get; set; }
        public string? ImagePath { get; set; }
        public string? ContentType { get; set; }
    }
}
