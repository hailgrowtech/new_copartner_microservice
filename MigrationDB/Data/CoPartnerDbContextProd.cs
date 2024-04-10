using Microsoft.EntityFrameworkCore;
using MigrationDB.Model;
using MigrationDB.Models;

namespace MigrationDB.Data;
public class CoPartnerDbContextProd : DbContext
{
    protected readonly IConfiguration _configuration;

    public CoPartnerDbContextProd(IConfiguration configuration) => _configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sql server database
        options.UseSqlServer(_configuration.GetConnectionString("CoPartnerConnectionString"));
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Experts> Experts { get; set; }
    public DbSet<ExpertsType> ExpertsType { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseBooking> CourseBookings { get; set; }
    public DbSet<CourseStatus> CourseStatuses { get; set; }
    public DbSet<SubscriptionMst> subscriptionMsts { get; set; }
    public DbSet<Subscriber> subscribers { get; set; }


}