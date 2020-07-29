using FreeSql;
using FreeSqlBuilder.Core;
using FreeSqlBuilder.Core.Helper;
using FreeSqlBuilder.Repository;
using FreeSqlBuilder.Services;
using FreeSqlBuilder.TemplateEngine;
using FreeSqlBuilder.TemplateEngine.Implement;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Encodings.Web;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 代码生成器服务相关拓展
    /// </summary>
    public static class FreeSqlBuilderExtensions
    {
        /// <summary>
        /// 添加FreeSqlGen相关 
        /// 模板地址默认:DefaultTemplatePath = "RazorTemplate"
        /// sqlite持久化默认地址 SqliteDbConnectionString="Data Source=fsbuilder.db;Version=3"
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction">生成器模板配置项</param>
        public static void AddFreeSqlBuilder(this IServiceCollection services, Action<TemplateOptions> setupAction = null)
        {
            var options = new TemplateOptions();
            setupAction?.Invoke(options);
            if (string.IsNullOrWhiteSpace(options.DbSet.ConnectionString)) throw new Exception("ConnectionString必须填写");
            services.AddSingleton(options);//配置导入
            services.AddMvc(opt => opt.EnableEndpointRouting = false)
                .AddNewtonsoftJson(option => option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)//防止递归导致json输出不正确 
                .AddRazorRuntimeCompilation();//MVC动态编译 
            ;
            services.AddFreeSqlCore();
            services.AddSingleton<HtmlEncoder>(NullHtmlEncoder.Default);//HTML中文编码处理
            services.AddSingleton<FileProviderHelper>();//文件相关处理
            services.AddMemoryCache();
            services.AddSingleton<IFreeSql<FsBuilder>>(x =>
                {
                    var builder = new FreeSql.FreeSqlBuilder()
                                .UseConnectionString(dataType: options.DbSet.DbType, options.DbSet.ConnectionString)
                                .UseAutoSyncStructure(true)
                                .Build<FsBuilder>();
                    builder.Aop.CommandAfter += (s, e) => Aop_CommandAfter(s, e, x.GetService<ILogger<IFreeSql>>());
                    return builder;
                });//持久化
            services.AddScoped<IUnitOfWork>(x => x.GetService<IFreeSql<FsBuilder>>().CreateUnitOfWork());
            services.AddScoped<ReflectionHelper>();//反射助手
            services.AddScoped<BuildTask>();//核心任务
            services.AddSingleton<RazorTemplateEngine>();//Razor模板引擎
            services.AddTransient<RazorViewToStringRender>();//Razor渲染字符串
            services.AddSingleton<DefaultDataInit>();
            //var fileProvider = services.BuildServiceProvider().GetRequiredService<FileProviderHelper>();
            //fileProvider.CopyToProjectRoot(typeof(FreeSqlBuilderExtensions));//拷贝模板到根目录
            //fileProvider.InitTemplate();//导入模板到数据库
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

        private static void AddFreeSqlCore(this IServiceCollection services)
        {
            //仓储
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<IConfigRepository, ConfigRepository>();
            services.AddScoped<IBuilderRepository, BuilderRepository>();
            //项目核心服务
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<IBuilderService, BuilderService>();
            services.AddScoped<IGeneratorConfigService, GeneratorConfigService>();
        }
    }
}
