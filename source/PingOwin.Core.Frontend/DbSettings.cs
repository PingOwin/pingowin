namespace PingOwin.Core.Frontend
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