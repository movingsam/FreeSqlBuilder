﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FreeSql.Internal.Model;
using FreeSqlBuilder.Core.Entities;
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
        private const string SkipAssemblies = "^System|^Mscorlib|^msvcr120|^Netstandard|^Microsoft|^Autofac|^AutoMapper|^EntityFramework|^Newtonsoft|^Castle|^NLog|^Pomelo|^AspectCore|^Xunit|^Nito|^Npgsql|^Exceptionless|^MySqlConnector|^Anonymously Hosted|^libuv|^api-ms|^clrcompression|^clretwrc|^clrjit|^coreclr|^dbgshim|^e_sqlite3|^hostfxr|^hostpolicy|^MessagePack|^mscordaccore|^mscordbi|^mscorrc|sni|sos|SOS.NETCore|^sos_amd64|^SQLitePCLRaw|^StackExchange|^Swashbuckle|WindowsBase|ucrtbase|^DotNetCore.CAP|^MongoDB|^Confluent.Kafka|^librdkafka|^EasyCaching|^RabbitMQ|^Consul|^Dapper|^EnyimMemcachedCore|^Pipelines|^DnsClient|^IdentityModel|^zlib|^YamlDotNet|^FreeSql$|^FreeSql.Provider";
        private readonly IFreeSql<FsBuilder> _freeSql;
        private readonly FreeSqlBuilderOption _option;
        public ReflectionHelper(IMemoryCache cache, IFreeSql<FsBuilder> freeSql, FreeSqlBuilderOption option)
        {
            _cache = cache;
            _freeSql = freeSql;
            _option = option;
        }
        /// <summary>
        /// 获取实体基类
        /// </summary>
        /// <returns></returns>
        public Task<List<Item>> GetEntityBase()
        {
            var res = GetAssemblies().Select(x =>
            {
                var item = new Item(x.FullName.Split(",")[0], x.FullName)
                {
                    Title = x.FullName.Split(",")[0],
                    Children = GetAbstractClass(x.FullName).Result
                };
                return item;
            }).ToList();
            res = res.Where(x => x.Children != null && x.Children.Count > 0).ToList();
            return Task.FromResult(res);
        }
        /// <summary>
        /// 获取所有程序集
        /// </summary>
        /// <returns></returns>
        public Task<List<Item>> GetAssembliesNameItems()
        {
            return Task.FromResult(GetAssemblies().Select(x =>
            {
                var item = new Item(x.FullName.Split(",")[0], x.FullName)
                {
                    Title = x.FullName.Split(",")[0],
                };
                return item;
            }).ToList());
        }
        /// <summary>
        /// 获取基类
        /// </summary>
        /// <returns></returns>
        public Task<List<Item>> GetAbstractClass(string assemblyName)
        {
            var types = new List<Type>();
            if (string.IsNullOrWhiteSpace(assemblyName))
            {
                types = GetAssemblies().SelectMany(s => s.GetTypes()).ToList();
            }
            else
            {
                types = GetAssemblies().FirstOrDefault(x => x.FullName == assemblyName)?.GetTypes().ToList();
            }
            if (types == null) return Task.FromResult(default(List<Item>));
            return Task.FromResult(types.Where(x => x.IsAbstract && !x.IsEnum && !x.IsSealed).Select(x =>
                new Item($"{x.Name}", x.FullName)
                {
                    Title = x.Name,
                    isLeaf = true

                }).ToList());
        }

        /// <summary>
        /// 获取相关表
        /// </summary>
        /// <param name="es"></param>
        /// <returns></returns>
        public Task<List<TableInfo>> GetTableInfos(EntitySource es)
        {
            if (es == null)
            {
                throw new WarningException("实体源不能为空");
            }
            var assembly = GetAssemblies();
            var entityAssembly = assembly;
            if (!string.IsNullOrWhiteSpace(es.EntityAssemblyName))
            {
                var entityAssemblies = es.EntityAssemblyName.Split(";");
                entityAssembly = assembly.Where(x => entityAssemblies.Contains(x.FullName)).ToList();
            }
            if (entityAssembly == null) return default;
            var types = entityAssembly
                .SelectMany(x => x.GetTypes()).ToList();
            Type baseType = null;
            if (!string.IsNullOrWhiteSpace(es.EntityBaseName))
            {
                var split = es.EntityBaseName.Split(";");
                var entityBaseAssembly = split[0];
                var entityBaseName = split[1];
                var filterAssembly = assembly.FirstOrDefault(x => x.FullName == entityBaseAssembly);
                baseType = filterAssembly.GetType(entityBaseName);
            }
            var res = GetTypesFromEntityBaseName(types, baseType)
                .Select(GetTableInfos)
                .Where(x => x != null)
                .ToList();
            return Task.FromResult(res);

        }

        //public Task<Type> GetEntityBase(List<Assembly> assemblies,string entityBaseName)
        //{
        //    var res = assemblies.Select(x => x.GetType(entityBaseName));

        //}
        private TableInfo GetTableInfos(Type type)
        {
            try
            {
                return _freeSql.CodeFirst.GetTableByEntity(type).Primarys.Any() ? _freeSql.CodeFirst.GetTableByEntity(type) : default;
                //return _freeSql.CodeFirst.GetTableByEntity(type);
            }
            catch
            {
                return default;
            }
        }
        /// <summary>
        /// 从TypeList中删选baseType相关联的类
        /// </summary>
        /// <param name="types"></param>
        /// <param name="baseType"></param>
        /// <returns></returns>
        private List<Type> GetTypesFromEntityBaseName(List<Type> types, Type baseType)
        {
            var entityBaseName = baseType?.Name;
            return entityBaseName == null ? types.Where(x => !x.IsAbstract && x.IsClass).ToList() :
                types.Where(x => Reflection.BaseFrom(x, baseType) && !x.IsAbstract && x.IsClass).ToList();
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
            return !Regex.IsMatch(assembly.FullName.Split(",")[0], SkipAssemblies + _option?.SkipAssembly, RegexOptions.IgnoreCase | RegexOptions.Compiled);
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
        /// <summary>
        /// 标识
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        public string Label => Title;
        public bool isLeaf { get; set; } = false;


        /// <summary>
        /// 禁用
        /// </summary>
        public bool Disabled { get; set; } = false;

        /// <summary>
        /// 选中
        /// </summary>
        public bool Checked { get; set; } = false;
        /// <summary>
        /// 子项
        /// </summary>
        public List<Item> Children { get; set; }
    }
}
