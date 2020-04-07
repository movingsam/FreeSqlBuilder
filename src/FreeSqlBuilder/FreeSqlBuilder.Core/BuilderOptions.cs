using FreeSql.DataAnnotations;
using FreeSqlBuilder.Core.WordsConvert;

namespace FreeSqlBuilder.Core
{
    public class BuilderOptions : IKey<long>, IPrefix, IOutPut, IConvertMode, ISuffix
    {
        public BuilderOptions()
        {

        }

        public BuilderOptions(string name, string outputPath,
            string preFix = "", string suffix = "", bool isIgnorePrefix = true,
            ConvertMode mode = ConvertMode.None)
        {
            Name = name;
            OutPutPath = outputPath;
            Prefix = preFix;
            Suffix = suffix;
            IsIgnorePrefix = isIgnorePrefix;
            Mode = mode;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public long Id { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public long ProjectId { get; set; }
        public Project Project { get; set; }
        /// <summary>
        /// 基类名
        /// </summary>
        public string ClassBase { get; set; }
        /// <summary>
        /// 构建器名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否只生成带有服务特性实体[ServiceAttribute]
        /// </summary>
        public bool IsServiceOnly { get; set; }
        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; set; }
        /// <summary>
        /// 忽略数据库表中的前缀的 前缀分隔符 （与IsIgnorePrefix搭配）
        /// </summary>
        public string SplitDot { get; set; } = "_";
        /// <summary>
        /// 名称是否忽略前缀
        /// </summary>
        public bool IsIgnorePrefix { get; set; } = true;
        /// <summary>
        /// 输出路径
        /// </summary>
        public string OutPutPath { get; set; }
        /// <summary>
        /// 名称转换模式
        /// </summary>
        public ConvertMode Mode { get; set; }
        /// <summary>
        /// 模板路径
        /// </summary>
        [Navigate(nameof(TemplateId))]
        public Template Template { get; set; } = new Template();
        /// <summary>
        /// 模板id
        /// </summary>
        public long TemplateId { get; set; }
        /// <summary>
        /// 后缀
        /// </summary>
        public string Suffix { get; set; }
        /// <summary>
        /// 构建类型
        /// </summary>
        public BuilderType Type { get; set; }

        public string FileExtensions { get; set; } = "cs";
    }
    public enum BuilderType
    {
        Builder, GlobalBuilder
    }
}