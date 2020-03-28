using FreeSql.Generator.Core;
using FreeSql.TemplateEngine;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FreeSql.Generator
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            BuildTask task = new BuildTask();
            task.InitSetting(Path.Combine(AppContext.BaseDirectory, "generator.store.json"));
            await task.Start();
        }
    }
}
