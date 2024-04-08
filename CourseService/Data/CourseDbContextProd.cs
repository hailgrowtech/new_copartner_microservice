using CourseService.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Data
{
    public class CourseDbContextProd : DbContext
    {
        protected readonly IConfiguration _configuration;

        public CourseDbContextProd(IConfiguration configuration) => _configuration = configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(_configuration.GetConnectionString("CoPartnerExpertsConnectionString"));
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseBooking> CourseBookings { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }
    }
}
