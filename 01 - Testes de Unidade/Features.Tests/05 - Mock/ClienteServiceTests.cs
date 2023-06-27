using Features.Clientes;
using MediatR;
using Moq;
using System.Linq;
using System.Threading;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteServiceTests
    {
        readonly ClienteTestsBogusFixture _clienteTestsBogus;

        public ClienteServiceTests(ClienteTestsBogusFixture clienteTestsFixture)
        {
            _clienteTestsBogus = clienteTestsFixture;
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service Mock Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsBogus.GerarClienteValido();
            var clienteRepo = new Mock<IClienteRepository>();
            var mediatior = new Mock<IMediator>();

            var clienteService = new ClienteService(clienteRepo.Object, mediatior.Object);

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            // O Moq cria os asserts internos usando o metodo Verify
            clienteRepo.Verify(r => r.Adicionar(cliente), Times.Once); //Verifica se o metodo informado foi executado apenas uma vez
            mediatior.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once); //Se foi passado ao publish qualquer classe que foi implementado por INotification
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service Mock Tests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            // Arrange
            var cliente = _clienteTestsBogus.GerarClienteInvalido();
            var clienteRepo = new Mock<IClienteRepository>();
            var mediatior = new Mock<IMediator>();

            var clienteService = new ClienteService(clienteRepo.Object, mediatior.Object);

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            // O Moq cria os asserts internos usando o metodo Verify
            clienteRepo.Verify(r => r.Adicionar(cliente), Times.Never); //Verifica se o metodo nunca foi chamado
            mediatior.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never); //Se não foi passado ao publish qualquer classe que foi implementado por INotification
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service Mock Tests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange
            var clienteRepo = new Mock<IClienteRepository>();
            var mediatior = new Mock<IMediator>();

            clienteRepo.Setup(c => c.ObterTodos()) //Ensina o metodo a fazer alguma coisa que eu quero
                .Returns(_clienteTestsBogus.ObterClientesVariados()); //Retorna o metodo que retorna o objeto que eu quero

            var clienteService = new ClienteService(clienteRepo.Object, mediatior.Object);

            // Act
            var clientes = clienteService.ObterTodosAtivos();

            // Assert
            clienteRepo.Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(clientes.Any());
            Assert.False(clientes.Count(c => !c.Ativo) > 0);
        }
    }
}