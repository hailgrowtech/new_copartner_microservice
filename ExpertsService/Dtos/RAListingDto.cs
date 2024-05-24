using Microsoft.EntityFrameworkCore;

namespace ExpertsService.Dtos;

public class RAListingDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public int UsersCount { get; set; }
    [Precision(18, 2)]
    public decimal? RAEarning {  get; set; }
    [Precision(18, 2)]
    public decimal? CPEarning { get; set; }

}
public class RAListingDataDto
{
    public string RAName { get; set; }
    public DateTime? Date { get; set; }
    public string? UserMobileNo { get; set; }
    public string? APName { get; set; }
    [Precision(18, 2)]
    public decimal? Amount { get; set; }
    public string Subscription { get; set; }
    public string PlanType { get; set; }
    public string TransactionId { get; set; }
}
