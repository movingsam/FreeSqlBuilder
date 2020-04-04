using System;
using System.Collections.Generic;
using System.Text;
using FreeSql.Generator.Core.CodeFirst;
using GRES.Framework.Entity;

namespace IdentityServer4.Dapper.Entities
{
    /// <summary>
    /// API资源表
    /// </summary>
    [Service]
    public class ApiResource: EntityBase<int>,IEnabled
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; } = true;
        /// <summary>
        /// 资源名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 资源显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 资源描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 相关秘钥
        /// </summary>
        public List<ApiSecret> Secrets { get; set; }
        /// <summary>
        /// 相关域
        /// </summary>
        public List<ApiScope> Scopes { get; set; }
        /// <summary>
        /// 相关用户身份
        /// </summary>
        public List<ApiResourceClaim> UserClaims { get; set; }
    }
}
