using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestEntity.cs.Entities
{
    public class UserEntity : EntityBase, IEntityBase<long>
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public string NickName { get; set; }
        [Column(IsIdentity = true, IsPrimary = true)]
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
