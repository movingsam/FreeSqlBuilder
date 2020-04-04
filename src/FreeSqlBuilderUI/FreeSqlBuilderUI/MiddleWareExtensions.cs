using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreeSql.GeneratorUI
{
    public static class MiddleWareExtensions
    {
        public static void UseFreeSqlGenUI(this IApplicationBuilder app, Action<GenUIOptions> setupAction = null)
        {
            var options = new GenUIOptions();
            if (setupAction != null)
            {
                setupAction(options);
            }
            else
            {
                options = app.ApplicationServices.GetRequiredService<IOptions<GenUIOptions>>().Value;
            }
            app.UseMvcWithDefaultRoute();
            app.UseMiddleware<FreeSqlGenUIMiddleware>(options);
        }
    }
}
