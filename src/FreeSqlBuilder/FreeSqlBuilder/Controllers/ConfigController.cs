using System.Threading.Tasks;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Modals.Base;
using FreeSqlBuilder.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Page = FreeSqlBuilder.Modals.Base.Page;

namespace FreeSqlBuilder.Controllers
{
    /// <summary>
    /// 配置项控制器
    /// </summary>
    [Route("api/[controller]")]
    public class ConfigController : ControllerBase
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        private IGeneratorConfigService ConfigService =>
            HttpContext.RequestServices.GetService<IGeneratorConfigService>();

        /// <summary>
        /// 获取配置项分页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPage(PageRequest page)
        {
            return Ok(await ConfigService.GetConfigPage(page));
        }
        /// <summary>
        /// 添加新配置
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddConfig(GeneratorModeConfig config)
        {
            return Ok(await ConfigService.AddGConfig(config, true));
        }
        /// <summary>
        /// 更新配置
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateConfig(GeneratorModeConfig config)
        {
            return Ok(await ConfigService.UpdateConfig(config, true));
        }
        /// <summary>
        /// 删除配置项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfig(long id)
        {
            return Ok(await ConfigService.DeleteConfig(id, true));
        }
        /// <summary>
        /// 新增数据源
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        [HttpPost("DataSource")]
        public async Task<IActionResult> AddDataSource(DataSource ds)
        {
            return Ok(await ConfigService.AddDataSource(ds, true));
        }
        /// <summary>
        /// 更新数据源
        /// </summary>
        /// <returns></returns>
        [HttpPut("DataSource")]
        public async Task<IActionResult> UpdateDataSource(DataSource ds)
        {
            return Ok(await ConfigService.UpdateDataSource(ds, true));
        }
        [HttpDelete("DataSource/{id}")]
        public async Task<IActionResult> DeleteDataqSource(long id)
        {
            return Ok(await ConfigService.DeleteDataSource(id,true))
        }

        /// <summary>
        /// 获取数据源分页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet("DataSource")]
        public async Task<IActionResult> GetDataSource(Page page)
        {
            return Ok(await ConfigService.GetDataSource(page));
        }
    }
}