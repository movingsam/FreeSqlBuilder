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
    /// 模板仓储
    /// </summary>
    public class TemplateRepository : RepositoryBase<Template, long>, ITemplateRepository
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="fsql"></param>
        /// <param name="logger"></param>
        /// <param name="uow"></param>
        /// <param name="filter"></param>
        public TemplateRepository(IFreeSql<FsBuilder> fsql, ILogger<TemplateRepository> logger, IUnitOfWork uow, Expression<Func<Template, bool>> filter = null)
            : base(fsql, logger, uow, filter)
        {
        }
    }
}