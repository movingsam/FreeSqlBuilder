using FreeSql.DataAnnotations;

namespace FreeSqlBuilder.Core.Entities
{
    /// <summary>
    /// 实体来源
    /// </summary>
    public class EntitySource : IKey<long>
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Column(IsPrimary = true, IsIdentity = true)]
        public long Id { get; set; }
        /// <summary>
        /// CodeFirst实体来源名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 从哪个程序集反射获取
        /// </summary>
        public string EntityAssemblyName { get; set; }
        /// <summary>
        /// 基类
        /// </summary>
        public string EntityBaseName { get; set; }
    }
}