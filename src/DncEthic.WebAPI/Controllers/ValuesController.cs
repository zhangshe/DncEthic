using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DncEthic.WebAPI.Controllers
{
    /// <summary>
    /// 测试API
    /// </summary>
    [ApiExplorerSettings(GroupName = "G1")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// GET api/values
        /// </summary>
        /// <returns></returns>
        //[ApiExplorerSettings(GroupName ="G1")]
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
       // [ApiExplorerSettings(GroupName = "G2")]
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// POST api/values
        /// </summary>
        /// <param name="value"></param>
        //[ApiExplorerSettings(GroupName = "Default")]
        [HttpPost]
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
