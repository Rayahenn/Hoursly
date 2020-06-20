using System.Reflection;
using DbUp;

namespace Hoursly.Database
{
    public class DatabaseMigrator : IDatabaseMigrator
    {
        public void MigrateDatabase(string connectionString)
        {
            var upgrader =
                DeployChanges.To
                    .SQLiteDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            upgrader.PerformUpgrade();
        }
    }
}