//*******************************
// 创建者 UnKnow
// 创建日期 2020-08-10 00:17
// 创建引擎 FreeSqlBuilder
//*******************************
using System;
using TestEntity.Entities;
using System.Collections.Generic;


namespace Default.Dto
{
    
        ///<summary>
        /// Dto
        ///</summary>
    public class UserEntityDto
    {

        ///<summary>
        ///</summary>
        public  string Account { get; set; }


        ///<summary>
        ///</summary>
        public  string Password { get; set; }


        ///<summary>
        ///</summary>
        public  Gender Gender { get; set; }


        ///<summary>
        ///</summary>
        public  string NickName { get; set; }

        public IEnumerable<Role> Roles { get; set; }
    }
}