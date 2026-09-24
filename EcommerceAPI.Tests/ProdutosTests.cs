using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace CatalogoProdutos.Tests
{
    public class ProdutosTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ProdutosTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        // 1. Testa se a página de produtos carrega sem dar erro técnico
        [Fact]
        public async Task Teste1_PaginaProdutos_DeveCarregarComSucesso()
        {
            var cliente = _factory.CreateClient();
            var resposta = await cliente.GetAsync("/Produtos");
            resposta.EnsureSuccessStatusCode();
        }

        // 2. Testa se o título principal aparece na tela
        [Fact]
        public async Task Teste2_PaginaProdutos_DeveTerOTituloCerto()
        {
            var cliente = _factory.CreateClient();
            var resposta = await cliente.GetAsync("/Produtos");
            var textoNaTela = await resposta.Content.ReadAsStringAsync();

            Assert.Contains("Catálogo de Produtos", textoNaTela);
        }

        // 3. DESAFIO 1: Testa se digitar uma página que não existe dá o erro certo (404 Not Found)
        [Fact]
        public async Task Teste3_PaginaQueNaoExiste_DeveDarErro404()
        {
            var cliente = _factory.CreateClient();
            var resposta = await cliente.GetAsync("/Produtos/ABC");

            Assert.Equal(HttpStatusCode.NotFound, resposta.StatusCode);
        }

        // 4. DESAFIO 2: Testa se a palavra "Notebook" realmente está escrita na página
        [Fact]
        public async Task Teste4_PaginaProdutos_DeveMostrarOItemNotebook()
        {
            var cliente = _factory.CreateClient();
            var resposta = await cliente.GetAsync("/Produtos");
            var textoNaTela = await resposta.Content.ReadAsStringAsync();

            Assert.Contains("Notebook", textoNaTela);
        }

        // 5. DESAFIO 3: Testa se o formato do arquivo que o site manda é uma página de internet (HTML)
        [Fact]
        public async Task Teste5_SiteDeveMandarFormatoHtml()
        {
            var cliente = _factory.CreateClient();
            var resposta = await cliente.GetAsync("/Produtos");
            var tipoDoArquivo = resposta.Content.Headers.ContentType?.MediaType;

            Assert.Contains("text/html", tipoDoArquivo);
        }

        // 6. DESAFIO EXTRA: Testa se a página de cadastro funciona e se tem o texto "Cadastrar Produto"
        [Fact]
        public async Task Teste6_PaginaCadastro_DeveAbrirCorretamente()
        {
            var cliente = _factory.CreateClient();
            var resposta = await cliente.GetAsync("/Produtos/Create");
            var textoNaTela = await resposta.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, resposta.StatusCode);
            Assert.Contains("Cadastrar Produto", textoNaTela);
        }
    }
}
