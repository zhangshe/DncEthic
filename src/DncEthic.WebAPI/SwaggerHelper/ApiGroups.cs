using System.ComponentModel;

namespace DncEthic.WebAPI.SwaggerHelper
{
    /// <summary>
    /// API分组
    /// </summary>
    public enum ApiGroups
    {
        /// <summary>
        /// API文档-未分组
        /// </summary>
        [Description("API文档-未分组")]
        Default = 0,
        /// <summary>
        /// API文档-平台
        /// </summary>
        [Description("API文档-平台")]
        Platform = 1,
        /// <summary>
        /// API文档-业务
        /// </summary>
        [Description("API文档-业务")]
        Business = 2,
    }
}
