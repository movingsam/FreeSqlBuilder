//*******************************
// 创建者 UnKnow
// 创建日期 2020-08-10 00:17
// 创建引擎 FreeSqlBuilder
//*******************************using System.Collections;
using System.Collections.Generic;
using FreeSql.DataAnnotations;
using Default.Dto;
using System;
using TestEntity.Entities;
using System.Collections.Generic;


namespace Default.PageViewDto
{
    
        ///<summary>
        ///</summary>
    public class UserEntityPageViewDto
    {
        public UserEntityPageViewDto (IEnumerable<UserEntityDto> datas,long total,int pageNumber=1,int pageSize=10)
        {
            this.Datas = datas;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Total = total;
        }
        
        ///<summary>
        /// 分页数据
        ///</summary>
        public IEnumerable<UserEntityDto> Datas { get; set;}
        
        ///<summary>
        /// 页号
        ///</summary>
        public int PageSize { get; set;}
        
        ///<summary>
        /// 页码
        ///</summary>        
        public int PageNumber { get; set;}
        
        ///<summary>
        /// 总数
        ///</summary>
        public long Total { get; set;}
    }
}