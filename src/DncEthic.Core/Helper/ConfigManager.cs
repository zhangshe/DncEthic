using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace DncEthic.Core.Helper
{
    public static class ConfigManager
    {
        public static IConfiguration Configuration = null;
        static ConfigManager()
        {
            //ReloadOnChange = true 当appsettings.json被修改时重新加载
            Configuration = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true, Optional=true })//请注意要把当前appsetting.json 文件->右键->属性->复制到输出目录->始终复制
            .Build();
            //Configuration = new ConfigurationBuilder()
            //  .SetBasePath(Directory.GetCurrentDirectory())
            //  .AddJsonFile("appsettings.json", true, true)
            //  .Build();
        }
        public static void SetConfiguration(IConfiguration _configuration)
        {
            if (Configuration == null)
                Configuration = _configuration;
        }

        /// <summary>
        /// 获取配置项 ConfigManager.SetConfiguration(new string[] { "AppSettings", "RedisCaching", "ConnectionString" });
        /// </summary>
        /// <param name="sections"></param>
        /// <returns></returns>
        public static string SetConfiguration(params string[] sections)
        {
            try
            {
                var val = string.Empty;
                for (int i = 0; i < sections.Length; i++)
                {
                    val += sections[i] + ":";
                }

                return Configuration[val.TrimEnd(':')];
            }
            catch (Exception)
            {
                return "";
            }

        }
    }
}
