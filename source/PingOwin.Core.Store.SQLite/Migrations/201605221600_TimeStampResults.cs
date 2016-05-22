using FluentMigrator;

namespace PingOwin.Core.Store.SQLite.Migrations
{
    [Migration(201605221600)]
    public class TimeStampResults : ForwardOnlyMigration
    {
        public override void Up()
        {
            Alter.Table("PenguinResults").AddColumn("TimeStamp").AsDateTime().Nullable();
        }
    }
}
