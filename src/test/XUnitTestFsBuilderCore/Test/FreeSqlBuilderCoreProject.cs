using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FreeSql;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Core.Helper;
using FreeSqlBuilder.Core.Utilities;
using FreeSqlBuilder.Core.WordsConvert;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace XUnitTestFsBuilderCore.Test
{
    public class FreeSqlBuilderCoreProject : TestBase
    {
        public override void ServiceConfig()
        {
            Service.AddMemoryCache();
            Service.AddScoped<ReflectionHelper>();
        }

        /// <summary>
        /// 项目初始化
        /// </summary>
        [Fact]
        public void TestProjectInit()
        {
            using var scope = ServiceProvider.GetService<IFreeSql>();
            var projectInfo = new ProjectInfo
            {
                Author = "Sam",
                NameSpace = "TestProject",
                RootPath = Directory.GetCurrentDirectory(),
            };
            var projectInfoId = scope.Insert<ProjectInfo>().AppendData(projectInfo).ExecuteIdentity();
            var projectId = scope.Insert<Project>().AppendData(new Project
            {
                ProjectInfoId = projectInfoId,
            }).ExecuteIdentity();
            var res = scope.Select<Project>()
                .Include(x => x.ProjectInfo)
                .IncludeMany(x => x.ProjectBuilders)
                .Where(x => x.Id == projectId).ToOne();
            Assert.NotNull(res);
            Assert.NotNull(res.ProjectInfo);
            var delete = scope.Delete<ProjectInfo>().Where(x => x.Id == projectInfoId).ExecuteAffrows();
            var deleteProject = scope.Delete<Project>().Where(x => x.Id == projectId).ExecuteAffrows();
            Assert.True(delete > 0);
            Assert.True(deleteProject > 0);
        }

        /// <summary>
        /// 测试项目查询
        /// </summary>
        [Fact]
        public void TestProjectQuery()
        {
            using var scope = ServiceProvider.GetService<IFreeSql>();
            var sql = scope.Select<Project>()
                .Include(x => x.ProjectInfo)
                .IncludeMany(x => x.ProjectBuilders)
                .Include(x => x.GeneratorModeConfig)
                .Include(x => x.GeneratorModeConfig.DataSource)
                .ToSql();
        }
        /// <summary>
        /// 配置CodeFirst逻辑测试
        /// </summary>
        [Fact]
        public async Task TestProjectCodeFirstConfig()
        {
            using var scope = ServiceProvider.CreateScope();
            var fsql = ServiceProvider.GetService<IFreeSql<FsBuilder>>();
            var config = new GeneratorModeConfig();
            var reflection = scope.ServiceProvider.GetService<ReflectionHelper>();
            var items = await reflection.GetAssembliesNameItems();
            var assemblyItem = items.FirstOrDefault(x => x.Value.Equals(Assembly.GetAssembly(typeof(IKey<>)).FullName));
            Assert.True(assemblyItem != null);
            var typeItem = await reflection.GetAbstractClass(assemblyItem.Value);
            var abstruct = typeItem.FirstOrDefault(x => x.Value == typeof(IKey<>).FullName);
            Assert.True(abstruct != null);
            var tables =  scope.ServiceProvider
                .GetService<ReflectionHelper>();
            var projectid = fsql.Insert<Project>().AppendData(new Project()).ExecuteIdentity();
            config.EntitySource.EntityAssemblyName = null;
            config.EntitySource.EntityBaseName = abstruct.Value;
            config.PickType = PickType.Ignore;
            config.GeneratorMode = GeneratorMode.CodeFirst;

            config.Validate();
            var id = fsql.Insert<GeneratorModeConfig>().AppendData(config).ExecuteIdentity();
            fsql.Delete<Project>().Where(x => x.Id == projectid).ExecuteAffrows();
            Assert.True(id > 0);
            fsql.Delete<GeneratorModeConfig>().Where(x => x.Id == id);
        }
        /// <summary>
        /// 测试dbfirst配置
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestProjectDbFirstConfig()
        {
            using var scope = ServiceProvider.CreateScope();
            var fsql = ServiceProvider.GetService<IFreeSql<FsBuilder>>();
            var config = new GeneratorModeConfig();
            config.EntitySource.EntityAssemblyName = null;
            var reflection = scope.ServiceProvider.GetService<ReflectionHelper>();
            var items = await reflection.GetAssembliesNameItems();
            var assemblyItem = items.FirstOrDefault(x => x.Value.Equals(Assembly.GetAssembly(typeof(IKey<>)).FullName));
            var typeItem = await reflection.GetAbstractClass(assemblyItem.Value);
            var abstruct = typeItem.FirstOrDefault(x => x.Value == typeof(IKey<>).FullName);
            var projectid = fsql.Insert<Project>().AppendData(new Project()).ExecuteIdentity();
            var datasourceId = fsql.Insert<DataSource>().AppendData(new DataSource
            {
                ConnectionString = "",
                DbType = DataType.Sqlite,
                Name = ""
            }).ExecuteIdentity();
            config.GeneratorMode = GeneratorMode.DbFirst;
            config.DataSourceId = datasourceId;
            config.Validate();
            var id = fsql.Insert<GeneratorModeConfig>().AppendData(config).ExecuteIdentity();
            fsql.Delete<Project>().Where(x => x.Id == projectid).ExecuteAffrows();
            Assert.True(id > 0);
            fsql.Delete<GeneratorModeConfig>().Where(x => x.Id == id);
            var aff = fsql.Delete<DataSource>().Where(x => x.Id == datasourceId).ExecuteAffrows();
            Assert.True(aff > 0);
        }


        /// <summary>
        /// 测试项目构建器添加
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestProjectBuilderOptions()
        {
            using var scope = ServiceProvider.CreateScope();
            var fsql = ServiceProvider.GetService<IFreeSql<FsBuilder>>();
            var template = fsql.Select<Template>().ToOne();
            var b = new BuilderOptions
            {
                Mode = ConvertMode.None,
                Name = "测试构建器",
                OutPutPath = "Test",
                Prefix = "",
                Suffix = "",
                TemplateId = template.Id,
                Type = BuilderType.Builder
            };
            b.Validate();
            var id = fsql.Insert<BuilderOptions>().AppendData(b).ExecuteIdentity();
            Assert.True(id > 0);
        }

        /// <summary>
        /// 测试项目选择构建器
        /// </summary>
        [Fact]
        public void TestProjectPickBuilder()
        {
            using var scope = ServiceProvider.CreateScope();
            var fsql = scope.ServiceProvider.GetService<IFreeSql<FsBuilder>>(); ;
            var projectOne = fsql.Select<Project>()
                .Include(x => x.ProjectInfo)
                .Include(x => x.GeneratorModeConfig)
                .Include(x => x.GeneratorModeConfig.DataSource)
                .Include(x=>x.GeneratorModeConfig.EntitySource)
                .IncludeMany(x => x.ProjectBuilders,
                    then => then
                        .Include(t => t.Builder)
                        .Include(it => it.Builder.Template))
                .ToOne();
        }


    }
}
