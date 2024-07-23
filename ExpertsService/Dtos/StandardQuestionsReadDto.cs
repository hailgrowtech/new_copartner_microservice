namespace ExpertsService.Dtos
{
    public class StandardQuestionsReadDto
    {
        public Guid Id { get; set; }
        public Guid? ExpertId { get; set; }
        public string? ServiceType { get; set; }
        public string? ChatId { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
    }
}
