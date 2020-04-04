using System;
using System.Collections.Generic;
using System.Text;
using GRES.Framework.Entity;
//using static IdentityServer4.IdentityServerConstants;

namespace IdentityServer4.Dapper.Entities
{
    public abstract class Secret : EntityBase<int>
    {
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }
        public string Type { get; set; } = "SharedSecret";
    }
}
