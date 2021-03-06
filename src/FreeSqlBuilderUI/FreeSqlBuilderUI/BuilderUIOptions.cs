﻿using System;
using System.IO;
using System.Reflection;

namespace FreeSqlBuilderUI
{
    public class BuilderUIOptions
    {
        public BuilderUIOptions() { }
        /// <summary>
        /// FreeSql代码生成器后端地址
        /// </summary>
        public string Path { get; set; } = "FsBuilder";
        ///// <summary>
        ///// 是否需要登录
        ///// </summary>
        //public bool NeedLogin { get; set; } = false;
        /// <summary>
        /// IndexStream通过反射获取资源流
        /// </summary>
        public Func<Stream> IndexStream { get; set; } = () => typeof(BuilderUIOptions).GetTypeInfo().Assembly
            .GetManifestResourceStream("FreeSql.GeneratorUI.dist.index.html");
    }
}
