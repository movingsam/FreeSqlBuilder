using FreeSql.DatabaseModel;
using FreeSql.Generator.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FreeSql.DataAnnotations;
using FreeSql.Generator.Core.CodeFirst;
using FreeSql.Generator.Core.Utilities;
using FreeSql.TemplateEngine.Implement;
using GRES.Framework.Utils;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FreeSql.TemplateEngine
{
    public class BuildTask
    {
        public IFreeSql FreeSql { get; set; }
        private RazorTemplateEngine _engine;
        public List<TableInfo> CodeAllTableInfo { get; set; }
        public TableInfo CodeCurrentTableInfo { get; set; }
        private int CurrentIndex { get; set; } = 0;
        public DbTableInfo[] AllTable { get; set; }
        public DbTableInfo CurrentTable { get; set; }
        public Project Project { get; set; }
        public BuildTask()
        {
            InitEngine();
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
            if (project.GeneratorMode == GeneratorMode.DbFirst)
            {
                FreeSql = new FreeSqlBuilder()
                    .UseConnectionString(project.DataSource.DbType, project.DataSource.ConnectionString)
                    .Build();
                AllTable = FreeSql.DbFirst.GetTablesByDatabase(Project.DataSource.Name).ToArray();
                if (project.IncludeTable != null)
                {
                    AllTable = AllTable.Where(x => project.IncludeTable.Contains(x.Name)).ToArray();
                }
                CurrentTable = AllTable[CurrentIndex];
            }
            else
            {
                var types = Assembly.GetEntryAssembly()?.GetTypes();
                var tables = types?.Where(type => Reflection.GetTopBaseType(type).Name == Project.EntityBaseName && !type.IsAbstract).ToList();
                tables?.ForEach(t => new TableInfo(t).Add());
                CodeAllTableInfo = new List<TableInfo>();
                if (project.IncludeTable != null)
                {
                    tables = tables?.Where(x => project.IncludeTable.Contains(x.GetCustomAttribute<TableAttribute>()?.Name ?? x.Name)).ToList();
                }
                tables?.ForEach(table =>
                {
                    var t = new TableInfo(table);
                    CodeAllTableInfo.Add(t);
                });
                CodeCurrentTableInfo = CodeAllTableInfo[CurrentIndex];
            }
        }

        public void InitEngine()
        {
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine("---------------------------------------FreeSqlGenerator-----------------------------------------");
            Console.WriteLine("-------------------------------------------Start------------------------------------------------");
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine("Razor引擎开始初始化");
            _engine = new RazorTemplateEngine();
            _engine.Initialize(null);
            Console.WriteLine("Razor引擎初始化成功");
        }

        public async Task Start()
        {
            ImportSetting(Project);
            do
            {
                string tableName = null;
                switch (Project.GeneratorMode)
                {
                    case GeneratorMode.DbFirst:
                        tableName = Project.Entity.GetName(CurrentTable.Name);
                        var entityContent = await _engine.Render(this, Project.Entity.Template.TemplatePath);
                        await Project.Entity.OutPut(Project, tableName, entityContent);
                        break;
                    case GeneratorMode.CodeFirst:
                        tableName = CodeCurrentTableInfo.Name;
                        break;
                    default:
                        break;
                }
                foreach (var value in Project.Builders)
                {
                    if (!value.IsServiceOnly || CodeCurrentTableInfo.IsServiceTable)
                    {
                        var content = await _engine.Render(this, value.Template.TemplatePath);
                        await value.OutPut(Project, tableName, content);
                    }
                }
            }
            while (Next());
            foreach (var value in Project.GlobalBuilders)
            {
                var content = await _engine.Render(this, value.Template.TemplatePath);
                await value.OutPut(Project, this.Project.ProjectName, content);
            }
        }


        public bool Next()
        {
            if (Project.GeneratorMode == GeneratorMode.DbFirst)
            {
                if (CurrentIndex == AllTable.Length - 1) return false;
                CurrentIndex++;
                CurrentTable = AllTable[CurrentIndex];
                return true;
            }
            if (CurrentIndex == CodeAllTableInfo.Count - 1) return false;
            CurrentIndex++;
            CodeCurrentTableInfo = CodeAllTableInfo[CurrentIndex];
            return true;
        }
    }
}
