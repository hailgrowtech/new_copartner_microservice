using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Dtos
{
    public class UserCreateDto
    {
        public Guid? UserId { get; set; }
        public Guid? StackholderId { get; set; }
        public string? UserType { get; set; }
        public string? Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }
        public string? MobileNo { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
