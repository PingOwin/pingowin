using PingOwin.Core.Processing;
using PingOwin.Core.Store.SQLite;

namespace PingOwin.Core.Frontend
{
    public static class IoCFactory
    {
        public static PenguinProcessor CreateProcessor(PingConfiguration configuration, DbSettings databaseSettings)
        {
            return new PenguinProcessor(new Pinger(configuration), new PenguinRepository(databaseSettings), new PenguinResultsRepository(databaseSettings));
        }
    }
}