using System;
using System.Collections.Generic;
using System.Text;
using FreeSql.Generator.Core.WordsConvert;

namespace FreeSql.Generator.Core
{
    /// <summary>
    /// 构建上下文
    /// </summary>
    public class BuildContext
    {
        public DataSource DataSource { get; set; }
        public Project Project { get; set; }
    }
}
