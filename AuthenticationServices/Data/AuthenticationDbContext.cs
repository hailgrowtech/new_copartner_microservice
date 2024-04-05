using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Data;
public class AuthenticationDbContext : AuthenticationDbContextProd
{
    public AuthenticationDbContext(IConfiguration configuration) : base(configuration) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sqlite database
        options.UseSqlServer(_configuration.GetConnectionString("CoPartnerAuthConnectionString"));
    }
}   