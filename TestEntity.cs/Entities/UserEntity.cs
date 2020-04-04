using System;
using System.Collections.Generic;
using System.Text;

namespace TestEntity.cs.Entities
{
    public class UserEntity : EntityBase
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public string NickName { get; set; }
    }
    public class Role :EntityBase{
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public enum Gender
    {
        Male, FeMale
    }
}
