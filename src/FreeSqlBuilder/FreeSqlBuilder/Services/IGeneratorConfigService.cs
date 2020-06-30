using System.Threading.Tasks;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Infrastructure;
using FreeSqlBuilder.Modals.Base;
using FreeSqlBuilder.Repository;

namespace FreeSqlBuilder.Services
{
    /// <summary>
    /// 配置服务
    /// </summary>
    public interface IGeneratorConfigService : IServiceBase
    {
        /// <summary>
        /// 获取配置分页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<PageView<GeneratorModeConfig>> GetConfigPage(IPage page);

        /// <summary>
        /// 新增配置
        /// </summary>
        /// <param name="config"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task<GeneratorModeConfig> AddGConfig(GeneratorModeConfig config, bool autoSave = false);

        /// <summary>
        /// 更新配置 Step2
        /// </summary>
        /// <param name="config"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task<GeneratorModeConfig> UpdateConfig(GeneratorModeConfig config, bool autoSave = false);
        /// <summary>
        /// 删除配置项
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task<bool> DeleteConfig(long id, bool autoSave = false);


        /// <summary>
        /// 新增数据源
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task<DataSource> AddDataSource(DataSource dataSource, bool autoSave = false);

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<PageView<DataSource>> GetDataSource(IPage page);

        /// <summary>
        /// 数据源更新
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task<DataSource> UpdateDataSource(DataSource ds, bool autoSave = false);
        /// <summary>
        /// 删除数据源
        /// </summary>
        /// <param name="id"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        Task<bool> DeleteDataSource(long id, bool b);



        /// <summary>
        /// 新增实体源
        /// </summary>
        /// <param name="entitySource"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task<EntitySource> AddEntitySource(EntitySource entitySource, bool autoSave = false);

        /// <summary>
        /// 获取实体源
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<PageView<EntitySource>> GetEntitySource(IPage page);

        /// <summary>
        /// 实体源更新
        /// </summary>
        /// <param name="entitySource"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task<EntitySource> UpdateEntitySource(EntitySource entitySource, bool autoSave = false);
        /// <summary>
        /// 删除实体源
        /// </summary>
        /// <param name="id"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        Task<bool> DeleteEntitySource(long id, bool b);
    }
}