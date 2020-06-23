using System.Reflection;
using DbUp;

namespace Hoursly.Database
{
    public class DatabaseMigrator : IDatabaseMigrator
    {
        public void MigrateDatabase(string connectionString)
        {
            var upgradeEngine =
                DeployChanges.To
                    .SQLiteDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            upgradeEngine.PerformUpgrade();
        }
    }
}