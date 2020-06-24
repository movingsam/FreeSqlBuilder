using FreeSql;
using Microsoft.Extensions.Logging;

namespace FreeSqlBuilder.Infrastructure
{
    /// <summary>
    /// 服务基类
    /// </summary>
    public abstract class ServiceBase : IServiceBase
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }
        /// <summary>
        /// 工作单元
        /// </summary>
        public ILogger Logger { get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="logger"></param>
        protected ServiceBase(IUnitOfWork uow, ILogger logger)
        {
            UnitOfWork = uow;
            Logger = logger;
        }
    }
}