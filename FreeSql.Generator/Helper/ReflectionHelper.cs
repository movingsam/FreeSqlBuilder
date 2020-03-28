using FreeSql.DataAnnotations;
using FreeSql.Generator.Core.CodeFirst;
using GRES.Framework.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static FreeSql.Generator.Core.Utilities.GloabalTableInfo;

namespace AngularGenerator.Helper
{
    public class ReflectionHelper
    {
        private readonly IWebHostEnvironment webEnv;
        private string OutputString;
        private string Output;
        public ReflectionHelper(IWebHostEnvironment env)
        {
            webEnv = env;
            OutputString = string.Empty;
            Output = Path.Combine(webEnv.ContentRootPath, "CodeFirst", "bin");
        }
        public Task<List<TableInfo>> TaskRun(string entityBaseName, string projectName)
        {
            var dllpath = Path.Combine(Output, $"{projectName}.dll");
            var asmblies = Assembly.LoadFrom(dllpath);
            var type = asmblies.GetTypes();
            var types = type.Where(x => Reflection.IsBaseClass(x, entityBaseName)).ToList();
            types?.ForEach(t => new TableInfo(t).Add());
            var res = types.Select(s => new TableInfo(s)).ToList();
            return Task.FromResult(res);
        }
        public string ReleaseDll(string path)
        {
            if (!Directory.Exists(Output))
            {
                Directory.CreateDirectory(Output);
            }
            var projectName = Path.GetFileName(path);
            var psi = new ProcessStartInfo("dotnet", $"build {path} -o {Output}") { RedirectStandardOutput = true };
            var proc = Process.Start(psi);
            if (proc == null)
            {
                OutputString += "执行失败";
            }
            else
            {
                OutputString += "------------开启执行结果日志--------------";
                //开始读取
                using (var sr = proc.StandardOutput)
                {
                    while (!sr.EndOfStream)
                    {
                        OutputString += sr.ReadLine();
                    }
                    if (!proc.HasExited)
                    {
                        proc.Kill();
                    }
                }
                OutputString += "---------------读取 结束------------------";
                OutputString += $"执行结束 总耗时:{(proc.ExitTime - proc.StartTime).TotalMilliseconds} ms";
                OutputString += $"结束代码 : {proc.ExitCode}";
            }
            return projectName.Replace(".csproj", "");
        }

    }
}
