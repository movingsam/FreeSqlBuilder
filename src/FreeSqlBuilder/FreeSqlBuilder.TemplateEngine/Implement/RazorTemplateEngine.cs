using System;
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
        private string _tempDir;
        public RazorTemplateEngine(IServiceScopeFactory service)
        {
            _scopeFactory = service;
            _root = service.CreateScope().ServiceProvider.GetService<IWebHostEnvironment>().ContentRootPath;
            _tempDir = Path.Combine(_root, TEMP);
            if (!Directory.Exists(_tempDir))
            {
                Directory.CreateDirectory(_tempDir);
            }
        }

        public async Task<string> Render(BuildTask context, string viewPath)
        {
            using var serviceScope = _scopeFactory.CreateScope();
            var helper = serviceScope.ServiceProvider.GetRequiredService<RazorViewToStringRender>();
            if (!Path.IsPathRooted(viewPath)) return await helper.RenderViewToStringAsync(viewPath, context);
            viewPath = GetTempViewPath(viewPath);
            var rel = Path.GetRelativePath(_root, viewPath);
            var result = await helper.RenderViewToStringAsync(rel, context);
            File.Delete(viewPath);
            return result;

        }

        public async Task<string> Render(TempBuildTask context, string viewPath)
        {
            using var serviceScope = _scopeFactory.CreateScope();
            var helper = serviceScope.ServiceProvider.GetRequiredService<RazorViewToStringRender>();
            if (!Path.IsPathRooted(viewPath)) return await helper.RenderViewToStringAsync(viewPath, context);
            viewPath = GetTempViewPath(viewPath);
            var rel = Path.GetRelativePath(_root, viewPath);
            var result = await helper.RenderViewToStringAsync(rel, context);
            File.Delete(viewPath);
            return result;

        }

        private string GetTempViewPath(string viewPath)
        {
            var tempFileName = $"{Path.GetFileNameWithoutExtension(viewPath)}-{Guid.NewGuid():N}{Path.GetExtension(viewPath)}";
            var destFileName = Path.Combine(_tempDir, tempFileName);
            File.Copy(viewPath, destFileName); 
            return destFileName;
        } 
    }
}
