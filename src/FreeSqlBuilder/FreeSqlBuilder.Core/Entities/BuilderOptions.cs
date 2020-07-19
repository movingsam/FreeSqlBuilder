﻿using System.Collections.Generic;
using System.Linq;
using FreeSql.DataAnnotations;
using FreeSqlBuilder.Core.Utilities;
using FreeSqlBuilder.Core.WordsConvert;
using Newtonsoft.Json;

namespace FreeSqlBuilder.Core.Entities
{
    public class BuilderOptions : IKey<long>, IPrefix, IOutPut, IConvertMode, ISuffix
    {
        public BuilderOptions()
        {

        }

        public BuilderOptions(
            string name,
            string outputPath,
            string preFix = "",
            string suffix = "",
            ConvertMode mode = ConvertMode.None,
            BuilderType type = BuilderType.Builder
            )
        {
            Name = name;
            OutPutPath = outputPath;
            Prefix = preFix;
            Suffix = suffix;
            Mode = mode;
            Type = type;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public long Id { get; set; }

        /// <summary>
        /// 拓展选项
        /// </summary>
        [Column(IsIgnore = true)]

        public IDictionary<string, string> ExtensionOptions => string.IsNullOrWhiteSpace(StrExtensionOptions) ? null :
            JsonConvert.DeserializeObject<IDictionary<string, string>>(StrExtensionOptions);
        /// <summary>
        /// 扩展选项入库字段
        /// </summary>
        public string StrExtensionOptions { get; set; }
        /// <summary>
        /// 构建器名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; set; }
        /// <summary>
        /// 输出路径
        /// </summary>
        public string OutPutPath { get; set; }
        /// <summary>
        /// 名称转换模式
        /// </summary>
        public ConvertMode Mode { get; set; }
        /// <summary>
        /// 模板
        /// </summary>
        public Template Template { get; set; }
        /// <summary>
        /// 模板ID
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
        /// <summary>
        /// 生成文件的类型
        /// </summary>
        public string FileExtensions { get; set; } = "cs";
        /// <summary>
        /// 快速生成使用的配置
        /// </summary>
        public long FastConfigId { get; set; }
        /// <summary>
        /// 配置对象
        /// </summary>
        public GeneratorModeConfig Config { get; set; }

        /// <summary>
        /// 项目构建器中间表
        /// </summary>
        public virtual ICollection<ProjectBuilder> ProjectBuilders { get; set; }
        /// <summary>
        /// 项目
        /// </summary>
        public virtual ICollection<Project> Projects
        {
            get
            {
                if (ProjectBuilders == null) return default;
                return ProjectBuilders?.Select(x => x.Project).ToList();
            }
        }
        /// <summary>
        /// 验证
        /// </summary>
        public void Validate()
        {
            Check.CheckCondition(() => !(TemplateId > 0), "模板必须选择 否则无法生成代码");
        }
    }
    /// <summary>
    /// 构建器类型
    /// </summary>
    public enum BuilderType
    {
        /// <summary>
        /// 单表构建器
        /// </summary>
        Builder,
        /// <summary>
        /// 全表构建器
        /// </summary>
        GlobalBuilder
    }
}