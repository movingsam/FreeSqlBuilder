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
    /// 构建器仓储
    /// </summary>
    public class BuilderRepository : RepositoryBase<BuilderOptions, long>, IBuilderRepository
    {
        public BuilderRepository(IFreeSql<FsBuilder> fsql, ILogger logger, IUnitOfWork uow, Expression<Func<BuilderOptions, bool>> filter = null)
            : base(fsql, logger, uow, filter)
        {
        }
    }
}