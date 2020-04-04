using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreeSql.Generator.Core
{
    public class GenTemplateOptions
    {
        public string DefaultTemplatePath { get; set; } = "RazorTemplate";
        /// <summary>
        /// Sqlite持久化地址
        /// </summary>
        public string SqliteDbConnectionString { get; set; } = "Data Source=db/generator;Version=3";
    }
}
