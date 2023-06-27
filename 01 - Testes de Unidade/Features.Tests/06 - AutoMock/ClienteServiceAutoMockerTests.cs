using Features.Clientes;
using MediatR;
using Moq;
using Moq.AutoMock;
using System.Linq;
using System.Threading;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteServiceAutoMockerTests
    {
        readonly ClienteTestsBogusFixture _clienteTestsBogus;

        public ClienteServiceAutoMockerTests(ClienteTestsBogusFixture clienteTestsFixture)
        {
            _clienteTestsBogus = clienteTestsFixture;
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service AutoMock Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsBogus.GerarClienteValido();
            var mocker = new AutoMocker();

            var clienteService = mocker.CreateInstance<ClienteService>(); //O Auto mock ja resolve todas as dependencias da classe ClienteService

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            // O Automock tem a mesma funcionalidade de verificar internamente os asserts usando o metodo Verify
            mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Once); //Verifica se o metodo informado foi executado apenas uma vez
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once); //Se foi passado ao publish qualquer classe que foi implementado por INotification
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service AutoMock Tests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            // Arrange
            var cliente = _clienteTestsBogus.GerarClienteInvalido();
            var mocker = new AutoMocker();

            var clienteService = mocker.CreateInstance<ClienteService>(); //O Auto mock ja resolve todas as dependencias da classe ClienteService

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            // O Automock tem a mesma funcionalidade de verificar internamente os asserts usando o metodo Verify
            mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never); //Verifica se o metodo nunca foi chamado
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never); //Se não foi passado ao publish qualquer classe que foi implementado por INotification
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service AutoMock Tests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange
            var mocker = new AutoMocker();
            var clienteService = mocker.CreateInstance<ClienteService>();

            //retorna uma instancia do mock do cliente Repository
            mocker.GetMock<IClienteRepository>().Setup(c => c.ObterTodos())
                .Returns(_clienteTestsBogus.ObterClientesVariados());

            // Act
            var clientes = clienteService.ObterTodosAtivos();

            // Assert
            mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(clientes.Any());
            Assert.False(clientes.Count(c => !c.Ativo) > 0);
        }
    }
}