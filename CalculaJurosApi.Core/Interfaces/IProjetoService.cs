using System.Threading.Tasks;

namespace CalculaJurosApi.Core.Interfaces
{
    public interface IProjetoService
    {
        Task<string> ObterUrlProjeto();
    }
}
