using Microsoft.EntityFrameworkCore;

namespace MigrationDB.Data;
public class CoPartnerDbContext : CoPartnerDbContextProd
{
    public CoPartnerDbContext(IConfiguration configuration) : base(configuration) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sqlite database
        options.UseSqlServer(_configuration.GetConnectionString("CoPartnerConnectionString"));
    }
}