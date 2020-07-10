using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;
using System.Threading.Tasks;
using FreeSqlBuilder.Controllers;

namespace FreeSqlBuilder.Infrastructure.Exception
{
    /// <summary>
    /// 异常处理过滤器
    /// </summary>
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 异常处理
        /// </summary>
        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = 200;
            var error = context.Exception;
            context.Result = new Result(StateCode.Fail, error.Message);
        }
    }
}