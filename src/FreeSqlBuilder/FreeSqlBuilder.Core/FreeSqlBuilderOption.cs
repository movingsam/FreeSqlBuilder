using FreeSql;

namespace FreeSqlBuilder.Core
{
    /// <summary>
    /// 模板配置项
    /// </summary>
    public class FreeSqlBuilderOption
    {
        /// <summary>
        /// 默认模板路径
        /// </summary>
        public string DefaultTemplatePath { get; set; } = "RazorTemplate";
        /// <summary>
        /// Sqlite持久化地址
        /// </summary>
        public DbSet DbSet { get; set; } = new DbSet();
        /// <summary>
        /// 跳过反射的程序集
        /// </summary>
        public string SkipAssembly { get; set; }
    }
    /// <summary>
    /// 数据源
    /// </summary>
    public class DbSet
    {
        public DbSet()
        {
        }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DataType DbType { get; set; } = DataType.Sqlite;
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; } = "Data Source=fsbuilder.db;Version=3";
    }
}
