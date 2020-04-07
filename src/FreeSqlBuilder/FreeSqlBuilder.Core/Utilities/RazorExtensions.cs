﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreeSql.DatabaseModel;
using FreeSql.Internal.Model;
using FreeSqlBuilder.Core.WordsConvert;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FreeSqlBuilder.Core.Utilities
{
    public static class RazorExtensions
    {
        /// <summary>
        /// 换行
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static String NewLine(this IHtmlHelper htmlHelper)
        {
            return Environment.NewLine;
        }
        /// <summary>
        /// 换行并加上文字
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="appendStr"></param>
        /// <returns></returns>
        public static String NewLine(this IHtmlHelper htmlHelper, string appendStr)
        {
            return Environment.NewLine + appendStr;
        }
        /// <summary>
        /// 左空格
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="totalWidth"></param>
        /// <returns></returns>
        public static String PadLeft(this IHtmlHelper htmlHelper, int totalWidth)
        {
            return String.Empty.PadLeft(totalWidth);
        }
        /// <summary>
        /// 右边空格
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="totalWidth"></param>
        /// <returns></returns>
        public static String PadRight(this IHtmlHelper htmlHelper, int totalWidth)
        {
            return String.Empty.PadRight(totalWidth);
        }
        /// <summary>
        /// 获取cs备注
        /// </summary>
        /// <param name="summary"></param>
        /// <returns></returns>
        public static string GetCSharpSummary(this string summary)
        {
            StringBuilder csharpSummary = new StringBuilder();
            csharpSummary.AppendLine();
            csharpSummary.AppendLine("///<summary>");
            if (!string.IsNullOrWhiteSpace(summary))
            {
                if (summary.Contains(Environment.NewLine))
                {
                    foreach (var summaryLine in summary.Split(Environment.NewLine.ToCharArray()))
                    {
                        if (String.IsNullOrEmpty(summaryLine))
                        {
                            continue;
                        }

                        csharpSummary.AppendLine($"/// {summaryLine}");
                    }
                }
                else
                {
                    csharpSummary.AppendLine($"/// {summary}");
                }
            }
            csharpSummary.Append("///</summary>");
            return csharpSummary.ToString();
        }

        public static string GetUsing(this List<string> usings)
        {
            StringBuilder usingStr = new StringBuilder();
            usings.ForEach(u => usingStr.AppendLine($"using {u}"));
            usingStr.AppendLine();
            return usingStr.ToString();
        }

        /// <summary>
        /// 获取导航类的命名空间
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetNameSpaceUsing(this TableInfo table)
        {
            var sb = new StringBuilder();
            table.Columns.Select(x => x.Value.CsType.Namespace).Distinct().ToList().ForEach(import =>
              {
                  sb.AppendLine($"using {import};");
              });
            return sb.ToString();
        }

        /// <summary>
        /// 获取实体特性
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetAttribute(this DbTableInfo table)
        {
            StringBuilder attribute = new StringBuilder();
            attribute.AppendLine();
            attribute.Append($"[Table(Name=\"{table.Name}\")]");
            attribute.AppendLine();
            return attribute.ToString();
        }
        /// <summary>
        /// 获取属性特性
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string GetAttribute(this DbColumnInfo column)
        {
            StringBuilder attribute = new StringBuilder();
            List<string> cloumnAttributes = new List<string>();
            if (column.IsIdentity) cloumnAttributes.Add("IsIdentity = true");
            if (column.IsNullable) cloumnAttributes.Add("IsNullable = true");
            else cloumnAttributes.Add("IsNullable = false");
            if (column.IsPrimary) cloumnAttributes.Add("IsPrimary = true");
            if (cloumnAttributes.Count > 0)
            {
                attribute.Append("[Column(");
                attribute.Append(string.Join(',', cloumnAttributes));
                attribute.Append(")]");
                attribute.AppendLine();
            }
            if (column.CsType == typeof(string) && column.MaxLength != 255)
            {
                attribute.Append($"[MaxLength({column.MaxLength})]");
                attribute.AppendLine();
            }
            return attribute.ToString();
        }
        /// <summary>
        /// 获取属性特性
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string GetDtoAttribute(this ColumnInfo column)
        {
            StringBuilder attribute = new StringBuilder();
            attribute.AppendLine();
            if (!(column.Attribute?.IsNullable ?? true)) attribute.AppendLine("[Required]");
            if (column.CsType == typeof(string))
            {
                attribute.AppendLine($"[MaxLength({column.Attribute.StringLength})]");
            }
            return attribute.ToString();
        }
        /// <summary>
        /// 通过名称转换器后的结果
        /// </summary>
        /// <param name="options"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetName(this BuilderOptions options, string tableName)
        {
            if (options == null) return tableName;
            var convert = new DefaultWordsConvert(options.Mode);
            var convertName = convert.Convert(tableName);
            if (options.SplitDot != null)
            {
                var splitArray = convertName.Split(options.SplitDot);
                if (splitArray.Length > 1) convertName = splitArray[1];
            }
            return options.IsIgnorePrefix
                ? $"{options.Prefix}{convertName}{options.Suffix}"
                : $"{options.Prefix}{convert.Convert(tableName)}{options.Suffix}";
        }
        /// <summary>
        /// 通过转换器后的字段名
        /// </summary>
        /// <param name="column"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetColumnName(this Column column, string name)
        {
            var convert = new DefaultWordsConvert(column.Mode);
            return $"{column.Prefix}{convert.Convert(name)}{column.Suffix}";
        }
        /// <summary>
        /// 获取某个构建类型
        /// </summary>
        /// <param name="project"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static BuilderOptions GetBuilder(this Project project, string key)
        {
            return project.Builders.Any(x => x.Name == key) ? project.Builders.FirstOrDefault(x => x.Name == key) : null;
        }
        /// <summary>
        /// 获取Dto名字
        /// </summary>
        /// <param name="project"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetDto(this Project project, string tableName)
        {
            return project.GetBuilder("Dtos") != null ? project.GetBuilder("Dtos").GetName(tableName) : $"{tableName}Dtos";
        }
        /// <summary>
        /// 获取PostDto名字
        /// </summary>
        /// <param name="project"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetPostDataDto(this Project project, string tableName)
        {
            return project.GetBuilder("PostDataDto") != null ? project.GetBuilder("PostDataDto").GetName(tableName) : $"{tableName}PostDataDto"; 
        }
        /// <summary>
        /// 获取分页视图Dto名称
        /// </summary>
        /// <param name="project"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetPageViewDto(this Project project, string tableName)
        {
            return project.GetBuilder("PageViewDto") != null ? project.GetBuilder("PageViewDto").GetName(tableName) : $"{tableName}PageViewDto"; 
        }
        /// <summary>
        /// 获取分页请求dto
        /// </summary>
        /// <param name="project"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetPagedDto(this Project project, string tableName)
        {
            return project.GetBuilder("PagedDtos") != null ? project.GetBuilder("PagedDtos").GetName(tableName) : $"{tableName}PagedDtos"; 
        }
        /// <summary>
        /// 获取主键名
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static string GetPrimarysName(this TableInfo info)
        {
            return string.Join(",", info.Primarys.Select(x => Reflection.ToCsType(x.CsType)));
        }
        /// <summary>
        /// csType转string 名称
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static string GetCsTypeName(this ColumnInfo info)
        {
            return Reflection.ToCsType(info.CsType);
        }

    }
}
