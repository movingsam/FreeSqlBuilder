using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using FreeSql;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Infrastructure;
using Microsoft.Extensions.Logging;

namespace FreeSqlBuilder.Repository
{
    /// <summary>
    /// 配置仓储
    /// </summary>
    public class ConfigRepository : RepositoryBase<GeneratorModeConfig, long>, IConfigRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fsql"></param>
        /// <param name="logger"></param>
        /// <param name="uow"></param>
        /// <param name="filter"></param>
        public ConfigRepository(IFreeSql<FsBuilder> fsql, ILogger<ConfigRepository> logger, IUnitOfWork uow, Expression<Func<GeneratorModeConfig, bool>> filter = null) : base(fsql, logger, uow, filter)
        {
        }
    }
}
