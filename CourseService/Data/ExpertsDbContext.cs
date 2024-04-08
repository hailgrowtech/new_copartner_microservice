using Microsoft.EntityFrameworkCore;

namespace ExpertService.Data;
public class ExpertsDbContext : ExpertsDbContextProd
{
    public ExpertsDbContext(IConfiguration configuration) : base(configuration) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sqlite database
        options.UseSqlServer(_configuration.GetConnectionString("CoPartnerExpertsConnectionString"));
    }
}