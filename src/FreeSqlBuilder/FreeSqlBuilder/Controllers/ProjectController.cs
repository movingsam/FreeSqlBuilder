﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Core.DbFirst;
using FreeSqlBuilder.Core.Entities;
using FreeSqlBuilder.Core.Helper;
using FreeSqlBuilder.Infrastructure.Extensions;
using FreeSqlBuilder.Modals;
using FreeSqlBuilder.Modals.Base;
using FreeSqlBuilder.Modals.Dtos;
using FreeSqlBuilder.Services;
using FreeSqlBuilder.TemplateEngine;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace FreeSqlBuilder.Controllers
{
    /// <summary>
    /// 项目控制器
    /// </summary>
    [Route("api/[controller]")]
    public class ProjectController : ApiControllerBase
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

        #region 项目部分

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("Page")]
        public async Task<IActionResult> GetPage(PageRequest request)
        {
            return Success(await _projectService.GetPage(request));
        }
        /// <summary>
        /// 项目 基础资料
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost("Info/New")]
        public async Task<IActionResult> NewInfo([FromBody] ProjectInfo info)
        {
            return Success(await _projectService.AddProjectInfoAsync(info));
        }
        /// <summary>
        /// 项目新增
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> NewProject([FromBody] ProjectDto project)
        {
            return Success(await _projectService.Add(project.ToEntity(), true));
        }
        /// <summary>
        /// 更新 项目基础资料
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPut("Info")]
        public async Task<IActionResult> UpdateInfo([FromBody] ProjectInfo info)
        {
            return Success(await _projectService.UpdateProjectInfoAsync(info, true));
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(long id)
        {
            return Success(await _projectService.Remove(id, true));
        }
        /// <summary>
        /// 获取项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            return Success(await _projectService.Get(id));
        }
        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProjectDto project)
        {
            return Success(await _projectService.Update(project.ToEntity(), true));
        }

        #endregion
        #region ToDo 待完成部分


        ///// <summary>
        ///// 获取服务器的盘符及相关根目录
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("/api/DriveInofs")]
        //public async Task<IActionResult> GetDriveInfos()
        //{
        //    return Success(await _fileProvider.GetDriveInfos());
        //}


        ///// <summary>
        ///// 通过目录获取下一层级的目录
        ///// </summary>
        ///// <param name="path"></param>
        ///// <returns></returns>
        //[HttpGet("/api/DriveInfos/Dir")]
        //public async Task<IActionResult> GetDir(string path)
        //{
        //    return Success(await _fileProvider.GetPathExsitDir(path));

        //}

        ///// <summary>
        /////  通过目录获取下一层级的目录及cshtml文件
        ///// </summary>
        ///// <param name="path"></param>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //[HttpGet("/api/File/{type}")]
        //public async Task<IActionResult> GetCshtml(string path, [FromRoute] string type)
        //{
        //    return Success(await _fileProvider.GetPathExsitCshtml(path, type));
        //}
        ///// <summary>
        ///// 获取cshtml相关内容
        ///// </summary>
        ///// <param name="path"></param>
        ///// <returns></returns>
        //[HttpGet("/api/Cshtml/Import")]
        //public async Task<IActionResult> ImportCshtml(string path)
        //{
        //    return Success(await _fileProvider.GetCshtml(path));
        //}
        ///// <summary>
        ///// 获取当前项目的根目录+模板路径
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("/api/RootPath")]
        //public IActionResult GetRootPath()
        //{
        //    var path = Path.Combine(_webHostEnv.ContentRootPath, "Template", "Razor");
        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }
        //    return Success(new { root = path });
        //}


        #endregion
        #region CodeFirst/DbFirst



        /// <summary>
        /// 获取所有的基类
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/BaseClass")]
        public async Task<IActionResult> GetAllAbstractClass()
        {
            return Success(await _reflection.GetEntityBase());
        }
        /// <summary>
        /// 获取程序集名称项
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/Assemblies")]
        public async Task<IActionResult> GetAssemblies()
        {
            return Success(await _reflection.GetAssembliesNameItems());
        }


        /// <summary>
        /// 数据库获取表结构
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        [HttpPost("DbTableInfo")]
        public Task<IActionResult> GetDbTableInfo([FromBody] DataSource ds)
        {
            var res = ds.GetAllTable();
            return Task.FromResult(Success(res.Select(x => new DbTableInfoDto(x)).ToList()));
        }
        /// <summary>
        /// 获取所有表
        /// </summary>
        /// <param name="es"></param>
        /// <returns></returns>
        [HttpPost("/api/AllTable")]
        public async Task<IActionResult> GetAllDbTable([FromBody]EntitySource es)
        {
            var res = (await _reflection.GetTableInfos(es)).Select(x => new TableInfoDto(x)).ToList();
            return Success(res);
        }
        #endregion
        #region 生成器

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
            var output = Path.Combine(project.ProjectInfo.RootPath, project.ProjectInfo.NameSpace);
            return Success(output);
        }
        /// <summary>
        /// 临时任务,只通过构建器生成任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Task/Temp/Build/{id}")]
        public async Task<IActionResult> TempBuildTask(long id)
        {
            var builder = await HttpContext.RequestServices.GetService<IBuilderService>().GetBuilder(id);
            _buildTask.ImportSetting(builder);
            await _buildTask.Start();
            var output = Path.Combine(builder.DefaultProject.ProjectInfo.RootPath, builder.DefaultProject.ProjectInfo.NameSpace);
            return Success(output);
        }
        #endregion


        #region Helper
        /// <summary>
        /// 检测默认数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/Check")]
        public Task<IActionResult> Check()
        {
            var res = HttpContext.RequestServices.GetService<DefaultDataInit>().CheckDefaultData();
            return Task.FromResult(Success(res));
        }
        /// <summary>
        /// 默认实体源新增
        /// </summary>
        /// <param name="es"></param>
        /// <returns></returns>
        [HttpPost("/api/Check/DefaultEntitySource")]
        public async Task<IActionResult> DefaultEntitySource([FromBody] EntitySource es)
        {
            var helper = HttpContext.RequestServices.GetService<DefaultDataInit>();
            var res = await helper.DefaultEntitySource(es);
            helper.DefaultProjectInit(res);
            return Success(true);
        }
        /// <summary>
        /// 默认数据源新增
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        [HttpPost("/api/Check/DefaultDataSource")]
        public async Task<IActionResult> DefaultDataSource([FromBody] DataSource ds)
        {
            var helper = HttpContext.RequestServices.GetService<DefaultDataInit>();
            helper.DefaultProjectInit(await helper.DefaultDataSource(ds));
            return Success(true);
        }
        #endregion
    }
}