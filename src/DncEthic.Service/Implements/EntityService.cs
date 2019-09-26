using DncEthic.Repository;
using DncEthic.Repository.SqlSugar;
using DncEthic.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DncEthic.Service.Implements
{
  public  class EntityService:Repository<object>, IEntityService
    {
        private readonly IRepository<Entity> _repository;

        public EntityService(IRepository<Entity> repository)
        {
            _repository = repository;
        }
        public EntityService()
        {
        }
    }
}
