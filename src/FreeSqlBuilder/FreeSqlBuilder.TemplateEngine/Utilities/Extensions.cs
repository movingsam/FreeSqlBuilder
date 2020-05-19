using FreeSqlBuilder.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FreeSqlBuilder.TemplateEngine.Utilities
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
        public static async Task OutPut(this BuilderOptions options, string tableName, string content)
        {
            var project = options.Project;
            var root = string.IsNullOrWhiteSpace(project.ProjectInfo.RootPath) ? AppContext.BaseDirectory : project.ProjectInfo.RootPath;
            var rootPath = Path.Combine(root, project.ProjectInfo.OutPutPath);
            var dirPath = Path.Combine(rootPath, options.ReplaceTablePath(tableName));
            var outputPath = Path.Combine(dirPath, $"{options.GetName(tableName)}.{options.FileExtensions}");
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            var fileExists = File.Exists(outputPath);
            if (fileExists)
            {
                File.Delete(outputPath);
            }
            await using var streamWriter = new StreamWriter(outputPath);
            await streamWriter.WriteAsync(content.Trim());
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
