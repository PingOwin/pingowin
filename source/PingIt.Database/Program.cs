using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


            using (var connection = CreateConnection(connectionString))
            {
                connection.Open();
                Execute(connection, "INSERT INTO Targets (url) values ('http://www.aftenposten.no')");

                var command = new SQLiteCommand("select * from Targets", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("{0} - {1}", reader["id"], reader["url"]);
                }
            }


            Console.ReadKey();
        }

        private static void Execute(SQLiteConnection connection, string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        private static SQLiteConnection CreateConnection(string connectionString)
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
