namespace AdminDashboardService.Dtos
{
    public class UserDataListingDto
    {
        public Guid UserId { get; set; }
        public DateTime Date {  get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public Guid? APId { get; set; }
        public Guid? ExpertId { get; set; }
    }
}
