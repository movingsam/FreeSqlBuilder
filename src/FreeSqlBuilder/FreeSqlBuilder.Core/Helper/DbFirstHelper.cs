using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreeSql.DatabaseModel;
using FreeSqlBuilder.Core.DbFirst;
using FreeSqlBuilder.Core.Entities;

namespace FreeSqlBuilder.Core.Helper
{
    public static class DbFirstHelper
    {

        /// <summary>
        /// 获取相关数据库所有表结构
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<DbTableInfo> GetAllTable(this DbFirstDto dto)
        {
            using IFreeSql fsql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(dto.DbType, dto.ConnectionString)
                .Build(); 
            var res = fsql.DbFirst.GetTablesByDatabase();
            return res;
        }
        /// <summary>
        /// 数据库检测
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static bool CheckDataSource(this DataSource ds)
        {
            using IFreeSql fsql = new FreeSql.FreeSqlBuilder().UseConnectionString(ds.DbType, ds.ConnectionString)
                .Build();
            return fsql.Ado.Query<bool>("select 1").FirstOrDefault();

        }
    }
}
