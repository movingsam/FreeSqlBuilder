using FreeSql.Generator.Modals.Base;

namespace FreeSql.Generator.Modals
{
    public class ProjectPageRequest : PageRequest
    {
        /// <summary>
        /// 项目配置关键字 {项目名称}
        /// </summary>
        public string Keyword { get; set; }
    }
}
