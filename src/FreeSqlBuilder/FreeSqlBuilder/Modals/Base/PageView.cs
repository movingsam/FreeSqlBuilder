using System.Collections.Generic;

namespace FreeSqlBuilder.Modals.Base
{
    /// <summary>
    /// 分页模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageView<T> : Page where T : class
    {
        /// <summary>
        /// 分页空构造
        /// </summary>
        public PageView()
        {
        }
        /// <summary>
        /// 带参构造
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="page"></param>
        public PageView(IEnumerable<T> datas, IPage page)
        {
            Datas = datas;
            PageNumber = page.PageNumber;
            PageSize = page.PageSize;
            SortFields = page.SortFields;
            Total = page.Total;
        }
        /// <summary>
        /// 数据 
        /// </summary>
        public IEnumerable<T> Datas { get; set; }
    }
}
