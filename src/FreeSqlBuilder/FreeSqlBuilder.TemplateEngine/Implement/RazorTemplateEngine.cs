using FreeSqlBuilder.Core.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FreeSqlBuilder.TemplateEngine.Implement
{
    public class RazorTemplateEngine : ITemplateEngine
    {
        private const string TEMP = "temp";
        public bool Initialized { get; private set; }
        public string Name { get; private set; } = "Razor";
        private readonly string _root;
        private readonly IServiceScopeFactory _scopeFactory;
        public RazorTemplateEngine(IServiceScopeFactory service)
        {
            _scopeFactory = service;
            _root = service.CreateScope().ServiceProvider.GetService<IWebHostEnvironment>().ContentRootPath;
            var temp = Path.Combine(_root, TEMP);
            if (!Directory.Exists(temp))
            {
                Directory.CreateDirectory(temp);
            }
        }

        public async Task<string> Render(BuildTask context, string viewPath)
        {
            using var serviceScope = _scopeFactory.CreateScope();
            var helper = serviceScope.ServiceProvider.GetRequiredService<RazorViewToStringRender>();
            if (!Path.IsPathRooted(viewPath)) return await helper.RenderViewToStringAsync(viewPath, context);
            viewPath = GetAbsolutePath(viewPath);
            var result = await helper.RenderViewToStringAsync(viewPath, context);
            return result;

        }

        private string GetAbsolutePath(string viewPath)
        {
            return Path.GetRelativePath(this._root, viewPath);
        }
    }
}
