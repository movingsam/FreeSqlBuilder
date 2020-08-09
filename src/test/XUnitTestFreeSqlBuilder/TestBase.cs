using System;
using System.Diagnostics;
using AngularGenerator;
using FreeSql;
using FreeSqlBuilder.Core;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace XUnitTestFreeSqlBuilder
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
            var listener = new DiagnosticListener("Microsoft.AspNetCore");
            Service.AddSingleton<DiagnosticListener>(listener);
            Service.AddSingleton<DiagnosticSource>(listener);
            ServiceProvider = Build();
        }

        public abstract void ServiceConfig();
        

        public IServiceProvider Build()
        {
            ServiceConfig();
            return Service.BuildServiceProvider();
        }

    }

    public class TestServerFactory : WebApplicationFactory<Startup>
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder(null)
                .UseStartup<Startup>();
        }
    }
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
