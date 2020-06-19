using System.Reflection;
using DbUp;

namespace Hoursly.Database
{
    public class DatabaseMigrator : IDatabaseMigrator
    {
        public bool MigrateDatabase(string connectionString)
        {
            var upgrader =
                DeployChanges.To
                    .SQLiteDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            return result.Successful;
        }
    }
}