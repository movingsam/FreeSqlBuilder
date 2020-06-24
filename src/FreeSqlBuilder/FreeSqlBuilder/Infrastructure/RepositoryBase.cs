using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FreeSql;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Core.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FreeSqlBuilder.Infrastructure
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    public abstract class RepositoryBase<T, TKey> : BaseRepository<T, TKey>, IRepositoryBase<T, TKey>
    where T : class, IKey<TKey>
    {
        /// <summary>
        /// 日志接口
        /// </summary>
        public ILogger Logger { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fsql"></param>
        /// <param name="logger"></param>
        /// <param name="uow"></param>
        /// <param name="filter"></param>
        protected RepositoryBase(IFreeSql<FsBuilder> fsql, ILogger logger, IUnitOfWork uow, Expression<Func<T, bool>> filter = null) : base(fsql, filter)
        {
            Logger = logger;
            UnitOfWork = uow;
        }
    }
}