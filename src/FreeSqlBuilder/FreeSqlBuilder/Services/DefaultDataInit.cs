using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FreeSql;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Modals.Base;
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
        /// 默认配置检测
        /// </summary>
        public void DefaultConfigCheck()
        {
            var anyProject = _freeSql.Select<Project>().Any();
            if (!anyProject)
            {
                DefaultProjectInit();
                DefaultGeneratorModeConfig();
                DefaultDataSource(new DataSource());
                DefaultEntitySource(new EntitySource());
                this.InitBuilder();
                _serviceProvider.GetService<IUnitOfWork>().Commit();
            }
            ////_freeSql.Select<GeneratorModeConfig>().Any();
            //return _freeSql.Select<Project>().Any();
        }


        /// <summary>
        /// 默认项目初始化
        /// </summary>
        public void DefaultProjectInit()
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
            project.GeneratorModeConfig = DefaultGeneratorModeConfig(); 

            projectServuce.Add(project);
        }
        /// <summary>
        /// 默认生成器配置新增
        /// </summary>
        public GeneratorModeConfig DefaultGeneratorModeConfig()
        {
            var config = _serviceProvider.GetService<IGeneratorConfigService>();
            var defaultConfig = new GeneratorModeConfig();
            defaultConfig.GeneratorMode = GeneratorMode.DbFirst;
            defaultConfig.Name = "DefaultConfig";
            defaultConfig.PickType = PickType.Ignore;
            return config.AddGConfig(defaultConfig).Result;
        }
        /// <summary>
        /// 默认数据源新增
        /// </summary>
        /// <param name="ds"></param>
        public void DefaultDataSource(DataSource ds)
        {
            var dsService = _serviceProvider.GetService<IGeneratorConfigService>();
            ds.Name = "DefaultDataSource";
            dsService.AddDataSource(ds);
        }
        /// <summary>
        /// 默认实体源新增
        /// </summary>
        /// <param name="es"></param>
        public void DefaultEntitySource(EntitySource es)
        {
            var configService = _serviceProvider.GetService<IGeneratorConfigService>();
            es.Name = "DefaultEntitySource";
            configService.AddEntitySource(es);
        }
        /// <summary>
        /// 初始化构建器
        /// </summary>
        public List<BuilderOptions> InitBuilder()
        {
            var page = new PageRequest { PageNumber = 1, PageSize = 1000 };
            //获取所有构建器
            var allBuilders = _serviceProvider.GetService<IBuilderService>().GetBuilderPage(page).Result.Datas.ToList();
            //获取所有模板
            var templates = _serviceProvider.GetService<ITemplateService>().GetTemplatePageAsync(page).Result.Datas
                .ToList();
            var templateIds = templates.Select(x => x.Id);
            var builderTemplateId = allBuilders.Select(x => x.TemplateId);
            //通过差集计算出没有构建器的模板
            var needInsert = templateIds.Except(builderTemplateId);
            //筛选出模板
            var ts = templates.Where(x => needInsert.Contains(x.Id)).ToList();
            var defaultProjectId = _freeSql.Select<Project>().Include(x => x.ProjectInfo)
                .Where(x => x.ProjectInfo.NameSpace == "Default").ToOne(x => x.Id);
            //添加模板对应的构建器 名称和路径均为模板名
            var builders = ts.Select(x =>
            {
                var builder = new BuilderOptions(x.TemplateName, x.TemplatePath, "", "");
                builder.TemplateId = x.Id;
                builder.DefaultProjectId = defaultProjectId;
                return builder;
            }).ToList();
            var options = builders.Select(b => _serviceProvider.GetService<IBuilderService>().AddBuilder(b).Result).ToList();
             
            return options;
        }
    }
}