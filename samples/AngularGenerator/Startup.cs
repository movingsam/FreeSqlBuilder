using FreeSql.Generator;
using FreeSql.GeneratorUI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AngularGenerator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFreeSqlGen(option =>
            {
                option.DefaultTemplatePath = "Template";//复制模板到项目文件夹 可以调试cshtml
                option.SqliteDbConnectionString = @"Data Source=D:\Template\generator.db;Version=3"; //sqlite持久化
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            app.UseFreeSqlGenUI(opt =>
            {
                opt.Path = "CodeGenerator";//访问路径修改
            });
            //app.UseSpa(x => x.UseProxyToSpaDevelopmentServer("http://localhost:4200"));
        }
    }
}
