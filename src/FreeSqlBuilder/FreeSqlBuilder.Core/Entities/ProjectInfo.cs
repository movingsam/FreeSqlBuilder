using FreeSql.DataAnnotations;

namespace FreeSqlBuilder.Core.Entities
{
    /// <summary>
    /// 项目基本信息
    /// </summary>
    public class ProjectInfo : IKey<long>, IOutPut
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public long Id { get; set; }
        /// <summary>
        /// 项目名——命名空间
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 根目录
        /// </summary>
        public string RootPath { get; set; }
        /// <summary>
        /// 输出路径 默认Output
        /// </summary>
        public string OutPutPath { get; set; } = "Output";
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
    }
}
