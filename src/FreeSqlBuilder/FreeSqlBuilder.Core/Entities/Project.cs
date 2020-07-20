using System.Collections.Generic;
using System.Linq;
using FreeSql.DataAnnotations;

namespace FreeSqlBuilder.Core.Entities
{
    [Table(Name = "Project"), Index("union_name", nameof(Entities.ProjectInfo.NameSpace), true)]
    public class Project : IKey<long>
    {
        public Project() { }

        [Column(IsPrimary = true, IsIdentity = true)]
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
        /// 多表构建器
        /// </summary>
        public ICollection<BuilderOptions> Builders
        {
            get
            {
                if (ProjectBuilders == null || ProjectBuilders.Any(a => a.Builder == null)) return default;
                return ProjectBuilders?.Where(x => x.Builder.Type == BuilderType.Builder).Select(s => s.Builder).ToList();
            }
        }
        /// <summary>
        /// 单表构建器Id集合
        /// </summary>
        public List<long> BuildersId
        {
            get
            {
                if (ProjectBuilders == null || ProjectBuilders.Any(a => a.Builder == null)) return new List<long>();
                return ProjectBuilders.Where(x => x.Builder.Type == BuilderType.Builder).Select(x => x.BuilderId)
                    .ToList();
            }
        }

        /// <summary>
        /// 全表构建器
        /// </summary>
        public ICollection<BuilderOptions> GlobalBuilders
        {
            get
            {
                if (ProjectBuilders == null || ProjectBuilders.Any(a => a.Builder == null)) return new List<BuilderOptions>();
                return ProjectBuilders?.Where(x => x.Builder.Type == BuilderType.GlobalBuilder).Select(s => s.Builder)
                    .ToList();
            }
        }
        /// <summary>
        /// 全表构建器
        /// </summary>
        public ICollection<long> GlobalBuildersId
        {
            get
            {
                if (ProjectBuilders == null || ProjectBuilders.Any(a => a.Builder == null)) return default;
                return ProjectBuilders?.Where(x => x.Builder.Type == BuilderType.GlobalBuilder).Select(s => s.BuilderId)
                    .ToList();
            }
        }

    }

    public interface IKey<out TKey>
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        TKey Id { get; }
    }
}
