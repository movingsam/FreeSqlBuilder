using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeSql.Generator.Core.WordsConvert
{
    public class DefaultWordsConvert : IWordsConvert, IConvertMode
    {
        public DefaultWordsConvert() { }
        public DefaultWordsConvert(ConvertMode mode)
        {
            Mode = mode;
        }

        public string Name { get; set; } = "Default";
        public ConvertMode Mode { get; set; } = ConvertMode.None;

        public string Convert(string words)
        {
            switch (Mode)
            {
                case ConvertMode.AllLower:
                    {
                        return words.ToLower();
                    }
                case ConvertMode.AllUpper:
                    {
                        return words.ToUpper();
                    }
                case ConvertMode.FirstUpper:
                    {
                        string firstChar = words.Substring(0, 1).ToUpper();
                        string leftChar = words.Substring(1).ToLower();
                        return firstChar + leftChar;
                    }
                case ConvertMode.None:
                    {
                        return words;
                    }
                default:
                    {
                        throw new Exception("未实现相关模式");
                    }
            }
        }
    }

    public interface IConvertMode
    {
        ConvertMode Mode { get; set; }

    }

    public enum ConvertMode
    {
        None,
        AllLower,
        AllUpper,
        FirstUpper
    }
}
