using FreeSqlBuilder.Core.Entities;

namespace FreeSqlBuilder.Modals.Base
{
    /// <summary>
    /// 页面请求基类
    /// </summary>
    public class PageRequest : Page
    {
        /// <summary>
        /// 模板类型
        /// </summary>
        public TemplateType? TemplateType { get; set; }
    }
}
