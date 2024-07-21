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
    public DbSet<WithdrawalMode> WithdrawalModes { get; set; }
    public DbSet<RelationshipManager> RelationshipManagers { get; set; }
    public DbSet<Join> Joins { get; set; }
    public DbSet<EmailStatus> EmailStatus { get; set; }
    public DbSet<APGeneratedLinks> APGeneratedLinks { get; set; }
    public DbSet<PaymentResponse> PaymentResponses { get; set; }
    public DbSet<WebinarMst> WebinarMsts { get; set; }
    public DbSet<WebinarBooking> WebinarBookings { get; set; }
    public DbSet<ExpertAvailability> ExpertAvailabilities { get; set; }
    public DbSet<TelegramMessage> TelegramMessages { get; set; }
    public DbSet<ChatUser> ChatUsers { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<MinisubscriptionLink> MinisubscriptionLink { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatMessage>()
            .HasOne(cm => cm.Sender)
            .WithMany()
            .HasForeignKey(cm => cm.SenderId)
            .OnDelete(DeleteBehavior.Restrict); // Ensure this is set to Restrict

        modelBuilder.Entity<ChatMessage>()
            .HasOne(cm => cm.Receiver)
            .WithMany()
            .HasForeignKey(cm => cm.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict); // Ensure this is set to Restrict

        base.OnModelCreating(modelBuilder);
    }

}
