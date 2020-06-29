using System.Threading.Tasks;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Modals.Base;

namespace FreeSqlBuilder.Services
{
    public interface IProjectService
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

        ///// <summary>
        /////  
        ///// </summary>
        ///// <param name="dataSource"></param>
        ///// <returns></returns>
        //Task<DataSource> AddDataSource(DataSource dataSource);

        ///// <summary>
        ///// 新增配置
        ///// </summary>
        ///// <param name="config"></param>
        ///// <param name="projectid"></param>
        ///// <returns></returns>
        //Task<GeneratorModeConfig> AddGConfig(GeneratorModeConfig config, long projectid);
        ///// <summary>
        ///// 更新配置
        ///// </summary>
        ///// <param name="config"></param>
        ///// <returns></returns>
        //Task<GeneratorModeConfig> UpdateConfig(GeneratorModeConfig config);
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
        ///// <summary>
        ///// 新增构建器信息
        ///// </summary>
        ///// <param name="builderOptions"></param>
        ///// <returns></returns>
        //Task<BuilderOptions> AddBuilder(BuilderOptions builderOptions);
        ///// <summary>
        ///// 更新构建器信息
        ///// </summary>
        ///// <param name="builderOptions"></param>
        ///// <returns></returns>
        //Task<BuilderOptions> UpdateBuilder(BuilderOptions builderOptions);
        ///// <summary>
        ///// 删除构建器
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //Task<bool> DelBuilder(long id);
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
        Task<int> Remove(long id);

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="project"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task<int> Update(Project project, bool autoSave = false);
        ///// <summary>
        ///// 获取模板
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //Task<PageView<Template>> GetTemplatePageAsync(PageRequest request);
        ///// <summary>
        ///// 新增模板
        ///// </summary>
        ///// <param name="template"></param>
        ///// <returns></returns>
        //Task<Template> AddTemplate(Template template);
        ///// <summary>
        ///// 删除模板
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //Task<bool> RemoveTemplate(long id);
        ///// <summary>
        ///// 更新模板
        ///// </summary>
        ///// <param name="template"></param>
        ///// <returns></returns>
        //Task<bool> UpdateTemplate(Template template);
        ///// <summary>
        ///// 获取模板
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //Task<Template> GetTemplateAsync(long id);
    }
}