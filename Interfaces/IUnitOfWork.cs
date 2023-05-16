using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface IUnitOfWork
    {
        ICityRepository CityRepository {get; }

         IUserRepository UserRepository {get; }

         IPropertyRepository PropertyRepository {get; }

         IFurnishingTypeRepository FurnishingTypeRepository {get; }

         IPropertyTypeRepository PropertyTypeRepository {get; }

         Task<bool> SaveAsync();    
    }
}