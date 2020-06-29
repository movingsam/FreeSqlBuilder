using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreeSql;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Infrastructure;
using FreeSqlBuilder.Infrastructure.Extensions;
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
        public ProjectService(IServiceProvider service) : base(service, service.GetService<ILogger<ProjectService>>())
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
            var query = _projectRep.Select
                .Include(x => x.ProjectInfo)
                .Include(x => x.GeneratorModeConfig)
                .Include(x => x.GeneratorModeConfig.DataSource)
                .IncludeMany(x => x.ProjectBuilders,
                    then => then.Include(t => t.Builder).Include(t => t.Builder.Template))
                .WhereIf(!string.IsNullOrWhiteSpace(request.Keyword),
                    x => x.ProjectInfo.ProjectName.Contains(request.Keyword));
            return await Mapper.GetPage<Project>(request, query);
        }

        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="project"></param>
        /// <param name="autoSave"></param>
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
        /// <param name="autoSave"></param>
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
        public async Task<ProjectInfo> UpdateProjectInfoAsync(ProjectInfo info, bool autoSave = false)
        {
            var res = await _projectRep.Orm.Update<ProjectInfo>(info).ExecuteAffrowsAsync();
            if (autoSave) UnitOfWork.Commit();
            return info;
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
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public async Task<int> Update(Project project, bool autoSave = false)
        {
            var res = await _projectRep.UpdateAsync(project);
            if (autoSave) UnitOfWork.Commit();
            return res;
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

       
    }

}
