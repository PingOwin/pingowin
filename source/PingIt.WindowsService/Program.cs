using System;
using PingIt.Store.SQLite;

namespace PingIt.WindowsService
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new ConfigFileSettings();
            var migrator = new Migrator(settings);
            migrator.Migrate();

            var repo = new PenguinRepository(settings);
            var all = repo.GetAll().GetAwaiter().GetResult();
            foreach (var penguin in all)
            {
                Console.WriteLine("{0} - {1}", penguin.Id, penguin.Url);
            }

            Console.ReadKey();
        }
    }
}
