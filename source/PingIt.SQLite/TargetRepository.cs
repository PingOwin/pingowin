using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using PingIt.Lib;

namespace PingIt.Store.SQLite
{
    public class TargetRepository : RepositoryBase, ITargetRepository
    {
        public TargetRepository(string connectionString) : base(connectionString) { }

        public Task<IEnumerable<Target>> GetAll()
        {
            string sql = @"SELECT * FROM Targets";
            return Run(async con => await con.QueryAsync<Target>(sql)); 
        }

        public async Task<bool> Insert(Target target)
        {
            string sql = @"INSERT INTO Targets(url) VALUES(@Url)";
            return (await Run(con => con.ExecuteScalarAsync<int>(sql, target))) == 1;
        }

    }
}
