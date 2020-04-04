using FreeSql.Generator.Core;
using FreeSql.Generator.Core.CodeFirst;
using FreeSql.Generator.Core.Utilities;
using FreeSql.Generator.Helper;
using FreeSql.TemplateEngine.Implement;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace FreeSql.TemplateEngine
{
    public class BuildTask
    {
        public IFreeSql FreeSql { get; set; }
        private RazorTemplateEngine _engine;
        private int CurrentIndex { get; set; } = 0;
        public TableInfo[] AllTable { get; set; }
        public TableInfo CurrentTable => AllTable[CurrentIndex];
        public BuilderOptions CurrentBuilder { get; set; }
        public Project Project { get; set; }
        private ReflectionHelper _reflectionHelper;
        public BuildTask(IServiceProvider serviceProvider)
        {
            _reflectionHelper = serviceProvider.GetService<ReflectionHelper>();
            _engine = serviceProvider.GetRequiredService<RazorTemplateEngine>();
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
                    break;
                case GeneratorMode.CodeFirst:
                    this.AllTable = _reflectionHelper.GetTableInfos(this.Project.GeneratorModeConfig.EntityAssemblyName, this.Project.GeneratorModeConfig.EntityBaseName).Result.ToArray();
                    break;
                default:
                    break;
            }
        }

        public async Task Start()
        {
            do
            {
                var tableName = CurrentTable.Name;
                foreach (var builder in Project.Builders)//构造器
                {
                    CurrentBuilder = builder;//记录当前执行的构建器
                    if (!builder.IsServiceOnly || CurrentTable.IsServiceTable)//只生成非主表模板以及主表模板中的主表
                    {
                        var content = await _engine.Render(this, builder.Template.TemplatePath);
                        await builder.OutPut(tableName, content);
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
            if (CurrentIndex == AllTable.Length - 1) return false;
            CurrentIndex++;
            return true;
        }
    }
}
