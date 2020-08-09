using System;
using System.Collections;
using System.Linq;
using System.Text;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Core.WordsConvert;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FreeSqlBuilder.TemplateEngine.Utilities
{
    /// <summary>
    /// Razor模板静态帮助类 通用部分
    /// </summary>
    public static partial class RazorTemplateStaticHelper
    {
        /// <summary>
        /// 换行
        /// </summary> 
        /// <returns></returns>
        public static string NewLine(this IHtmlHelper html)
        {
            return Environment.NewLine;
        }
        /// <summary>
        /// 换行并加上文字
        /// </summary>
        /// <param name="appendStr"></param>
        /// <returns></returns>
        public static string NewLine(this string appendStr)
        {
            return Environment.NewLine + appendStr;
        }
        /// <summary>
        /// 左空格
        /// </summary>
        /// <param name="totalWidth">空格长度</param>
        /// <returns></returns>
        public static string PadLeft(this int totalWidth)
        {
            return string.Empty.PadLeft(totalWidth);
        }
        /// <summary>
        /// 右边空格
        /// </summary>
        /// <param name="totalWidth"></param>
        /// <returns></returns>
        public static string PadRight(int totalWidth)
        {
            return string.Empty.PadRight(totalWidth);
        }

        /// <summary>
        /// 获取cs备注 (默认左边空格8位 若修改请加参数)
        /// </summary>
        /// <param name="summary"></param>
        /// <param name="padLeftWidth"></param>
        /// <returns></returns>
        public static string GetCSharpSummary(this string summary, int padLeftWidth = 8)
        {
            StringBuilder csharpSummary = new StringBuilder();
            var lp = PadLeft(padLeftWidth);
            csharpSummary.AppendLine();
            csharpSummary.AppendLine($"{lp}///<summary>");
            if (!string.IsNullOrWhiteSpace(summary))
            {
                if (summary.Contains(Environment.NewLine))
                {
                    foreach (var summaryLine in summary.Split(Environment.NewLine.ToCharArray()))
                    {
                        if (string.IsNullOrEmpty(summaryLine))
                        {
                            continue;
                        }
                        csharpSummary.AppendLine($"{lp}/// {summaryLine}");
                    }
                }
                else
                {
                    csharpSummary.AppendLine($"{lp}/// {summary}");
                }
            }
            csharpSummary.Append($"{lp}///</summary>");
            return csharpSummary.ToString();
        }

        /// <summary>
        /// 计算引用并生成字符串
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static string GetUsing(this IBuilderTask task)
        {
            //获取当前实体表的所有属性 并获取实体属性的类型所在命名空间
            var namespaces = task.CurrentTable.Properties
                ?.Select(x => x.Value.PropertyType.Namespace)
                .Distinct()
                .ToList();
            StringBuilder usingStr = new StringBuilder();
            namespaces?.ForEach(u => usingStr.AppendLine($"using {u};"));
            return usingStr.ToString();
        }

        /// <summary>
        /// 获取命名空间
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static string GetNameSpace(this IBuilderTask task)
        {
            var currentTable = task.CurrentTable.CsName;
            var res = $"{task?.Project?.ProjectInfo?.NameSpace}.{task?.CurrentBuilder?.ReplaceTablePath(currentTable)}";
            return res;
        }

        /// <summary>
        /// 获取CodeFirst名称
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static string GetCodeFirstName(this IBuilderTask task)
        {
            
            if (task.CurrentBuilder == null) return task.CurrentTable.CsName;
            var convert = new DefaultWordsConvert(task.CurrentBuilder.Mode);
            var convertName = convert.Convert(task.CurrentTable.CsName);
            return $"{task.CurrentBuilder.Prefix}{convertName}{task.CurrentBuilder.Suffix}";
        }
        /// <summary>
        /// 获取CodeFirst名称
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetName(this BuilderOptions builder,string tableName)
        {
            if (builder == null) return tableName;
            var convert = new DefaultWordsConvert(builder.Mode);
            var convertName = convert.Convert(tableName);
            return $"{builder.Prefix}{convertName}{builder.Suffix}";
        }
        /// <summary>
        /// 获取DbFirst名称
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static string GetDbFirstName(this IBuilderTask task)
        {
            if (task.CurrentBuilder == null) return task.CurrentDbTable.Name;
            var convert = new DefaultWordsConvert(task.CurrentBuilder.Mode);
            var convertName = convert.Convert(task.CurrentDbTable.Name);
            return $"{task.CurrentBuilder.Prefix}{convertName}{task.CurrentBuilder.Suffix}";
        }
        

    }
}