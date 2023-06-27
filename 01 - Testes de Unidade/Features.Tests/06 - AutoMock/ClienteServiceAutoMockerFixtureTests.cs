using Features.Clientes;
using MediatR;
using Moq;
using System.Linq;
using System.Threading;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClienteAutoMockerCollection))]
    public class ClienteServiceAutoMockerFixtureTests
    {
        readonly ClienteTestsAutoMockerFixture _clienteTestsAutoMockFixture;
        readonly ClienteService _clienteService; //Opicional

        public ClienteServiceAutoMockerFixtureTests(ClienteTestsAutoMockerFixture clienteTestsFixture)
        {
            _clienteTestsAutoMockFixture = clienteTestsFixture;
            _clienteService = _clienteTestsAutoMockFixture.ObterClienteService(); //Opicional
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service AutoMockFixture Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsAutoMockFixture.GerarClienteValido();

            // Act
            _clienteService.Adicionar(cliente);

            // Assert
            // O Automock tem a mesma funcionalidade de verificar internamente os asserts usando o metodo Verify
            _clienteTestsAutoMockFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Once); //Verifica se o metodo informado foi executado apenas uma vez
            _clienteTestsAutoMockFixture.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once); //Se foi passado ao publish qualquer classe que foi implementado por INotification
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service AutoMockFixture Tests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            // Arrange
            var cliente = _clienteTestsAutoMockFixture.GerarClienteInvalido();
            var clienteService = _clienteTestsAutoMockFixture.ObterClienteService();

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            // O Automock tem a mesma funcionalidade de verificar internamente os asserts usando o metodo Verify
            _clienteTestsAutoMockFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never); //Verifica se o metodo nunca foi chamado
            _clienteTestsAutoMockFixture.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never); //Se não foi passado ao publish qualquer classe que foi implementado por INotification
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service AutoMockFixture Tests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange
            var clienteService = _clienteTestsAutoMockFixture.ObterClienteService();

            //retorna uma instancia do mock do cliente Repository
            _clienteTestsAutoMockFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.ObterTodos())
                .Returns(_clienteTestsAutoMockFixture.ObterClientesVariados());

            // Act
            var clientes = clienteService.ObterTodosAtivos();

            // Assert
            _clienteTestsAutoMockFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(clientes.Any());
            Assert.False(clientes.Count(c => !c.Ativo) > 0);
        }
    }
}