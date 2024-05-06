using Microsoft.EntityFrameworkCore;

namespace ExpertsService.Dtos;

public class RAListingReadDto
{
    public string Name { get; set; }
    public int UsersCount { get; set; }
    [Precision(18, 2)]
    public decimal? RAEarning { get; set; }
    [Precision(18, 2)]
    public decimal? CPEarning { get; set; }
}
