using System.Threading.Tasks;
using FreeSql.Generator.Core;

namespace FreeSql.TemplateEngine
{
    public interface ITemplateEngine
    {
        Task<string> Render(BuildTask context, string viewPath);
    }
}