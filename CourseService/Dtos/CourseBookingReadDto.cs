namespace CourseService.Dtos
{ 
    public class CourseBookingReadDto
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime BookingDate { get; set; }
        public int ExpertId { get; set; }
    }
}
