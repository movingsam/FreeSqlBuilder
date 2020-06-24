using FreeSqlBuilder.Core.Entities;
using Microsoft.Extensions.Logging;

namespace FreeSqlBuilder.Infrastructure
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepositoryBase<T, TKey> : FreeSql.IBaseRepository<T, TKey>
    where T : class, IKey<TKey>
    {
        /// <summary>
        /// 日志
        /// </summary>
        ILogger Logger { get; }
    }
}