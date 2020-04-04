using System;
using System.Collections.Generic;
using System.Text;
using GRES.Framework.Entity;

namespace IdentityServer4.Dapper.Entities
{
    public class ClientIdPRestriction : EntityBase<int>
    {
        public string Provider { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
