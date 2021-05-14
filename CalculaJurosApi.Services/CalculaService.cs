using CalculaJurosApi.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CalculaJurosApi.Services
{
    public class CalculaService : ICalculaService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpclient;

        #region CalculaService
        public CalculaService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpclient = new HttpClient();
        }
        #endregion

        #region CalcularJuros
        public async Task<decimal> CalcularJuros(decimal valorInicial, int meses)
        {
            decimal taxaJuros = await ObterTaxaJuros();
            decimal valorFinal = (1 + taxaJuros);
            valorFinal = (decimal)Math.Pow((double)valorFinal, meses);
            valorFinal = valorInicial * valorFinal;
            valorFinal = decimal.Truncate(valorFinal * 100) / 100;

            return valorFinal;
        }
        #endregion

        #region ObterTaxaJuros
        private async Task<decimal> ObterTaxaJuros()
        {
            var url = _configuration.GetSection("urlApiTaxaJuros").Value;
            url = string.Concat(url, "/taxaJuros");
            var response = await _httpclient.GetAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(responseContent))
            {
                return decimal.Parse(responseContent.Replace('.', ','));
            }
            else
            {
                return decimal.Zero;
            }
        }
        #endregion
    }
}
