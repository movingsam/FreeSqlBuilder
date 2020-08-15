using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FreeSqlBuilderUI
{
    public class FreeSqlBuilderUIMiddleware
    {
        private const string EmbeddedFileNamespace = "FreeSqlBuilderUI.dist";
        private readonly BuilderUIOptions _genOptions;
        private readonly StaticFileMiddleware _staticFileMiddleware;
        public FreeSqlBuilderUIMiddleware(RequestDelegate next, BuilderUIOptions genOptions
            , IWebHostEnvironment hostingEnv,
            ILoggerFactory loggerFactory)
        {
            //_next = next;
            _genOptions = genOptions ?? new BuilderUIOptions();
            _staticFileMiddleware = CreateStaticFileMiddleware(next, hostingEnv, loggerFactory, _genOptions);
        }
        /// <summary>
        /// 中间件处理逻辑
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var httpMethod = context.Request.Method;
            var path = context.Request.Path.Value;
            if (httpMethod.ToUpper() == "GET" && Regex.IsMatch(path.ToLower(), $"^/{Regex.Escape(_genOptions.Path.ToLower())}/?$"))
            {
                var relativeRedirectPath = path.ToLower().EndsWith("/")
                   ? "index.html"
                   : $"{path.Split('/').Last()}/index.html";
                RespondWithRedirect(context.Response, relativeRedirectPath);
                return;
            }
            if (httpMethod == "GET" && Regex.IsMatch(path.ToLower(), $"^/{Regex.Escape(_genOptions.Path)}/?index.html$"))
            {
                await RespondWithIndexHtml(context.Response);
                return;
            }
            await _staticFileMiddleware.Invoke(context);
        }
        /// <summary>
        /// 静态资源中间件创建
        /// </summary>
        /// <param name="next"></param>
        /// <param name="hostingEnv"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private StaticFileMiddleware CreateStaticFileMiddleware(
           RequestDelegate next,
           IWebHostEnvironment hostingEnv,
           ILoggerFactory loggerFactory,
           BuilderUIOptions options
           )
        {
            var staticFileOptions = new StaticFileOptions
            {
                RequestPath = $"/{options.Path}",
                FileProvider = new EmbeddedFileProvider(typeof(BuilderUIOptions).GetTypeInfo().Assembly, EmbeddedFileNamespace),
            };
            return new StaticFileMiddleware(next, hostingEnv, Options.Create(staticFileOptions), loggerFactory);
        }
        private void RespondWithRedirect(HttpResponse response, string location)
        {
            response.StatusCode = 301;
            response.Headers["Location"] = location;
        }
        /// <summary>
        /// Index.html渲染
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task RespondWithIndexHtml(HttpResponse response)
        {
            response.StatusCode = 200;
            response.ContentType = "text/html;charset=utf-8";
            using (var stream = _genOptions.IndexStream())
            {

                var htmlBuilder = new StringBuilder(new StreamReader(stream).ReadToEnd());
                await response.WriteAsync(htmlBuilder.ToString(), Encoding.UTF8);
            }
        }
    }
}
