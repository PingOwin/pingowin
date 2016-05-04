using FluentMigrator;

namespace PingOwin.Core.Store.SQLite.Migrations
{
    [Migration(201604291351)]
    public class _201604291351_SeedPenguins : ForwardOnlyMigration
    {
        public override void Up()
        {
            Insert.IntoTable("Penguins").Row(new {Url = "http://pingow.in"});
        }
    }
}
