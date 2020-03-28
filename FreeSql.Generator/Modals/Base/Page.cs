using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFreeSqlGenerator.Modals.Base
{
    public abstract class Page : IPage
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public string SortFields { get; set; } = "Id ";
        public string Keyword { get; set; } = string.Empty;
        public long Total { get; set; } = 0;
    }
}
