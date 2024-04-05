using Microsoft.EntityFrameworkCore;

namespace UserService.Data;
public class UserDbContext : UserDbContextProd
{
    public UserDbContext(IConfiguration configuration) : base(configuration) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sqlite database
        options.UseSqlServer(_configuration.GetConnectionString("CoPartnerUserConnectionString"));
    }
}