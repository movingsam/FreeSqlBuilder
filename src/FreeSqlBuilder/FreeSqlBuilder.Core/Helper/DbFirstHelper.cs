using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreeSql.DatabaseModel;
using FreeSqlBuilder.Core.DbFirst;
using FreeSqlBuilder.Core.Entities;

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
            using IFreeSql fsql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(dto.DbType, dto.ConnectionString)
                .Build();
            var res = string.IsNullOrWhiteSpace(dto.Name) ? fsql.DbFirst.GetTablesByDatabase() : fsql.DbFirst.GetTablesByDatabase(dto.Name);
            return res;
        }

        public bool CheckDataSource(DataSource ds)
        {
            try
            {
                using IFreeSql fsql = new FreeSql.FreeSqlBuilder().UseConnectionString(ds.DbType, ds.ConnectionString)
                    .Build();
                return fsql.Ado.Query<bool>("select 1").FirstOrDefault();
            }
            catch (Exception e)
            {
                return false;
            }

        }
    }
}
