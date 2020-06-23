using System.Data.Entity;
using Hoursly.Entities;

namespace Hoursly.Repositories.Common
{
    public class HourslyDbContex : DbContext
    {
        public DbSet<Project> Projects { get; set; }
    }
}