using FreeSql.DataAnnotations;
using System;

namespace TestEntity.cs
{
    public abstract class EntityBase
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
    }
}
