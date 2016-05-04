using PingOwin.Core;

namespace PingOwin.Web
{
    public class DbSettings : IDatabaseSettings
    {
        public DbSettings(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }
    }
}