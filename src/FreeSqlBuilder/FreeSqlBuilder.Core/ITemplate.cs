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
        /// <summary>
        /// 文件后缀
        /// </summary>
        string FileExtension { get; set; }
    }
    
}