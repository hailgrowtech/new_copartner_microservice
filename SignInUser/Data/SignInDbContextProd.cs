using Microsoft.EntityFrameworkCore;
using SignInService.Models;

namespace SignInService.Data;
public class SignInDbContextProd : DbContext
{
    protected readonly IConfiguration _configuration;

    public SignInDbContextProd(IConfiguration configuration) => _configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_configuration.GetConnectionString("CoPartnerSignInConnectionString"));
    }
    public DbSet<PotentialCustomer> Leads { get; set; }

   
}