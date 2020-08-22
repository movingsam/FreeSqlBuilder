# FreeSqlBuilder (未完成)
---
#### 利用FreeSql的CodeFirst和DbFirst获取相关信息 并使用.NetCore Razor引擎进行模板绘制的代码生成器
---
#### 环境支持
- dotnet core 3.1+
- DotnetCore WebAPI/MVC/(RazorPage 未测试过理论上支持)
- DB FreeSql驱动支持的理论上支持
---
### 相关组件：
- FreeSqlBuilder 
- FreeSqlBuilder.Core
- FreeSqlBuilder.TemplateEngine
- FreeSqlBuilderUI
---
### 用法:
> 添加Nuget引用
```
dotnet add package FreeSqlBuilder
dotnet add package FreeSqlBuilderUI
dotnet add package FreeSql.Provider.Sqlite  #这是FreeSql数据库驱动 自行切换
```


> ConfigureServices添加相关服务

``` CSharp
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

```
> ！！！ 需要注意随着使用的数据库不同需要自行加载相关FreeSqlProvider Nuget包 包括默认的Sqlite

> Configure添加中间件

``` CSharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{

    //导入默认模板取下面注释
    //app.UseDefaultTemplateImport();//初次启动导入模板
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

```

### 操作指南
> 流程演示
![演示gif](./doc/screen/work.gif)

  [查看传入对象Modal](https://github.com/movingsam/FreeSqlBuilder/blob/master/src/FreeSqlBuilder/FreeSqlBuilder.TemplateEngine/BuildTask.cs)
  [查看CURD传入对象](https://github.com/movingsam/FreeSqlBuilder/blob/master/src/FreeSqlBuilder/FreeSqlBuilder.TemplateEngine/CurdTask.cs)
  [查看Project对象](https://github.com/movingsam/FreeSqlBuilder/blob/master/src/FreeSqlBuilder/FreeSqlBuilder.Core/Project.cs)
  [查看帮助类](https://github.com/movingsam/FreeSqlBuilder/blob/master/src/FreeSqlBuilder/FreeSqlBuilder.TemplateEngine/Utilities)
> !!!全局构建器和表构建器类似，不同的是他只会执行一次 而不是每个表都执行一次
> 注意DbFirst只能拿到CurrentDbTable对象 CurrentTable的对象无法拿到
> 生成后会返回文件地址 



