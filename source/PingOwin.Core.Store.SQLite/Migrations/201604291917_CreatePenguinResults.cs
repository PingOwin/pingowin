using FluentMigrator;

namespace PingOwin.Core.Store.SQLite.Migrations
{
    [Migration(201604291917)]
    public class CreatePenguinResults : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("PenguinResults")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Url").AsString(1000).NotNullable()
                .WithColumn("ResponseTime").AsInt16().NotNullable();
        }
    }
}
