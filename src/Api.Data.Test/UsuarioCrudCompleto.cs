using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Api.Data.Context;
using Api.Data.Repositorio.Implementacoes;
using Api.Domain.Entities;

namespace Api.Data.Test
{
    public class UsuarioCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;

        public UsuarioCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "Crud de Usuario.")]
        [Trait("CRUD", "UserEntity")]
        public async Task E_Possivel_Realizar_Crud_Usuario()
        {
            using (var contexto = _serviceProvider.GetService<Contexto>())
            {
                var repositorio = new RepositorioUser(contexto);

                var userEntity = new UserEntity 
                {
                    nome = Faker.Name.FullName(),
                    email = Faker.Internet.Email()
                };

                //Inserção
                var registroCriado = await repositorio.SalvarAsync(userEntity);

                Assert.NotNull(registroCriado);
                Assert.Equal(userEntity.email, registroCriado.email);
                Assert.Equal(userEntity.nome, registroCriado.nome);
                Assert.False(registroCriado.id == Guid.Empty);


                userEntity.nome = Faker.Name.First();

                var registroAtualizado = await repositorio.AtualizeAsync(userEntity);

                Assert.NotNull(registroAtualizado);
                Assert.Equal(userEntity.email, registroAtualizado.email);
                Assert.Equal(userEntity.nome, registroAtualizado.nome);

                var registroExiste = await repositorio.ExistsAsync(registroAtualizado.id);
                Assert.True(registroExiste);

                var registroSelecionado = await repositorio.SelecioneAsync(registroAtualizado.id);
                Assert.NotNull(registroSelecionado);
                Assert.Equal(registroAtualizado.email, registroSelecionado.email);
                Assert.Equal(registroAtualizado.nome, registroSelecionado.nome);

                var todosRegistros = await repositorio.SelecioneListaAsync();
                Assert.NotNull(todosRegistros);
                Assert.True(todosRegistros.Count() > 1);

                var _removeu = await repositorio.DeleteAsync(registroSelecionado.id);
                Assert.True(_removeu);

                var usuarioPadrao = await repositorio.ConsulteLogin("maxwbs@gmail.com");
                Assert.NotNull(usuarioPadrao);
                Assert.Equal("maxwbs@gmail.com", usuarioPadrao.email);
                Assert.Equal("Administrador", usuarioPadrao.nome);

            }
        }
    }
}
