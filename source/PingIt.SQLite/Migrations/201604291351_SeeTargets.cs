using FluentMigrator;

namespace PingIt.Store.SQLite.Migrations
{
    [Migration(201604291351)]
    public class _201604291351_SeeTargets : ForwardOnlyMigration
    {
        public override void Up()
        {
            Insert.IntoTable("Targets").Row(new {Url = "http://pingow.in"});
        }
    }
}
