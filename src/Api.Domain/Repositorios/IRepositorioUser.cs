using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces;

namespace Api.Domain.Repositorios
{
    public interface IRepositorioUser : IRepositorio<UserEntity>
    {
         Task<UserEntity> ConsulteLogin(string email);
    }
}
