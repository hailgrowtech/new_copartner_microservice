namespace ExpertsService.Dtos
{
    public class StandardQuestionsCreateDto
    {
        public Guid ExpertId { get; set; }
        public string ChatId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
