using System.Threading.Tasks;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Infrastructure;
using FreeSqlBuilder.Modals.Base;

namespace FreeSqlBuilder.Services
{
    /// <summary>
    /// 构造器服务
    /// </summary>
    public interface IBuilderService : IServiceBase
    {
        /// <summary>
        /// 获取构建器分页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<PageView<BuilderOptions>> GetBuilderPage(IPage page);
        /// <summary>
        /// 新增构建器信息
        /// </summary>
        /// <param name="builderOptions"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task<BuilderOptions> AddBuilder(BuilderOptions builderOptions, bool autoSave = false);

        /// <summary>
        /// 更新构建器信息
        /// </summary>
        /// <param name="builderOptions"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task<BuilderOptions> UpdateBuilder(BuilderOptions builderOptions, bool autoSave = false);

        /// <summary>
        /// 选择项目
        /// </summary>
        /// <param name="builderId"></param>
        /// <param name="projectId"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task<bool> PickerProject(long builderId, long projectId, bool autoSave = false);
        /// <summary>
        /// 删除构建器
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task<bool> DelBuilder(long id, bool autoSave = false);
    }
}