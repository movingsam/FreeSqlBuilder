using System;
using AutoMapper;
using FreeSql;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

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
        /// 映射
        /// </summary>
        public IMapper Mapper { get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        protected ServiceBase(IServiceProvider service, ILogger logger)
        {
            UnitOfWork = service.GetService<IUnitOfWork>();
            Logger = logger;
            Mapper = service.GetService<IMapper>();
        }
    }
}