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
        /// <summary>
        /// 输出文件拓展
        /// </summary>
        /// <param name="options"></param>
        /// <param name="tableName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async static Task OutPut(this BuilderOptions options, string tableName, string content)
        {
            var project = options.Project;
            var root = string.IsNullOrWhiteSpace(project.ProjectInfo.RootPath) ? AppContext.BaseDirectory : project.ProjectInfo.RootPath;
            var rootPath = Path.Combine(root, project.ProjectInfo.OutPutPath);
            var dirPath = Path.Combine(rootPath, project.ProjectInfo.ReplaceTablePath(tableName), options.ReplaceTablePath(tableName));
            var outputPath = Path.Combine(dirPath, $"{options.GetName(tableName)}.{options.Template.FileExtension}");
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
        /// <summary>
        /// 表名替换
        /// </summary>
        /// <param name="outPut"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string ReplaceTablePath(this IOutPut outPut, string tableName)
        {
            return outPut.OutPutPath.Replace("{TableName}", tableName);
        }
    }
}
