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


namespace Default.PageRequest
{
    
        ///<summary>
        /// -分页请求
        ///</summary>
    public class UserEntityPageRequest
    {
        public  long Id { get; set; }
        public  string Account { get; set; }
        public  string Password { get; set; }
        public  Gender Gender { get; set; }
        public  IEnumerable<Role> Roles { get; set; }
        public  string NickName { get; set; }
        
        ///<summary>
        /// 关键词
        ///</summary>
        public string Keyword { get; set; }
        
        ///<summary>
        /// 页号
        ///</summary>
        public int PageNumber { get; set; }
        
        ///<summary>
        /// 页码
        ///</summary>
        public int PageSize { get; set;}
     }
}


//*******************************
// 所有属性都带出来 
// 不需要的自行删除
//*******************************