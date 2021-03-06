using System;
using FreeSql;
using FreeSqlBuilder.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace XUnitTestFsBuilderCore
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
