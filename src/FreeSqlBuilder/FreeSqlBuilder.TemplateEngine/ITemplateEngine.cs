using System.Threading.Tasks;

namespace FreeSqlBuilder.TemplateEngine
{
    public interface ITemplateEngine
    {
        Task<string> Render(BuildTask context, string viewPath);
    }
}