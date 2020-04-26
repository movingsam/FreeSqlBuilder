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
            services.AddFreeSqlBuilder(opt =>
            {
                opt.DbSet.DbType = DataType.SqlServer;
                opt.DbSet.ConnectionString = "Data Source=.;Initial Catalog=DHQ;User Id=sa;Password=sl52788542;";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMvc();
            //app.UseFreeSqlBuilderUI(opt =>
            //{
            //    opt.Path = "Gen";//可以自行修改url
            //});
            app.UseSpa(x => x.UseProxyToSpaDevelopmentServer("http://localhost:4200"));

        }
    }
}
