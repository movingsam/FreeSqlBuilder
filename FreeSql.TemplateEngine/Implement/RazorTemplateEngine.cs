﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using FreeSql.Generator.Core;
using FreeSql.Generator.Core.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.ObjectPool;

namespace FreeSql.TemplateEngine.Implement
{
    public class RazorTemplateEngine : ITemplateEngine
    {
        private const string TEMP = ".temp";
        public bool Initialized { get; private set; }
        public string Name { get; private set; } = "Razor";
        private string _root = Path.Combine(AppContext.BaseDirectory, "RazorTemplate");
        private string _temp;
        private IServiceScopeFactory _scopeFactory;
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
            _temp = Path.Combine(_root, TEMP);
            if (!Directory.Exists(_temp))
            {
                Directory.CreateDirectory(_temp);
            }
            InitializeServices();
        }

        public async Task<string> Render(BuildTask context, string viewPath)
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                var helper = serviceScope.ServiceProvider.GetRequiredService<RazorViewToStringRender>();
                if (Path.IsPathRooted(viewPath))
                {
                    var tempFileName = $"{Path.GetFileNameWithoutExtension(viewPath)}-{Guid.NewGuid():N}{Path.GetExtension(viewPath)}";
                    var destFileName = Path.Combine(_temp, tempFileName);
                    File.Copy(Path.Combine(_root, viewPath), destFileName);
                    viewPath = Path.Combine(TEMP, tempFileName);
                    var result = await helper.RenderViewToStringAsync(viewPath, context);
                    File.Delete(destFileName);
                    return result;
                }
                else
                {
                    return await helper.RenderViewToStringAsync(viewPath, context);
                }
            }
        }

        private void InitializeServices()
        {
            var services = ConfigureDefaultServices();
            var serviceProvider = services.BuildServiceProvider();
            _scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
        }
        private IServiceCollection ConfigureDefaultServices()
        {
            var services = new ServiceCollection();
            IFileProvider fileProvider = new PhysicalFileProvider(_root, ExclusionFilters.None);
            services.AddSingleton<IHostingEnvironment>(new HostingEnvironment
            {
                ApplicationName = Assembly.GetEntryAssembly()?.GetName().Name,
                WebRootFileProvider = fileProvider,
            });
            services.AddSingleton<HtmlEncoder>(NullHtmlEncoder.Default);
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProviders.Clear();
                options.FileProviders.Add(fileProvider);
            });
            var diagnosticSource = new DiagnosticListener("Microsoft.AspNetCore");
            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddSingleton<DiagnosticSource>(diagnosticSource);
            services.AddLogging();
            services.AddMvc();
            services.AddTransient<RazorViewToStringRender>();
            return services;
        }
    }
}
