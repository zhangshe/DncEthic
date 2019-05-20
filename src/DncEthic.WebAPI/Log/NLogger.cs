using NLog;
using NLog.Config;
using System;
using System.IO;

namespace DncEthic.WebAPI.Log
{
    /// <summary>
    /// 日志操作类
    /// </summary>
    public static class NLogger
    {
        #region 初始化
        /// <summary>
        /// 数据错误无法获取用户时使用
        /// </summary>
        public static string DefaultUser = "system";
        /// <summary>
        /// 默认地址
        /// </summary>
        public static string DefaultIP = "127.0.0.1";

        private static ILogger logger = GetLogger();

        private static ILogger GetLogger()
        {
            LogManager.Configuration = new XmlLoggingConfiguration(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"\Nlog.config"));
            return LogManager.GetCurrentClassLogger();
        }
        #endregion

        #region Nlog默认方法

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="debug"></param>
        public static void Debug(this string debug)
        {
            logger.Debug(debug);
        }
        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="info"></param>
        public static void Info(this string info)
        {
            logger.Info(info);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="warn"></param>
        public static void Warn(this string warn)
        {
            logger.Warn(warn);
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="error"></param>
        public static void Error(this string error)
        {
            logger.Error(error);
        }
        /// <summary>
        /// 严重错误
        /// </summary>
        /// <param name="fatal"></param>
        public static void Fatal(this string fatal)
        {
            logger.Fatal(fatal);
        }

        /// <summary>
        /// 跟踪
        /// </summary>
        /// <param name="trace"></param>
        public static void Trace(this string trace)
        {
            logger.Trace(trace);
        }

        #endregion

        ///// <summary>
        ///// NLog写日志
        ///// </summary>
        //public static void Log(LogLevel level,string user,string mesessage,string remark)
        //{
        //    LogEventInfo lei = new LogEventInfo();
        //    lei.Properties["@operateuser"] =  string.IsNullOrEmpty(user) ? DefaultUser : user;
        //    lei.Properties["@message"] = mesessage;
        //    lei.Properties["@level"] = level.ToString();
        //    lei.Level = level;
        //    logger.Log(lei);
        //}
    }
}
