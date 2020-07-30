using System.Threading.Tasks;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Modals.Base;
using FreeSqlBuilder.Modals.Dtos;
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
    public class BuilderController : ApiControllerBase
    {
        private IBuilderService BuilderService => HttpContext.RequestServices.GetService<IBuilderService>();
        /// <summary>
        /// 获取构建器
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(BuilderPageParam page)
        {
            return Success(await BuilderService.GetBuilderPage(page));
        }
        /// <summary>
        /// 通过ID获取构建器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Success(await BuilderService.GetBuilder(id));

        }
        /// <summary>
        /// 新增构建器
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BuilderOptions options)
        {
            return Success(await BuilderService.AddBuilder(options, true));
        }
        /// <summary>
        /// 删除构建器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuilder(long id)
        {
            return Success(await BuilderService.DelBuilder(id, true));
        }
        /// <summary>
        /// 更新构建器
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateBuilder([FromBody] BuilderOptions options)
        {
            return Success(await BuilderService.UpdateBuilder(options, true));
        }


    }
}