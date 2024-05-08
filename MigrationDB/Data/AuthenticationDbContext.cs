using Microsoft.EntityFrameworkCore;

namespace MigrationDB.Data;
public class AuthenticationDbContext : AuthenticationDbContextProd
{
    public AuthenticationDbContext(IConfiguration configuration) : base(configuration) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sqlite database
        options.UseSqlServer(_configuration.GetConnectionString("CoPartnerAuthConnectionString"));
    }
}   