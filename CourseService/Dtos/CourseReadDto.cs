namespace CourseService.Dtos
{
    public class CourseReadDto
    {
        public Guid Id { get; set; }
        public string CourseName { get; set; }
        public int VideoCount { get; set; }
        public DateTime Duration { get; set; }
        public DateTime LaunchDate { get; set; }
        public int ExpertId { get; set; }
        public int Amount { get; set; }
    }
}
