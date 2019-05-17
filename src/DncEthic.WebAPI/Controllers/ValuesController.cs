using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DncEthic.WebAPI.Log;
using DncEthic.WebAPI.SwaggerHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DncEthic.WebAPI.Controllers
{
    /// <summary>
    /// 测试API
    /// </summary>

    //[ApiExplorerSettings(GroupName = "Platform")]
    [Route("api/[controller]/[action]")]
    //[CustomRoute(ApiVersions.V2, ApiGroups.Platform)]
    [ApiController]

    public class ValuesController : ControllerBase
    {
            /// <summary>
            /// GET api/values
            /// </summary>
            /// <returns></returns>
            //[ApiExplorerSettings(GroupName = "Platform")]
        [CustomRoute(ApiVersions.V1,ApiGroups.Platform)]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            NLogger.Error("发生未处理异常！");
            try
            {
                object t = null;
                t.ToString();
            }
            catch (Exception ex)
            {
                NLogger.Error(ex.Message);
            }
            //NLogger.Log(NLog.LogLevel.Error, "11", "出错啦！", "测试");
            return new string[] { "value1", "value2" };

        }

        /// <summary>
        /// GET api/values/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[ApiExplorerSettings(GroupName = "Business")]
        [CustomRoute(ApiVersions.V2, ApiGroups.Platform)]
        //[HttpGet("{id}")]
        [HttpGet]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// POST api/values
        /// </summary>
        /// <param name="value"></param>

        [HttpPost]
        [AllowAnonymous]
        [CustomRoute(ApiVersions.V2, ApiGroups.Business)]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// PUT api/values/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [ApiExplorerSettings(GroupName = "Default")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// DELETE api/values/5
        /// </summary>
        /// <param name="id"></param>
        //[ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
