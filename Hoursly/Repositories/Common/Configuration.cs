using System.Data.Entity.Migrations;

namespace Hoursly.Repositories.Common
{
    internal sealed class Configuration : DbMigrationsConfiguration<HourslyDbContex>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}