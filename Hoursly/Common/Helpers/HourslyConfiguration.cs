using System.Configuration;

namespace Hoursly.Common.Helpers
{
    public static class HourslyConfiguration
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["HourslyDbContex"].ToString();
    }
}