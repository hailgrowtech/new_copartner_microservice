using Microsoft.EntityFrameworkCore;
using AuthenticationService.Models;
namespace AuthenticationService.Helpers;
public class DataContext : DbContext
{
    public DbSet<Authentication> Authentications { get; set; }

    private readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // in memory database used for simplicity, change to a real db for production applications
       // options.UseInMemoryDatabase("Copartner_Auth");
    }
}