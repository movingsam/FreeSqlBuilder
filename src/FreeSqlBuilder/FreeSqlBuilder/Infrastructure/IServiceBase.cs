using FreeSql;
using Microsoft.Extensions.Logging;

namespace FreeSqlBuilder.Infrastructure
{
    /// <summary>
    /// 服务抽象接口
    /// </summary>
    public interface IServiceBase
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
        /// <summary>
        /// 日志
        /// </summary>
        ILogger Logger { get; }
    }
}