using FreeSql.DatabaseModel;
using FreeSql.Internal.Model;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Core.Utilities;
using FreeSqlBuilder.Core.WordsConvert;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreeSqlBuilder.Core.Entities;

namespace FreeSqlBuilder.TemplateEngine.Utilities
{
    /// <summary>
    /// Razor拓展
    /// 若有别的想法 可以自定义 在模板中可以自行调用
    /// </summary>
    public static class RazorExtensions
    {
        /// <summary>
        /// 换行
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static string NewLine(this IHtmlHelper htmlHelper)
        {
            return Environment.NewLine;
        }
        /// <summary>
        /// 换行并加上文字
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="appendStr"></param>
        /// <returns></returns>
        public static string NewLine(this IHtmlHelper htmlHelper, string appendStr)
        {
            return Environment.NewLine + appendStr;
        }
        /// <summary>
        /// 左空格
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="totalWidth"></param>
        /// <returns></returns>
        public static string PadLeft(this IHtmlHelper htmlHelper, int totalWidth)
        {
            return string.Empty.PadLeft(totalWidth);
        }
        /// <summary>
        /// 右边空格
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="totalWidth"></param>
        /// <returns></returns>
        public static string PadRight(this IHtmlHelper htmlHelper, int totalWidth)
        {
            return string.Empty.PadRight(totalWidth);
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
            csharpSummary.AppendLine("        ///<summary>");
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

                        csharpSummary.AppendLine($"        /// {summaryLine}");
                    }
                }
                else
                {
                    csharpSummary.AppendLine($"        /// {summary}");
                }
            }
            csharpSummary.Append("        ///</summary>");
            return csharpSummary.ToString();
        }
        /// <summary>
        /// 计算引用
        /// </summary>
        /// <param name="usings"></param>
        /// <returns></returns>
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
            if (!(column.Attribute?.IsNullable ?? true)) attribute.AppendLine("        [Required]");
            if (column.CsType == typeof(string))
            {
                var length = column.Attribute.StringLength == 0 ? 255 : column.Attribute.StringLength;
                attribute.AppendLine($"        [MaxLength({length})]");
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
            return $"{options.Prefix}{convertName}{options.Suffix}";
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
        /// <summary>
        /// 获取导航属性
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, TableInfo>> GetNavigates(this IBuilderTask task)
        {
            var navigateKeys = task.CurrentTable.Properties.Keys.Except(task.CurrentTable.Columns.Keys);
            var types = navigateKeys.Select(t => GetGenericFirstTypeName(task.CurrentTable.Properties.FirstOrDefault(p => p.Key == t).Value.PropertyType)).ToList();
            var gTypes = task.AllTable.Select(s =>
            {
                if (s.Type.IsGenericType)
                {
                    return new KeyValuePair<string, TableInfo>(s.Type.GetGenericArguments().FirstOrDefault().Name, s);
                }
                return new KeyValuePair<string, TableInfo>(s.Type.Name, s);
            });
            var res = gTypes.Where(g => types.Contains(g.Key))
                 .Select(s =>
                 new KeyValuePair<string, TableInfo>
                 (task.CurrentTable.Properties.FirstOrDefault(p => GetGenericFirstTypeName(p.Value.PropertyType) == s.Key).Key, s.Value)
                 ).ToList();
            return res;

        }

        private static string GetGenericFirstTypeName(this Type type)
        {
            if (type.IsGenericType)
            {
                return type.GetGenericArguments().FirstOrDefault().Name;
            }
            return type.Name;
        }
        /// <summary>
        /// 获取CRUD导航属性
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static List<TableRef> GetNavigations(this CurdTask task)
        {
            var navigateKeys = task.CurrentTable.Properties.Keys.Except(task.CurrentTable.Columns.Keys).ToList();
            var types = navigateKeys.Select(t =>
                task.CurrentTable.GetTableRef(t, false)).Where(x => x != null).ToList();
            return types;
        }
        /// <summary>
        /// 获取导航属性关联字符串
        /// </summary>
        /// <param name="task"></param>
        /// <param name="Html"></param>
        /// <returns></returns>
        public static string GetIncludeStr(this CurdTask task, IHtmlHelper Html)
        {
            var nick = task.Task.CurrentTable.CsName.ToLower();
            var includes = new List<string>();
            var includeManys = new List<string>();
            var navigates = task.GetNavigations();
            if (navigates.Where(x => x.RefType == FreeSql.Internal.Model.TableRefType.OneToOne)?.Count() > 0)
            {
                includes.AddRange(navigates.Where(x => x.RefType == FreeSql.Internal.Model.TableRefType.OneToOne).Select(s => $"Include({nick}=>{nick}.{s.Property.Name})"));
            }
            var includeStr = string.Join(Html.NewLine(Html.PadLeft(8) + "."), includes);
            includeStr = (!string.IsNullOrWhiteSpace(includeStr) ? "." : "") + includeStr;
            if (navigates.Where(x => x.RefType == FreeSql.Internal.Model.TableRefType.ManyToOne)?.Count() > 0)
            {
                includeManys.AddRange(navigates.Where(x => x.RefType == FreeSql.Internal.Model.TableRefType.ManyToOne).Select(s => $"IncludeMany({nick}=>{nick}.{s.Property.Name})"));
            }
            if (navigates.Where(x => x.RefType == FreeSql.Internal.Model.TableRefType.ManyToMany)?.Count() > 0)
            {
                includeManys.AddRange(navigates.Where(x => x.RefType == FreeSql.Internal.Model.TableRefType.ManyToMany).Select(s => $"IncludeMany({nick}=>{nick}.{s.Property.Name})"));
            }
            var includeManysStr = string.Join(Html.NewLine(Html.PadLeft(8) + "."), includeManys);
            includeManysStr = (!string.IsNullOrWhiteSpace(includeManysStr) ? Html.NewLine(Html.PadLeft(8) + ".") : "") + includeManysStr;
            return $"{includeStr}{includeManysStr}";
        }
        public static string ToDtoType(this Project project, Type type)
        {
            if (type == null)
            {
                return string.Empty;
            }
            if (Reflection.IsCollection(type))
            {
                var typeDefinition = type.GetGenericTypeDefinition();
                var types = string.Join(',', type.GetGenericArguments().Select(x => project.GetDto(x.Name)));
                var collectionType = typeDefinition == typeof(IEnumerable<>) ? "IEnumerable" :
                    typeDefinition == typeof(IReadOnlyCollection<>) ? "IReadOnlyCollection" :
                    typeDefinition == typeof(IReadOnlyList<>) ? "IReadOnlyList" :
                    typeDefinition == typeof(ICollection<>) ? "ICollection" :
                    typeDefinition == typeof(IList<>) ? "IList" :
                    typeDefinition == typeof(List<>) ? "List" : "";
                return $"{collectionType}<{types}>";
            }
            return project.GetDto(type.Name);
        }
        public static string ToPostDtoType(this Project project, Type type) {
            if (type == null)
            {
                return string.Empty;
            }
            if (Reflection.IsCollection(type))
            {
                var typeDefinition = type.GetGenericTypeDefinition();
                var types = string.Join(',', type.GetGenericArguments().Select(x => project.GetPostDataDto(x.Name)));
                var collectionType = typeDefinition == typeof(IEnumerable<>) ? "IEnumerable" :
                    typeDefinition == typeof(IReadOnlyCollection<>) ? "IReadOnlyCollection" :
                    typeDefinition == typeof(IReadOnlyList<>) ? "IReadOnlyList" :
                    typeDefinition == typeof(ICollection<>) ? "ICollection" :
                    typeDefinition == typeof(IList<>) ? "IList" :
                    typeDefinition == typeof(List<>) ? "List" : "";
                return $"{collectionType}<{types}>";
            }
            return project.GetPostDataDto(type.Name);
        }
    }
}
