# FreeSqlBuilder (未完成)
---
#### 利用[FreeSql](https://github.com/dotnetcore/FreeSql)的CodeFirst和DbFirst获取相关信息 并使用.NetCore Razor引擎进行模板绘制的代码生成器
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/2881099/FreeSql/master/LICENSE.txt)
---
#### 环境支持
- dotnet core 3.1+
- DotnetCore WebAPI/MVC/(RazorPage 未测试过理论上支持)
- DB FreeSql驱动支持的理论上支持
---
### 相关组件：
- FreeSqlBuilder (控制器、服务等)
- FreeSqlBuilder.Core (核心组件这里定义了使用的核心实体、帮助类和基础类)
- FreeSqlBuilder.TemplateEngine (参考了[SmartCode](https://github.com/dotnetcore/SmartCode)实现方式)
- FreeSqlBuilderUI （[Angular](https://angular.cn/)+[TS](https://www.tslang.cn/docs/home.html)+[Ng-Alain](https://ng-alain.com/theme/getting-started/zh) 这个UI参考了[SwaggerUI](https://github.com/domaindrivendev/Swashbuckle)的实现方式-将前端文件打包好后嵌入到资源文件中并通过中间件拦截对应地址并通过文件流返回对应的页面)
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


> 流程说明
> 1. 运行项目 打开FreeSqlBuilder地址 默认FsGen (这个地方用法参考swagger的打开方式)
> 2. 第一次开发会要求用户选择一种生成模式 CodeFirst/DbFirst来作为默认配置项
> 3. CodeFirst
 1.1 选择好后点击添加可以指定一个程序集来作为实体类的程序集 生成器会根据这个程序集获取相对应的可以被FreeSql识别到的实体作为表结构进行生成 (注:程序集可以多选。不选择默认所有程序集)
> 1.2 程序集下面一项为实体基类项，选择后会根据选择的接口或者基类来反射获取对应的实体。若选择了程序集，则只获取程序集中的 实现/继承 了实体基类项的 实现/派生 类
> 1.3 预览按钮可以预览反射出来的实体类 即 即将被代码生成器生成的实体
> 1.4 确认后会保存 并根据这个默认的实体源生成默认的项目->配置(实体源/数据源)->构建器（模板也会根据指定的模板路径来获取对应的文件进行生成。同时构建器基于模板来生成）
> DbFirst
 2.1 默认提示界面点击数据源则会根据数据库来生成对应的配置文件
 2.2 DbFirst可以使用测试连接来确保数据源可用 (注意:你需要确定你是否有对应数据源的FreeSql.Provider 如果不存在会报错) 
> 4. 选择默认创建的项目 编辑他
> 1 你可以根据你想让代码生成器保存到的地址来更改输出路径
> 2 命名空间会是代码生成器的namespace同时他还影响你的输出路径
> 3 作者默认会反应在代码生成器的备注中
> 4 配置默认选择了初始化创建时自动创建的配置DefaultConfig,当然你可以在配置栏里面修改它,或者新增一个你想要的配置并套用他
> 5 构建器决定了你生成的代码文件 构建器中有一系列选项 这些选项均可以再Razor模板文件中找到他们,你可以利用他们自定义自己的模板
> 6 选中的构建器会被项目生成
> 7 全表构建器只会生成一个文件 这个构建器对应的模板中可以获得所有的实体（AutomapperConfig、DbContext、可能会用到它）
> ！！注意事项：目前我没有对CodeFirst/DbFirst的模板进行分组区分 所以你要注意你的模板使用的是CodeFirst还是DbFirst的对象 DbFirst只能拿到CurrentDbTable对象 CodeFirst只能拿到CurrentTable对象


> 自定义模板涉及到的对象及静态帮助类
> - [查看传入对象Modal](https://github.com/movingsam/FreeSqlBuilder/blob/master/src/FreeSqlBuilder/FreeSqlBuilder.TemplateEngine/BuildTask.cs)
> - [查看CURD传入对象](https://github.com/movingsam/FreeSqlBuilder/blob/master/src/FreeSqlBuilder/FreeSqlBuilder.TemplateEngine/CurdTask.cs)
> - [查看Project对象](https://github.com/movingsam/FreeSqlBuilder/blob/master/src/FreeSqlBuilder/FreeSqlBuilder.Core/Project.cs)
> - [查看静态帮助类(可自行拓展帮助类 在Razor文件中使用静态方法)](https://github.com/movingsam/FreeSqlBuilder/blob/master/src/FreeSqlBuilder/FreeSqlBuilder.TemplateEngine/Utilities)
> - [默认模板](https://github.com/movingsam/FreeSqlBuilder/tree/master/src/FreeSqlBuilder/FreeSqlBuilder/RazorTemplate)
> ！！注意事项：模板是可以打断点调试的 为此默认开启了Mvc的动态编译 需要注意的是代码如果生成到了项目本地调试可能因为后端文件被修改了所以终止调试 
建议把生成的文件根目录放到其他系统目录下再来调试模板 第一次加载模板的时候会从类库中复制出模板并动态加载到项目中 所以第一次生成文件可能会比较慢





