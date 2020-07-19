namespace FreeSqlBuilder.Core.Entities
{
    /// <summary>
    /// 项目-构建器中间表
    /// </summary>
    public class ProjectBuilder
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public long ProjectId { get; set; }
        /// <summary>
        /// 构建器Id
        /// </summary>
        public long BuilderId { get; set; }
        /// <summary>
        /// 项目
        /// </summary>
        public Project Project { get; set; }
        /// <summary>
        /// 构建器
        /// </summary>
        public BuilderOptions Builder { get; set; }
    }
}