using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Infrastructure;
using FreeSqlBuilder.Infrastructure.Extensions;
using FreeSqlBuilder.Modals.Base;
using FreeSqlBuilder.Modals.Dtos;
using FreeSqlBuilder.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FreeSqlBuilder.Services
{
    /// <summary>
    /// 构建器服务
    /// </summary>
    public class BuilderService : ServiceBase, IBuilderService
    {
        private readonly IBuilderRepository _builderRep;
        private readonly ITemplateRepository _templateRepository;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public BuilderService(IServiceProvider service, ILogger<BuilderService> logger) : base(service, logger)
        {
            _builderRep = service.GetService<IBuilderRepository>();
            _templateRepository = service.GetService<ITemplateRepository>();
        }
        /// <summary>
        /// 获取构建器分页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<PageView<BuilderOptions>> GetBuilderPage(BuilderPageParam page)
        {
            return await _builderRep
                .Select
                .Include(x => x.Template)
                .WhereIf(page.BuilderType != null, x => x.Type == page.BuilderType)
                .WhereIf(!string.IsNullOrWhiteSpace(page.Keyword), x => x.Name.Contains(page.Keyword))
                .GetPage(page);
        }
        /// <summary>
        /// 通过Id获取构建器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BuilderOptions> GetBuilder(long id)
        {
            var builders = await _builderRep.Select.Include(x => x.Template)
                .LeftJoin(x => x.DefaultProject.Id == x.DefaultProjectId)
                .Include(x => x.DefaultProject.ProjectInfo)
                .Include(x => x.DefaultProject.GeneratorModeConfig)
                .Include(x => x.DefaultProject.GeneratorModeConfig.DataSource)
                .Include(x => x.DefaultProject.GeneratorModeConfig.EntitySource)
                .Where(x => x.Id == id).ToOneAsync();
            builders.DefaultProject.ProjectBuilders = new List<ProjectBuilder>()
            {
                new ProjectBuilder()
                {
                    BuilderId = builders.Id,
                    Builder= builders,
                    Project = builders.DefaultProject,
                    ProjectId = builders.DefaultProjectId
                }
            };
            return builders;
        }

        /// <summary>
        /// 新增构建器
        /// </summary>
        /// <param name="builderOptions"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public async Task<BuilderOptions> AddBuilder(BuilderOptions builderOptions, bool autoSave = false)
        {
            var template = await _templateRepository.GetAsync(builderOptions.TemplateId);
            if (template != null)
            {
                builderOptions = await _builderRep.InsertAsync(builderOptions);
            }
            else
                throw new Exception("新增失败!找不到相关模板");
            if (autoSave) UnitOfWork.Commit();
            return builderOptions;
        }
        /// <summary>
        /// 更新构建器
        /// </summary>
        /// <param name="builderOptions"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public async Task<BuilderOptions> UpdateBuilder(BuilderOptions builderOptions, bool autoSave = false)
        {
            var template = await _templateRepository.GetAsync(builderOptions.TemplateId);
            if (template != null)
                await _builderRep.UpdateAsync(builderOptions);
            else
                throw new Exception("更新失败!找不到相关模板");
            if (autoSave) UnitOfWork.Commit();
            return builderOptions;
        }
        /// <summary>
        /// 选择项目
        /// </summary>
        /// <param name="builderId"></param>
        /// <param name="projectId"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public async Task<bool> PickerProject(long builderId, long projectId, bool autoSave = false)
        {
            await _builderRep.Orm.Insert(new ProjectBuilder
            {
                BuilderId = builderId,
                ProjectId = projectId
            }).ExecuteAffrowsAsync();
            if (autoSave) UnitOfWork.Commit();
            return true;
        }

        /// <summary>
        /// 删除某个构建器
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public async Task<bool> DelBuilder(long id, bool autoSave = false)
        {
            await _builderRep.DeleteAsync(x => x.Id == id);
            if (autoSave)
                UnitOfWork.Commit();
            return true;
        }
    }
}