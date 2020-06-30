using System;
using FreeSql;
using FreeSqlBuilder.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace XUnitTestFsBuilderProject
{
    public abstract class TestBase
    {
        protected IServiceCollection Service = new ServiceCollection();
        protected IServiceProvider ServiceProvider;
        protected IConfiguration Configuration;
        protected TestBase()
        {
            Configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json").Build();
            Service.AddLogging();
            Service.AddSingleton<ILoggerFactory, NullLoggerFactory>();
            Service.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            Service.AddSingleton<IFreeSql<FsBuilder>>(x => new FreeSql.FreeSqlBuilder()
                .UseConnectionString(DataType.Sqlite, Configuration.GetConnectionString("Sqlite"))
                .UseAutoSyncStructure(true)
                .Build<FsBuilder>());
            ServiceProvider = Build();
        }

        public abstract void ServiceConfig();

        public IServiceProvider Build()
        {
            ServiceConfig();
            return Service.BuildServiceProvider();
        }

    }
}
