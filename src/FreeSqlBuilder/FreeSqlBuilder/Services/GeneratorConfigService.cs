using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreeSql;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Infrastructure;
using FreeSqlBuilder.Infrastructure.Extensions;
using FreeSqlBuilder.Modals.Base;
using FreeSqlBuilder.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace FreeSqlBuilder.Services
{
    /// <summary>
    /// 配置服务
    /// </summary>
    public class GeneratorConfigService : ServiceBase, IGeneratorConfigService
    {
        private readonly IConfigRepository _configRepository;
        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="service"></param>
        public GeneratorConfigService(ILogger<GeneratorConfigService> logger, IServiceProvider service) : base(service, logger)
        {
            _configRepository = service.GetService<IConfigRepository>();
        }

        /// <summary>
        /// 获取配置分页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<PageView<GeneratorModeConfig>> GetConfigPage(IPage page)
        {
            var res = _configRepository
                .Select
                .Include(x => x.DataSource)
                .IncludeMany(x => x.Projects);
            return await Mapper.GetPage<GeneratorModeConfig>(page, res);
        }

        /// <summary>
        /// 新增配置
        /// </summary>
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
            if (autoSave) UnitOfWork.Commit();
            return res;
        }

        /// <summary>
        /// 更新配置 Step2
        /// </summary>
        /// <param name="config"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public async Task<GeneratorModeConfig> UpdateConfig(GeneratorModeConfig config, bool autoSave = false)
        {
            await _configRepository.UpdateAsync(config);
            if (config.GeneratorMode == GeneratorMode.DbFirst)
            {
                CheckConfig(config.DataSourceId);
            }
            if (autoSave) UnitOfWork.Commit();
            return config;
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
                .Insert(dataSource)
                .ExecuteIdentityAsync();
            dataSource.Id = id;
            if (autoSave)
            {
                UnitOfWork.Commit();
            }
            return dataSource;
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<PageView<DataSource>> GetDataSource(IPage page)
        {
            var res = _configRepository.Orm.Select<DataSource>();
            return await Mapper.GetPage<DataSource>(page, res);
        }

        /// <summary>
        /// 数据源更新
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public async Task<DataSource> UpdateDataSource(DataSource ds, bool autoSave = false)
        {
            await _configRepository.Orm.Update<DataSource>(ds).Where(x => x.Id == ds.Id).ExecuteAffrowsAsync();
            if (autoSave) UnitOfWork.Commit();
            return ds;
        }
        /// <summary>
        /// 删除配置项
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public async Task<bool> DeleteConfig(long id, bool autoSave = false)
        {
            var res = await _configRepository.DeleteAsync(id);
            if (autoSave) UnitOfWork.Commit();
            return res > 0;
        }
        /// <summary>
        /// 删除数据源
        /// </summary>
        /// <param name="id"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public async Task<bool> DeleteDataSource(long id, bool b)
        {
            var res = await _configRepository.Orm.Delete<DataSource>().Where(x => x.Id == id).ExecuteAffrowsAsync();
            if (b) UnitOfWork.Commit();
            return res > 0;
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
    }
}