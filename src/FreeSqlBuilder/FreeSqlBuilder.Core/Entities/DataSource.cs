﻿using System.Collections.Generic;
using System.Linq;
using FreeSql;
using FreeSql.DataAnnotations;

namespace FreeSqlBuilder.Core.Entities
{
    [Index("fk_datasource_name_index", nameof(Name))]
    public class DataSource
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public long Id { get; set; }
        /// <summary>
        /// 数据源名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// MySql,
        /// SqlServer,
        /// PostgreSQL,
        /// Oracle,
        /// Sqlite,
        /// OdbcOracle,
        /// OdbcSqlServer,
        /// OdbcMySql,
        /// OdbcPostgreSQL,
        /// <summary>
        /// 通用的 Odbc 实现，只能做基本的 Crud 操作 <para></para>
        /// 不支持实体结构迁移、不支持分页（只能 Take 查询） <para></para>
        /// 
        /// 通用实现为了让用户自己适配更多的数据库，比如连接 mssql 2000、db2 等数据库<para></para>
        /// 默认适配 SqlServer，可以继承后重新适配 FreeSql.Odbc.Default.OdbcAdapter，最好去看下代码 <para></para> 
        /// 适配新的 OdbcAdapter，请在 FreeSqlBuilder.Build 之后调用 IFreeSql.SetOdbcAdapter 方法设置
        /// </summary>Odbc,
        /// <summary>武汉达梦数据库有限公司，基于 Odbc 的实现</summary>
        /// OdbcDameng,
        /// <summary>Microsoft Office Access 是由微软发布的关联式数据库管理系统</summary>
        /// MsAccess,
        /// <summary>武汉达梦数据库有限公司，基于 DmProvider.dll 的实现</summary>
        /// Dameng,
        /// </summary>
        public DataType DbType { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DataBaseName { get; set; }
        /// <summary>
        /// 选中的数据库
        /// </summary>
        [Column(IsIgnore = true)]
        public string[] DataBaseNames => DataBaseName?.Split(',');
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 生成器配置
        /// </summary>
        public ICollection<GeneratorModeConfig> GeneratorModeConfigs { get; set; }
    }
}
