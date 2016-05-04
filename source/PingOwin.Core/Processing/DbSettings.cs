using PingOwin.Core.Interfaces;

namespace PingOwin.Core.Processing
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