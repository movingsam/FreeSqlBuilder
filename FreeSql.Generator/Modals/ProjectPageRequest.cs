using BlazorFreeSqlGenerator.Modals.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFreeSqlGenerator.Modals
{
    public class ProjectPageRequest : PageRequest
    {
        /// <summary>
        /// 项目配置关键字 {项目名称}
        /// </summary>
        public string Keyword { get; set; }
    }
}
