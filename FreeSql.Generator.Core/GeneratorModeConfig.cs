using FreeSql.DataAnnotations;
using FreeSql.Generator.Core.CodeFirst;
using System.Collections.Generic;
using System.Linq;

namespace FreeSql.Generator.Core
{
    public class GeneratorModeConfig : IKey<long>
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public long Id { get; set; }
        /// <summary>
        /// 项目
        /// </summary>
        public Project Project { get; set; }
        /// <summary>
        /// 项目id
        /// </summary>
        [Column(IsNullable = false)]
        public long ProjectId { get; set; }
        public string EntityAssemblyName { get; set; }
        ///// <summary>
        ///// 数据表选项
        ///// </summary>
        //public Entity Entity { get; set; }
        ///// <summary>
        ///// 数据库表选项
        ///// </summary>
        //public long EntityId { get; set; }
        /// <summary>
        /// 生成器模式 CodeFirst根据实体代码来生成 DbFirst根据数据库生成代码
        /// </summary>
        [Column(IsNullable = false)]
        public GeneratorMode GeneratorMode { get; set; } = GeneratorMode.CodeFirst;
        /// <summary>
        /// 基类
        /// </summary>
        public string EntityBaseName { get; set; } = "EntityBase`1";
        /// <summary>
        /// 数据库信息
        /// </summary>
        public DataSource DataSource { get; set; }
        public long DataSourceId { get; set; }
        /// <summary>
        /// 只生成某些表
        /// </summary>
        public string IncludeTables { get; set; }
        /// <summary>
        /// 忽略某些表
        /// </summary>
        public string IgnoreTables { get; set; }
        /// <summary>
        /// 只生成某些表
        /// </summary>
        [Column(IsIgnore = true)]
        public List<string> IncludeTable => IncludeTables?.Split(",").ToList();
        /// <summary>
        /// 忽略某些表
        /// </summary>
        [Column(IsIgnore = true)]
        public List<string> IgnoreTable => IgnoreTables?.Split(",").ToList();

    }
}
