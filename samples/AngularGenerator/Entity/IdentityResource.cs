using System;
using System.Collections.Generic;
using System.Text;
using FreeSql.Generator.Core.CodeFirst;
using GRES.Framework.Entity;

namespace IdentityServer4.Dapper.Entities
{
    [Service]
    public class IdentityResource : EntityBase<int>
    {
        public bool Enabled { get; set; } = true;
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;
        public List<IdentityClaim> UserClaims { get; set; }
    }
}
