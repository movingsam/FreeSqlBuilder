using FreeSqlBuilder.Core.WordsConvert;

namespace FreeSqlBuilder.Core
{
    public class Column : IPrefix, IConvertMode, ISuffix
    {
        public string Prefix { get; set; }
        public ConvertMode Mode { get; set; } = ConvertMode.None;
        public string Suffix { get; set; }
    }
}
