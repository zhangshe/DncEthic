using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DncEthic.WebAPI.Log
{
    /// <summary>
    /// 日志模型
    /// </summary>
    public class LogEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string S_Id { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        public string Application { get; set; }
        /// <summary>
        /// 级别  Trace|Debug|Info|Warn|Error|Fatal
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperatingTime { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string OperateUser { get; set; }
        /// <summary>
        /// 记录器的名字
        /// </summary>
        public string Logger { get; set; }
        /// <summary>
        /// 类名称、方法名称和相关信息的源信息
        /// </summary>
        public string CallSite { get; set; }
        /// <summary>
        /// 请求URL
        /// </summary>
        public string RequestUrl { get; set; }
        /// <summary>
        /// 方法名
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public string ExceptionInfo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }
}
