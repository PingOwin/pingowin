using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using PingOwin.Core.Interfaces;
using PingOwin.Core.Processing;

namespace PingOwin.Core.Store.SQLite
{
    public class PenguinResultsRepository : RepositoryBase, IPenguinResultsRepository
    {
        public PenguinResultsRepository(IDatabaseSettings settings) : base(settings) { }

        public Task<IEnumerable<PenguinResult>> GetAll()
        {
            string sql = @"SELECT * FROM PenguinResults";
            return Run(async con => await con.QueryAsync<PenguinResult>(sql));
        }

        public Task<IEnumerable<PenguinResult>> GetAll(int skip, int take)
        {
            string sql = @"SELECT * FROM PenguinResults ORDER BY TimeStamp DESC LIMIT @Skip, @Count";
            return Run(async con => await con.QueryAsync<PenguinResult>(sql, new { Skip = skip, Count = take}));
        }

        public async Task<bool> Insert(PenguinResult target)
        {
            string sql = @"INSERT INTO PenguinResults(Url,ResponseTime,TimeStamp) VALUES(@Url, @ResponseTime, @TimeStamp)";
            return (await Run(con => con.ExecuteScalarAsync<int>(sql, target))) == 1;
        }
    }
}