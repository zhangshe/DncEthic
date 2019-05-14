using System.ComponentModel;

namespace DncEthic.WebAPI.SwaggerHelper
{
    /// <summary>
    /// API版本
    /// </summary>
    public enum ApiVersions
    {
        /// <summary>
        /// V1 版本
        /// </summary>
        [Description("版本1")]
        V1 = 1,
        /// <summary>
        /// V2 版本
        /// </summary>
        [Description("版本2")]
        V2 = 2,
    }
}
