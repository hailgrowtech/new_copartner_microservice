using Microsoft.EntityFrameworkCore;

namespace CourseService.Data
{
    public class CourseDbContext : CourseDbContextProd
    {
        public CourseDbContext(IConfiguration configuration) : base(configuration) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlServer(_configuration.GetConnectionString("CoPartnerExpertsConnectionString"));
        }
    }
}
