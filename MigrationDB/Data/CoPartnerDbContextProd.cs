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
    public DbSet<Course> Course { get; set; }
    public DbSet<CourseBooking> CourseBookings { get; set; }
    public DbSet<CourseStatus> CourseStatus { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Subscriber> Subscribers { get; set; }
    public DbSet<AffiliatePartner> AffiliatePartners { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<AdvertisingAgency> AdvertisingAgencies { get; set; }
    public DbSet<ExpertsAdvertisingAgency> ExpertsAdvertisingAgencies { get; set; }
    public DbSet<MarketingContent> MarketingContents { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Withdrawal> Withdrawals { get; set; }
    public DbSet<WithdrawalPaymentMode> WithdrawalPaymentModes { get; set; }
    public DbSet<RelationshipManager> RelationshipManagers { get; set; }

}