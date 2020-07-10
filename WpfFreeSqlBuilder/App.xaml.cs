using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FreeSqlBuilder;
using FreeSqlBuilder.TemplateEngine;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using WpfFreeSqlBuilder.ViewModal;

namespace WpfFreeSqlBuilder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider ServiceProvider;
        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var main = ServiceProvider.GetRequiredService<MainWindow>();
            main.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.AddSingleton<IWebHostEnvironment>(f => new DefaultWebHostEnvironment
            {
                WebRootFileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory()),
                ApplicationName = "WpfFreeSqlBuilder",
                WebRootPath = Directory.GetCurrentDirectory(),
                ContentRootFileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory()),
                ContentRootPath = Directory.GetCurrentDirectory()

            });
            services.AddFreeSqlBuilder();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModal>();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
        }
    }
}
