using System;
using Api.Data.Context;
using Api.Data.Repositorio;
using Api.Data.Repositorio.Implementacoes;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.User;
using Api.Domain.Repositorios;
using Api.Service.Servicos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependenciasInjetadas
{
    public class ConfigureDependenciasDeRepositorios
    {
        public static void ConfiguracaoDeDependenciaDeRepositorios(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositorio<>), typeof(RepositorioBase<>));
            services.AddScoped<IRepositorioUser, RepositorioUser>();

            if(Environment.GetEnvironmentVariable("DATABASE").ToLower() == "SQLSERVER".ToLower())
            {
                 services.AddDbContext<Contexto>
                (
                    options => options.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION"))
                );
            }
        }
    }
}
