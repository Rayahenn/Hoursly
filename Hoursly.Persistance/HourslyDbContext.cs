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

        public DbSet<Project> Projects { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HourslyDbContext).Assembly);
        }
    }
}