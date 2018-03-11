using System.Configuration;

namespace DiplomaProjectManagement.Common
{
    public static class ConfigHelper
    {
        public static string GetByKey(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}