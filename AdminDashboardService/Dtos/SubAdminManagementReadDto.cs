namespace AdminDashboardService.Dtos
{
    public class SubAdminManagementReadDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
    }
}
