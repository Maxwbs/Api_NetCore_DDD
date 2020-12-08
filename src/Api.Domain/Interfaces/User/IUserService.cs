using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.User
{
    public interface IUserService
    {
         Task<UserDtoCreate> Get(Guid id);
         
         Task<IEnumerable<UserDtoCreate>> GetAll();

         Task<UserDtoCreateResult> Post(UserDtoCreate user);

         Task<UserDtoUpdateResult> Put(UserDtoUpdate user);

         Task<bool> Delete(Guid id);
    }
}
