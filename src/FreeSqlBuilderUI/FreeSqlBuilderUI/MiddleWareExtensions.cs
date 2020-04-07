using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FreeSqlBuilderUI
{
    public static class MiddleWareExtensions
    {
        /// <summary>
        /// 添加FreeSqlBuilder的UI中间件
        /// steupAction可修改配置
        /// ex: opt=>opt.Path = "GenUI"; 
        /// 意为:访问路径修改成 当前项目路径/GenUI
        /// Path默认为FsGen
        /// </summary>
        /// <param name="app"></param>  
        /// <param name="setupAction"></param>
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
            app.UseMvcWithDefaultRoute();
            app.UseMiddleware<FreeSqlBuilderUIMiddleware>(options);
        }
    }
}
