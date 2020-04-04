using System;
using System.Collections.Generic;
using System.Text;
using FreeSql.Generator.Core.CodeFirst;
using GRES.Framework.Entity;

namespace IdentityServer4.Dapper.Entities
{
    [Service]
    public class ClientCorsOrigin : EntityBase<int>
    {
        public string Origin { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
