using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreeSql.DataAnnotations;
using FreeSql.Generator.Core.WordsConvert;

namespace FreeSql.Generator.Core
{
    [Table(Name ="Project")]
    public class Project : IOutPut, IKey<long>
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public long Id { get; set; }
        /// <summary>
        /// 项目名——命名空间
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 根目录
        /// </summary>
        public string RootPath { get; set; }
        /// <summary>
        /// 输出路径 默认Output
        /// </summary>
        public string OutPutPath { get; set; } = "Output";
        /// <summary>
        /// 生成器模式 CodeFirst根据实体代码来生成 DbFirst根据数据库生成代码
        /// </summary>
        public GeneratorMode GeneratorMode { get; set; } = GeneratorMode.CodeFirst;
        /// <summary>
        /// 实体基类名称 CodeFirst根据此项反射根据此项
        /// </summary>
        public string EntityBaseName { get; set; } = "EntityBase`1";
        /// <summary>
        /// 数据库信息
        /// </summary>
        public DataSource DataSource { get; set; }
        /// <summary>
        /// 数据库id
        /// </summary>
        public long DataSourceId { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 数据表选项
        /// </summary>
        public Entity Entity { get; set; }
        public string IncludeTables { get; set; }
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
        TKey Id { get; }
    }
}
