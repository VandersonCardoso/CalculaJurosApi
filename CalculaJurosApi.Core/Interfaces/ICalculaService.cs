using System.Threading.Tasks;

namespace CalculaJurosApi.Core.Interfaces
{
    public interface ICalculaService
    {
        Task<decimal> CalcularJuros(decimal valorInicial, int meses);
    }
}
