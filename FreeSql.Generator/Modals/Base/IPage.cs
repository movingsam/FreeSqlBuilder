namespace FreeSql.Generator.Modals.Base
{
    /// <summary>
    /// 分页参数
    /// </summary>
    public interface IPage
    {
        /// <summary>
        /// 页码
        /// </summary>
        int PageSize { get; set; }
        /// <summary>
        /// 页号
        /// </summary>
        int PageNumber { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        string SortFields { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        string Keyword { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        long Total { get; set; }
    }
}