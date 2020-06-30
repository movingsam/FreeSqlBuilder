﻿using FreeSql.Internal.Model;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Core.Helper;
using FreeSqlBuilder.TemplateEngine.Implement;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FreeSql.DatabaseModel;
using FreeSqlBuilder.Core.DbFirst;
using FreeSqlBuilder.Core.Entities;
using Microsoft.Extensions.Logging;
using FreeSqlBuilder.TemplateEngine.Utilities;

namespace FreeSqlBuilder.TemplateEngine
{
    public class BuildTask
    {
        public IFreeSql FreeSql { get; set; }
        private RazorTemplateEngine _engine;
        private int CurrentIndex { get; set; } = 0;
        public TableInfo[] AllTable { get; set; }
        public DbTableInfo[] AllDbTable { get; set; }
        public TableInfo[] GAllTable { get; set; }
        public DbTableInfo CurrentDbTable => AllDbTable[CurrentIndex];
        public TableInfo CurrentTable => AllTable[CurrentIndex];
        public BuilderOptions CurrentBuilder { get; set; }
        public Project Project { get; set; }
        private readonly ReflectionHelper _reflectionHelper;
        private readonly ILogger<BuildTask> _logger;
        public BuildTask(IServiceProvider serviceProvider)
        {
            _reflectionHelper = serviceProvider.GetService<ReflectionHelper>();
            _engine = serviceProvider.GetRequiredService<RazorTemplateEngine>();
            _logger = serviceProvider.GetRequiredService<ILogger<BuildTask>>();
        }

        public void InitSetting(string jsonPath = null)
        {
            Project = new Project();
            if (!string.IsNullOrWhiteSpace(jsonPath))
            {
                using var configStream = new StreamReader(jsonPath);
                var jsonConfigStr = configStream.ReadToEnd();
                this.ImportSetting(JsonConvert.DeserializeObject<Project>(jsonConfigStr));
            }
        }

        public void ImportSetting(Project project)
        {
            this.Project = project;
            switch (this.Project.GeneratorModeConfig.GeneratorMode)
            {
                case GeneratorMode.DbFirst:
                    var dbHelper = new DbFirstHelper();
                    var dataSource = this.Project.GeneratorModeConfig.DataSource;
                    this.AllDbTable = dbHelper.GetAllTable(
                         new DbFirstDto(dataSource.Name, dataSource.DbType, dataSource.ConnectionString)).ToArray();
                    break;
                case GeneratorMode.CodeFirst:
                    var tempRes = _reflectionHelper
                        .GetTableInfos(this.Project.GeneratorModeConfig.EntitySource.EntityAssemblyName, this.Project.GeneratorModeConfig.EntitySource.EntityBaseName).Result;
                    this.GAllTable = tempRes.ToArray();
                    if (this.Project.GeneratorModeConfig.PickType == PickType.Ignore)
                    {
                        this.AllTable = tempRes.Where(t => !this.Project.GeneratorModeConfig.IgnoreTable.Contains(t.CsName)).ToArray();
                    }
                    else
                    {
                        this.AllTable = tempRes.Where(x => this.Project.GeneratorModeConfig.IncludeTable.Contains(x.CsName)).ToArray();
                    }
                    break;
                default:
                    break;
            }
        }

        public async Task Start()
        {
            do
            {
                if (this.Project.GeneratorModeConfig.GeneratorMode == GeneratorMode.CodeFirst)
                {
                    var tableName = CurrentTable.CsName;
                    foreach (var builder in Project.Builders)//构造器
                    {
                        CurrentBuilder = builder;//记录当前执行的构建器
                        var content = await _engine.Render(this, builder.Template.TemplatePath);
                        await builder.OutPut(tableName, content);
                        _logger.LogInformation($"生成文件{builder.GetName(tableName)}");
                        _logger.LogInformation($"内容:{content}");
                    }
                }
                else
                {
                    var tableName = CurrentDbTable.Name;
                    foreach (var builder in Project.Builders)//构造器
                    {
                        CurrentBuilder = builder;//记录当前执行的构建器
                        var content = await _engine.Render(this, builder.Template.TemplatePath);
                        await builder.OutPut(tableName, content);
                        _logger.LogInformation($"生成文件{builder.GetName(tableName)}");
                        _logger.LogInformation($"内容:{content}");
                    }

                }
            }
            while (Next());
            foreach (var value in Project.GlobalBuilders)//全表构造器
            {
                CurrentBuilder = value;//记录当前执行的构建器
                var content = await _engine.Render(this, value.Template.TemplatePath);
                await value.OutPut(this.Project.ProjectInfo.ProjectName, content);
            }
        }


        public bool Next()
        {
            if (Project.GeneratorModeConfig.GeneratorMode == GeneratorMode.CodeFirst)
            {
                if (CurrentIndex == AllTable.Length - 1) return false;
                CurrentIndex++;
                return true;
            }
            else
            {
                if (CurrentIndex == AllDbTable.Length - 1) return false;
                CurrentIndex++;
                return true;
            }
        }
    }
}
