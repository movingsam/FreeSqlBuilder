using AngularGenerator.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using BlazorFreeSqlGenerator.Modals.Base;
using FreeSql.Generator.Core;
using AngularGenerator.Helper;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace AngularGenerator.Controllers
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
        public ProjectController(IServiceProvider service)
        {
            _projectService = service.GetService<IProjectService>();
            fileProvider = service.GetService<FileProviderHelper>();
            _webhostEnv = service.GetService<IWebHostEnvironment>();
            reflection = service.GetService<ReflectionHelper>();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<IActionResult> GetPage(PageRequest request)
        {
            return Ok(await this._projectService.GetPage(request));
        }

        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPost("New")]
        public async Task<IActionResult> Add([FromBody]Project project)
        {
            return Ok(await this._projectService.Add(project));
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
            return Ok(await _projectService.UpdateTemplate(template));
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
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="entityBaseName"></param>
        /// <returns></returns>
        [HttpGet("/api/AllTable")]
        public async Task<IActionResult> GetAllDbTable(string path, string entityBaseName)
        {
            return Ok(await reflection.TaskRun(entityBaseName, reflection.ReleaseDll(path)));
        }
    }
}