using System;
using System.Collections.Generic;

namespace FreeSql.Generator.Core.WordsConvert
{
    public interface IWordsConvert
    {
        string Name { set; }
        string Convert(string words);
    }
}