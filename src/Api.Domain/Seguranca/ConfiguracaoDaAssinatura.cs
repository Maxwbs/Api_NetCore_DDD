using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Api.Domain.Seguranca
{
    public class ConfiguracaoDaAssinatura
    {
        public SecurityKey key { get; set; }

        public SigningCredentials SigningCredentials { get; set; }

        public ConfiguracaoDaAssinatura()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}
