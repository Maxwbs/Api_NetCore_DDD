using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.User;
using Api.Domain.Repositorios;
using Api.Domain.Seguranca;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Servicos
{
    public class LoginServicoImpl : ILoginService
    {
        private IRepositorioUser _repositorioUser;

        private TokenConfiguracao _tokenConfiguracao;

        private ConfiguracaoDaAssinatura _configuracaoDaAssinatura;

        private IConfiguration _configuracao;

        public LoginServicoImpl(IRepositorioUser repositorioUser, 
                                TokenConfiguracao tokenConfiguracao, 
                                ConfiguracaoDaAssinatura configuracaoDaAssinatura, 
                                IConfiguration configuracao)
        {
            _repositorioUser = repositorioUser;
            _tokenConfiguracao = tokenConfiguracao;
            _configuracaoDaAssinatura = configuracaoDaAssinatura;
            _configuracao = configuracao;
        }

        public async Task<object> ConsulteLogin(LoginDto userDto)
        {
            var baseUser = new UserEntity();

            if (userDto != null && !string.IsNullOrWhiteSpace(userDto.email))
            {
                baseUser = await _repositorioUser.ConsulteLogin(userDto.email);
                if (baseUser == null)
                {
                    return new
                    {
                        authenticated = false,
                        message = "Falha ao autenticar"
                    };
                }
                else
                {
                    var identity = new ClaimsIdentity(
                        new GenericIdentity(baseUser.email),
                        new []
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, baseUser.email)
                        }
                    );

                    var dataDeCriacao = DateTime.Now;
                    var dataDeExpiracao  = dataDeCriacao + TimeSpan.FromSeconds(_tokenConfiguracao.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    var token = CreateToken(identity,dataDeCriacao, dataDeExpiracao, handler);
                    return SuccessObject(dataDeCriacao, dataDeExpiracao, token, baseUser);

                }
            }
            else
            {
                return new
                    {
                        authenticated = false,
                        message = "Falha ao autenticar"
                    };
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguracao.Issuer,
                Audience = _tokenConfiguracao.Audience,
                SigningCredentials = _configuracaoDaAssinatura.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate,
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime datadeCriacao, DateTime dataDeExpiracao, string token, UserEntity user)
        {
            return new
            {
                authenticated = true,
                create = datadeCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = dataDeExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                userName = user.email,
                name = user.nome,
                message = "Usuário Logado com sucesso"
            };
        }
    }
}
