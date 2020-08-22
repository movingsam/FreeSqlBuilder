using System.Collections.Generic;
using System.Text;
using FreeSql.DatabaseModel;
using FreeSql.Internal.Model;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Core.Entities;
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
                sb.AppendLine($"{pl}[MaxLength({attributes.StringLength})]");
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
    }
}