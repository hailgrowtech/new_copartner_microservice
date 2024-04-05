using Microsoft.EntityFrameworkCore;
using AuthenticationService.Models;

namespace AuthenticationService.Data;
public class AuthenticationDbContextProd : DbContext
{
    protected readonly IConfiguration _configuration;

    public AuthenticationDbContextProd(IConfiguration configuration) => _configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sql server database
        options.UseSqlServer(_configuration.GetConnectionString("COPartnerAuthConnectionString"));
    }

    public DbSet<Authentication> Authentications { get; set; }
    public DbSet<AuthenticationDetail> AuthenticationDetails { get; set; }

}