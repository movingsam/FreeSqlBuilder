using FreeSql.Internal.Model;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Core.Helper;
using FreeSqlBuilder.TemplateEngine.Implement;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    public class BuildTask : IBuilderTask
    {
        /// <summary>
        /// 引擎对象
        /// </summary>
        private readonly RazorTemplateEngine _engine;
        private int CurrentIndex { get; set; } = 0;
        /// <summary>
        /// 所有实体表 忽略后的
        /// </summary>
        public TableInfo[] AllTable { get; set; }
        /// <summary>
        /// 全局获取所有实体表 不管有没有忽略生成
        /// </summary>
        public TableInfo[] GAllTable { get; set; }
        /// <summary>
        /// 所有数据库中的表
        /// </summary>
        public DbTableInfo[] AllDbTable { get; set; }
        /// <summary>
        /// 当前数据库表
        /// </summary>
        public DbTableInfo CurrentDbTable => AllDbTable[CurrentIndex];
        /// <summary>
        /// 当前实体表
        /// </summary>
        public TableInfo CurrentTable => AllTable[CurrentIndex];
        /// <summary>
        /// 当前构建器选项
        /// </summary>
        public BuilderOptions CurrentBuilder { get; set; }
        /// <summary>
        /// 所有项目信息
        /// </summary>
        public Project Project { get; set; }
        /// <summary>
        /// 反射帮助类
        /// </summary>
        private readonly ReflectionHelper _reflectionHelper;
        /// <summary>
        /// 日志帮助
        /// </summary>
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
        /// <summary>
        /// 配置导入
        /// </summary>
        /// <param name="project"></param>
        public void ImportSetting(Project project)
        {
            this.Project = project;
            switch (this.Project.GeneratorModeConfig.GeneratorMode)
            {
                case GeneratorMode.DbFirst:
                    var dataSource = this.Project.GeneratorModeConfig.DataSource;
                    this.AllDbTable = dataSource.GetAllTable().ToArray();
                    break;
                case GeneratorMode.CodeFirst:
                    var tempRes = _reflectionHelper
                        .GetTableInfos(this.Project.GeneratorModeConfig.EntitySource).Result;
                    this.GAllTable = tempRes.ToArray();
                    this.AllTable = this.Project.GeneratorModeConfig.PickType == PickType.Ignore ? tempRes.Where(t => !this.Project.GeneratorModeConfig.IgnoreTable.Contains(t.CsName)).ToArray() : tempRes.Where(x => this.Project.GeneratorModeConfig.IncludeTable.Contains(x.CsName)).ToArray();
                    break;
                default:
                    break;
            }
        }

        public void ImportSetting(BuilderOptions builderOption)
        {
            this.ImportSetting(builderOption.DefaultProject);
        }

        public async Task Start()
        {
            
            do
            {
                var tableName = this.Project.GeneratorModeConfig.GeneratorMode == GeneratorMode.CodeFirst ? CurrentTable.CsName : CurrentDbTable.Name;
                foreach (var builder in Project.Builders)//构造器
                {
                    CurrentBuilder = builder;//记录当前执行的构建器
                    var content = await _engine.Render(this, builder.Template.TemplatePath);
                    await this.OutPut(tableName, content);
                    _logger.LogInformation($"生成文件{builder.GetName(tableName)}");
                    _logger.LogInformation($"内容:{content}");
                }
            }
            while (Next());
            foreach (var value in Project.GlobalBuilders)//全表构造器
            {
                CurrentBuilder = value;//记录当前执行的构建器
                var content = await _engine.Render(this, value.Template.TemplatePath);
                await this.OutPut(this.Project.ProjectInfo.NameSpace, content);
                _logger.LogInformation($"生成文件{value.GetName(this.Project.ProjectInfo.NameSpace)}");
                _logger.LogInformation($"内容:{content}");
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
            if (CurrentIndex == AllDbTable.Length - 1) return false;
            CurrentIndex++;
            return true;
        }
    }
}
