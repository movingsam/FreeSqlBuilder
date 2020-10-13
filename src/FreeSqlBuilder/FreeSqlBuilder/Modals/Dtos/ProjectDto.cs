using System;
using System.Collections.Generic;
using System.Linq;
using FreeSql.DataAnnotations;
using FreeSqlBuilder.Core.Entities;

namespace FreeSqlBuilder.Modals.Dtos
{
    /// <summary>
    /// 项目领域模型
    /// </summary>
    public class ProjectDto
    {
        public long Id { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public long ProjectInfoId { get; set; }
        /// <summary>
        /// 项目基本信息
        /// </summary>
        public ProjectInfo ProjectInfo { get; set; }
        /// <summary>
        /// 生成器模式Id
        /// </summary>
        public long GeneratorModeConfigId { get; set; }
        /// <summary>
        /// 生成器模式相关配置
        /// </summary>
        public GeneratorModeConfig GeneratorModeConfig { get; set; }
        /// <summary>
        /// 项目构建器中间表
        /// </summary>
        public virtual ICollection<ProjectBuilder> ProjectBuilders { get; set; }
        /// <summary>
        /// 单表构建器Id集合
        /// </summary>
        public List<long> BuildersId { get; set; }
        /// <summary>
        /// 全表构建器
        /// </summary>
        public ICollection<long> GlobalBuildersId { get; set; }

         

    }
}