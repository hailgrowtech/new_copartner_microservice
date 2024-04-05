using Microsoft.EntityFrameworkCore;
using ExpertService.Models;

namespace ExpertService.Data;
public class ExpertsDbContextProd : DbContext
{
    protected readonly IConfiguration _configuration;

    public ExpertsDbContextProd(IConfiguration configuration) => _configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sql server database
        options.UseSqlServer(_configuration.GetConnectionString("CoPartnerExpertsConnectionString"));
    }

    public DbSet<Experts> Experts { get; set; }


}