using System;
using PingIt.Store.SQLite;

namespace PingIt.Database
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Data Source=mydb.db;Version=3;Pooling=True;Max Pool Size=100;";

            var migrator = new Migrator(connectionString);
            migrator.Migrate();

            var repo = new TargetRepository(connectionString);

            repo.Insert(new Target {Url = "http://www.aftenposten.no"}).Wait();

            var targets = repo.GetAll().GetAwaiter().GetResult();

            foreach (var target in targets)
            {
                Console.WriteLine("{0} - {1}", target.Id, target.Url);
            }

            Console.ReadKey();
        }
    }
}
