using CalculaJurosApi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace CalculaJurosApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculaController : ControllerBase
    {
        private readonly ICalculaService _calculaService;
        private readonly IProjetoService _projetoService;

        #region CalculaController
        public CalculaController(ICalculaService calculaService, IProjetoService projetoService)
        {
            _calculaService = calculaService;
            _projetoService = projetoService;
        }
        #endregion

        #region GetCalculoJuros
        /// <summary alignment="right">
        /// Endpoint do cálculo de juros
        /// </summary>
        /// <remarks>
        /// Este endpoint tem como objetivo retornar o cálculo de juros.
        /// </remarks>
        /// <returns>Códigos de retorno para o endpoint calculaJuros</returns>
        /// <response code="202">Código de retorno caso o endpoint obtenha o cálculo de juros corretamente.</response>
        /// <response code="400">Código de retorno caso o conteúdo da requisição esteja divergente da especificação ou em caso de erro ao obter o cálculo de juros.</response>
        [HttpGet("/calculaJuros")]
        [ProducesResponseType(typeof(decimal), (int)HttpStatusCode.Accepted)]
        [ProducesResponseType(typeof(decimal), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCalculoJuros(decimal valorInicial, int meses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _calculaService.CalcularJuros(valorInicial, meses);

            if (response > 0)
            {
                return Accepted(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        #endregion

        #region GetUrlProjeto
        /// <summary alignment="right">
        /// Endpoint da url do projeto
        /// </summary>
        /// <remarks>
        /// Este endpoint tem como objetivo retornar url do projeto no github.
        /// </remarks>
        /// <returns>Códigos de retorno para o endpoint showmethecode</returns>
        /// <response code="202">Código de retorno caso o endpoint obtenha a url do projeto corretamente.</response>
        /// <response code="400">Código de retorno caso o conteúdo da requisição esteja divergente da especificação ou em caso de erro ao obter a url do projeto.</response>
        [HttpGet("/showmethecode")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Accepted)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUrlProjeto()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _projetoService.ObterUrlProjeto();

            if (!string.IsNullOrEmpty(response))
            {
                return Accepted(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        #endregion
    }
}
