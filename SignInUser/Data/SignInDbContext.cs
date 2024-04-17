using Microsoft.EntityFrameworkCore;

namespace SignInService.Data;
public class SignInDbContext : SignInDbContextProd
{
    public SignInDbContext(IConfiguration configuration) : base(configuration) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_configuration.GetConnectionString("CoPartnerSignInConnectionString"));
    }
}