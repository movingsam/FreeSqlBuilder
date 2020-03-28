using System;
using System.Collections.Generic;
using System.Text;
using GRES.Framework.Entity;

namespace IdentityServer4.Dapper.Entities
{
    public abstract class UserClaim : EntityBase<int>
    {
        public string Type { get; set; }
    }
}
