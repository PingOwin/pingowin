using System;
using System.Configuration;
using PingOwin.Core.Interfaces;

namespace PingOwin.Core.Frontend
{
    public class ConfigFileSettingsDuplicate : IDatabaseSettings
    {
        private readonly string _connectionString;

        public ConfigFileSettingsDuplicate()
        {
            
        }

        public ConfigFileSettingsDuplicate(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string ConnectionString
        {
            get
            {
                return _connectionString ?? ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            }
        }

        public int TickInterval => GetAppsettingInt("TickInterval", 5000);

        private int GetAppsettingInt(string key, int defaultValue)
        {
            int dummy;
            return Int32.TryParse(ConfigurationManager.AppSettings[key], out dummy) ? dummy : defaultValue;
        }
    }
}