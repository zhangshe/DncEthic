using DncEthic.Repository;
using DncEthic.Repository.SqlSugar;
using DncEthic.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DncEthic.Service.Implements
{
  public  class EntityService : Repository<Entity>, IEntityService
    {
        //private readonly IRepository<Entity> EntityRepository;

        //public EntityService(IRepository<Entity> repository)
        //{
        //    EntityRepository = repository;
        //}
    }
}
