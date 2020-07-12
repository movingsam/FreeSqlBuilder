using System.Threading.Tasks;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Core.Helper;
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
    public class ConfigController : ApiControllerBase
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
            return Success(await ConfigService.GetConfigPage(page));
        }
        /// <summary>
        /// 获取配置项
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetConfig(long Id)
        {
            return Success(await ConfigService.GetConfig(Id));
        }

        /// <summary>
        /// 添加新配置
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddConfig([FromBody] GeneratorModeConfig config)
        {
            return Success(await ConfigService.AddGConfig(config, true));
        }
        /// <summary>
        /// 更新配置
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateConfig([FromBody] GeneratorModeConfig config)
        {
            return Success(await ConfigService.UpdateConfig(config, true));
        }
        /// <summary>
        /// 删除配置项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfig(long id)
        {
            return Success(await ConfigService.DeleteConfig(id, true));
        }
        /// <summary>
        /// 新增数据源
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        [HttpPost("DataSource")]
        public async Task<IActionResult> AddDataSource([FromBody] DataSource ds)
        {
            return Success(await ConfigService.AddDataSource(ds, true));
        }
        /// <summary>
        /// 更新数据源
        /// </summary>
        /// <returns></returns>
        [HttpPut("DataSource")]
        public async Task<IActionResult> UpdateDataSource([FromBody] DataSource ds)
        {
            return Success(await ConfigService.UpdateDataSource(ds, true));
        }
        [HttpPost("DataSource/Check")]
        public Task<IActionResult> CheckDataSource([FromBody] DataSource ds)
        {
            return Task.FromResult(Success(ds.CheckDataSource()));
        }
        /// <summary>
        /// 数据源删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DataSource/{id}")]
        public async Task<IActionResult> DeleteDataqSource(long id)
        {
            return Success(await ConfigService.DeleteDataSource(id, true));
        }

        /// <summary>
        /// 获取数据源分页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet("DataSource")]
        public async Task<IActionResult> GetDataSource(PageRequest page)
        {
            return Success(await ConfigService.GetDataSource(page));
        }

        /// <summary>
        /// 获取数据源信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("DataSource/{id}")]
        public async Task<IActionResult> GetDataSource(long id)
        {
            return Success(await ConfigService.GetDataSource(id));
        }
        /// <summary>
        /// 获取实体源信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("EntitySource/{id}")]
        public async Task<IActionResult> GetEntitySource(long id)
        {
            return Success(await ConfigService.GetEntitySource(id));
        }

        /// <summary>
        /// 获取实体源列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet("EntitySource")]
        public async Task<IActionResult> GetEntitySourceList(PageRequest page)
        {
            return Success(await ConfigService.GetEntitySource(page));
        }
        /// <summary>
        /// 更新实体源信息
        /// </summary>
        /// <param name="entitySource"></param>
        /// <returns></returns>
        [HttpPut("EntitySource")]
        public async Task<IActionResult> UpdateEntitySource([FromBody] EntitySource entitySource)
        {
            return Success(await ConfigService.UpdateEntitySource(entitySource, true));
        }
        /// <summary>
        /// 新增实体源信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("EntitySource")]
        public async Task<IActionResult> NewEntitySource([FromBody]EntitySource entity)
        {
            return Success(await ConfigService.AddEntitySource(entity,true));
        }
        /// <summary>
        /// 删除实体源信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("EntitySource/{id}")]
        public async Task<IActionResult> DeletEntitySource(long id)
        {
            return Success(await ConfigService.DeleteEntitySource(id, true));
        }
         
    }
}