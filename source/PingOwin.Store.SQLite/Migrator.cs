using System;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.SQLite;

namespace PingIt.Store.SQLite
{
    public class Migrator
    {
        private readonly string _connectionString;

        public Migrator(IDatabaseSettings settings)
        {
            _connectionString = settings.ConnectionString;
        }

        public void Migrate()
        {
            var announcer = new TextWriterAnnouncer(Console.Out)
            {
                ShowElapsedTime = true,
                ShowSql = true
            };
            
            IRunnerContext migrationContext = new RunnerContext(announcer);

            var factory = new SQLiteProcessorFactory();
            IMigrationProcessor processor = factory.Create(_connectionString, announcer, new ProcessorOptions());
            var runner = new MigrationRunner(typeof(Migrator).Assembly, migrationContext, processor);
            runner.MigrateUp(true);
        }
        
    }
}
