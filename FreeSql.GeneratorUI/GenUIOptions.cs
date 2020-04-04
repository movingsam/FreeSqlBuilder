using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FreeSql.GeneratorUI
{
    public class GenUIOptions
    {
        public GenUIOptions() { }
        /// <summary>
        /// FreeSql代码生成器后端地址
        /// </summary>
        public string Path { get; set; } = "FsGen";
        /// <summary>
        /// 是否需要登录
        /// </summary>
        public bool NeedLogin { get; set; } = false;
        /// <summary>
        /// IndexStream通过反射获取资源流
        /// </summary>
        public Func<Stream> IndexStream { get; set; } = () => typeof(GenUIOptions).GetTypeInfo().Assembly
            .GetManifestResourceStream("FreeSql.GeneratorUI.dist.index.html");
    }
}
