﻿using System;
using System.Collections.Generic;
using System.Text;
using FreeSql.Generator.Core.CodeFirst;

namespace IdentityServer4.Dapper.Entities
{
    [Service]
    public class PersistedGrant
    {
        public string Key { get; set; }
        public string Type { get; set; }
        public string SubjectId { get; set; }
        public string ClientId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? Expiration { get; set; }
        public string Data { get; set; }
    }
}
