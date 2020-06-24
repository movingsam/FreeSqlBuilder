using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Core.DbFirst;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Core.Helper;
using FreeSqlBuilder.Modals;
using FreeSqlBuilder.Modals.Base;
using FreeSqlBuilder.Services;
using FreeSqlBuilder.TemplateEngine;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FreeSqlBuilder.Controllers
{
    /// <summary>
    /// 代码生成器相关控制器
    /// </summary>
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly FileProviderHelper _fileProvider;
        private readonly IWebHostEnvironment _webHostEnv;
        private readonly ReflectionHelper _reflection;
        private readonly BuildTask _buildTask;
        /// <summary>
        /// 控制器构造注入
        /// </summary>
        /// <param name="service"></param>
        public ProjectController(IServiceProvider service)
        {
            _projectService = service.GetService<IProjectService>();
            _fileProvider = service.GetService<FileProviderHelper>();
            _webHostEnv = service.GetService<IWebHostEnvironment>();
            _reflection = service.GetService<ReflectionHelper>();
            _buildTask = service.GetService<BuildTask>();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<IActionResult> GetPage(PageRequest request)
        {
            return Ok(await _projectService.GetPage(request));
        }
        /// <summary>
        /// 项目资料
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost("Info/New")]
        public async Task<IActionResult> NewInfo([FromBody]ProjectInfo info)
        {
            return Ok(await _projectService.AddProjectInfoAsync(info));
        }
        /// <summary>
        /// 项目资料更新
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPut("Info")]
        public async Task<IActionResult> UpdateInfo([FromBody]ProjectInfo info)
        {
            return Ok(await _projectService.UpdateProjectInfoAsync(info));
        }
        /// <summary>
        /// 新增配置
        /// </summary>
        /// <returns></returns>
        [HttpPost("Config/New/{projectid}")]
        public async Task<IActionResult> NewConfig([FromBody]GeneratorModeConfig config, [FromRoute]long projectid)
        {
            return Ok(await _projectService.AddGConfig(config, projectid));
        }
        /// <summary>
        /// 配置项更新
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        [HttpPut("Config")]
        public async Task<IActionResult> UpdateConfig([FromBody]GeneratorModeConfig config)
        {
            return Ok(await _projectService.UpdateConfig(config));
        }
        /// <summary>
        /// 新增构建器
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost("Builder/New")]
        public async Task<IActionResult> NewBuilder([FromBody]BuilderOptions options)
        {
            return Ok(await _projectService.AddBuilder(options));
        }
        /// <summary>
        /// 更新构建器
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPut("Builder")]
        public async Task<IActionResult> UpdateBuilder([FromBody]BuilderOptions options)
        {
            return Ok(await _projectService.UpdateBuilder(options));
        }
        /// <summary>
        /// 构建器删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Builder/{id}")]
        public async Task<IActionResult> DelBuilder(long id)
        {
            return Ok(await _projectService.DelBuilder(id));
        }
        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(long id)
        {
            return Ok(await _projectService.Remove(id));
        }
        /// <summary>
        /// 获取项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            return Ok(await _projectService.Get(id));
        }
        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]Project project)
        {
            return Ok(await _projectService.Update(project));
        }
        /// <summary>
        /// 模板列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/Template/Page")]
        public async Task<IActionResult> TemplateGetPage(PageRequest request)
        {
            return Ok(await _projectService.GetTemplatePageAsync(request));
        }
        /// <summary>
        /// 获取模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/api/Template/{id}")]
        public async Task<IActionResult> TemplateGet(long id)
        {
            return Ok(await _projectService.GetTemplateAsync(id));
        }

        /// <summary>
        /// 模板删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/api/Template/{id}")]
        public async Task<IActionResult> TemplateRemove(long id)
        {
            return Ok(await _projectService.RemoveTemplate(id));
        }
        /// <summary>
        /// 更新模板
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        [HttpPut("/api/Template")]
        public async Task<IActionResult> UpdateTemplate([FromBody]Template template)
        {
            if (await _projectService.UpdateTemplate(template))
            {
                return Ok(template);
            }
            return BadRequest();
        }
        /// <summary>
        /// 新增模板
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        [HttpPost("/api/Template")]
        public async Task<IActionResult> AddTemplate([FromBody] Template template)
        {
            return Ok(await _projectService.AddTemplate(template));
        }
        /// <summary>
        /// 获取服务器的盘符及相关根目录
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/DriveInofs")]
        public async Task<IActionResult> GetDriveInfos()
        {
            return Ok(await _fileProvider.GetDriveInfos());
        }
        /// <summary>
        /// 通过目录获取下一层级的目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [HttpGet("/api/DriveInfos/Dir")]
        public async Task<IActionResult> GetDir(string path)
        {
            return Ok(await _fileProvider.GetPathExsitDir(path));

        }

        /// <summary>
        ///  通过目录获取下一层级的目录及cshtml文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet("/api/File/{type}")]
        public async Task<IActionResult> GetCshtml(string path, [FromRoute]string type)
        {
            return Ok(await _fileProvider.GetPathExsitCshtml(path, type));
        }
        /// <summary>
        /// 获取cshtml相关内容
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [HttpGet("/api/Cshtml/Import")]
        public async Task<IActionResult> ImportCshtml(string path)
        {
            return Ok(await _fileProvider.GetCshtml(path));
        }
        /// <summary>
        /// 获取当前项目的根目录+模板路径
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/RootPath")]
        public IActionResult GetRootPath()
        {
            var path = Path.Combine(_webHostEnv.ContentRootPath, "Template", "Razor");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return Ok(new { root = path });
        }
        /// <summary>
        /// 获取所有表
        /// </summary>
        /// <param name="entityBaseName"></param>
        /// <param name="entityAssemblyName"></param>
        /// <returns></returns>
        [HttpGet("/api/AllTable/{entityAssemblyName}")]
        public async Task<IActionResult> GetAllDbTable(string entityAssemblyName, string entityBaseName)
        {
            var res = (await _reflection.GetTableInfos(entityAssemblyName, entityBaseName)).Select(x => new TableInfoDto(x)).ToList();
            return Ok(res);
        }
        /// <summary>
        /// 获取所有的基类
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/BaseClass/{entityAssemblyName}")]
        public async Task<IActionResult> GetAllAbstractClass(string entityAssemblyName)
        {
            return Ok(await _reflection.GetAbstractClass(entityAssemblyName));
        }
        /// <summary>
        /// 获取程序集名称项
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/Assemblies")]
        public async Task<IActionResult> GetAssemblies()
        {
            return Ok(await _reflection.GetAssembliesName());
        }
        /// <summary>
        /// 生成项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Task/Build/{id}")]
        public async Task<IActionResult> BuildTask(long id)
        {
            var project = await _projectService.Get(id);
            _buildTask.ImportSetting(project);
            await _buildTask.Start();
            return Ok();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("DbTableInfo")]
        public async Task<IActionResult> GetDbTableInfo([FromBody]DbFirstDto dto)
        {
            var dbFirstHelper = new DbFirstHelper();
            var res = dbFirstHelper.GetAllTable(dto);
            return Ok(res.Select(x => new DbTableInfoDto(x)).ToList());
        }
    }
}