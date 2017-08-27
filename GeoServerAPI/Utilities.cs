using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServerAPI
{
    public static class Utilities
    {
        public static string GetAppConfigValue(string key)
        {
            string value = string.Empty;

            if (ConfigurationManager.AppSettings[key] != null)
            {
                value = ConfigurationManager.AppSettings[key];
            }
            else
            {
                throw new Exception(key + " does not exist in config file");
            }

            return value;
        }

        public static string GetJsonText(string fileName, string workspace = null, string dataStore = null, string layerName = null, string nativeName = null, string styleName = null, string styleFileName = null, string shapeFileName = null)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Admin/Template", fileName);
            string jsonText = string.Empty;

            if (!File.Exists(filePath))
            {
                throw new Exception(fileName + " not found.");
            }

            try
            {
                jsonText = File.ReadAllText(filePath);
                jsonText = jsonText.Replace("#workspace_name#", workspace).Replace("#datastore_name#", dataStore).Replace("#layer_name#", layerName).Replace("#native_name#", nativeName).Replace("#style_name#", styleName).Replace("#style_file_name#", styleFileName).Replace("#shape_file_name#", shapeFileName);
                jsonText = jsonText.Replace("#API_BASE_URL#", GetAppConfigValue("API_BASE_URL"));
                jsonText = jsonText.Replace("#GEO_URL#", GetAppConfigValue("GEO_URL"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return jsonText;
        }
    }
}
