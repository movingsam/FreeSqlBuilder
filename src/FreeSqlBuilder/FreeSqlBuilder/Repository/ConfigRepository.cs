using System;
using System.Collections.Generic;
using System.Text;
using FreeSql;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Infrastructure;
using Microsoft.Extensions.Logging;

namespace FreeSqlBuilder.Repository
{
    public class ConfigRepository : RepositoryBase<GeneratorModeConfig, long>, IConfigRepository
    {
        public ConfigRepository(IFreeSql<FsBuilder> fsql, ILogger<ConfigRepository> logger, IUnitOfWork uow) : base(fsql, logger, uow)
        {
        }
    }
}
