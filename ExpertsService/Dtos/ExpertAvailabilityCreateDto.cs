namespace ExpertsService.Dtos
{
    public class ExpertAvailabilityCreateDto
    {
        public Guid? ExpertId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Module { get; set; }
        public bool Availability { get; set; }
    }
}
