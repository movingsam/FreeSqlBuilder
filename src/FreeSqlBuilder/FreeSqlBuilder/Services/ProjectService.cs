using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Modals.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace FreeSqlBuilder.Services
{
    /// <summary>
    /// 项目服务
    /// </summary>
    public class ProjectService : IProjectService
    {
        private readonly IFreeSql<FsBuilder> _freeSql;
        private readonly IWebHostEnvironment _webHostEnv;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public ProjectService(IServiceProvider service)
        {
            _freeSql = service.GetRequiredService<IFreeSql<FsBuilder>>();
            _webHostEnv = service.GetService<IWebHostEnvironment>();
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PageView<Project>> GetPage(PageRequest request)
        {
            var res = await _freeSql.GetRepository<Project>().Select
                .Include(x => x.ProjectInfo)
                .Include(x => x.GeneratorModeConfig)
                .Include(x => x.GeneratorModeConfig.DataSource)
                .IncludeMany(x => x.Builders.Where(b => b.ProjectId == x.Id), then => then.Where(t => t.Type == BuilderType.Builder).Include(t => t.Template))
                .IncludeMany(x => x.GlobalBuilders.Where(b => b.ProjectId == x.Id), then => then.Where(t => t.Type == BuilderType.GlobalBuilder).Include(t => t.Template))
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
        public async Task<Project> Add(Project project)
        {
            var res = await _freeSql.GetRepository<Project>().InsertAsync(project);
            return res;
        }
        /// <summary>
        /// 新增项目基础信息 Step1
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<Project> AddProjectInfoAsync(ProjectInfo info)
        {
            var res = await _freeSql.Insert(info).ExecuteIdentityAsync();
            info.Id = res;
            var project = await this.Add(new Project
            {
                ProjectInfoId = res
            });
            project.ProjectInfo = info;
            return project;
        }
        /// <summary>
        /// 更新项目基础信息 Step1
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<ProjectInfo> UpdateProjectInfoAsync(ProjectInfo info)
        {
            var res = await _freeSql.Update<ProjectInfo>().SetSource(info).ExecuteAffrowsAsync();
            return info;
        }
        /// <summary>
        /// 新增配置 Step2
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task<GeneratorModeConfig> AddGConfig(GeneratorModeConfig config)
        {

            var res = await _freeSql.GetRepository<GeneratorModeConfig>().InsertAsync(config);
            if (config.GeneratorMode == GeneratorMode.DbFirst)
            {
                CheckConfig(config.DataSource);
                config.DataSource.GeneratorModeConfigId = res.Id;
                var ds = await _freeSql.GetRepository<DataSource>().InsertAsync(config.DataSource);
                await _freeSql.Update<GeneratorModeConfig>().Set(x => x.DataSourceId, ds.Id).Where(x => x.Id == res.Id)
                    .ExecuteAffrowsAsync();
            }
            var project = await this.Get(config.ProjectId);
            project.GeneratorModeConfigId = res.Id;
            await this.Update(project);
            return config;
        }
        /// <summary>
        /// 检测必填项
        /// </summary>
        /// <param name="ds"></param>
        private void CheckConfig(DataSource ds)
        {
            if (string.IsNullOrWhiteSpace((ds.ConnectionString))) throw new Exception("连接数据库字符串不能为空");
            if (string.IsNullOrWhiteSpace(ds.Name)) throw new Exception("数据库名称不能为空");
        }
        /// <summary>
        /// 更新配置 Step2
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task<GeneratorModeConfig> UpdateConfig(GeneratorModeConfig config)
        {
            await _freeSql.GetRepository<GeneratorModeConfig>().UpdateAsync(config);
            if (config.GeneratorMode == GeneratorMode.DbFirst)
            {
                CheckConfig(config.DataSource);
                var ds = await _freeSql.GetRepository<DataSource>().UpdateAsync(config.DataSource);
            }

            return config;
        }
        /// <summary>
        /// 新增数据库配置 Step2-2
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public async Task<DataSource> AddDataSource(DataSource dataSource)
        {
            await _freeSql.Insert(dataSource).ExecuteIdentityAsync();
            return dataSource;
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
            return await _freeSql.Delete<BuilderOptions>().Where(x => x.Id == id).ExecuteAffrowsAsync() > 0;
        }
        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Remove(long id)
        {
            return await _freeSql.GetRepository<Project>().DeleteAsync(x => x.Id == id);
        }
        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public async Task<int> Update(Project project)
        {
            return await _freeSql.GetRepository<Project>().UpdateAsync(project);
        }
        /// <summary>
        /// 查询项目
        /// </summary>
        /// <returns></returns>
        public async Task<Project> Get(long id)
        {
            return await _freeSql.GetRepository<Project>()
                .Select
                .Include(x => x.ProjectInfo)
                .Include(x => x.GeneratorModeConfig)
                .Include(x => x.GeneratorModeConfig.DataSource)
                .IncludeMany(x => x.Builders.Where(b => b.ProjectId == x.Id), then => then.Where(t => t.Type == BuilderType.Builder).Include(t => t.Template))
                .IncludeMany(x => x.GlobalBuilders.Where(b => b.ProjectId == x.Id), then => then.Where(t => t.Type == BuilderType.GlobalBuilder).Include(t => t.Template))
                .Where(x => x.Id == id)
                .ToOneAsync();
        }

        /// <summary>
        /// 模板列表
        /// </summary>
        /// <returns></returns>
        public async Task<PageView<Template>> GetTemplatePageAsync(PageRequest request)
        {
            var res = await _freeSql.GetRepository<Template>().Select
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
        /// #ToDo 数据源新增 #ToDo
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        Task<DataSource> AddDataSource(DataSource dataSource);
        /// <summary>
        /// 新增配置
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        Task<GeneratorModeConfig> AddGConfig(GeneratorModeConfig config);
        /// <summary>
        /// 更新配置
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        Task<GeneratorModeConfig> UpdateConfig(GeneratorModeConfig config);
        /// <summary>
        /// 新增项目详情
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        Task<Project> AddProjectInfoAsync(ProjectInfo info);
        /// <summary>
        /// 更新项目详情
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        Task<ProjectInfo> UpdateProjectInfoAsync(ProjectInfo info);
        /// <summary>
        /// 新增构建器信息
        /// </summary>
        /// <param name="builderOptions"></param>
        /// <returns></returns>
        Task<BuilderOptions> AddBuilder(BuilderOptions builderOptions);
        /// <summary>
        /// 更新构建器信息
        /// </summary>
        /// <param name="builderOptions"></param>
        /// <returns></returns>
        Task<BuilderOptions> UpdateBuilder(BuilderOptions builderOptions);
        /// <summary>
        /// 删除构建器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DelBuilder(long id);
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
        Task<Template> AddTemplate(Template template);
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
