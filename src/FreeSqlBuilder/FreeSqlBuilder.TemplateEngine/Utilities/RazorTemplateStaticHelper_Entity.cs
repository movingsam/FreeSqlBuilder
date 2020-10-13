using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreeSql.DatabaseModel;
using FreeSql.Internal.Model;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Core.Utilities;
using FreeSqlBuilder.Core.WordsConvert;

namespace FreeSqlBuilder.TemplateEngine.Utilities
{
    /// <summary>
    /// Razor模板静态帮助类 实体部分
    /// </summary>
    public static partial class RazorTemplateStaticHelper
    {
        /// <summary>
        /// DbFirst实体获取特性
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string GetAttribute(this DbColumnInfo column, int padleft = 8)
        {
            if (column == null)
            {
                return "";
            }
            var pl = PadRight(8);
            var sb = new StringBuilder();
            if (column.IsPrimary)
            {
                var primaryAttr = "IsPrimary=true";
                if (column.IsIdentity)
                {
                    primaryAttr += ",IsIdentity=true";
                }
                sb.AppendLine($"{pl}[Column({primaryAttr})]");
            }
            var attributes = new List<string>();
            attributes.Add($"Name=\"{column.Name}\"");
            attributes.Add($"DbType=\"{column.DbTypeTextFull}\"");
            if (column.CsType == typeof(string) && column.MaxLength != 255)
            {
                attributes.Add($"StringLength={column.MaxLength}");
            }
            attributes.Add($"IsNullable = {column.IsNullable}");
            var res = string.Join(",", attributes);
            sb.AppendLine($"{pl}[Column({res})]");
            return sb.ToString();
        }

        /// <summary>
        /// 请求入参校验Dto获取特性 必填/长度等校验 DbFirst
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string RequestDtoGetAttribute(this DbColumnInfo column, int padleft = 8)
        {
            if (column == null)
            {
                return "";
            }
            var sb = new StringBuilder();
            var pl = PadRight(8);
            if (column.CsType == typeof(string))
            {
                sb.AppendLine($"{pl}[MaxLength({column.MaxLength})]");
            }
            if (!column.IsNullable)
            {
                sb.AppendLine($"{pl}[Required]");
            }
            if (column.Name.ToLower().Contains("email"))
            {
                sb.AppendLine($"{pl}[EmailAddress]");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 请求入参校验Dto获取特性 必填/长度等校验 CodeFirst
        /// </summary>
        /// <param name="column"></param>
        /// <param name="padleft"></param>
        /// <returns></returns>
        public static string RequestDtoGetAttribute(this ColumnInfo column, int padleft = 8)
        {
            if (column == null)
            {
                return "";
            }
            var sb = new StringBuilder();
            var attributes = column.Attribute;
            var pl = PadRight(8);
            if (column.CsType == typeof(string))
            {
                var stringLength = attributes.StringLength == 0 ? 255 : attributes.StringLength;
                sb.AppendLine($"{pl}[MaxLength({stringLength})]");
            }
            if (!attributes.IsNullable)
            {
                sb.AppendLine($"{pl}[Required]");
            }
            if (column.CsName.ToLower().Contains("email"))
            {
                sb.AppendLine($"{pl}[EmailAddress]");
            }
            return sb.ToString();
        }


        /// <summary>
        /// 通过转换器后的字段名
        /// </summary>
        /// <param name="column"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetColumnName(this BuilderOptions column, string name)
        {
            var convert = new DefaultWordsConvert(column.Mode);
            return $"{column.Prefix}{convert.Convert(name)}{column.Suffix}";
        }
        /// <summary>
        /// 获取主键属性名
        /// </summary>
        /// <returns></returns>
        public static string GetPkName(this TableInfo table)
        {
            return table.Primarys.FirstOrDefault()?.CsName;
        }
        /// <summary>
        /// 获取主键类型名称
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetPkTypeName(this TableInfo table)
        {
            return Reflection.ToCsType(table.Primarys.FirstOrDefault()?.CsType);
        }
        /// <summary>
        /// 获取主键列名
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetPkName(this DbTableInfo table)
        {
            return table.Primarys.FirstOrDefault()?.Name;
        }


        /// <summary>
        /// 获取主键属性名
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetPkTypeName(this DbTableInfo table)
        {
            return Reflection.ToCsType(table.Primarys.FirstOrDefault()?.CsType);
        }
        /// <summary>
        /// 如果不是系统类型则自动转换成构建器生成的类名 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToBuilderType(this BuilderOptions builder, Type type)
        {
            if (type == null)
            {
                return string.Empty;
            }
            var res = Reflection.SystemCsType(type);
            if (!string.IsNullOrWhiteSpace(res)) return res;
            if (Reflection.IsCollection(type))
            {
                var typeDefinition = type.GetGenericTypeDefinition();
                var types = string.Join(',', type.GetGenericArguments().Select(x =>
                {
                    var sysType = Reflection.SystemCsType(x);
                    return string.IsNullOrWhiteSpace(sysType) ? builder.GetName(x.Name) : sysType;
                }));
                var collectionType = typeDefinition == typeof(IEnumerable<>) ? "IEnumerable" :
                    typeDefinition == typeof(IReadOnlyCollection<>) ? "IReadOnlyCollection" :
                    typeDefinition == typeof(IReadOnlyList<>) ? "IReadOnlyList" :
                    typeDefinition == typeof(ICollection<>) ? "ICollection" :
                    typeDefinition == typeof(IList<>) ? "IList" :
                    typeDefinition == typeof(List<>) ? "List" :
                    typeDefinition == typeof(IDictionary<,>) ? "IDictionary" :
                    typeDefinition == typeof(Dictionary<,>) ? "Dictionary" : "";

                return $"{collectionType}<{types}>";
            }

            if (Reflection.IsEnum(type))
            {
                return type.FullName;
            }
            return builder.GetName(type.Name);
        }
    }
}