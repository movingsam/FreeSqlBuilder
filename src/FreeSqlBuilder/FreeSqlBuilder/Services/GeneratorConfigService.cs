using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreeSql;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Infrastructure;
using FreeSqlBuilder.Modals.Base;
using FreeSqlBuilder.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace FreeSqlBuilder.Services
{
    /// <summary>
    /// 配置服务
    /// </summary>
    public class GeneratorConfigService : ServiceBase
    {
        private readonly IConfigRepository _configRepository;
        public GeneratorConfigService(IUnitOfWork uow, ILogger<GeneratorConfigService> logger, IServiceProvider service) : base(uow, logger)
        {
            _configRepository = service.GetService<IConfigRepository>();
        }

        /// <summary>
        /// 新增配置
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="config"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public async Task<GeneratorModeConfig> AddGConfig(GeneratorModeConfig config, bool autoSave = false)
        {
            if (config.GeneratorMode == GeneratorMode.DbFirst)
            {
                CheckConfig(config.DataSourceId);
            }
            var res = await _configRepository.InsertAsync(config);
            if (autoSave)
            {
                UnitOfWork.Commit();
            }
            return config;
        }
        /// <summary>
        /// 获取配置分页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<List<GeneratorModeConfig>> GetConfigPage(IPage page)
        {
            var res = await _configRepository
                            .Select
                            .Include(x => x.DataSource)
                            .IncludeMany(x => x.Projects)
                            .Page(page.PageNumber, page.PageSize)
                            .ToListAsync();
            return res;
        }

        /// <summary>
        /// 检测必填项
        /// </summary>
        /// <param name="dataSourceId"></param>
        private void CheckConfig(long dataSourceId)
        {
            var ds = _configRepository.Orm.Select<DataSource>().Where(x => x.Id == dataSourceId).ToOne();
            if (ds == null) throw new Exception("数据源不存在");
        }

        /// <summary>
        /// 新增数据源
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public async Task<DataSource> AddDataSource(DataSource dataSource, bool autoSave = false)
        {
            var id = await _configRepository
                .Orm
                .Insert<DataSource>(dataSource)
                .ExecuteIdentityAsync();
            dataSource.Id = id;
            if (autoSave)
            {
                UnitOfWork.Commit();
            }
            return dataSource;
        }

        /// <summary>
        /// 更新配置 Step2
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task<GeneratorModeConfig> UpdateConfig(GeneratorModeConfig config)
        {
            await _configRepository.UpdateAsync(config);
            if (config.GeneratorMode == GeneratorMode.DbFirst)
            {
                CheckConfig(config.DataSourceId);
            }
            return config;
        }
    }
}