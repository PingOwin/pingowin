using System.Configuration;
using PingIt.Store.SQLite;

namespace PingIt.WindowsService
{
    internal class ConfigFileSettings :  IDatabaseSettings
    {
        public string ConnectionString => ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        public int TickInterval => GetAppsettingInt("TickInterval", 5000);

        private int GetAppsettingInt(string key, int defaultValue)
        {
            int dummy;
            return int.TryParse(ConfigurationManager.AppSettings[key], out dummy) ? dummy : defaultValue;
        }
    }
}