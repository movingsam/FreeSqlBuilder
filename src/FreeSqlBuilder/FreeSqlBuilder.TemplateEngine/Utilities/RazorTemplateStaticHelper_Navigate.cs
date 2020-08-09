using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreeSql.Internal.Model;
using FreeSqlBuilder.Core.Entities;

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
            var navigateKeys = table.Properties.Select(x => x.Key).Except(table.ColumnsByCs.Select(x => x.Key)).ToList();
            var navigates = table.Properties
                            .Where(x => navigateKeys.Contains(x.Key))
                            .Select(x => table.GetTableRef(x.Value.Name, false))
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
        /// <returns></returns>
        public static string GetIncludeStr(this TableInfo table)
        {
            StringBuilder sb = new StringBuilder();
            var includes = table.GetInclude();
            foreach (var includeNavigate in includes)
            {
                sb.AppendLine($".Include(x=>x.{includeNavigate})");
            }
            return sb.ToString();
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
        public static string GetIncludeManyMTMStr(this TableInfo table)
        {
            var navigates = table.GetIncludeManyMTM();
            StringBuilder sb = new StringBuilder();
            var nickName = table.CsName.Length >= 3
                ? table.CsName.ToLower().Substring(0, 3)
                : table.CsName.ToLower();
            foreach (var includeNavigate in navigates)
            {
                sb.AppendLine($".IncludeMany({nickName}=>{nickName}.{includeNavigate})");
            }
            return sb.ToString();
        }
        /// <summary>
        /// IncludeMany 为IncludeMany抽象分别生成字符串
        /// </summary>
        /// <param name="many"></param>
        /// <param name="pKey"></param>
        /// <returns></returns>
        public static string IncludeManyStr(this TIIncludeMany many, string pKey)
        {
            var sb = new StringBuilder();
            var includeSb = new StringBuilder();
            bool then = false;
            var thenStr = string.Empty;
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
                var includeManyStr = m.IncludeManyStr(m.Key.ToLower());
                m.Include.ForEach(x => includeSb.Append($"{thenStr}{includeManyStr}.Include({m.Key}=>{m.Key}.{x})"));
            });
            sb.AppendLine($".IncludeMany({pKey}=>{pKey}.{many.Key}{includeSb})");

            return sb.ToString();
        }

        /// <summary>
        /// 生成生成后的Include字符串
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetIncludeManyStr(this TableInfo table)
        {
            StringBuilder sb = new StringBuilder();
            var includes = table.GetIncludeManyOTM();
            foreach (var includeNavigate in includes)
            {
                var nickName = includeNavigate.Key.Length >= 3
                    ? includeNavigate.Key.ToLower().Substring(0, 3)
                    : includeNavigate.Key.ToLower();
                sb.Append(includeNavigate.IncludeManyStr(nickName));
            }
            sb.Append(table.GetIncludeManyMTMStr());
            return sb.ToString();
        }
        #endregion
    }
}