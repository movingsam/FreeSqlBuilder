using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FreeSql;
using FreeSqlBuilder;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Core.Helper;
using FreeSqlBuilder.Modals.Base;
using FreeSqlBuilder.Repository;
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
                    x.DbSet.ConnectionString =
                       "Data Source=E:\\Github\\movingsam\\FreeSqlBuilder\\samples\\AngularGenerator\\fsbuilder.db;Version=3";
                    x.DbSet.DbType = DataType.Sqlite;
                    
                });
        }

        [Fact]
        public void TestTemplateImport()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var pages = sp.GetService<ITemplateService>().GetTemplatePageAsync(new PageRequest()).Result;
                Assert.True(pages.Datas.Any());

            }
        }
        [Fact]
        public void TestProjectInit()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var res = sp.GetService<IProjectService>().AddProjectInfoAsync(DataFaker.GetProjectInfo(), true).Result;
                Assert.True(res.Id > 0);
            }
        }
        [Fact]
        public void TestProjectPickerConfig()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var datas = sp.GetService<IProjectService>().GetPage(new PageRequest()).Result.Datas;
                var choice = new Faker().PickRandom(datas);
                var configs = sp.GetService<IGeneratorConfigService>().GetConfigPage(new PageRequest()).Result.Datas;
                var cconfigs = new Faker().PickRandom(configs);
                choice.GeneratorModeConfigId = cconfigs.Id;
                var res = sp.GetService<IProjectService>().Update(choice, true).Result;
                Assert.True(res > 0);
            }
        }

      


        [Fact]
        public void TestDataSourceInit()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var dataSource = sp.GetService<IGeneratorConfigService>().AddDataSource(DataFaker.GetDataSource(), true).Result;

                Assert.True(dataSource.Id > 0);
            }
        }
        [Fact]
        public void TestDataSourceCheck()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider; 
               var page=  scope.ServiceProvider.GetService<IGeneratorConfigService>()
                    .GetDataSource(new PageRequest()).Result;
               var res= page.Datas.First().CheckDataSource();
                Assert.True(res);
                var error= new DataSource()
                {
                    ConnectionString = "testset",
                    DbType = DataType.Sqlite
                }.CheckDataSource();
                Assert.False(error);
            }
        }


        [Fact]
        public void TestDataSourceGetAllTable()
        {
            using (var scope= ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var page = scope.ServiceProvider.GetService<IGeneratorConfigService>()
                    .GetDataSource(new PageRequest())
                    .Result;
                var res = page.Datas.First().GetAllTable();
                Assert.True(res.Count > 0);
            }
        }

        [Fact]
        public void TestEntitySourceInit()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var entitySource = sp.GetService<IGeneratorConfigService>().AddEntitySource(DataFaker.GetEntitySource(), true);
                Assert.True(entitySource.Id > 0);
            }
        }
        [Fact]
        public void TestConfigDataSourcePick()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var service = sp.GetService<IGeneratorConfigService>();
                var dsids = service.GetDataSource(new PageRequest()).Result.Datas.Select(x => x.Id);
                var config = DataFaker.GetDbFirstConfig()
                    .SetDataSourceRandom(dsids);
                var r = service.AddGConfig(config, true).Result;
                Assert.True(r.Id > 0);
            }
        }

        [Fact]
        public void TestConfigEntitySourcePick()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var service = sp.GetService<IGeneratorConfigService>();
                var dsids = service.GetEntitySource(new PageRequest()).Result.Datas.Select(x => x.Id);
                var config = DataFaker.GetCodeFirstConfig()
                    .SetEntitySourceRandom(dsids);
                var r = service.AddGConfig(config, true).Result;
                Assert.True(r.Id > 0);
            }
        }



        [Fact]
        public void TestDataSourcePick()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var service = sp.GetService<IGeneratorConfigService>();
                var dsids = service.GetEntitySource(new PageRequest()).Result.Datas.Select(x => x.Id);
                var datas = service.GetConfigPage(new PageRequest()).Result.Datas;
                var choice = new Faker().PickRandom(datas);
                choice.GeneratorMode = GeneratorMode.DbFirst;
                choice.DataSourceId = new Faker().PickRandom(dsids);
                var r = service.UpdateConfig(choice, true).Result;
                Assert.True(r.Id > 0);
            }
        }

        [Fact]
        public void TestBuilderOptionsInit()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var random = sp.GetService<ITemplateService>()
                    .GetTemplatePageAsync(new PageRequest())
                    .Result
                    .Datas
                    .Select(x => x.Id);
                var res = sp.GetService<IBuilderService>().AddBuilder(DataFaker.GetBuilderOption(random), true).Result;
                Assert.True(res.Id > 0);
            }
        }
        [Fact]
        public void TestBuilderOptionsPickerProject()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var random = sp.GetService<ITemplateService>().GetTemplatePageAsync(new PageRequest())
                     .Result.Datas.Select(x => x.Id);
                var builders = sp.GetService<IBuilderService>().GetBuilderPage(new PageRequest()).Result
                    .Datas;
                var choice = new Faker().PickRandom(builders);
                var projects = sp.GetService<IProjectService>().GetPage(new PageRequest()).Result.Datas;
                var projectid = new Faker().PickRandom(projects).Id;
                sp.GetService<IBuilderService>().PickerProject(choice.Id, projectid, true);
                var res = sp.GetService<IProjectService>().GetPage(new PageRequest()).Result.Datas.FirstOrDefault(x => x.Id == projectid);
                Assert.True(res.Builders.Count > 0);
            }

        }
    }
}