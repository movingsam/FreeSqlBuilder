using FreeSql.Generator.Core.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FreeSql.TemplateEngine.Implement
{
    public class RazorTemplateEngine : ITemplateEngine
    {
        private const string TEMP = "temp";
        public bool Initialized { get; private set; }
        public string Name { get; private set; } = "Razor";
        private string _root;
        private string _temp;
        private IServiceScopeFactory _scopeFactory;
        public RazorTemplateEngine(IServiceScopeFactory service)
        {
            _scopeFactory = service;
            _root = service.CreateScope().ServiceProvider.GetService<IWebHostEnvironment>().ContentRootPath;
            _temp = Path.Combine(_root, TEMP);
            if (!Directory.Exists(_temp))
            {
                Directory.CreateDirectory(_temp);
            }
        }
        public void Initialize(IDictionary<string, object> parameters)
        {
            Initialized = true;
            if (parameters != null)
            {
                if (parameters.Value("Name", out string name))
                {
                    Name = name;
                }
                if (parameters.Value("Root", out string root))
                {
                    _root = root;
                }
            }

        }

        public async Task<string> Render(BuildTask context, string viewPath)
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                var helper = serviceScope.ServiceProvider.GetRequiredService<RazorViewToStringRender>();
                if (Path.IsPathRooted(viewPath))
                {
                    viewPath = GetAbsolutePath(viewPath);
                    var result = await helper.RenderViewToStringAsync(viewPath, context);
                    return result;
                }
                else
                {
                    return await helper.RenderViewToStringAsync(viewPath, context);
                }
            }
        }

        private string GetAbsolutePath(string viewPath)
        {
            return Path.GetRelativePath(this._root, viewPath);
        }
    }
}
