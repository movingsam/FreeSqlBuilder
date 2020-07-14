using System;
using System.Collections.Generic;
using System.Text;
using FreeSql;

namespace FreeSqlBuilder.Core.DbFirst
{
    public class DbFirstDto
    {
        public DbFirstDto()
        {
        }

        public DbFirstDto(string name, DataType dbType, string connectionString,string[] dataBaseNames)
        {
            this.Name = name;
            this.DbType = dbType;
            this.ConnectionString = connectionString;
        }

        public string Name { get; set; }
        public DataType DbType { get; set; }
        public string ConnectionString { get; set; }
    }
}
