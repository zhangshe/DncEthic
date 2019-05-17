using System.ComponentModel;

namespace DncEthic.Core.Enums
{
    /// <summary>
    /// 接口返回枚举
    /// </summary>
    public enum ApiStatusEnum
    {
        /// <summary>
        /// 请求(或处理)成功
        /// </summary>
        [Description("请求(或处理)成功")]
        Status = 200, //请求(或处理)成功

        /// <summary>
        /// 请求(或处理)成功
        /// </summary>
        [Description("请求(或处理)错误")]
        Failure = 203, //请求(或处理)成功

        /// <summary>
        /// 内部请求出错
        /// </summary>
        [Description("内部请求出错")]
        Error = 500, //内部请求出错

        /// <summary>
        /// 未授权标识
        /// </summary>
        [Description("未授权标识")]
        Unauthorized = 401,//未授权标识

        /// <summary>
        /// 请求参数不完整或不正确
        /// </summary>
        [Description("请求参数不完整或不正确")]
        ParameterError = 400,//请求参数不完整或不正确

        /// <summary>
        /// 请求TOKEN失效
        /// </summary>
        [Description("请求TOKEN失效")]
        TokenInvalid = 403,//请求TOKEN失效

        /// <summary>
        /// HTTP请求类型不合法
        /// </summary>
        [Description("HTTP请求类型不合法")]
        HttpMehtodError = 405,//HTTP请求类型不合法

        /// <summary>
        /// HTTP请求不合法,请求参数可能被篡改
        /// </summary>
        [Description("HTTP请求不合法,请求参数可能被篡改")]
        HttpRequestError = 406,//HTTP请求不合法

        /// <summary>
        /// 该URL已经失效
        /// </summary>
        [Description("该URL已经失效")]
        URLExpireError = 407,//HTTP请求不合法
    }
}
