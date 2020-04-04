using AngularGenerator.Services;
using FreeSql.Generator.Core;
using FreeSql.Generator.Helper;
using FreeSql.Generator.Modals.Base;
using FreeSql.TemplateEngine;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FreeSql.Generator.Controllers
{
    /// <summary>
    /// 代码生成器相关控制器
    /// </summary>
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly FileProviderHelper fileProvider;
        private readonly IWebHostEnvironment _webhostEnv;
        private readonly ReflectionHelper reflection;
        private readonly BuildTask _buildTask;
        public ProjectController(IServiceProvider service)
        {
            _projectService = service.GetService<IProjectService>();
            fileProvider = service.GetService<FileProviderHelper>();
            _webhostEnv = service.GetService<IWebHostEnvironment>();
            reflection = service.GetService<ReflectionHelper>();
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
        [HttpPut("Info")]
        public async Task<IActionResult> UpdateInfo([FromBody]ProjectInfo info)
        {
            return Ok(await _projectService.UpdateProjectInfoAsync(info));
        }
        /// <summary>
        /// 新增配置
        /// </summary>
        /// <returns></returns>
        [HttpPost("Config/New")]
        public async Task<IActionResult> NewConfig([FromBody]GeneratorModeConfig config)
        {
            return Ok(await _projectService.AddGConfig(config));
        }
        [HttpPut("Config")]
        public async Task<IActionResult> UpdateConfig([FromBody]GeneratorModeConfig config)
        {
            return Ok(await _projectService.UpdateConfig(config));
        }
        [HttpPost("Builder/New")]
        public async Task<IActionResult> NewBuilder([FromBody]BuilderOptions options)
        {
            return Ok(await _projectService.AddBuilder(options));
        }
        [HttpPut("Builder")]
        public async Task<IActionResult> UpdateBuilder([FromBody]BuilderOptions options)
        {
            return Ok(await _projectService.UpdateBuilder(options));
        }
        [HttpDelete("Builder/{id}")]
        public async Task<IActionResult> UpdateBuilder([FromQuery]long id)
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
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("/api/Template/{id}")]
        public async Task<IActionResult> TemlateRemove(long id)
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
        public async Task<IActionResult> AddTempalte([FromBody] Template template)
        {
            return Ok(await _projectService.AddTempalte(template));
        }
        /// <summary>
        /// 获取服务器的盘符及相关根目录
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/DriveInofs")]
        public async Task<IActionResult> GetDriveInfos()
        {
            return Ok(await fileProvider.GetDriveInfos());
        }
        /// <summary>
        /// 通过目录获取下一层级的目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [HttpGet("/api/DriveInfos/Dir")]
        public async Task<IActionResult> GetDir(string path)
        {
            return Ok(await fileProvider.GetPathExsitDir(path));

        }
        /// <summary>
        ///  通过目录获取下一层级的目录及cshtml文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [HttpGet("/api/File/{type}")]
        public async Task<IActionResult> GetCshtml(string path, [FromRoute]string type)
        {
            return Ok(await fileProvider.GetPathExsitCshtml(path, type));
        }
        /// <summary>
        /// 获取cshtml相关内容
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [HttpGet("/api/Cshtml/Import")]
        public async Task<IActionResult> ImportCshtml(string path)
        {
            return Ok(await fileProvider.GetCshtml(path));
        }
        /// <summary>
        /// 获取当前项目的根目录+模板路径
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/RootPath")]
        public IActionResult GetRootPath()
        {
            var path = Path.Combine(_webhostEnv.ContentRootPath, "Template", "Razor");
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
        [HttpGet("/api/AllTable/{entityAssemblyName}/{entityBaseName}")]
        public async Task<IActionResult> GetAllDbTable(string entityBaseName, string entityAssemblyName)
        {
            return Ok(await reflection.GetTableInfos(entityAssemblyName, entityBaseName));
        }
        /// <summary>
        /// 获取所有的基类
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/BaseClass/{entityAssemblyName}")]
        public async Task<IActionResult> GetAllAbstractClass(string entityAssemblyName)
        {
            return Ok(await reflection.GetAbstractClass(entityAssemblyName));
        }
        /// <summary>
        /// 获取程序集名称项
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/Assemblies")]
        public async Task<IActionResult> GetAssemblies()
        {
            return Ok(await reflection.GetAssembliesName());
        }
        /// <summary>
        /// 生成项目
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost("Task/Build/{id}")]
        public async Task<IActionResult> BuildTask(long id)
        {
            var project = await _projectService.Get(id);
            _buildTask.ImportSetting(project);
            await _buildTask.Start();
            return Ok();
        }
    }
}