using FreeSql.Internal.Model;

namespace FreeSqlBuilder.TemplateEngine
{
    /// <summary>
    /// 增删改查执行任务
    /// </summary>
    public class CurdTask
    {
        public CurdTask(IBuilderTask task, TableInfo info)
        {
            this.Task = task;
            this.CurrentTable = info;
        }

        /// <summary>
        /// 任务进程
        /// </summary>
        public IBuilderTask Task { get; set; }
        /// <summary>
        /// 当前执行的表
        /// </summary>
        public TableInfo CurrentTable { get; set; }
    }
}
