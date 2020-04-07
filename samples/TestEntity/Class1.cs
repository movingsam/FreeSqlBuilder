using FreeSql.DataAnnotations;

namespace TestEntity
{
    public abstract class EntityBase
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public long Id { get; set; }
    }
}
