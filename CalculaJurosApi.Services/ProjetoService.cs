using CalculaJurosApi.Core.Interfaces;
using System.Threading.Tasks;

namespace CalculaJurosApi.Services
{
    public class ProjetoService : IProjetoService
    {
        #region ObterUrlProjeto
        public async Task<string> ObterUrlProjeto()
        {
            try
            {
                return ThisAssembly.Git.RepositoryUrl;
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
