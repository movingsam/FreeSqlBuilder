using FreeSql.DataAnnotations;

namespace TestEntity.Entities
{
    public class UserEntity : EntityBase, IEntityBase<long>
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public string NickName { get; set; }
    }

    public class EntityBase
    {
        public long Id { get; set; }
    }

    public class Role : EntityBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public enum Gender
    {
        Male, FeMale
    }
    public interface IEntityBase<out TKey>
    {
        TKey Id { get; }
    }
}
