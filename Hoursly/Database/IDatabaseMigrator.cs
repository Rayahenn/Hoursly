namespace Hoursly.Database
{
    public interface IDatabaseMigrator
    {
        bool MigrateDatabase(string connectionString);
    }
}