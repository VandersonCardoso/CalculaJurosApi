using CalculaJurosApi.Api.Controllers;
using CalculaJurosApi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CalculaJurosApi.Tests
{
    [TestFixture]
    public class CalculaTests
    {
        private Mock<ICalculaService> mockCalculaService;
        private Mock<IProjetoService> mockProjetoService;

        #region Setup
        [SetUp]
        public void Setup()
        {
            mockCalculaService = new Mock<ICalculaService>();
            mockProjetoService = new Mock<IProjetoService>();
        }
        #endregion

        #region GetCalculoJuros_ModelStateInvalid
        [Test]
        public async Task GetCalculoJuros_ModelStateInvalid()
        {
            CalculaController calculaController = new CalculaController(mockCalculaService.Object, mockProjetoService.Object);
            calculaController.ModelState.AddModelError("Key", "errorMessage");
            var result = await calculaController.GetCalculoJuros(decimal.Zero, 1);
            Assert.IsFalse(calculaController.ModelState.IsValid);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region GetCalculoJuros_BadRequest
        [Test]
        public async Task GetCalculoJuros_BadRequest()
        {
            mockCalculaService.Setup(calcula => calcula.CalcularJuros(It.IsAny<decimal>(), It.IsAny<int>())).ReturnsAsync(decimal.Zero);
            CalculaController calculaController = new CalculaController(mockCalculaService.Object, mockProjetoService.Object);
            var result = await calculaController.GetCalculoJuros(decimal.Zero, 5);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region GetCalculoJuros_Accepted
        [Test]
        public async Task GetCalculoJuros_Accepted()
        {
            mockCalculaService.Setup(calcula => calcula.CalcularJuros(It.IsAny<decimal>(), It.IsAny<int>())).ReturnsAsync(new decimal(105.10));
            CalculaController calculaController = new CalculaController(mockCalculaService.Object, mockProjetoService.Object);
            var result = await calculaController.GetCalculoJuros(new decimal(100), 5);
            Assert.IsInstanceOf<AcceptedResult>(result);
        }
        #endregion

        #region GetUrlProjeto_ModelStateInvalid
        [Test]
        public async Task GetUrlProjeto_ModelStateInvalid()
        {
            CalculaController calculaController = new CalculaController(mockCalculaService.Object, mockProjetoService.Object);
            calculaController.ModelState.AddModelError("Key", "errorMessage");
            var result = await calculaController.GetUrlProjeto();
            Assert.IsFalse(calculaController.ModelState.IsValid);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region GetUrlProjeto_BadRequest
        [Test]
        public async Task GetUrlProjeto_BadRequest()
        {
            mockProjetoService.Setup(proj => proj.ObterUrlProjeto()).ReturnsAsync(string.Empty);
            CalculaController calculaController = new CalculaController(mockCalculaService.Object, mockProjetoService.Object);
            var result = await calculaController.GetUrlProjeto();
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region GetUrlProjeto_Accepted
        [Test]
        public async Task GetUrlProjeto_Accepted()
        {
            mockProjetoService.Setup(proj => proj.ObterUrlProjeto()).ReturnsAsync("https://github.com/VandersonCardoso/CalculaJurosApi.git");
            CalculaController calculaController = new CalculaController(mockCalculaService.Object, mockProjetoService.Object);
            var result = await calculaController.GetUrlProjeto();
            Assert.IsInstanceOf<AcceptedResult>(result);
        }
        #endregion
    }
}