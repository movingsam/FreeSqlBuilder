using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FreeSql.DataAnnotations;
using FreeSqlBuilder.Core.Utilities;

namespace FreeSqlBuilder.Core.Entities
{
    [Index("uni_name_index", "Name,GeneratorMode", true)]
    public class GeneratorModeConfig : IKey<long>
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Column(IsPrimary = true, IsIdentity = true)]
        public long Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 生成器模式 CodeFirst根据实体代码来生成 DbFirst根据数据库生成代码
        /// </summary>
        [Column(IsNullable = false)]
        public GeneratorMode GeneratorMode { get; set; } = GeneratorMode.CodeFirst;
        /// <summary>
        /// 数据源Id
        /// </summary>
        public long DataSourceId { get; set; }
        /// <summary>
        /// 数据源
        /// </summary>
        public DataSource DataSource { get; set; }
        /// <summary>
        /// 实体源Id
        /// </summary>
        public long EntitySourceId { get; set; }
        /// <summary>
        /// 实体源
        /// </summary>
        public EntitySource EntitySource { get; set; }

        /// <summary>
        /// 选中模式
        /// </summary>
        public PickType PickType { get; set; }
        /// <summary>
        /// 项目对象
        /// </summary>
        public List<Project> Projects { get; set; }
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
        public List<string> IncludeTable => IncludeTables?.Split(",").ToList() ?? new List<string>();
        /// <summary>
        /// 忽略某些表
        /// </summary>
        [Column(IsIgnore = true)]
        public List<string> IgnoreTable => IgnoreTables?.Split(",").ToList() ?? new List<string>();

        public void Validate()
        {
            switch (GeneratorMode)
            {
                case GeneratorMode.DbFirst:
                    Check.CheckCondition(() => this.EntitySource != null || this.EntitySource.Id > 0, "DbFirst模式不能选择实体源");
                    break;
                case GeneratorMode.CodeFirst:
                    Check.CheckCondition(() => DataSourceId > 0 || this.DataSource != null, "CodeFirst模式不需要填写数据库信息");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum PickType
    {
        Pick, Ignore
    }
}
