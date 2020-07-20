using FreeSql.DataAnnotations;

namespace FreeSqlBuilder.Core.Entities
{
    /// <summary>
    /// 项目基本信息
    /// </summary>
    [Index("uk_namespace_index", "NameSpace,Author", true)]
    public class ProjectInfo : IKey<long>, IOutPut
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public long Id { get; set; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace { get; set; }
        /// <summary>
        /// 根目录
        /// </summary>
        public string RootPath { get; set; }
        /// <summary>
        /// 输出路径 默认Output
        /// </summary>
        public string OutPutPath => NameSpace;
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
    }
}
