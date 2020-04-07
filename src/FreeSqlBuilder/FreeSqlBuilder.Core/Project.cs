using System.Collections.Generic;
using FreeSql.DataAnnotations;

namespace FreeSqlBuilder.Core
{
    [Table(Name = "Project")]
    public class Project : IKey<long>
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public long Id { get; set; }
        /// <summary>
        /// 项目基本信息
        /// </summary>
        public ProjectInfo ProjectInfo { get; set; }
        public long ProjectInfoId { get; set; }
        /// <summary>
        /// 生成器模式相关配置
        /// </summary>
        public GeneratorModeConfig GeneratorModeConfig { get; set; }

        public long GeneratorModeConfigId { get; set; }
        /// <summary>
        /// 多表构建器
        /// </summary>
        public List<BuilderOptions> Builders { get; set; } = new List<BuilderOptions>();
        /// <summary>
        /// 全表构建器
        /// </summary>
        public List<BuilderOptions> GlobalBuilders { get; set; } = new List<BuilderOptions>();

    }

    public interface IKey<out TKey>
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        TKey Id { get; }
    }
}
