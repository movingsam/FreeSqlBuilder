using System.Threading.Tasks;

namespace FreeSqlBuilder.TemplateEngine
{
    /// <summary>
    /// 模板引擎抽象接口
    /// </summary>
    public interface ITemplateEngine
    {
        Task<string> Render(BuildTask context, string viewPath);
    }
}