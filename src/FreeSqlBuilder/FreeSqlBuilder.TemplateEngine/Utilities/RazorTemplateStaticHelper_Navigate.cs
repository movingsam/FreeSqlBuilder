using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreeSql.Internal.Model;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Core.Utilities;

namespace FreeSqlBuilder.TemplateEngine.Utilities
{
    /// <summary>
    /// Razor模板静态帮助类 导航对象部分
    /// CodeFirst
    /// </summary>
    public static partial class RazorTemplateStaticHelper
    {


        /// <summary>
        /// 通过CodeFirst TableInfo对象来获取导航属性
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<TableRef> GetNavigates(this TableInfo table)
        {
            //通过计算差集获取导航对象
            var navigateKeys = table.Properties.Select(x => x.Key).Except(table.ColumnsByCs.Select(x => x.Key))
                .ToList();
            var navigates = table.Properties
                            .Where(x => navigateKeys.Contains(x.Key))
                            .Where(x => x.Value.PropertyType != table.Type && x.Value.PropertyType.GetGenericArguments().FirstOrDefault() != table.Type)
                            .Select(x => table.GetTableRef(x.Value.Name, true))
                            .Distinct()
                            .Where(x => x != null)
                            .ToList();
            return navigates;
        }


        #region Include
        /// <summary>
        /// 获取Include 对象字符串集合
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<string> GetInclude(this TableInfo table)
        {
            var navigates = table.GetNavigates();
            var res = navigates?
                .Where(x => x.RefType == TableRefType.OneToOne || x.RefType == TableRefType.ManyToOne)
                .ToList();
            return res?.Select(x => x.Property.Name).ToList();
        }

        /// <summary>
        /// 生成生成后的Include字符串
        /// </summary>
        /// <param name="table"></param>
        /// <param name="padLeft"></param>
        /// <returns></returns>
        public static string GetIncludeStr(this TableInfo table,int padLeft=8)
        {
            StringBuilder sb = new StringBuilder();
            var pl = string.Empty.PadLeft(padLeft);
            var includes = table.GetInclude();
            foreach (var includeNavigate in includes)
            {
                sb.AppendLine($"{pl}.Include(x=>x.{includeNavigate})");
            }
            return sb.ToString().Trim();
        }


        #endregion

        #region IncludeMany



        /// <summary>
        /// 获取IncludeMany 对象字符串集合 一对多
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static List<TIIncludeMany> GetIncludeManyOTM(this TableInfo table)
        {
            var navigates = table.GetNavigates();
            var res = navigates?
                .Where(x => x.RefType == TableRefType.OneToMany)
                .ToList();
            var trees = res?.Select(x =>
            {
                TIIncludeMany temp = new TIncludeMany()
                {
                    Key = x?.Property?.Name,
                    Value = x?.RefColumns,
                    IsRoot = true
                };
                return temp;
            }).ToList();
            trees?.ForEach(tree =>
            {
                if (tree.Value is List<ColumnInfo> columnInfos)
                {
                    tree.Include = columnInfos.SelectMany(x => x.Table.GetInclude()).ToList();
                    tree.IncludeManys = columnInfos
                         .Select(x => x.Table)
                         .ToList()
                         .SelectMany(t => t.GetIncludeManyOTM())
                         .ToList();
                }
            });
            return trees;

        }

        /// <summary>
        /// 获取IncludeMany 对象字符串集合 多对多
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static List<string> GetIncludeManyMTM(this TableInfo table)
        {
            var navigates = table.GetNavigates();
            var res = navigates?
                .Where(x => x.RefType == TableRefType.ManyToMany)
                .ToList();
            return res?.Select(x => x.Property.Name).ToList();
        }
        /// <summary>
        /// 获取IncludeMany 生成字符串 多对多
        /// </summary>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static string GetIncludeManyMTMStr(this TableInfo table,int leftWidth)
        {
            var navigates = table.GetIncludeManyMTM();
            var pl = string.Empty.PadLeft(leftWidth);
            StringBuilder sb = new StringBuilder();
            var nickName = table.CsName.Length >= 3
                ? table.CsName.ToLower().Substring(0, 3)
                : table.CsName.ToLower();
            foreach (var includeNavigate in navigates)
            {
                sb.AppendLine($"{pl}.IncludeMany({nickName}=>{nickName}.{includeNavigate})");
            }
            return sb.ToString().Trim();
        }

        /// <summary>
        /// IncludeMany 为IncludeMany抽象分别生成字符串
        /// </summary>
        /// <param name="many"></param>
        /// <param name="pKey"></param>
        /// <param name="leftWidth"></param>
        /// <returns></returns>
        public static string IncludeManyStr(this TIIncludeMany many, string pKey,int leftWidth)
        {
            var sb = new StringBuilder();
            var includeSb = new StringBuilder();
            bool then = false;
            var thenStr = string.Empty;
            var pl= string.Empty.PadLeft(leftWidth);
            many.Include.ForEach(i =>
            {
                thenStr = !then ? ",then=>then" : string.Empty;
                then = true;
                includeSb.Append($"{thenStr}.Include({pKey}=>{pKey}.{i})");
            });
            many.IncludeManys.ForEach(m =>
            {
                thenStr = !then ? ",then=>then" : string.Empty;
                then = true;
                var includeManyStr = m.IncludeManyStr(m.Key.ToLower(), leftWidth);
                m.Include.ForEach(x => includeSb.Append($"{thenStr}{includeManyStr}.Include({m.Key}=>{m.Key}.{x})"));
            });
            sb.AppendLine($"{pl}.IncludeMany({pKey}=>{pKey}.{many.Key}{includeSb})");
            return sb.ToString().Trim();
        }

        /// <summary>
        /// 生成生成后的Include字符串
        /// </summary>
        /// <param name="table"></param>
        /// <param name="leftWidth"></param>
        /// <returns></returns>
        public static string GetIncludeManyStr(this TableInfo table,int leftWidth = 8)
        {
            StringBuilder sb = new StringBuilder();
            var includes = table.GetIncludeManyOTM();
            foreach (var includeNavigate in includes)
            {
                var nickName = includeNavigate.Key.Length >= 3
                    ? includeNavigate.Key.ToLower().Substring(0, 3)
                    : includeNavigate.Key.ToLower();
                sb.Append(includeNavigate.IncludeManyStr(nickName,leftWidth));
            }
            sb.Append(table.GetIncludeManyMTMStr(leftWidth));
            return sb.ToString().SafeString();
        }
        #endregion
    }
}