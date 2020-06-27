using System.Data.Entity;
using Hoursly.Entities;

namespace Hoursly.Repositories.Common
{
    public class HourslyDbContex : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<TimeLog> TimeLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeLog>()
                .HasRequired(timeLog => timeLog.Project)
                .WithMany(project => project.TimeLogs)
                .HasForeignKey(timeLog => timeLog.ProjectId);
        }
    }
}