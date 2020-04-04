using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using FreeSql.Generator.Core.WordsConvert;

namespace FreeSql.Generator.Core
{
    public class Entity : BuilderOptions
    {
        public Entity() : base("Entity",  outputPath: "Entity", preFix: "", suffix: "", isIgnorePrefix: true, mode: ConvertMode.None)
        {

        }
        public Column Column { get; set; }
    }
}
