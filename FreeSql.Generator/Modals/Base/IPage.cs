namespace BlazorFreeSqlGenerator.Modals.Base
{
    public interface IPage
    {
        int PageSize { get; set; }
        /// <summary>
        /// 页号
        /// </summary>
        int PageNumber { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        string SortFields { get; set; }
        string Keyword { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        long Total { get; set; }
    }
}