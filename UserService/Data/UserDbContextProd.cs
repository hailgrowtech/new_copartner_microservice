using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Data;
public class UserDbContextProd : DbContext
{
    protected readonly IConfiguration _configuration;

    public UserDbContextProd(IConfiguration configuration) => _configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sql server database
        options.UseSqlServer(_configuration.GetConnectionString("CoPartnerUserConnectionString"));
    }

    public DbSet<User> Users { get; set; }


}