using DncEthic.Repository;
using System;
using System.Text;

namespace DncEthic.Service.Interfaces
{
    /// <summary>
    /// 实体数据接口（DBFirst）
    /// </summary>
    public interface IEntityService:IRepository<Repository.SqlSugar.Entity>
    {

    }
}
