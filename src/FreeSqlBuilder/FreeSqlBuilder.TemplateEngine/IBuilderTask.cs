using FreeSql.DatabaseModel;
using FreeSql.Internal.Model;
using FreeSqlBuilder.Core.Entities;

namespace FreeSqlBuilder.TemplateEngine
{
    public interface IBuilderTask
    {
        TableInfo[] AllTable { get; }
        TableInfo[] GAllTable { get; }
        DbTableInfo[] AllDbTable { get; }
        DbTableInfo CurrentDbTable { get; }
        TableInfo CurrentTable { get; }
        BuilderOptions CurrentBuilder { get; }
        string Author { get; }

    }
}