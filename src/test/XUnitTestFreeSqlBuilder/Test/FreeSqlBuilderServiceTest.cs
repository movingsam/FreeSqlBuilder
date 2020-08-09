using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bogus;
using FreeSql;
using FreeSql.DataAnnotations;
using FreeSqlBuilder;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Core.Helper;
using FreeSqlBuilder.Modals.Base;
using FreeSqlBuilder.Modals.Dtos;
using FreeSqlBuilder.Services;
using FreeSqlBuilder.TemplateEngine;
using FreeSqlBuilder.TemplateEngine.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.ObjectPool;
using Xunit;
//using FreeSql;
//using FreeSql.DataAnnotations;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;
using DbContextOptionsBuilder = Microsoft.EntityFrameworkCore.DbContextOptionsBuilder;

namespace XUnitTestFreeSqlBuilder.Test
{
    public class FreeSqlBuilderServiceTest : TestBase
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
            Service.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            Service.AddFreeSqlBuilder(x =>
                {
                    x.DbSet.ConnectionString =
                        @"Data Source=G:\\github\\movingsam\\FreeSqlBuilder\\samples\\AngularGenerator\\fsbuilder.db;Version=3";
                });
            Service.AddEntityFrameworkSqlite();
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
                var page = scope.ServiceProvider.GetService<IGeneratorConfigService>()
                     .GetDataSource(new PageRequest()).Result;
                var res = page.Datas.First().CheckDataSource();
                Assert.True(res);
                var error = new DataSource()
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
            using (var scope = ServiceProvider.CreateScope())
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
                var builders = sp.GetService<IBuilderService>().GetBuilderPage(new BuilderPageParam()).Result
                    .Datas;
                var choice = new Faker().PickRandom(builders);
                var projects = sp.GetService<IProjectService>().GetPage(new PageRequest()).Result.Datas;
                var projectid = new Faker().PickRandom(projects).Id;
                sp.GetService<IBuilderService>().PickerProject(choice.Id, projectid, true);
                var res = sp.GetService<IProjectService>().GetPage(new PageRequest()).Result.Datas.FirstOrDefault(x => x.Id == projectid);
                Assert.True(res.Builders.Count > 0);
            }

        }

        [Fact]
        public void TestInitData()
        {
            var service = new DefaultDataInit(ServiceProvider);
            ServiceProvider.GetService<IFreeSql<FsBuilder>>().Delete<Project>().Where(x => x.Id > 0).ExecuteAffrows();
            //service.InitBuilder()();
            ;
        }
        [Fact]
        public void TestCodeFirstTableRef()
        {
            var fsql = ServiceProvider.GetService<IFreeSql<FsBuilder>>();
            fsql.CodeFirst.SyncStructure(typeof(TestEntity), typeof(SubTestEntity), typeof(NavigateTest));
            var ti = fsql.CodeFirst.GetTableByEntity(typeof(TestEntity));
            var res = ti.GetNavigates();
            //var include = res.Where(x => x.RefType == TableRefType.OneToOne).ToList();
            var includes = ti.GetIncludeStr();
            var includeManys = ti.GetIncludeManyOTM();
            var includeManystr = ti.GetIncludeManyStr();
            var selectStr = $"fsql.Select<TestEntity>(){includes}{includeManystr}.ToList()";
            //fsql.Select<TestEntity>()
            //    //.Include(x => x.Navigate)
            //    .IncludeMany(tes => tes.OneToManyTests, then => then.Include(tes => tes.TestEntity))
            //    .ToList()
            ;
            //fsql.Select<TestEntity>().Include(x => x.Navigate)
            //    .IncludeMany(tes => tes.MiddleTypes, then => then.Include(tes => tes.TestEntity).Include(tes => tes.SubTestEntity))
            //    .IncludeMany(tes => tes.OneToManyTests, then => then.Include(tes => tes.TestEntity))
            //    .IncludeMany(x => x.SubTestEntities)
            //    .ToList()
        }
        [Fact]
        public void TestCodeFirstInsert()
        {
            var _freeSql = ServiceProvider.GetService<IFreeSql<FsBuilder>>();
            //_freeSql.GetRepository<TestEntity>().Select
            var fsql = ServiceProvider.GetService<IFreeSql<FsBuilder>>();
            fsql.CodeFirst.SyncStructure(typeof(TestEntity), typeof(SubTestEntity), typeof(NavigateTest));
            var ti = fsql.CodeFirst.GetTableByEntity(typeof(TestEntity));
            var listdata = new List<TestEntity>();
            fsql.Insert<TestEntity>(listdata).ExecuteInserted();
            var t = ti.GetInsertStr("_freeSql", "data");
        }

        public class TestEntityDbContext : DbContext
        {
            public static readonly LoggerFactory LoggerFactory =
                new LoggerFactory(new[] { new DebugLoggerProvider() });
            public Microsoft.EntityFrameworkCore.DbSet<TestEntity> TestEntitys { get; set; }
            public Microsoft.EntityFrameworkCore.DbSet<SubTestEntity> SubTestEntity { get; set; }
            public Microsoft.EntityFrameworkCore.DbSet<NavigateTest> NavigateTest { get; set; }
            public Microsoft.EntityFrameworkCore.DbSet<OneToManyTest> OneToManyTest { get; set; }

            public TestEntityDbContext()
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<TestEntity>()
                    .HasMany<OneToManyTest>(x => x.OneToManyTests);

                base.OnModelCreating(modelBuilder);
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite(@"Data Source=G:\\github\\movingsam\\FreeSqlBuilder\\samples\\AngularGenerator\\fsbuilder.db");
                optionsBuilder.UseLoggerFactory(LoggerFactory);
            }
        }

        [Fact]
        public void TestEfCoreSubSelect()
        {
            //var freeSql = ServiceProvider.GetService<IFreeSql<FsBuilder>>();
            //freeSql.CodeFirst.SyncStructure(typeof(OneToManyTest));
            //var oneToMany = freeSql.Select<OneToManyTest>()
            //     .ToList();
            //oneToMany.ForEach(s=>s.Name = "test1");
            //freeSql.Update<OneToManyTest>()
            //    .SetSource(oneToMany)
            //    .ExecuteAffrows();
            //ef core sqlite 使用
            using (TestEntityDbContext db = new TestEntityDbContext())
            {
                var tests = db.TestEntitys
                    .Include(x => x.OneToManyTests)
                    .Where(x => x.Test == "test1")
                    .Select(x => new
                    {
                        TestOneToManyStr = string.Join(",", x.OneToManyTests.Where(o => o.TestOneToMany > 0).Select(s => s.TestOneToMany)),
                        EqualOne = string.Join(",", x.OneToManyTests.Where(o => o.TestOneToMany == 0).Select(s => s.TestOneToMany)),
                        //ParentParam = string.Join(",", x.OneToManyTests.Where(o => o.TestEntityId == x.Id).Select(s => s.TestOneToMany)),
                        MainParam = string.Join(",",x.OneToManyTests.Where(o=>o.Name ==x.Test ).Select(s=>s.TestOneToMany))
                    })
                    .Skip(0)
                    .Take(10)
                    .ToList();
            }

        }
        [System.ComponentModel.DataAnnotations.Schema.Table("TestEntity")]
        public class TestEntity
        {
            [FreeSql.DataAnnotations.Column(IsPrimary = true, IsIdentity = true)]
            public int Id { get; set; }
            public string Test { get; set; }
            public int NavigateId { get; set; }
            public NavigateTest Navigate { get; set; }
            [Navigate(ManyToMany = typeof(MiddleType))]
            public ICollection<SubTestEntity> SubTestEntities { get; set; }
            [Navigate(nameof(MiddleType.TestEntityId))]
            public virtual List<MiddleType> MiddleTypes { get; set; }
            public List<OneToManyTest> OneToManyTests { get; set; }
        }

        public class MiddleType
        {
            public int SubTestEntityId { get; set; }
            public int TestEntityId { get; set; }
            public TestEntity TestEntity { get; set; }
            public SubTestEntity SubTestEntity { get; set; }
        }
        [System.ComponentModel.DataAnnotations.Schema.Table("SubTestEntity")]
        public class SubTestEntity
        {
            [FreeSql.DataAnnotations.Column(IsPrimary = true, IsIdentity = true)]
            public int Id { get; set; }
            public string SubTestEntityString { get; set; }
            [FreeSql.DataAnnotations.Navigate(ManyToMany = typeof(MiddleType))]
            public ICollection<TestEntity> MiddleTypes { get; set; }
        }
        [System.ComponentModel.DataAnnotations.Schema.Table("NavigateTest")]
        public class NavigateTest
        {
            [FreeSql.DataAnnotations.Column(IsPrimary = true, IsIdentity = true)]
            public int Id { get; set; }
            public int TestEntityId { get; set; }
            public TestEntity TestEntity { get; set; }
        }
        [System.ComponentModel.DataAnnotations.Schema.Table("OneToManyTest")]
        public class OneToManyTest
        {
            public int Id { get; set; }
            public int TestOneToMany { get; set; }
            public int TestEntityId { get; set; }
            public string Name { get; set; }
            public TestEntity TestEntity { get; set; }
        }
        //[Fact]
        //public void TestTempBuilderOptionsRunTask()
        //{
        //    using (var scope = ServiceProvider.CreateScope())
        //    {
        //        var sp = scope.ServiceProvider;
        //        var dl = sp.GetService<DiagnosticListener>();
        //        var options = sp.GetService<IBuilderService>().GetBuilderPage(new PageRequest()).Result.Datas
        //            .FirstOrDefault();
        //        var config = sp.GetService<IGeneratorConfigService>().GetConfigPage(new PageRequest()).Result.Datas.Where(x => x.PickType == PickType.Ignore).FirstOrDefault();
        //        options.DefaultConfig = config;
        //        var task = sp.GetService<TempBuildTask>();
        //        task.ImportSetting(options);
        //        task.Start().Wait();
        //        ;
        //    }

        //}
    }
}