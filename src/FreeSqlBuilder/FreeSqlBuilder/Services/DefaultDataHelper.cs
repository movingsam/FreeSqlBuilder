using System;
using System.IO;
using System.Linq;
using FreeSql;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Core.Helper;
using FreeSqlBuilder.Core.Utilities;
using FreeSqlBuilder.Modals.Base;
using Microsoft.Extensions.DependencyInjection;

namespace FreeSqlBuilder.Services
{
    /// <summary>
    /// 默认数据帮助
    /// </summary>
    public class DefaultDataHelper
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IFreeSql<FsBuilder> _freeSql;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public DefaultDataHelper(IServiceProvider service)
        {
            _serviceProvider = service;
            _freeSql = _serviceProvider.GetService<IFreeSql<FsBuilder>>();
        }
        /// <summary>
        /// 检测默认数据
        /// </summary>
        public bool CheckDefaultData()
        {
            var hasDs = _freeSql.Select<DataSource>().Where(x => x.Name == "DefaultDataSource").Any();
            var hasEs = _freeSql.Select<EntitySource>().Where(x => x.Name == "DefaultEntitySource").Any();
            var hasConfig = _freeSql.Select<GeneratorModeConfig>().Where(x => x.Name == "DefaultConfig").Any();
            return (hasDs || hasEs) && hasConfig;
        }
        /// <summary>
        /// 项目初始化
        /// </summary>
        public void ProjectInit()
        {
            var projectService = _serviceProvider.GetService<IProjectService>();
            var project = new Project();
            var info = new ProjectInfo
            {
                Author = "UnKnown",
                NameSpace = "DefaultProject",
                RootPath = Directory.GetCurrentDirectory()
            };
            project.ProjectInfo = info;
            projectService.Add(project, true);
        }
        /// <summary>
        /// 默认配置
        /// </summary>
        public void DefaultGeneratorModeConfig()
        {
            var config = _serviceProvider.GetService<IGeneratorConfigService>();
            var defaultConfig = new GeneratorModeConfig();
            defaultConfig.GeneratorMode = GeneratorMode.DbFirst;
            defaultConfig.Name = "DefaultConfig";
            defaultConfig.PickType = PickType.Ignore;
            config.AddGConfig(defaultConfig);
        }
        /// <summary>
        /// 默认数据源
        /// </summary>
        /// <param name="ds"></param>
        public void DefaultDataSource(DataSource ds)
        {
            var dsService = _serviceProvider.GetService<IGeneratorConfigService>();
            ds.Name = "DefaultDataSource";
            dsService.AddDataSource(ds);
        }
        /// <summary>
        /// 默认实体源
        /// </summary>
        /// <param name="es"></param>
        public void DefaultEntitySource(EntitySource es)
        {
            var configService = _serviceProvider.GetService<IGeneratorConfigService>();
            es.Name = "DefaultEntitySource";
            configService.AddEntitySource(es);
        }
        /// <summary>
        /// 刷新构建器
        /// </summary>
        public void RefreshDefaultBuilder()
        {
            var page = new PageRequest { PageNumber = 1, PageSize = 1000 };
            var datas = _serviceProvider.GetService<IBuilderService>().GetBuilderPage(page).Result.Datas.ToList();
            var templates = _serviceProvider.GetService<ITemplateService>().GetTemplatePageAsync(page).Result.Datas
                .ToList();
            var templateIds = templates.Select(x => x.Id);
            var builderTemplateId = datas.Select(x => x.TemplateId);
            var needInsert = templateIds.Except(builderTemplateId);
            var ts = templates.Where(x => needInsert.Contains(x.Id)).ToList();
            var builders = ts.Select(x => new BuilderOptions(x.TemplateName, x.TemplatePath, "", "")).ToList();
            builders.ForEach(b =>
            {
                _serviceProvider.GetService<IBuilderService>().AddBuilder(b).Wait();
            });
        }
        /// <summary>
        /// 刷新模板
        /// </summary>
        public void RefreshTemplate()
        {
            _serviceProvider.GetService<FileProviderHelper>().RefreshTemplate();
            _serviceProvider.GetService<IUnitOfWork>().Commit();
        }
    }
}