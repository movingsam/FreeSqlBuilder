﻿using FreeSql;
using FreeSqlBuilder.Modals.Base;
using System.Threading.Tasks;

namespace FreeSqlBuilder.Infrastructure.Extensions
{
    /// <summary>
    /// 分页拓展
    /// </summary>
    public static class PageExtensions
    {
        /// <summary>
        /// 获取分页信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static async Task<PageView<T>> GetPage<T>(this ISelect<T> query, IPage page)
            where T : class
        {
            var pgRes = await query
                .Count(out var total)
                .Page(page.PageNumber, page.PageSize)
                .ToListAsync();
            page.Total = total;
            return new PageView<T>(pgRes, page);
        }

        /// <summary>
        /// 获取分页信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Dto"></typeparam>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static async Task<PageView<Dto>> GetPage<T, Dto>(this ISelect<T> query, IPage page)
            where T : class
            where Dto : class
        {
            var pgRes = await query
                .Count(out var total)
                .Page(page.PageNumber, page.PageSize)
                .ToListAsync<Dto>();
            page.Total = total;
            return new PageView<Dto>(pgRes, page);
        }
    }
}