using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FreeSql;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Modals.Base;
using FreeSqlBuilder.Modals.Dtos;
using Microsoft.Extensions.DependencyInjection;

namespace FreeSqlBuilder.Services
{
    /// <summary>
    /// 默认数据初始化帮助类
    /// </summary>
    public class DefaultDataInit
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IFreeSql<FsBuilder> _freeSql;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="service"></param>
        public DefaultDataInit(IServiceProvider service)
        {
            _serviceProvider = service;
            _freeSql = service.GetService<IFreeSql<FsBuilder>>();
        }



        /// <summary>
        /// 检测默认数据是否创建
        /// </summary>
        /// <returns></returns>
        public bool CheckDefaultData()
        {
            var hasDs = _freeSql.Select<DataSource>().Where(x => x.Name == "DefaultDataSource").Any();
            var hasEs = _freeSql.Select<EntitySource>().Where(x => x.Name == "DefaultEntitySource").Any();
            var hasConfig = _freeSql.Select<GeneratorModeConfig>().Where(x => x.Name == "DefaultConfig").Any();
            return (hasDs || hasEs) && hasConfig;
        }

        /// <summary>
        /// 默认项目初始化
        /// </summary>
        public void DefaultProjectInit(EntitySource es)
        {
            var projectService = _serviceProvider.GetService<IProjectService>();
            var project = new Project();
            var info = new ProjectInfo
            {
                Author = "UnKnow",
                NameSpace = "Default",
                RootPath = Directory.GetCurrentDirectory()
            };
            project.ProjectInfo = info;
            project.GeneratorModeConfigId = DefaultGeneratorModeConfig(es).Id;
            var p = projectService.Add(project).Result;
            InitBuilder(p);
            projectService.UnitOfWork.Commit();
        }
        /// <summary>
        /// 默认项目初始化
        /// </summary>
        /// <param name="ds"></param>
        public void DefaultProjectInit(DataSource ds)
        {
            var projectService = _serviceProvider.GetService<IProjectService>();
            var project = new Project();
            var info = new ProjectInfo
            {
                Author = "UnKnow",
                NameSpace = "Default",
                RootPath = Directory.GetCurrentDirectory()
            };
            project.ProjectInfo = info;
            project.GeneratorModeConfigId = DefaultGeneratorModeConfig(ds).Id;
            var defaultProject = projectService.Add(project).Result;
            InitBuilder(defaultProject);
            projectService.UnitOfWork.Commit();
        }
        /// <summary>
        /// 默认生成器实体源配置新增
        /// </summary>
        public GeneratorModeConfig DefaultGeneratorModeConfig(EntitySource es)
        {
            var config = _serviceProvider.GetService<IGeneratorConfigService>();
            var defaultConfig = new GeneratorModeConfig()
            {
                Name = "DefaultConfig",
                PickType = PickType.Ignore,
                GeneratorMode = GeneratorMode.CodeFirst,
                EntitySourceId = es.Id
            };
            return config.AddGConfig(defaultConfig).Result;
        }
        /// <summary>
        /// 默认生成器数据源新增
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public GeneratorModeConfig DefaultGeneratorModeConfig(DataSource ds)
        {
            var config = _serviceProvider.GetService<IGeneratorConfigService>();
            var defaultConfig = new GeneratorModeConfig()
            {
                Name = "DefaultConfig",
                PickType = PickType.Ignore,
                GeneratorMode = GeneratorMode.DbFirst,
                EntitySourceId = ds.Id
            };
            return config.AddGConfig(defaultConfig).Result;
        }
        /// <summary>
        /// 默认数据源新增
        /// </summary>
        /// <param name="ds"></param>
        public async Task<DataSource> DefaultDataSource(DataSource ds)
        {
            var dsService = _serviceProvider.GetService<IGeneratorConfigService>();
            ds.Name = "DefaultDataSource";
            return await dsService.AddDataSource(ds);
        }
        /// <summary>
        /// 默认实体源新增
        /// </summary>
        /// <param name="es"></param>
        public Task<EntitySource> DefaultEntitySource(EntitySource es)
        {
            var configService = _serviceProvider.GetService<IGeneratorConfigService>();
            es.Name = "DefaultEntitySource";
            return configService.AddEntitySource(es);
        }
        /// <summary>
        /// 初始化构建器
        /// </summary>
        public List<BuilderOptions> InitBuilder(Project project)
        {
            var page = new BuilderPageParam { PageNumber = 1, PageSize = 1000, BuilderType = BuilderType.Builder };
            //获取所有构建器
            var builderParam = page;
            var allBuilders = _serviceProvider.GetService<IBuilderService>().GetBuilderPage(builderParam).Result.Datas.ToList();
            //获取所有模板
            var templates = _serviceProvider.GetService<ITemplateService>().GetTemplatePageAsync(page).Result.Datas
                .ToList();
            var templateIds = templates.Select(x => x.Id);
            var builderTemplateId = allBuilders.Select(x => x.TemplateId);
            //通过差集计算出没有构建器的模板
            var needInsert = templateIds.Except(builderTemplateId);
            //筛选出模板
            var ts = templates.Where(x => needInsert.Contains(x.Id)).ToList();
            var defaultProjectId = project.Id;
            //添加模板对应的构建器 名称和路径均为模板名
            var builders = ts.Select(x =>
            {
                var output = Path.GetFileNameWithoutExtension(x.TemplateName);
                var builder = new BuilderOptions(x.TemplateName, output, "", output)
                {
                    TemplateId = x.Id,
                    DefaultProjectId = defaultProjectId
                };
                return builder;
            }).ToList();
            var options = builders.Select(b => _serviceProvider.GetService<IBuilderService>().AddBuilder(b).Result).ToList();

            return options;
        }
    }
}