using System;
using System.Linq.Expressions;
using FreeSql;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Infrastructure;
using Microsoft.Extensions.Logging;

namespace FreeSqlBuilder.Repository
{
    /// <summary>
    /// 项目仓储
    /// </summary>
    public class ProjectRepository : RepositoryBase<Project, long>, IProjectRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fsql"></param>
        /// <param name="logger"></param>
        /// <param name="uow"></param>
        /// <param name="filter"></param>
        public ProjectRepository(IFreeSql<FsBuilder> fsql, ILogger<ProjectRepository> logger, IUnitOfWork uow, Expression<Func<Project, bool>> filter = null) : base(fsql, logger, uow, filter)
        {
            fsql.CodeFirst.SyncStructure<Project>();
            fsql.CodeFirst.SyncStructure<ProjectInfo>();
        }
    }
}