using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServer4.Dapper.Entities
{
    public class ApiResourceClaim : UserClaim
    {
        /// <summary>
        /// 关联Id
        /// </summary>
        public int ApiResourceId { get; set; }
        /// <summary>
        /// API资源导航
        /// </summary>
        public ApiResource ApiResource { get; set; }
    }
}
