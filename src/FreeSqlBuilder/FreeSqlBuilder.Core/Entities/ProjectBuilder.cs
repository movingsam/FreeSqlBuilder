namespace FreeSqlBuilder.Core.Entities
{
    /// <summary>
    /// 项目-构建器中间表
    /// </summary>
    public class ProjectBuilder
    {
        public long ProjectId { get; set; }
        public long BuilderId { get; set; }
        public Project Project { get; set; }
        public BuilderOptions Builder { get; set; }
    }
}