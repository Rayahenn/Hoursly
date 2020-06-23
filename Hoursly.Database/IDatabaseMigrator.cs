namespace Hoursly.Database
{
    public interface IDatabaseMigrator
    {
        void MigrateDatabase(string connectionString);
    }
}