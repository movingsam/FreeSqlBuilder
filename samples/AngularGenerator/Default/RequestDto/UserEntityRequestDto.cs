//*******************************
// 创建者 UnKnow
// 创建日期 2020-08-10 00:17
// 创建引擎 FreeSqlBuilder
//*******************************

using System.Collections;
using System.Collections.Generic;
using FreeSql.DataAnnotations;
using System;
using TestEntity.Entities;
using System.Collections.Generic;


namespace Default.RequestDto
{
    
        ///<summary>
        /// Request
        ///</summary>
    public class UserEntityRequestDto
    {
        public  long Id { get; set; }

        public  string Account { get; set; }

        public  string Password { get; set; }

        public  Gender Gender { get; set; }

        public  IEnumerable<Role> Roles { get; set; }

        public  string NickName { get; set; }

     }
}