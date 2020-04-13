using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;

namespace FreeSqlBuilder.Core
{
    public interface ITemplate
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        string TemplateName { get; }
        /// <summary>
        /// 模板路径
        /// </summary>
        string TemplatePath { get; }
        /// <summary>
        /// 模板内容
        /// </summary>
        string TemplateContent { get; }
        string FileExtension { get; set; }
    }
    /// <summary>
    /// 模板
    /// </summary>
    [Index("template_name_index", nameof(TemplateName))]
    public class Template : ITemplate
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public long Id { get; set; }
        /// <summary>
        /// 构建配置
        /// </summary>
        public List<BuilderOptions> BuilderOptions { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>                    
        /// 模板路径                     
        /// </summary>                   
        public string TemplatePath { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        [MaxLength(4000)]
        public string TemplateContent { get; set; }
        /// <summary>
        /// 生成的文件后缀
        /// </summary>
        public string FileExtension { get; set; } = "cs";
    }
}