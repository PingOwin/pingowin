using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using PingIt.Lib;
using PingOwin;

namespace PingIt.Store.SQLite
{
    public class PenguinRepository : RepositoryBase, IPenguinRepository
    {
        public PenguinRepository(IDatabaseSettings settings) : base(settings) { }

        public Task<IEnumerable<Penguin>> GetAll()
        {
            string sql = @"SELECT * FROM Penguins";
            return Run(async con => await con.QueryAsync<Penguin>(sql)); 
        }

        public async Task<bool> Insert(Penguin target)
        {
            string sql = @"INSERT INTO Penguins(url) VALUES(@Url)";
            return (await Run(con => con.ExecuteScalarAsync<int>(sql, target))) == 1;
        }
    }
}
