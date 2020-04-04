﻿using FreeSql.Generator.Core.CodeFirst;
using GRES.Framework.Utils;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static FreeSql.Generator.Core.Utilities.GloabalTableInfo;

namespace FreeSql.Generator.Helper
{
    /// <summary>
    /// 反射助手
    /// </summary>
    public class ReflectionHelper
    {
        /// <summary>
        /// 反射助手
        /// </summary>
        private const string SkipAssemblies = "^System|^Mscorlib|^msvcr120|^Netstandard|^Microsoft|^Autofac|^AutoMapper|^EntityFramework|^Newtonsoft|^Castle|^NLog|^Pomelo|^AspectCore|^Xunit|^Nito|^Npgsql|^Exceptionless|^MySqlConnector|^Anonymously Hosted|^libuv|^api-ms|^clrcompression|^clretwrc|^clrjit|^coreclr|^dbgshim|^e_sqlite3|^hostfxr|^hostpolicy|^MessagePack|^mscordaccore|^mscordbi|^mscorrc|sni|sos|SOS.NETCore|^sos_amd64|^SQLitePCLRaw|^StackExchange|^Swashbuckle|WindowsBase|ucrtbase|^DotNetCore.CAP|^MongoDB|^Confluent.Kafka|^librdkafka|^EasyCaching|^RabbitMQ|^Consul|^Dapper|^EnyimMemcachedCore|^Pipelines|^DnsClient|^IdentityModel|^zlib|^FreeSql|^YamlDotNet";
        public ReflectionHelper()
        {
        }
        /// <summary>
        /// 获取程序集名称
        /// </summary>
        /// <returns></returns>
        public Task<List<Item>> GetAssembliesName()
        {
            return Task.FromResult(GetAssemblies().Select(x => new Item(x.FullName.Split(",")[0],x.FullName)).ToList());
        }
        /// <summary>
        /// 获取基类
        /// </summary>
        /// <returns></returns>
        public Task<List<Item>> GetAbstractClass(string assemblyName)
        {
            var types = GetAssemblies().FirstOrDefault(x => x.FullName == assemblyName).GetTypes().ToList();
            return Task.FromResult(types.Where(x => x.IsAbstract && !x.IsInterface).Select(x => new Item(x.Name, $"{x.Name}")).ToList());
        }
        /// <summary>
        /// 获取相关表
        /// </summary>
        /// <param name="entityBaseName"></param>
        /// <returns></returns>
        public Task<List<TableInfo>> GetTableInfos(string assemblyName,string entityBaseName)
        {
            var types = GetAssemblies().FirstOrDefault(x => x.FullName == assemblyName).GetTypes().ToList();
            types.Where(x => Reflection.BaseFrome(x, entityBaseName)).ToList().ForEach(x => new TableInfo(x).Add());
            var res = types.Where(x => Reflection.BaseFrome(x, entityBaseName)).ToList().Select(t => new TableInfo(t)).ToList();
            return Task.FromResult(res);
        }

        /// <summary>
        /// 获取程序集列表
        /// </summary>
        public virtual List<Assembly> GetAssemblies()
        {
            LoadAssemblies(PlatformServices.Default.Application.ApplicationBasePath);
            return GetAssembliesFromCurrentAppDomain();
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