using FluentMigrator;

namespace PingIt.Store.SQLite.Migrations
{
    [Migration(201604291116)]
    public class _201604291116_CreateTargets : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Targets")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Url").AsString(1000).NotNullable();
        }
    }
}
