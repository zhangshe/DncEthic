using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DncEthic.Core.Enums;
using DncEthic.WebAPI.MiddleWare;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DncEthic.WebAPI.Controllers
{
    /// <summary>
    /// 测试API
    /// </summary>
    //[CustomRoute(ApiVersions.V2, ApiGroups.Platform)]
    //[ApiExplorerSettings(GroupName = "Platform")]
    [Route("api/[controller]")]
    [ApiController]

    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// GET api/values
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(GroupName = "Platform")]
        //[CustomRoute(ApiVersions.V2,ApiGroups.Platform)]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// GET api/values/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ApiExplorerSettings(GroupName = "Business")]
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// POST api/values
        /// </summary>
        /// <param name="value"></param>
        [ApiExplorerSettings(GroupName = "Default")]
        [HttpPost]
        [AllowAnonymous]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// PUT api/values/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        //[ApiExplorerSettings(GroupName = "G1")]
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
