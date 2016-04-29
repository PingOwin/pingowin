using System;
using System.Data;
using System.Data.SQLite;

namespace PingIt.Store.SQLite
{
    public class RepositoryBase
    {
        private readonly string _connectionString;

        protected RepositoryBase(IDatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        protected TReturn Run<TReturn>(Func<IDbConnection, TReturn> getter)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return getter(connection);
            }
        }

        private SQLiteConnection CreateConnection()
        {
            return new SQLiteConnection(_connectionString);
        }
    }
}