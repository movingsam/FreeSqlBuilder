using System;
using FreeSqlBuilder.Core.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace FreeSqlBuilderUI
{
    public static class MiddleWareExtensions
    {
        /// <summary>
        /// 添加FreeSqlBuilder的UI中间件
        /// setupAction可修改配置
        /// ex: opt=>opt.Path = "GenUI"; 
        /// 意为:访问路径修改成 当前项目路径/GenUI
        /// Path默认为FsGen
        /// </summary>
        /// <param name="app"></param>  
        /// <param name="setupAction"></param>
        // ReSharper disable once InconsistentNaming
        public static void UseFreeSqlBuilderUI(this IApplicationBuilder app, Action<BuilderUIOptions> setupAction = null)
        {
            var options = new BuilderUIOptions();
            if (setupAction != null)
            {
                setupAction(options);
            }
            else
            {
                options = app.ApplicationServices.GetRequiredService<IOptions<BuilderUIOptions>>().Value;
            }

            var fileHelper = app.ApplicationServices.GetService<FileProviderHelper>();
            fileHelper.CopyToProjectRoot(typeof(FreeSqlBuilderExtensions));
            fileHelper.InitTemplate();
            app.UseMvcWithDefaultRoute();
            app.UseMiddleware<FreeSqlBuilderUIMiddleware>(options);
        }
    }
}
