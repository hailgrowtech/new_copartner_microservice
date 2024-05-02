namespace ExpertService.Dtos;
public class ExpertsPasswordDTO
{
    public Guid Id { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public DateTime LastPasswordUpdateDate { get; set; } = DateTime.Now;
}

