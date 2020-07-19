using System.Threading.Tasks;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Infrastructure;
using FreeSqlBuilder.Modals.Base;

namespace FreeSqlBuilder.Services
{
    /// <summary>
    /// 项目服务
    /// </summary>
    public interface IProjectService : IServiceBase
    {
        /// <summary>
        /// 获取项目分页信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PageView<Project>> GetPage(PageRequest request);

        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="project"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task<Project> Add(Project project, bool autoSave = false);

        /// <summary>
        /// 新增项目详情
        /// </summary>
        /// <param name="info"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task<Project> AddProjectInfoAsync(ProjectInfo info, bool autoSave = false);

        /// <summary>
        /// 更新项目详情
        /// </summary>
        /// <param name="info"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task<ProjectInfo> UpdateProjectInfoAsync(ProjectInfo info, bool autoSave = false);

        /// <summary>
        /// 获取项目详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Project> Get(long id);

        /// <summary>
        /// 删除ID相关项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Remove(long id,bool autoSave=false);

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="project"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task<int> Update(Project project, bool autoSave = false);


    }
}