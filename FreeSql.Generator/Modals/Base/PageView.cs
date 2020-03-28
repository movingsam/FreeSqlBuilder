using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFreeSqlGenerator.Modals.Base
{
    public class PageView<T> : Page where T : class
    {
        public PageView()
        {
        }
        public PageView(IEnumerable<T> datas, IPage page)
        {
            this.Datas = datas;
            this.PageNumber = page.PageNumber;
            this.PageSize = page.PageSize;
            this.SortFields = page.SortFields;
            this.Total = page.Total;
        }
        public IEnumerable<T> Datas { get; set; }
    }
}
