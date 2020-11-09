using System;
using System.Linq;
using System.Text;
using FreeSql.Internal.Model;

namespace FreeSqlBuilder.TemplateEngine.Utilities
{
    /// <summary>
    /// Razor模板静态帮助类 查询部分
    /// </summary>
    public static partial class RazorTemplateStaticHelper
    {
        /// <summary>
        /// FreeSql查询语法生成
        /// </summary>
        /// <param name="table">CodeFirstTable对象</param>
        /// <param name="freeSqlObjString">IFreeSql对象 (变量)字符串</param>
        /// <param name="padleft"></param>
        /// <returns></returns>
        public static string MakeFreeSqlSelectStr(this TableInfo table, string freeSqlObjString, int padleft = 8)
        {
            var include = table.GetIncludeStr(padleft);
            var includeMany = table.GetIncludeManyStr();
            var res = $"{freeSqlObjString}.Select<{table.CsName}>(){include}{includeMany}";
            return res;
        }

        /// <summary>
        /// 获取查询语句 同步
        /// </summary>
        /// <param name="table">CodeFirstTable对象</param>
        /// <param name="freeSqlObjString">IFreeSql对象 (变量)字符串</param>
        /// <param name="padleft"></param>
        /// <returns></returns>
        public static string GetFreeSqlSelectStr(this TableInfo table, string freeSqlObjString, int padleft = 8)
        {
            return $"{table.MakeFreeSqlSelectStr(freeSqlObjString, padleft)}.ToList()";
        }

        /// <summary>
        /// 获取查询语句 异步
        /// </summary>
        /// <param name="table">CodeFirstTable对象</param>
        /// <param name="freeSqlObjString">IFreeSql对象 (变量)字符串</param>
        /// <param name="padleft"></param>
        /// <returns></returns>
        public static string GetFreeSqlAsyncSelectStr(this TableInfo table, string freeSqlObjString, int padleft = 8)
        {
            return $"{table.MakeFreeSqlSelectStr(freeSqlObjString, padleft)}.ToListAsync()";
        }

        /// <summary>
        /// 生成FreeSql分页查询语法
        /// </summary>
        /// <param name="table"></param>
        /// <param name="freeSqlObjString"></param>
        /// <param name="pageObjString"></param>
        /// <param name="padleft"></param>
        /// <returns></returns>
        public static string MakeFreeSqlPageSelectStr(this TableInfo table, string freeSqlObjString, string pageObjString = null, int padleft = 8)
        {
            var pageSize = "PageSize";
            var pageNumber = "PageNumber";
            if (!string.IsNullOrWhiteSpace(pageObjString))
            {
                pageSize = $"{pageObjString}.{pageSize}";
                pageNumber = $"{pageObjString}.{pageNumber}";
            }
            return $"{table.MakeFreeSqlSelectStr(freeSqlObjString, padleft)}" +
                   $".Count(var out total)" +
                   $".Page({pageNumber},{pageSize})";
        }
        /// <summary>
        /// 获取FreeSql查询语句 同步
        /// </summary>
        /// <param name="table"></param>
        /// <param name="freeSqlObjString"></param>
        /// <param name="pageObjString"></param>
        /// <returns></returns>
        public static string GetFreeSqlPageSelectStr(this TableInfo table, string freeSqlObjString, string pageObjString = null)
        {
            return $"{MakeFreeSqlPageSelectStr(table, freeSqlObjString, pageObjString)}.ToList()";
        }

        /// <summary>
        /// 获取FreeSql查询语句 异步
        /// </summary>
        /// <param name="table"></param>
        /// <param name="freeSqlObjString"></param>
        /// <param name="pageObjString"></param>
        /// <returns></returns>
        public static string GetFreeSqlPageAsyncSelectStr(this TableInfo table, string freeSqlObjString, string pageObjString = null)
        {
            return $"{MakeFreeSqlPageSelectStr(table, freeSqlObjString, pageObjString)}.ToListAsync()";
        }

        /// <summary>
        /// WhereIf
        /// </summary>
        /// <param name="table"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static string GetWhereIfByColumns(this TableInfo table, string dto, int padLeftWidth = 8,
            params string[] ignores
            )
        {
            var where = new StringBuilder();
            if (ignores.Length == 0)
            {
                ignores = new string[]
               {
               "Id", "IsDeleted", "Enabled","UpdateBy", "UpdateDate", "CreateBy", "CreateDate","Children","Parent","ParentId","NodePath"
               };
            }
            var lp = PadLeft(padLeftWidth);
            foreach (var column in table.Columns)
            {
                if (ignores.Contains(column.Value.CsName))
                {
                    continue;
                }
                if (column.Value.CsType == typeof(string))
                {
                    where.AppendLine($"{lp}.WhereIf(!string.IsNullOrWhiteSpace({dto}.{column.Key}),x=>x.{column.Key}.Contains({dto}.{column.Key}))");
                }
                else if (column.Value.CsType.IsNullableType())
                {
                    where.AppendLine($"{lp}.WhereIf({dto}.{column.Key} != null ,x=>x.{column.Key} =={dto}.{column.Key})");
                }
            }
            var res = where.ToString();
            if (!string.IsNullOrWhiteSpace(res))
            {
                res = res.NewLine();
            }
            return res;
        }
    }
}