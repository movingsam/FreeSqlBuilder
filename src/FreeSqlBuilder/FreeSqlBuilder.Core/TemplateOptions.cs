using FreeSql;

namespace FreeSqlBuilder.Core
{
    public class TemplateOptions
    {
        public string DefaultTemplatePath { get; set; } = "RazorTemplate";
        /// <summary>
        /// Sqlite持久化地址
        /// </summary>
        public DbSet DbSet { get; set; } = new DbSet();
    }

    public class DbSet
    {
        public DbSet()
        {
            this.DbType =  DataType.Sqlite;
            this.ConnectionString = "Data Source=fsbuilder.db;Version=3";
        }
        public DataType DbType { get; set; }
        public string ConnectionString { get; set; }
    }
}
