using System.Threading.Tasks;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Infrastructure;
using FreeSqlBuilder.Modals.Base;

namespace FreeSqlBuilder.Services
{
    /// <summary>
    /// 模板服务
    /// </summary>
    public interface ITemplateService : IServiceBase
    {
        /// <summary>
        /// 获取模板
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PageView<Template>> GetTemplatePageAsync(PageRequest request);

        /// <summary>
        /// 新增模板
        /// </summary>
        /// <param name="template"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task<Template> AddTemplate(Template template, bool autoSave = false);

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task<bool> RemoveTemplate(long id, bool autoSave = false);

        /// <summary>
        /// 更新模板
        /// </summary>
        /// <param name="template"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task<bool> UpdateTemplate(Template template, bool autoSave = false);
        /// <summary>
        /// 获取模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Template> GetTemplateAsync(long id);
    }
}