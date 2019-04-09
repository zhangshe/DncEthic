using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
namespace DncEthic.Core.Model
{
    /// <summary>
    /// 配置文件模型
    /// </summary>
    public class BaseConfigModel
    {
        /// <summary>
        /// 配置项
        /// </summary>
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// 绝对路径
        /// </summary>
        public static string ContentRootPath { get; set; }

        /// <summary>
        /// 相对路径
        /// </summary>
        public static string WebRootPath { get; set; }

        /// <summary>
        /// 对象赋值
        /// </summary>
        /// <param name="config"></param>
        /// <param name="contentRootPath"></param>
        /// <param name="webRootPath"></param>
        public static void SetBaseConfig(IConfiguration config, string contentRootPath, string webRootPath)
        {
            Configuration = config;
            ContentRootPath = contentRootPath;
            WebRootPath = webRootPath;
        }
    }
}
