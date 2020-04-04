using System;
using FreeSql.DataAnnotations;

namespace GRES.Framework.Entity
{
    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class EntityBase<TKey> : IEntityBase<TKey>
    {
        protected EntityBase()
        {
        }
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Column(IsPrimary = true, IsIdentity = true)]
        public TKey Id { get; set; }
    }
}
