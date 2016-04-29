using FluentMigrator;

namespace PingIt.Store.SQLite.Migrations
{
    [Migration(201604291116)]
    public class _201604291116_CreatePenguins : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Penguins")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Url").AsString(1000).NotNullable();
        }
    }
}
