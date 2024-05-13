using Microsoft.EntityFrameworkCore;

namespace ExpertsService.Dtos;

public class RAListingReadDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public int UsersCount { get; set; }
    [Precision(18, 2)]
    public decimal? RAEarning { get; set; }
    [Precision(18, 2)]
    public decimal? CPEarning { get; set; }
}

public class RAListingDataReadDto
{
    public string RAName { get; set; }
    public DateTime? Date { get; set; }
    public string? UserMobileNo { get; set; }
    public string? APName { get; set; }

    public decimal? Amount { get; set; }
    public string Subscription { get; set; }
}
