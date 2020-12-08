using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositorio
{
    public class RepositorioBase<T> : IRepositorio<T> where T : BaseEntity
    {
        protected readonly Contexto _contexto;
        private DbSet<T> _dataSet;

        public RepositorioBase(Contexto contexto)
        {
            _contexto = contexto;
            _dataSet = _contexto.Set<T>();
        }

        public async Task<T> SalvarAsync(T item)
        {
            try
            {
                if (item.id == Guid.Empty)
                {
                    item.id = Guid.NewGuid();
                }

                item.CreateAt = DateTime.UtcNow;
                _dataSet.Add(item);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return item;
        }

        public async Task<T> AtualizeAsync(T item)
        {
            try
            {
                var resultado = await _dataSet.SingleOrDefaultAsync(p => p.id == item.id);

                if (resultado == null)
                {
                    return null;
                }

                item.UpdateAt = DateTime.UtcNow;
                item.CreateAt = resultado.CreateAt;

                _contexto.Entry(resultado).CurrentValues.SetValues(item);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return item;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var resultado = await _dataSet.SingleOrDefaultAsync(p => p.id == id);

                if (resultado == null)
                {
                    return false;
                }

                _dataSet.Remove(resultado);

                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
        public async Task<T> SelecioneAsync(Guid id)
        {
            try
            {
                return await _dataSet.SingleOrDefaultAsync(p => p.id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> SelecioneListaAsync()
        {
            try
            {
                return await _dataSet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dataSet.AnyAsync(p => p.id == id);
        }
    }
}
