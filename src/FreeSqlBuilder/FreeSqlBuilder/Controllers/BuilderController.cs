using System.Threading.Tasks;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Modals.Base;
using FreeSqlBuilder.Repository;
using FreeSqlBuilder.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FreeSqlBuilder.Controllers
{
    /// <summary>
    /// 构建器控制器
    /// </summary>
    [Route("api/[controller]")]
    public class BuilderController : ControllerBase
    {
        private IBuilderService BuilderService => HttpContext.RequestServices.GetService<IBuilderService>();
        /// <summary>
        /// 获取构建器
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(Page page)
        {
            return Ok(await BuilderService.GetBuilderPage(page));
        }
        /// <summary>
        /// 新增构建器
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]BuilderOptions options)
        {
            return Ok(await BuilderService.AddBuilder(options, true));
        }
        /// <summary>
        /// 删除构建器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuilder(long id)
        {
            return Ok(await BuilderService.DelBuilder(id, true));
        }
        /// <summary>
        /// 更新构建器
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateBuilder([FromBody]BuilderOptions options)
        {
            return Ok(await BuilderService.UpdateBuilder(options, true));
        }


    }
}