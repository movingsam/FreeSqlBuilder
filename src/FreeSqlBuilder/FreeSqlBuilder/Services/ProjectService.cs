﻿using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Infrastructure;
using FreeSqlBuilder.Infrastructure.Extensions;
using FreeSqlBuilder.Modals.Base;
using FreeSqlBuilder.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                    x => x.ProjectInfo.NameSpace.Contains(request.Keyword));
            return await query.GetPage(request);
        }

        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="project"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public async Task<Project> Add(Project project, bool autoSave = false)
        {
            await CheckProject(project);
            var res = await _projectRep.InsertAsync(project);
            if (project.ProjectInfo != null)
            {
                var projectInfoId = _projectRep.Orm.Insert<ProjectInfo>().AppendData(project.ProjectInfo)
                    .ExecuteIdentity();
                await _projectRep.UpdateDiy.Set(x => x.ProjectInfoId, projectInfoId).Where(x => x.Id == res.Id)
                    .ExecuteAffrowsAsync();
            }
            if (project.ProjectBuilders?.Count > 0)
            {
                project.ProjectBuilders.ToList().ForEach(pb => pb.ProjectId = res.Id);
                _projectRep.Orm.Insert<ProjectBuilder>().AppendData(project.ProjectBuilders).ExecuteAffrows();
            }
            if (autoSave)
            {
                this.UnitOfWork.Commit();
            }
            return res;
        }
        /// <summary>
        /// 项目检测
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public async Task CheckProject(Project project)
        {
            if (project.BuildersId.Count > 0)
            {
                var builders = await _projectRep.Orm.Select<BuilderOptions>().Where(x => project.BuildersId.Contains(x.Id)).ToListAsync();
                switch (project.GeneratorModeConfig.GeneratorMode)
                {
                    case GeneratorMode.DbFirst:
                        if (builders.Any(b => b.TemplateType != TemplateType.DbFirst))
                        {
                            throw new Exception("DbFirst不能使用非DbFirst的配置");
                        }
                        break;
                    case GeneratorMode.CodeFirst:
                        if (builders.Any(b => b.TemplateType != TemplateType.CodeFirst))
                        {
                            throw new Exception("DbFirst不能使用非CodeFirst的配置");
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
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
        public async Task<int> Remove(long id, bool autoSave = false)
        {
            var res = await _projectRep.DeleteAsync(x => x.Id == id);
            if (autoSave) UnitOfWork.Commit();
            return res;
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="project"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public async Task<int> Update(Project project, bool autoSave = false)
        {
            project.ProjectInfo.Id = project.ProjectInfoId;
            var res = await _projectRep.UpdateAsync(project);
            if (project.ProjectInfo != null)
            {
                await _projectRep.Orm.Update<ProjectInfo>().SetSource(project.ProjectInfo).ExecuteAffrowsAsync();
            }
            //比较两次Builder之间的区别
            if (project.ProjectBuilders.Count > 0)
            {
                var change = BuilderChange(project);
                if (change.insert.Count > 0)
                {
                    var insert = change.insert.Select(s => new ProjectBuilder() { BuilderId = s, ProjectId = project.Id });
                    _projectRep.Orm.Insert<ProjectBuilder>().AppendData(insert).ExecuteAffrows();
                }
                if (change.delete.Count > 0)
                {
                    _projectRep.Orm.Delete<ProjectBuilder>().Where(x => x.ProjectId == project.Id && change.delete.Contains(x.BuilderId)).ExecuteAffrows();
                }
            }
            if (autoSave) UnitOfWork.Commit();
            return res;
        }
        /// <summary>
        /// 计算新旧差别并计算出新增删除的中间表
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        private (List<long> insert, List<long> delete) BuilderChange(Project project)
        {
            var oldBuilderList = _projectRep.Orm.Select<ProjectBuilder>()
                .Where(x => x.ProjectId == project.Id)
                .ToList(x => x.BuilderId);
            var newBuilderList = project.ProjectBuilders.Select(s => s.BuilderId).ToList();
            var totalBuilderId = newBuilderList;
            if (project.GlobalBuildersId?.Count > 0)
            {
                totalBuilderId = project.BuildersId?.Union(project.GlobalBuildersId).ToList();
            }
            if (totalBuilderId?.Count > 0)
            {
                var intersect = totalBuilderId?.Intersect(oldBuilderList).ToList();
                var insert = newBuilderList.Except(intersect).ToList();
                var delete = oldBuilderList.Except(intersect).ToList();
                return (insert, delete);
            }
            return (totalBuilderId, oldBuilderList);
        }



        /// <summary>
        /// 查询项目
        /// </summary>
        /// <returns></returns>
        public async Task<Project> Get(long id)
        {
            var res = await _projectRep
                .Select
                .Include(x => x.ProjectInfo)
                .Include(x => x.GeneratorModeConfig)
                .Include(x => x.GeneratorModeConfig.DataSource)
                .Include(x => x.GeneratorModeConfig.EntitySource)
                .IncludeMany(x => x.ProjectBuilders, then => then.Include(x => x.Builder).Include(t => t.Builder.Template))
                .Where(x => x.Id == id)
                .ToOneAsync();
            res.GeneratorModeConfig.Projects = new List<Project> { res };
            return res;
        }


    }

}
