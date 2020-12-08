using System;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositorio.Implementacoes
{
    public class RepositorioUser : RepositorioBase<UserEntity>, IRepositorioUser
    {
        private DbSet<UserEntity> _dataSet;

        public RepositorioUser(Contexto contexto) : base(contexto)
        {
            _dataSet = contexto.Set<UserEntity>();
        }

        public async Task<UserEntity> ConsulteLogin(string email)
        {
            try
            {
                return await _dataSet.FirstOrDefaultAsync(c => c.email.ToUpper() == email.ToUpper());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
