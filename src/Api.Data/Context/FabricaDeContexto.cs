using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class FabricaDeContexto : IDesignTimeDbContextFactory<Contexto>
    {
        public Contexto CreateDbContext(string[] args)
        {
            // USADO PARA CRIAR MIGRAÇÕES
            string strCon = "Data Source=.\\SQLEXPRESS;Initial Catalog=CLI_API;Integrated Security=False;User ID=sa;Password=fpw;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;MultipleActiveResultSets=True;";
            var optionBuilder = new DbContextOptionsBuilder<Contexto>();
            optionBuilder.UseSqlServer(strCon);
            return new Contexto(optionBuilder.Options);
        }
    }

}
