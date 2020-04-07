using FreeSql.Internal.Model;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Core.Helper;
using FreeSqlBuilder.Core.Utilities;
using FreeSqlBuilder.TemplateEngine.Implement;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FreeSqlBuilder.TemplateEngine
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
        private readonly ReflectionHelper _reflectionHelper;
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
                    this.AllTable = _reflectionHelper
                        .GetTableInfos(this.Project.GeneratorModeConfig.EntityAssemblyName, this.Project.GeneratorModeConfig.EntityBaseName).Result
                        .Where(t => !this.Project.GeneratorModeConfig.IgnoreTable.Contains(t.CsName)).ToArray();
                    break;
                default:
                    break;
            }
        }

        public async Task Start()
        {
            do
            {
                var tableName = CurrentTable.CsName;
                foreach (var builder in Project.Builders)//构造器
                {
                    CurrentBuilder = builder;//记录当前执行的构建器
                    var content = await _engine.Render(this, builder.Template.TemplatePath);
                    await builder.OutPut(tableName, content);
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
