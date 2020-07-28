using FreeSqlBuilder.Core;
using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FreeSqlBuilder.Core.Entities;

namespace FreeSqlBuilder.TemplateEngine.Utilities
{
    public static class Extensions
    {
        /// <summary>
        /// 输出文件拓展
        /// </summary>
        /// <param name="task"></param>
        /// <param name="tableName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static async Task OutPut(this IBuilderTask task, string tableName, string content)
        {
            var project = task.Project;
            var root = project == null ? AppContext.BaseDirectory : project.ProjectInfo.RootPath;
            var outPutPath = project == null ? "FreeSqlBuilder" : project.ProjectInfo.OutPutPath;
            var rootPath = Path.Combine(root, outPutPath);
            var dirPath = Path.Combine(rootPath, project?.ProjectInfo.ReplaceTablePath(tableName));
            var outputPath = Path.Combine(dirPath, $"{task.CurrentBuilder.GetName(tableName)}.{task.CurrentBuilder.FileExtensions}");
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
