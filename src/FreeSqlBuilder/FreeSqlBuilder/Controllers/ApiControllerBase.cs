using System.Threading.Tasks;
using FreeSqlBuilder.Infrastructure.Exception;
using Microsoft.AspNetCore.Mvc;

namespace FreeSqlBuilder.Controllers
{
    /// <summary>
    /// api接口基类
    /// </summary>
    [Route("api/[controller]")]
    
    [ExceptionHandler]
    public class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="message">消息</param>
        protected virtual IActionResult Success(dynamic data = null, string message = null)
        {
            if (message == null)
                message = "成功";
            return new Result(StateCode.Ok, message, data);
        }

        /// <summary>
        /// 返回失败消息
        /// </summary>
        /// <param name="message">消息</param>
        protected virtual IActionResult Fail(string message)
        {
            return new Result(StateCode.Fail, message);
        }

    }

    /// <summary>
    /// 返回结果
    /// </summary>
    public class Result : JsonResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public StateCode Code { get; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; }
        /// <summary>
        /// 数据
        /// </summary>
        public dynamic Data { get; }

        /// <summary>
        /// 初始化返回结果
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        public Result(StateCode code, string message, dynamic data = null) : base(null)
        {
            Code = code;
            Message = message;
            Data = data;
        }

        /// <summary>
        /// 执行结果
        /// </summary>
        public override Task ExecuteResultAsync(ActionContext context)
        {
            Value = new
            {
                Code = Code,
                Message,
                Data
            };
            return base.ExecuteResultAsync(context);
        }
    }
    /// <summary>
    /// 状态码
    /// </summary>
    public enum StateCode
    {  /// <summary>
       /// 成功
       /// </summary>
        Ok = 1,
        /// <summary>
        /// 失败
        /// </summary>
        Fail = 2
    }
}