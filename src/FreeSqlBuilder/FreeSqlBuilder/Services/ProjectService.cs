using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FreeSql;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Infrastructure;
using FreeSqlBuilder.Modals.Base;
using FreeSqlBuilder.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FreeSqlBuilder.Services
{
    /// <summary>
    /// 项目服务
    /// </summary>
    public class ProjectService : ServiceBase, IProjectService
    {
        private readonly IWebHostEnvironment _webHostEnv;
        private readonly IProjectRepository _projectRep;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public ProjectService(IServiceProvider service) : base(service.GetService<IUnitOfWork>(), service.GetService<ILogger<ProjectService>>())
        {
            _projectRep = service.GetService<IProjectRepository>();
            _webHostEnv = service.GetService<IWebHostEnvironment>();
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PageView<Project>> GetPage(PageRequest request)
        {
            var res = await _projectRep.Select
                .Include(x => x.ProjectInfo)
                .Include(x => x.GeneratorModeConfig)
                .Include(x => x.GeneratorModeConfig.DataSource)
                .IncludeMany(x => x.ProjectBuilders, then => then.Include(t => t.Builder).Include(t => t.Builder.Template))
                .WhereIf(!string.IsNullOrWhiteSpace(request.Keyword), x => x.ProjectInfo.ProjectName.Contains(request.Keyword))
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
        public async Task<Project> Add(Project project, bool autoSave = false)
        {
            var res = await _projectRep.InsertAsync(project);
            if (autoSave)
            {
                this.UnitOfWork.Commit();
            }
            return res;
        }
        /// <summary>
        /// 新增项目基础信息 Step1
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<Project> AddProjectInfoAsync(ProjectInfo info, bool autoSave = false)
        {
            var res = _projectRep.Orm.Insert(info).ExecuteIdentity();
            info.Id = res;
            var project = await this.Add(new Project
            {
                ProjectInfoId = res
            });
            project.ProjectInfo = info;
            if (autoSave)
            {
                UnitOfWork.Commit();
            }
            return project;
        }
        /// <summary>
        /// 更新项目基础信息 Step1
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Task<ProjectInfo> UpdateProjectInfoAsync(ProjectInfo info)
        {
            var res = _projectRep.Orm.Update<ProjectInfo>(info).ExecuteUpdated().FirstOrDefault();
            return Task.FromResult(res);
        }


        /// <summary>
        /// 新增构建器
        /// </summary>
        /// <param name="builderOptions"></param>
        /// <returns></returns>
        public async Task<BuilderOptions> AddBuilder(BuilderOptions builderOptions)
        {
            var template = await GetTemplateAsync(builderOptions.TemplateId);
            if (template != null)
            {
                builderOptions.Id = await _freeSql.Insert(builderOptions).ExecuteIdentityAsync();
            }
            else
                throw new Exception("新增失败!找不到相关模板");
            return builderOptions;
        }
        /// <summary>
        /// 更新构建器
        /// </summary>
        /// <param name="builderOptions"></param>
        /// <returns></returns>
        public async Task<BuilderOptions> UpdateBuilder(BuilderOptions builderOptions)
        {
            var template = await GetTemplateAsync(builderOptions.TemplateId);
            if (template != null)
                await _freeSql.Update<BuilderOptions>().SetSource(builderOptions).ExecuteAffrowsAsync();
            else
                throw new Exception("更新失败!找不到相关模板");
            return builderOptions;
        }
        /// <summary>
        /// 删除某个构建器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DelBuilder(long id)
        {
            await _projectRep.DeleteAsync(id);
            UnitOfWork.Commit();
            return true;
        }
        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Remove(long id)
        {
            return await _projectRep.DeleteAsync(x => x.Id == id);
        }
        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public async Task<int> Update(Project project)
        {
            return await _projectRep.UpdateAsync(project);
        }
        /// <summary>
        /// 查询项目
        /// </summary>
        /// <returns></returns>
        public async Task<Project> Get(long id)
        {
            return await _projectRep
                .Select
                .Include(x => x.ProjectInfo)
                .Include(x => x.GeneratorModeConfig)
                .Include(x => x.GeneratorModeConfig.DataSource)
                .IncludeMany(x => x.ProjectBuilders, then => then.Include(x => x.Builder).Include(t => t.Builder.Template))
                .Where(x => x.Id == id)
                .ToOneAsync();
        }

        /// <summary>
        /// 模板列表
        /// </summary>
        /// <returns></returns>
        public async Task<PageView<Template>> GetTemplatePageAsync(PageRequest request)
        {
            var res = await _projectRep.Select
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
        public async Task<Template> AddTemplate(Template template)
        {
            _freeSql.CodeFirst.SyncStructure<Template>();
            var path = _webHostEnv.ContentRootPath;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            File.WriteAllText(template.TemplatePath, template.TemplateContent);
            var res = await _freeSql.GetRepository<Template>()
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
            var res = await _freeSql.GetRepository<Template>().DeleteAsync(x => x.Id == id);
            return res > 0;
        }
        /// <summary>
        /// 更新模板
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTemplate(Template template)
        {
            if (File.Exists(template.TemplatePath))
            {
                File.Delete(template.TemplatePath);
                File.WriteAllText(template.TemplatePath, template.TemplateContent);
            }
            var res = await _freeSql.Update<Template>().SetSource(template).ExecuteAffrowsAsync();
            return res > 0;
        }
        /// <summary>
        /// 获取某个模板内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Template> GetTemplateAsync(long id)
        {
            var res = await _freeSql.GetRepository<Template>()
                .Select
                .Where(x => x.Id == id)
                .ToOneAsync();
            return res;
        }
    }

}
