using System;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public abstract class BaseTest
    { 
        public BaseTest()
        {
            
        }
    }

    public class DbTeste : IDisposable
    {
        private string dataBaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
        public ServiceProvider ServiceProvider { get; private set; }

        public DbTeste()
        {
            var serviceCollection = new ServiceCollection();
            string strCon = $"Data Source=.\\SQLEXPRESS;Initial Catalog={dataBaseName};Integrated Security=False;User ID=sa;Password=fpw;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;MultipleActiveResultSets=True;";
            serviceCollection.AddDbContext<Contexto>(o =>
                o.UseSqlServer(strCon),
                  ServiceLifetime.Transient
            );

            ServiceProvider = serviceCollection.BuildServiceProvider();
            using (var context = ServiceProvider.GetService<Contexto>())
            {
                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<Contexto>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
