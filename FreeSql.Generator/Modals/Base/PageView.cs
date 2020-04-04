using System.Collections.Generic;

namespace FreeSql.Generator.Modals.Base
{
    public class PageView<T> : Page where T : class
    {
        public PageView()
        {
        }
        public PageView(IEnumerable<T> datas, IPage page)
        {
            Datas = datas;
            PageNumber = page.PageNumber;
            PageSize = page.PageSize;
            SortFields = page.SortFields;
            Total = page.Total;
        }
        public IEnumerable<T> Datas { get; set; }
    }
}
