using System.Net;
using System.Net.Http.Json; // api do tipo resh, retorna respostas em json
using EcommerceApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace EcommerceApi.Tests
{ 
    [TestClass]
    public class PagamentoIntegracaoTests //retirei o sealed class pois ele nao permite herança
    {
        private static WebApplicationFactory<Program> _factory = null!; 
        private static HttpClient _client = null!;

        [ClassInitialize]
        public void Initializar(TestContext context)
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [ClassCleanup]
        public static void Finalizar()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [TestMethod]
        public async Task DeveProcessarPagamentoComSucesso()
        {
            //arrange
            var pagamento = new PagamentoRequest
            {
                PedidoId = 1,
                Valor = "10000",
                Moeda = "BRL",
                MetodoPagamento = "Cartao"
               
            };

            //act 
            var response = await _client.PostAsJsonAsync(
                "/api/pagamento", pagamento);

            //assert 
            Assert.AreEqual(
                HttpStatusCode.OK, response.StatusCode);
        }
    }
}
