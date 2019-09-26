using DncEthic.WebAPI.Log;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DncEthic.WebAPI.Filter
{
    /// <summary>
    /// 全局异常过滤器
    /// </summary>
    public class GlobalExceptionFilter: ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _env;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public GlobalExceptionFilter(IHostingEnvironment env)
        {
            _env = env;
        }
        /// <summary>
        /// 异常过滤器
        /// </summary>
        /// <param name="context"></param>
        public new void OnException(ExceptionContext context)
        {
            var json = new JsonErrorResponse();
            //这里面是自定义的操作记录日志
            //if (context.Exception.GetType() == typeof(UserOperationException))
            //{
            //    json.Message = context.Exception.Message;
            //    if (_env.IsDevelopment())
            //    {
            //        json.DevelopmentMessage = context.Exception.StackTrace;//堆栈信息
            //    }
            //    context.Result = new BadRequestObjectResult(json);//返回异常数据
            //}
            json.Message = "发生了未知内部错误"+ context.Exception.Message;//错误信息
            if (_env.IsDevelopment())
            {
                json.DevelopmentMessage = context.Exception.StackTrace;//堆栈信息
            }
            context.Result = new InternalServerErrorObjectResult(json);
            //获取客户端Ip
            string clientIP = context.HttpContext?.Connection.RemoteIpAddress.ToString();
            //获取httpmethod
            string strHttpMethod = context.HttpContext.Request.Method.ToString();
            //请求的url
            string url =context.HttpContext.Request.Path;
            //异常信息
            string exceptionMsg = context.Exception.Message;
            //异常定位
            string exceptionPosition = context.Exception.StackTrace.Split(new string[] { "\r\n" }, StringSplitOptions.None).Where(s => !string.IsNullOrWhiteSpace(s)).First().Trim();
            //string stack
            //记录的message
            string message = $"----1.[客户端Ip]：{ clientIP} " + Environment.NewLine + $"----2.[请求方法]：{ strHttpMethod} " + Environment.NewLine + $"----3.[请求url]：{ url }" + Environment.NewLine + $"----4.[异常信息]：{exceptionMsg} " + Environment.NewLine + $"----5.[异常定位]：{exceptionPosition}";

            //nlog记录
            //NLogger.Error(ErrorLog(message, context.Exception));

         
        }


        /// <summary>
        /// 自定义返回格式
        /// </summary>
        /// <param name="throwMsg"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public string ErrorLog(string throwMsg, Exception ex)
        {
            string errorMsg = string.Format("【异常相关信息】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3}", new object[] { throwMsg,
                ex.GetType().Name, ex.Message, ex.StackTrace });
            errorMsg = errorMsg.Replace("\r\n", "<br>");
            errorMsg = errorMsg.Replace("位置", "<strong style=\"color:red\">位置</strong>");
            return errorMsg;
        }
    }
    /// <summary>
    /// 异常码
    /// </summary>
    public class InternalServerErrorObjectResult : ObjectResult
    {
        /// <summary>
        /// 异常错误码
        /// </summary>
        /// <param name="value"></param>
        public InternalServerErrorObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
    /// <summary>
    /// 返回错误信息
    /// </summary>
    public class JsonErrorResponse
    {
        /// <summary>
        /// 生产环境的消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 开发环境的消息
        /// </summary>
        public string DevelopmentMessage { get; set; }
    }
}
