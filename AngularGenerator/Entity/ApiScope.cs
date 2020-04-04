using System;
using System.Collections.Generic;
using System.Text;
using GRES.Framework.Entity;

namespace IdentityServer4.Dapper.Entities
{
    public class ApiScope:EntityBase<int> 
    {
        /// <summary>
        /// 域名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 域显示名
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 域描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否必须
        /// </summary>
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        /// <summary>
        /// 是否显示在发现文档中
        /// </summary>
        public bool ShowInDiscoveryDocument { get; set; } = true;
        /// <summary>
        /// 
        /// </summary>
        public List<ApiScopeClaim> UserClaims { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ApiResource ApiResource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ApiResourceId { get; set; }
    }
}
