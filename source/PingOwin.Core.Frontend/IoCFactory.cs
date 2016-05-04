using PingOwin.Core.Interfaces;
using PingOwin.Core.Notifiers;
using PingOwin.Core.Processing;
using PingOwin.Core.Store.SQLite;

namespace PingOwin.Core.Frontend
{
    public static class IoCFactory
    {
        public static PenguinProcessor CreateProcessor(PingConfiguration configuration, 
            DbSettings databaseSettings, 
            INotifierFactory notifierFactory,
            ITransformerFactory transformerFactory)
        {
            return new PenguinProcessor(configuration, 
                        new PenguinRepository(databaseSettings), 
                        new PenguinResultsRepository(databaseSettings),
                        notifierFactory,
                        transformerFactory);
        }
    }
}