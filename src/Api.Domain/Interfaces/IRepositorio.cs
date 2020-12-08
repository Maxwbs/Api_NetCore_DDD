
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces
{
    public interface IRepositorio<T> where T : BaseEntity
    {
        Task<T> SalvarAsync(T item);
        Task<T> AtualizeAsync(T item);
        Task<bool> DeleteAsync(Guid id);
        Task<T> SelecioneAsync(Guid id);
        Task<IEnumerable<T>> SelecioneListaAsync();
        Task<bool> ExistsAsync(Guid id);
    }
}
