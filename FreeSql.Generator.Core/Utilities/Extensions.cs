using FreeSql.TemplateEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FreeSql.Generator.Core.Utilities
{
    public static class Extensions
    {
        public async static Task OutPut(this BuilderOptions options, Project project, string tableName, string content)
        {
            var root = string.IsNullOrWhiteSpace(project.RootPath) ? AppContext.BaseDirectory : project.RootPath;
            var dirPath = Path.Combine(root, project.ReplaceTablePath(tableName), options.ReplaceTablePath(tableName));
            var outputPath = Path.Combine(dirPath, $"{options.GetName(tableName)}.cs");
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            var fileExists = File.Exists(outputPath);
            if (fileExists)
            {
                File.Delete(outputPath);
            }
            await using (var streamWriter = new StreamWriter(outputPath))
            {
                await streamWriter.WriteAsync(content.Trim());
            }
            Console.WriteLine($"生成文件{options.GetName(tableName)}->输出路径{outputPath}");
            Console.WriteLine($"内容:{content}");
        }
        public static string ReplaceTablePath(this IOutPut outPut, string tableName)
        {
            return outPut.OutPutPath.Replace("{TableName}", tableName);
        }
    }
}
