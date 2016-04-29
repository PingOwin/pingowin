using System.Configuration;
using PingIt.Store.SQLite;

namespace PingIt.WindowsService
{
    internal class ConfigFileSettings :  IDatabaseSettings
    {
        public string ConnectionString => ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
    }
}