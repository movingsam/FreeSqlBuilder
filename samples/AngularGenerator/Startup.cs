//using FreeSql.Generator;
//using AngularGenerator.Services;
//using FreeSql.Generator;
using FreeSql.Generator;
using FreeSql.GeneratorUI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Linq;
using System.Reflection;

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
                option.DefaultTemplatePath = "Template";//����ģ�嵽��Ŀ�ļ��� ���Ե���cshtml
                option.SqliteDbConnectionString = @"Data Source=D:\Template\generator.db;Version=3"; //sqlite�־û�
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseFreeSqlGenUI(opt =>
            {
                opt.Path = "";//���������޸�url
            });

        }
    }
}
