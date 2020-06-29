using System;
using System.IO;
using System.Threading.Tasks;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Infrastructure;
using FreeSqlBuilder.Infrastructure.Extensions;
using FreeSqlBuilder.Modals.Base;
using FreeSqlBuilder.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace FreeSqlBuilder.Services
{
    /// <summary>
    /// 模板服务
    /// </summary>
    public class TemplateService : ServiceBase, ITemplateService
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly IWebHostEnvironment _webHostEnv;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public TemplateService(IServiceProvider service, ILogger<TemplateService> logger) : base(service, logger)
        {
            _templateRepository = service.GetService<ITemplateRepository>();
            _webHostEnv = service.GetService<IWebHostEnvironment>();

        }

        /// <summary>
        /// 模板列表
        /// </summary>
        /// <returns></returns>
        public async Task<PageView<Template>> GetTemplatePageAsync(PageRequest request)
        {
            var query = _templateRepository.Select
                 .IncludeMany(x => x.BuilderOptions);
            return await Mapper.GetPage<Template>(request, query);
        }

        /// <summary>
        /// 新增模板
        /// </summary>
        /// <param name="template"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public async Task<Template> AddTemplate(Template template, bool autoSave = false)
        {
            var path = _webHostEnv.ContentRootPath;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            File.WriteAllText(template.TemplatePath, template.TemplateContent);
            var res = await _templateRepository
                .InsertAsync(template);
            if (autoSave) UnitOfWork.Commit();
            return res;
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public async Task<bool> RemoveTemplate(long id, bool autoSave = false)
        {
            var res = await _templateRepository.DeleteAsync(x => x.Id == id);
            if (autoSave) UnitOfWork.Commit();
            return res > 0;
        }

        /// <summary>
        /// 更新模板
        /// </summary>
        /// <param name="template"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTemplate(Template template, bool autoSave = false)
        {
            if (File.Exists(template.TemplatePath))
            {
                File.Delete(template.TemplatePath);
                File.WriteAllText(template.TemplatePath, template.TemplateContent);
            }
            var res = await _templateRepository.UpdateAsync(template);
            if (autoSave) UnitOfWork.Commit();
            return res > 0;
        }
        /// <summary>
        /// 获取某个模板内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Template> GetTemplateAsync(long id)
        {
            var res = await _templateRepository
                .Select
                .Where(x => x.Id == id)
                .ToOneAsync();
            return res;
        }
    }
}