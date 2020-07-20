using System;
using System.IO;
using System.Linq;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Modals.Base;
using Microsoft.Extensions.DependencyInjection;

namespace FreeSqlBuilder.Services
{
    public class DefaultDataInit
    {
        private readonly IServiceProvider _serviceProvider;
        public DefaultDataInit(IServiceProvider service)
        {
            _serviceProvider = service;
        }

        public void FirstInit()
        {

        }
        /// <summary>
        /// 项目初始化
        /// </summary>
        public void ProjectInit()
        {
            var projectServuce = _serviceProvider.GetService<IProjectService>();
            var project = new Project();
            var info = new ProjectInfo
            {
                Author = "Unknow",
                NameSpace = "Default",
                RootPath = Directory.GetCurrentDirectory()
            };
            project.ProjectInfo = info;
            projectServuce.Add(project, true);
        }

        public void GeneratorModeConfig()
        {
            var config = _serviceProvider.GetService<IGeneratorConfigService>();
            var defaultConfig = new GeneratorModeConfig();
            defaultConfig.GeneratorMode = GeneratorMode.DbFirst;
            defaultConfig.Name = "DefaultConfig";
            defaultConfig.PickType = PickType.Ignore;
            config.AddGConfig(defaultConfig);
        }

        public void DefaultDataSource(DataSource ds)
        {
            var dsService = _serviceProvider.GetService<IGeneratorConfigService>();
            ds.Name = "DefaultDataSource";
            dsService.AddDataSource(ds);
        }

        public void DefaultEntitySource(EntitySource es)
        {
            var configService = _serviceProvider.GetService<IGeneratorConfigService>();
            es.Name = "DefaultEntitySource";
            configService.AddEntitySource(es);
        }

        public void InitBuilder()
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
    }
}