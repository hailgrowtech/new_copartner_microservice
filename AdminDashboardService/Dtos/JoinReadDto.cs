namespace AdminDashboardService.Dtos
{
    public class JoinReadDto
    {
        public Guid Id { get; set; }
        public string ChannelName { get; set; }
        public string Link { get; set; }
        public long Count { get; set; }
    }
}
