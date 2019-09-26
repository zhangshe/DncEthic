using DncEthic.Service.Interfaces;
using DncEthic.WebAPI.SwaggerHelper;
using Microsoft.AspNetCore.Mvc;
using DncEthic.Core.Model;

namespace DncEthic.WebAPI.Controllers.PlatForm
{
    /// <summary>
    /// 数据库实体类操作DBFirst
    /// </summary>
    public class EntityController
    {
        private readonly IEntityService entityService;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_entityservice"></param>
        public EntityController(IEntityService _entityservice)
        {
            entityService = _entityservice;

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
