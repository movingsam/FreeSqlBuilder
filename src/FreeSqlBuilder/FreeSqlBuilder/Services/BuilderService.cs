using System;
using System.Threading.Tasks;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Infrastructure;
using FreeSqlBuilder.Infrastructure.Extensions;
using FreeSqlBuilder.Modals.Base;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace FreeSqlBuilder.Repository
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
        }
        /// <summary>
        /// 获取构建器分页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<PageView<BuilderOptions>> GetBuilderPage(IPage page)
        {
            return await _builderRep
                .Select
                .Include(x => x.Template)
                .GetPage(page);
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