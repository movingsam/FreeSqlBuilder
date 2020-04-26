using System;
using System.Collections.Generic;
using System.Text;
using FreeSql.DatabaseModel;
using FreeSqlBuilder.Core.DbFirst;

namespace FreeSqlBuilder.Core.Helper
{
    public class DbFirstHelper
    {
        public DbFirstHelper()
        {

        }
        /// <summary>
        /// 获取相关数据库所有表结构
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<DbTableInfo> GetAllTable(DbFirstDto dto)
        {
            IFreeSql fsql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(dto.DbType, dto.ConnectionString)
                .Build();
            return fsql.DbFirst.GetTablesByDatabase();
        }
    }
}
