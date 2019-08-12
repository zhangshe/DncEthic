using DncEthic.Core.Model;
using DncEthic.Service.Interfaces;
using DncEthic.WebAPI.Log;
using DncEthic.WebAPI.SwaggerHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
        private readonly IEntityService entityService;
        ///// <summary>
        ///// 初始化
        ///// </summary>
        ///// <param name="_entityservice"></param>

        public ValuesController(IEntityService _entityservice)
        {
            entityService = _entityservice;

        }
  
        /// <summary>
        /// GET api/values
        /// </summary>
        /// <returns></returns>
        //[ApiExplorerSettings(GroupName = "Platform")]
        [CustomRoute(ApiVersions.V1, ApiGroups.Platform)]
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

        #region 生成实体类
        /// <summary>
        /// 生成实体类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [CustomRoute(ApiVersions.V1, ApiGroups.Platform)]
        public JsonResult CreateEntity()
        {
            //return new JsonResult(entityService.CreateEntity(BaseConfigModel.ContentRootPath));
            string[] arr = BaseConfigModel.ContentRootPath.Split('\\');
            string baseFileProvider = "";
            for (int i = 0; i < arr.Length - 1; i++)
            {
                baseFileProvider += arr[i];
                baseFileProvider += "\\";
            }
            string filePath = baseFileProvider + "DncEthic.Domain";
            return new JsonResult(entityService.CreateEntity(filePath));
        }
        #endregion
    }
}
