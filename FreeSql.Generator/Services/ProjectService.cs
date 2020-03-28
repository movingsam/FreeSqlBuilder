using BlazorFreeSqlGenerator.Modals.Base;
using FreeSql;
using FreeSql.Generator.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AngularGenerator.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IFreeSql _freesql;
        private readonly IHostingEnvironment _webhostEnv;
        public ProjectService(IServiceProvider service)
        {
            _freesql = service.GetRequiredService<IFreeSql>();
            _webhostEnv = service.GetService<IHostingEnvironment>();
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PageView<Project>> GetPage(PageRequest request)
        {
            var res = await _freesql.GetRepository<Project>().Select
                .Include(x => x.DataSource)
                .IncludeMany(x => x.Builders.Where(b => b.ProjectId == x.Id), then => then.Where(t => t.Type == BuilderType.Builder))
                .IncludeMany(x => x.GlobalBuilders.Where(b => b.ProjectId == x.Id), then => then.Where(t => t.Type == BuilderType.GlobalBuilder))
                .WhereIf(!string.IsNullOrWhiteSpace(request.Keyword), x => x.ProjectName.Contains(request.Keyword))
                .Count(out var total)
                .Page(request.PageNumber, request.PageSize)
                .ToListAsync();
            request.Total = total;
            return new PageView<Project>(res, request);
        }
        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public async Task<Project> Add(Project project)
        {
            var res = await _freesql.GetRepository<Project>().InsertAsync(project);
            project.DataSource.ProjectId = res.Id;
            await _freesql.GetRepository<DataSource>().InsertAsync(project.DataSource);
            project.Builders.ForEach(b => b.ProjectId = res.Id);
            await _freesql.GetRepository<BuilderOptions>().InsertAsync(project.Builders);
            project.GlobalBuilders.ForEach(b => b.ProjectId = res.Id);
            await _freesql.GetRepository<BuilderOptions>().InsertAsync(project.GlobalBuilders);
            return res;
        }
        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Remove(long id)
        {
            return await _freesql.GetRepository<Project>().DeleteAsync(x => x.Id == id);
        }
        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public async Task<int> Update(Project project)
        {
            return await _freesql.GetRepository<Project>().UpdateAsync(project);
        }
        /// <summary>
        /// 查询项目
        /// </summary>
        /// <returns></returns>
        public async Task<Project> Get(long id)
        {
            return await _freesql.GetRepository<Project>().Select
                .IncludeMany(x => x.Builders)
                .IncludeMany(x => x.GlobalBuilders)
                .Include(x => x.DataSource)
                .Where(x => x.Id == id)
                .ToOneAsync();
        }

        /// <summary>
        /// 模板列表
        /// </summary>
        /// <returns></returns>
        public async Task<PageView<Template>> GetTemplatePageAsync(PageRequest request)
        {
            var res = await _freesql.GetRepository<Template>().Select
                 .IncludeMany(x => x.BuilderOptions)
                 .Count(out var total)
                 .Page(request.PageNumber, request.PageSize)
                 .ToListAsync();
            request.Total = total;
            return new PageView<Template>(res, request);
        }
        /// <summary>
        /// 新增模板
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public async Task<Template> AddTempalte(Template template)
        {
            _freesql.CodeFirst.SyncStructure<Template>();
            var path = _webhostEnv.ContentRootPath;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            File.WriteAllText(template.TemplatePath, template.TemplateContent);
            var res = await _freesql.GetRepository<Template>()
                .InsertAsync(template);
            return res;
        }
        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveTemplate(long id)
        {
            var res = await _freesql.GetRepository<Template>().DeleteAsync(x => x.Id == id);
            return res > 0;
        }
        /// <summary>
        /// 更新模板
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTemplate(Template template)
        {
            var res = await _freesql.GetRepository<Template>().UpdateAsync(template);
            return res > 0;
        }
        /// <summary>
        /// 获取某个模板内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Template> GetTemplateAsync(long id)
        {
            var res = await _freesql.GetRepository<Template>()
                .Select
                .Include(x => x.BuilderOptions)
                .Where(x => x.Id == id)
                .ToOneAsync();
            return res;
        }
    }

    /// <summary>
    /// 
    /// </summary>
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
        /// <returns></returns>
        Task<Project> Add(Project project);
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
        /// <returns></returns>
        Task<int> Update(Project project);
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
        /// <returns></returns>
        Task<Template> AddTempalte(Template template);
        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> RemoveTemplate(long id);
        /// <summary>
        /// 更新模板
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        Task<bool> UpdateTemplate(Template template);
        /// <summary>
        /// 获取模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Template> GetTemplateAsync(long id);
    }
}
