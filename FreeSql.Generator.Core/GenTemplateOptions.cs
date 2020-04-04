using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreeSql.Generator.Core
{
    public class GenTemplateOptions
    {
        public string DefaultTemplatePath { get; set; } = "RazorTemplate";
    }
}
