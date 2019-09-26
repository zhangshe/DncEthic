using DncEthic.Repository;
using DncEthic.Repository.SqlSugar;
using System;
using System.Text;

namespace DncEthic.Service.Interfaces
{
    /// <summary>
    /// 实体数据接口（DBFirst）
    /// </summary>
    public interface IEntityService: IRepository<object>
    {
    }
}
