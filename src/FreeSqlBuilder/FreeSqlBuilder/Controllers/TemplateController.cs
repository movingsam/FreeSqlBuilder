using System.Threading.Tasks;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Modals.Base;
using FreeSqlBuilder.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FreeSqlBuilder.Controllers
{
    /// <summary>
    /// 模板控制器
    /// </summary>
    [Route("api/[controller]")]
    public class TemplateController : ControllerBase
    {
        private ITemplateService TemplateService => HttpContext.RequestServices.GetService<ITemplateService>();

        /// <summary>
        /// 获取模板分页
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<IActionResult> GetPage(PageRequest page)
        {
            return Ok(await TemplateService.GetTemplatePageAsync(page));
        }
        /// <summary>
        /// 新增模板
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Template template)
        {
            return Ok(await TemplateService.AddTemplate(template));
        }

    }
}