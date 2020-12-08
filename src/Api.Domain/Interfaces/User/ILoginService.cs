using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.User
{
    public interface ILoginService
    {
        Task<object> ConsulteLogin(LoginDto user);
    }
}
