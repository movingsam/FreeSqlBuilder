using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Infrastructure;
using FreeSqlBuilder.Infrastructure.Extensions;
using FreeSqlBuilder.Modals.Base;
using FreeSqlBuilder.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

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


        #region 配置主体



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
                .Include(x => x.EntitySource)
                .IncludeMany(x => x.Projects, then => then.Include(t => t.ProjectInfo))
                .WhereIf(!string.IsNullOrWhiteSpace(page.Keyword), x => x.Name.Contains(page.Keyword));
            return await res.GetPage(page);
        }
        /// <summary>
        /// 获取配置项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GeneratorModeConfig> GetConfig(long id)
        {
            var res = _configRepository
                .Select
                .Include(x => x.DataSource)
                .Include(x => x.EntitySource)
                .IncludeMany(x => x.Projects, then => then.Include(t => t.ProjectInfo));
            return await res.Where(x => x.Id == id).ToOneAsync();
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
            else
            {
                CheckEntityConfig(config.EntitySourceId);
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
            if (config.GeneratorMode == GeneratorMode.DbFirst)
            {
                CheckConfig(config.DataSourceId);
                config.EntitySourceId = 0;
            }
            else
            {
                CheckEntityConfig(config.EntitySourceId);
                config.DataSourceId = 0;
            }
            await _configRepository.UpdateAsync(config);
            if (autoSave) UnitOfWork.Commit();
            return config;
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


        #endregion

        #region DbFirst数据源相关



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
            if (autoSave) UnitOfWork.Commit();
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
            return await res.GetPage(page);
        }
        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DataSource> GetDataSource(long id)
        {
            var res = _configRepository.Orm.Select<DataSource>();
            return await res.Where(x => x.Id == id).ToOneAsync();
        }

        /// <summary>
        /// 数据源更新
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public async Task<DataSource> UpdateDataSource(DataSource ds, bool autoSave = false)
        {
            await _configRepository.Orm.Update<DataSource>().SetSource(ds).Where(x => x.Id == ds.Id).ExecuteAffrowsAsync();
            if (autoSave) UnitOfWork.Commit();
            return ds;
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
        #endregion

        #region CodeFirst实体源相关
        /// <summary>
        /// 新增实体源
        /// </summary>
        /// <param name="entitySource"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public async Task<EntitySource> AddEntitySource(EntitySource entitySource, bool autoSave = false)
        {
            var id = await _configRepository
                .Orm
                .Insert(entitySource)
                .ExecuteIdentityAsync();
            entitySource.Id = id;
            if (autoSave) UnitOfWork.Commit();
            return entitySource;
        }

        /// <summary>
        /// 获取实体源
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<PageView<EntitySource>> GetEntitySource(IPage page)
        {
            var res = _configRepository.Orm.Select<EntitySource>();
            return await res.GetPage(page);
        }
        /// <summary>
        /// 获取实体源信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EntitySource> GetEntitySource(long id)
        {
            var res = _configRepository.Orm.Select<EntitySource>();
            return await res.Where(x => x.Id == id).ToOneAsync();
        }

        /// <summary>
        /// 实体源更新
        /// </summary>
        /// <param name="es"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public async Task<EntitySource>  UpdateEntitySource(EntitySource es, bool autoSave = false)
        {
            await _configRepository.Orm.Update<EntitySource>().SetSource(es).Where(x => x.Id == es.Id).ExecuteAffrowsAsync();
            if (autoSave) UnitOfWork.Commit();
            return es;
        }

        /// <summary>
        /// 删除实体源
        /// </summary>
        /// <param name="id"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public async Task<bool> DeleteEntitySource(long id, bool b)
        {
            var res = await _configRepository.Orm.Delete<EntitySource>().Where(x => x.Id == id).ExecuteAffrowsAsync();
            if (b) UnitOfWork.Commit();
            return res > 0;
        }
        /// <summary>
        /// 检测必填项
        /// </summary>
        /// <param name="dataSourceId"></param>
        private void CheckEntityConfig(long dataSourceId)
        {
            var ds = _configRepository.Orm.Select<EntitySource>().Where(x => x.Id == dataSourceId).ToOne();
            if (ds == null) throw new Exception("数据源不存在");
        }


        #endregion

    }
}