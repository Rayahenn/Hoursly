using Hoursly.Domain.Projects;
using Microsoft.EntityFrameworkCore;

namespace Hoursly.Persistance
{
    public class HourslyDbContext : DbContext
    {
        public HourslyDbContext(
            DbContextOptions<HourslyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(HourslyDbContext).Assembly);
        }
    }
}