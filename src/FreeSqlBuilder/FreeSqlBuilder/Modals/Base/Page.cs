namespace FreeSqlBuilder.Modals.Base
{
    /// <summary>
    /// 分页通用基类
    /// </summary>
    public abstract class Page : IPage
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageSize { get; set; } = 10;
        /// <summary>
        /// 页号
        /// </summary>
        public int PageNumber { get; set; } = 1;
        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortFields { get; set; } = "Id ";
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; } = string.Empty;
        /// <summary>
        /// 总条数
        /// </summary>
        public long Total { get; set; } = 0;
    }
}
