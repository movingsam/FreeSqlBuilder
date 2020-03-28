using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using FreeSql.Generator.Core.WordsConvert;

namespace FreeSql.Generator.Core
{
    public class Entity : BuilderOptions
    {
        public Entity() : base("Entity", templatePath: "Entity.cshtml", outputPath: "Entity", preFix: "", suffix: "", isIgnorePrefix: true, mode: ConvertMode.None)
        {

        }
    }
}
