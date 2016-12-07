using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServerAPI
{
    public static class Global
    {
        public static string API_BASE_URL = Utilities.GetAppConfigValue("API_BASE_URL");
    }
}
