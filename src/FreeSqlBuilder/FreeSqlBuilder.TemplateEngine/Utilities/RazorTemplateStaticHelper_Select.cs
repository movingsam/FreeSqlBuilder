using System;
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
        /// <returns></returns>
        public static string MakeFreeSqlSelectStr(this TableInfo table, string freeSqlObjString)
        {
            var include = table.GetIncludeStr();
            var includeMany = table.GetIncludeManyStr();
            var res = $"{freeSqlObjString}.Select<{table.CsName}>(){include}{includeMany}";
            return res;
        }

        /// <summary>
        /// 获取查询语句 同步
        /// </summary>
        /// <param name="table">CodeFirstTable对象</param>
        /// <param name="freeSqlObjString">IFreeSql对象 (变量)字符串</param>
        /// <returns></returns>
        public static string GetFreeSqlSelectStr(this TableInfo table, string freeSqlObjString)
        {
            return $"{table.MakeFreeSqlSelectStr(freeSqlObjString)}.ToList()";
        }

        /// <summary>
        /// 获取查询语句 异步
        /// </summary>
        /// <param name="table">CodeFirstTable对象</param>
        /// <param name="freeSqlObjString">IFreeSql对象 (变量)字符串</param>
        /// <returns></returns>
        public static string GetFreeSqlAsyncSelectStr(this TableInfo table, string freeSqlObjString)
        {
            return $"{table.MakeFreeSqlSelectStr(freeSqlObjString)}.ToListAsync()";
        }

        /// <summary>
        /// 生成FreeSql分页查询语法
        /// </summary>
        /// <param name="table"></param>
        /// <param name="freeSqlObjString"></param>
        /// <param name="pageObjString"></param>
        /// <returns></returns>
        public static string MakeFreeSqlPageSelectStr(this TableInfo table, string freeSqlObjString, string pageObjString = null)
        {
            var pageSize = "PageSize";
            var pageNumber = "PageNumber";
            if (!string.IsNullOrWhiteSpace(pageObjString))
            {
                pageSize = $"{pageObjString}.{pageSize}";
                pageNumber = $"{pageObjString}.{pageNumber}";
            }
            return $"{table.MakeFreeSqlSelectStr(freeSqlObjString)}" +
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
        public static string GetWhereIfByColumns(this TableInfo table, string dto)
        {
            var where = new StringBuilder();
            foreach (var column in table.Columns)
            {
                if (column.Value.CsType == typeof(string))
                {
                    where.AppendLine(
                        $".WhereIf(!string.IsNullOrWhiteSpace({dto}.{column.Key}),x=>x.{column.Key}.Contains({dto}.{column.Key}))");
                }
                else
                {
                    where.AppendLine($".WhereIf({dto}.{column.Key} != null ,x=>x.{column.Key} =={dto}.{column.Key})");
                }
            }
            return where.ToString();
        }
    }
}