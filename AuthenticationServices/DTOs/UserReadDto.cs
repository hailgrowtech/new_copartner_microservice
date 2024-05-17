namespace AuthenticationService.Dtos
{
    public class UserReadDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid StackholderId { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
    }
}
