namespace AdminDashboardService.Dtos
{
    public class RelationshipManagerReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string? Email { get; set; }
        public string? ImagePath { get; set; }
        public string? DocumentPath { get; set; }
    }
}
