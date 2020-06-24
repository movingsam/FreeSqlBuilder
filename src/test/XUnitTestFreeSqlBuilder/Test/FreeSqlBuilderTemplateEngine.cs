using System.IO;
using System.Threading.Tasks;
using FreeSql;
using FreeSqlBuilder;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Core.Helper;
using FreeSqlBuilder.Services;
using FreeSqlBuilder.TemplateEngine;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Xunit;

namespace XUnitTestFsBuilderProject
{
    public class FreeSqlBuilderTemplateEngine : TestBase
    {
        public override void ServiceConfig()
        {
            Service.AddSingleton<IWebHostEnvironment>(f => new DefaultWebHostEnvironment
            {
                WebRootFileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory()),
                ApplicationName = "TestApp",
                WebRootPath = Directory.GetCurrentDirectory(),
                ContentRootFileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory()),
                ContentRootPath = Directory.GetCurrentDirectory()

            });
            Service.AddFreeSqlBuilder(x =>
            {
                x.DbSet = new DbSet();
            });
        }

        [Fact]
        public async Task TestTemplateImport()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                //sp.GetService<>()
                sp.GetService<IProjectService>();


            }


        }
    }
}