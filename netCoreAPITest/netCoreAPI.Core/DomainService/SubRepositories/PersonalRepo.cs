using netCoreAPI.Core.ApplicationService;
using netCoreAPI.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace netCoreAPI.Core.DomainService.SubRepositories
{
    public class PersonalRepo
    {
        private readonly IUnitOfWork _uow;

        public PersonalRepo(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Personal Add(Personal data)//different way set to db
        {
            var entity = _uow.Db<Personal>().Add(data);
            return entity.Entity;
        }

        public Personal Delete(Personal data)//different way set to db
        {
            var entity = _uow.Db<Personal>().Delete(data);
            return entity.Entity;
        }

        public Personal Update(Personal data)//different way set to db
        {
            var entity = _uow.Db<Personal>().Update(data);
            return entity.Entity;
        }

        public IEnumerable<Personal> GetAll()
        {
            return _uow.Db<Personal>().All().AsEnumerable();
        }
    }
}