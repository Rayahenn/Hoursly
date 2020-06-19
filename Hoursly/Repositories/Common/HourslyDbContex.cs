using System.Data.Entity;
using System.Data.Entity.Migrations;
using Hoursly.Entities;

namespace Hoursly.Repositories.Common
{
    public class HourslyDbContex : DbContext
    {
        public DbSet<Project> Projects { get; set; }
    }

    internal sealed class Configuration : DbMigrationsConfiguration<HourslyDbContex>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}