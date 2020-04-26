using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FreeSql.Internal.Model;
using FreeSqlBuilder.Core.Utilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.PlatformAbstractions;

namespace FreeSqlBuilder.Core.Helper
{
    /// <summary>
    /// 反射助手
    /// </summary>
    public class ReflectionHelper
    {
        private readonly IMemoryCache _cache;
        /// <summary>
        /// 反射助手
        /// </summary>
        private const string SkipAssemblies = "^System|^Mscorlib|^msvcr120|^Netstandard|^Microsoft|^Autofac|^AutoMapper|^EntityFramework|^Newtonsoft|^Castle|^NLog|^Pomelo|^AspectCore|^Xunit|^Nito|^Npgsql|^Exceptionless|^MySqlConnector|^Anonymously Hosted|^libuv|^api-ms|^clrcompression|^clretwrc|^clrjit|^coreclr|^dbgshim|^e_sqlite3|^hostfxr|^hostpolicy|^MessagePack|^mscordaccore|^mscordbi|^mscorrc|sni|sos|SOS.NETCore|^sos_amd64|^SQLitePCLRaw|^StackExchange|^Swashbuckle|WindowsBase|ucrtbase|^DotNetCore.CAP|^MongoDB|^Confluent.Kafka|^librdkafka|^EasyCaching|^RabbitMQ|^Consul|^Dapper|^EnyimMemcachedCore|^Pipelines|^DnsClient|^IdentityModel|^zlib|^YamlDotNet";
        private readonly IFreeSql<FsBuilder> _freeSql;
        public ReflectionHelper(IMemoryCache cache, IFreeSql<FsBuilder> freeSql)
        {
            _cache = cache;
            _freeSql = freeSql;
        }
        /// <summary>
        /// 获取程序集名称
        /// </summary>
        /// <returns></returns>
        public Task<List<Item>> GetAssembliesName()
        {
            return Task.FromResult(new List<Item> { new Item("请选择程序集", "") }.Concat(GetAssemblies().Select(x => new Item(x.FullName.Split(",")[0], x.FullName)).ToList()).ToList());
        }
        /// <summary>
        /// 获取基类
        /// </summary>
        /// <returns></returns>
        public Task<List<Item>> GetAbstractClass(string assemblyName)
        {
            var types = GetAssemblies().FirstOrDefault(x => x.FullName == assemblyName)?.GetTypes().ToList();
            return Task.FromResult(new List<Item> { new Item("请选择基类", "") }.Concat(types.Where(x => x.IsAbstract).Select(x => new Item($"{x.Name}", x.FullName))).ToList());
        }
        /// <summary>
        /// 获取相关表
        /// </summary>
        /// <param name="entityBaseName"></param>
        /// <returns></returns>
        public Task<List<TableInfo>> GetTableInfos(string assemblyName, string entityBaseName)
        {
            var assembly = GetAssemblies().FirstOrDefault(x => x.FullName == assemblyName);
            var types = assembly.GetTypes().ToList();
            Type BaseType = string.IsNullOrWhiteSpace(entityBaseName) ? null : assembly.GetType(entityBaseName);
            var res = GetTypesFromEntityBaseName(types, BaseType)
                .Where(x => GetTableInfos(x) != null)
                .Select(GetTableInfos).ToList();
            return Task.FromResult(res);
        }
        private TableInfo GetTableInfos(Type type)
        {
            try
            {
                return _freeSql.CodeFirst.GetTableByEntity(type).Primarys.Any() ? _freeSql.CodeFirst.GetTableByEntity(type) : default;
            }
            catch
            {
                return default;
            }
        }
        /// <summary>
        /// 从Typelist中删选baseType相关联的类
        /// </summary>
        /// <param name="types"></param>
        /// <param name="baseType"></param>
        /// <returns></returns>
        private List<Type> GetTypesFromEntityBaseName(List<Type> types, Type baseType)
        {
            var entityBaseName = baseType?.Name;
            if (entityBaseName == null)
            {
                return types.Where(x => !x.IsAbstract && x.IsClass).ToList();
            }
            var res = types.Where(x => (Reflection.BaseFrome(x, baseType)) && !x.IsAbstract && x.IsClass).ToList();
            return res;
        }

        /// <summary>
        /// 获取程序集列表
        /// </summary>
        public virtual List<Assembly> GetAssemblies()
        {
            var res = _cache.GetOrCreate<List<Assembly>>("allAsemblies", f =>
           {
               LoadAssemblies(PlatformServices.Default.Application.ApplicationBasePath);
               return GetAssembliesFromCurrentAppDomain();
           });
            return res;
        }

        /// <summary>
        /// 加载程序集到当前应用程序域
        /// </summary>
        /// <param name="path">目录绝对路径</param>
        protected void LoadAssemblies(string path)
        {
            foreach (string file in Directory.GetFiles(path, "*.dll"))
            {
                if (Match(Path.GetFileName(file)) == false)
                    continue;
                LoadAssemblyToAppDomain(file);
            }
        }
        /// <summary>
        /// 将程序集添加当前应用程序域
        /// </summary>
        private void LoadAssemblyToAppDomain(string file)
        {
            try
            {
                var assemblyName = AssemblyName.GetAssemblyName(file);
                AppDomain.CurrentDomain.Load(assemblyName);
            }
            catch (BadImageFormatException)
            {
            }
        }
        /// <summary>
        /// 从当前应用程序域获取程序集列表
        /// </summary>
        private List<Assembly> GetAssembliesFromCurrentAppDomain()
        {
            var result = new List<Assembly>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (Match(assembly))
                    result.Add(assembly);
            }
            return result.Distinct().ToList();
        }
        /// <summary>
        /// 程序集是否匹配
        /// </summary>
        private bool Match(Assembly assembly)
        {
            return !Regex.IsMatch(assembly.FullName.Split(",")[0], SkipAssemblies, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        /// <summary>
        /// 程序集是否匹配
        /// </summary>
        protected virtual bool Match(string assemblyName)
        {
            if (assemblyName.StartsWith($"{PlatformServices.Default.Application.ApplicationName}.Views"))
                return false;
            if (assemblyName.StartsWith($"{PlatformServices.Default.Application.ApplicationName}.PrecompiledViews"))
                return false;
            return Regex.IsMatch(assemblyName, SkipAssemblies, RegexOptions.IgnoreCase | RegexOptions.Compiled) == false;
        }

    }
    /// <summary>
    /// 项
    /// </summary>
    public class Item
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public Item(string key, string value)
        {
            Key = key;
            Value = value;
        }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
