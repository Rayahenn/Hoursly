using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoursly.Common.Helpers
{
    public static class HourslyConfiguration
    {
       public static string ConnectionString => ConfigurationManager.ConnectionStrings["HourslyDbContex"].ToString();
    }
}
