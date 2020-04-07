namespace FreeSqlBuilder.Core
{
    public class TemplateOptions
    {
        public string DefaultTemplatePath { get; set; } = "RazorTemplate";
        /// <summary>
        /// Sqlite持久化地址
        /// </summary>
        public string SqliteDbConnectionString { get; set; } = "Data Source=fsbuilder.db;Version=3";
    }
}
