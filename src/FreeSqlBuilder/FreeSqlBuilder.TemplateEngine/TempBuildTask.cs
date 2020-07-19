﻿using FreeSql.DatabaseModel;
using FreeSql.Internal.Model;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Core.Helper;
using FreeSqlBuilder.TemplateEngine.Implement;
using FreeSqlBuilder.TemplateEngine.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FreeSqlBuilder.TemplateEngine
{
    public class TempBuildTask : IBuilderTask
    {
        /// <summary>
        /// 引擎对象
        /// </summary>
        private readonly RazorTemplateEngine _engine;
        private int CurrentIndex { get; set; } = -1;
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
        public string Author { get; set; }
        ///// <summary>
        ///// 默认项目
        ///// </summary>
        //public Project Project
        //{
        //    get
        //    {
        //        var project = new Project
        //        {
        //            ProjectInfo =new ProjectInfo
        //            {
        //                Author = "FreeSqlBuilder",
        //                RootPath = Directory.GetCurrentDirectory(),
        //                ProjectName = "Default",
        //                OutPutPath = "FreeSqlBuilder"
        //            },
        //        };

        //        project.GeneratorModeConfig = CurrentBuilder.Config;
        //        Project.ProjectBuilders = new List<ProjectBuilder>
        //        { new ProjectBuilder
        //        {
        //            BuilderId = CurrentBuilder.Id,
        //            ProjectId =  project.Id
        //        } };
        //        return project;
        //    }
        //}

        /// <summary>
        /// 反射帮助类
        /// </summary>
        private readonly ReflectionHelper _reflectionHelper;
        /// <summary>
        /// 日志帮助
        /// </summary>
        private readonly ILogger<BuildTask> _logger;
        public TempBuildTask(IServiceProvider serviceProvider)
        {
            _reflectionHelper = serviceProvider.GetService<ReflectionHelper>();
            _engine = serviceProvider.GetRequiredService<RazorTemplateEngine>();
            _logger = serviceProvider.GetRequiredService<ILogger<BuildTask>>();
        }


        /// <summary>
        /// 配置导入
        /// </summary>
        /// <param name="builderOptions"></param>
        public void ImportSetting(BuilderOptions builderOptions)
        {
            CurrentBuilder = builderOptions;
            switch (this.CurrentBuilder.Config.GeneratorMode)
            {
                case GeneratorMode.DbFirst:
                    var dataSource = this.CurrentBuilder.Config.DataSource;
                    this.AllDbTable = dataSource.GetAllTable().ToArray();
                    break;
                case GeneratorMode.CodeFirst:
                    var tempRes = _reflectionHelper
                        .GetTableInfos(CurrentBuilder.Config.EntitySource.EntityAssemblyName, this.CurrentBuilder.Config.EntitySource.EntityBaseName).Result;
                    this.GAllTable = tempRes.ToArray();
                    this.AllTable = this.CurrentBuilder.Config.PickType == PickType.Ignore ? tempRes.Where(t => !this.CurrentBuilder.Config.IgnoreTable.Contains(t.CsName)).ToArray() : tempRes.Where(x => this.CurrentBuilder.Config.IncludeTable.Contains(x.CsName)).ToArray();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 开始任务
        /// </summary>
        /// <returns></returns>
        public async Task Start()
        {

            while (Next())
            {
                var tableName = this.CurrentBuilder.Config.GeneratorMode == GeneratorMode.CodeFirst ? CurrentTable.CsName : CurrentDbTable.Name;
                var content = await _engine.Render(this, CurrentBuilder.Template.TemplatePath);
                await CurrentBuilder.OutPut(tableName, content);
                _logger.LogInformation($"生成文件{CurrentBuilder.GetName(tableName)}");
                _logger.LogInformation($"内容:{content}");
            }

        }

        /// <summary>
        /// 判断是否有下一个
        /// </summary>
        /// <returns></returns>
        public bool Next()
        {
            if (CurrentBuilder.Config.GeneratorMode == GeneratorMode.CodeFirst)
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