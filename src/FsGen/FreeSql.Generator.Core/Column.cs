using System;
using System.Collections.Generic;
using System.Text;
using FreeSql.Generator.Core.WordsConvert;

namespace FreeSql.Generator.Core
{
    public class Column : IPrefix, IConvertMode, ISuffix
    {
        public string Prefix { get; set; }
        public ConvertMode Mode { get; set; } = ConvertMode.None;
        public string Suffix { get; set; }
    }
}
