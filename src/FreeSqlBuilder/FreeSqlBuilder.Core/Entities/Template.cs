﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;

namespace FreeSqlBuilder.Core.Entities
{
    /// <summary>
    /// 模板
    /// </summary>
    [Index("template_name_index", nameof(TemplatePath), true)]
    public class Template : ITemplate, IKey<long>
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
        /// 模板类型
        /// 暂时根据所在文件夹判断
        /// </summary>
        public TemplateType TemplateType { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        [MaxLength(4000)]
        public string TemplateContent { get; set; }
        /// <summary>
        /// 生成的文件后缀
        /// </summary>
        public string FileExtension { get; set; } = "cs";
        /// <summary>
        /// 设置模板类型
        /// </summary>
        public void SetTemplateType()
        {
            this.TemplateType = TemplatePath.Contains("CodeFirst") ? TemplateType.CodeFirst :
                TemplatePath.Contains("DbFirst") ? TemplateType.DbFirst : TemplatePath.Contains("Global") ? TemplateType.Global : TemplateType.UnKnow;
        }
    }
    /// <summary>
    /// 模板类型
    /// </summary>
    public enum TemplateType
    {
        /// <summary>
        /// CodeFirst
        /// </summary>
        CodeFirst,
        /// <summary>
        /// DbFirst
        /// </summary>
        DbFirst,
        /// <summary>
        /// 全局
        /// </summary>
        Global,
        /// <summary>
        /// 未知
        /// </summary>
        UnKnow

    }
}