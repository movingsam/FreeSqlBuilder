using AngularGenerator.Services;
using FreeSql.Aop;
using FreeSql.Generator.Core;
using FreeSql.Generator.Helper;
using FreeSql.TemplateEngine;
using FreeSql.TemplateEngine.Implement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;

namespace FreeSql.Generator
{
    /// <summary>
    /// 代码生成器服务相关拓展
    /// </summary>
    public static class FreeSqlGenExtensions
    {
        /// <summary>
        /// 添加FreeSqlGen相关
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction">生成器模板配置项</param>
        public static void AddFreeSqlGen(this IServiceCollection services, Action<GenTemplateOptions> setupAction = null)
        {
            var options = new GenTemplateOptions();
            setupAction?.Invoke(options);
            services.AddSingleton(options);//配置导入
            services.AddMvc(opt => opt.EnableEndpointRouting = false)
                .AddNewtonsoftJson(option => option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)//防止递归导致json输出不正确
                .AddRazorRuntimeCompilation();//MVC动态编译
            services.AddScoped<IProjectService, ProjectService>();//项目核心服务
            services.AddSingleton<HtmlEncoder>(NullHtmlEncoder.Default);//HTML中文乱码
            services.AddSingleton<FileProviderHelper>();//文件相关处理
            services.AddSingleton<IFreeSql<FsGen>>(x =>
                {
                    var builder = new FreeSqlBuilder()
                                .UseConnectionString(dataType: DataType.Sqlite, options.SqliteDbConnectionString)
                                .UseAutoSyncStructure(true)
                                .Build<FsGen>();
                    builder.Aop.CommandAfter += (s, e) => Aop_CommandAfter(s, e, x.GetService<ILogger<IFreeSql>>());
                    return builder;
                });//持久化
            services.AddScoped<ReflectionHelper>();//反射助手
            services.AddScoped<BuildTask>();//核心任务
            services.AddSingleton<RazorTemplateEngine>();//Razor模板引擎
            services.AddTransient<RazorViewToStringRender>();//Razor渲染字符串工具
            var fileProvider = services.BuildServiceProvider().GetRequiredService<FileProviderHelper>();
            fileProvider.CopyToProjectRoot();//拷贝模板到根目录
            fileProvider.InitTemplate();//导入模板到数据库
        }
        /// <summary>
        /// sql监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="logger"></param>
        private static void Aop_CommandAfter(object sender, FreeSql.Aop.CommandAfterEventArgs e, ILogger logger)
        {
            logger.LogInformation($"======================执行耗时:{e.ElapsedMilliseconds.ToString()}======================");
            logger.LogInformation($"======================执行语句======================");
            logger.LogInformation($"{e.Log}");
            logger.LogInformation($"======================执行语句======================");
        }

    }
}
