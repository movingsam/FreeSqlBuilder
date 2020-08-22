using System.Reflection;
using FreeSql;
using FreeSqlBuilder;
using FreeSqlBuilderUI;
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
            //注入FreeSqlBuilder的核心服务组件 
            //注意修改了DbType及ConnectionString 别忘了添加响应的FreeSql.Provider包
            services.AddFreeSqlBuilder(x =>
            {
                x.DefaultTemplatePath = "DefaultTemplate";//修改这个配置可以变更模板初始化导入目录（可以指定在本站点根目录的相对路径上)模板也都会从此路径读取
                x.DbSet.DbType = DataType.Sqlite;
                x.DbSet.ConnectionString = "Data Source=fsbuilder.db;Version=3";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //导入默认模板取下面注释
            app.UseDefaultTemplateImport();//初次启动导入模板
            app.UseFreeSqlBuilderUI(o =>
            {
                o.Path = "FreeSqlBuilder";//默认地址为FsGen
                //o.IndexStream=()=> typeof(BuilderUIOptions).GetTypeInfo().Assembly
                //    .GetManifestResourceStream("FreeSql.GeneratorUI.dist.index.html");//如果想要自己编写前端UI可以通过修改这个配置来完成前端替换
            });//使用FreeSqlBuilderUI
            //调试前端项目可以注释掉FreeSqlBuilderUI并取消下面注释
            //app.UseMvc();
            //app.UseSpa(x => x.UseProxyToSpaDevelopmentServer("http://localhost:4200"));

        }
    }
}
