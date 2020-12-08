using Api.Domain.Interfaces.User;
using Api.Service.Servicos;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependenciasInjetadas
{
    public class ConfigureDependenciasDeServicos
    {
        public static void ConfiguracaoDeDependenciaDeServicos(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserServicoImpl> ();
            services.AddTransient<ILoginService, LoginServicoImpl> ();
        }
    }
}
