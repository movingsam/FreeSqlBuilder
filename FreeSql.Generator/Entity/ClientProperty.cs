using System;
using System.Collections.Generic;
using System.Text;
using GRES.Framework.Entity;

namespace IdentityServer4.Dapper.Entities
{
    public class ClientProperty : EntityBase<int>
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
