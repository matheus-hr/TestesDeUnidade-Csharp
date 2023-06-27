using Features.Clientes;
using FluentAssertions;
using FluentAssertions.Extensions;
using MediatR;
using Moq;
using System.Linq;
using System.Threading;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClienteAutoMockerCollection))]
    public class ClienteServiceFluentAssertionTests
    {
        readonly ClienteTestsAutoMockerFixture _clienteTestsAutoMockerFixture;

        private readonly ClienteService _clienteService;

        public ClienteServiceFluentAssertionTests(ClienteTestsAutoMockerFixture clienteTestsFixture)
        {
            _clienteTestsAutoMockerFixture = clienteTestsFixture;
            _clienteService = _clienteTestsAutoMockerFixture.ObterClienteService();
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service Fluent Assertion Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteValido();

            // Act
            _clienteService.Adicionar(cliente);

            // Assert
            //Assert.True(cliente.EhValido());

            // Assert - Usando Fluent Assertion
            cliente.EhValido().Should().BeTrue();

            // Assert
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Once);
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service Fluent Assertion Tests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            // Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteInvalido();

            // Act
            _clienteService.Adicionar(cliente);

            // Assert
            //Assert.False(cliente.EhValido());

            // Assert - Usando Fluent Assertion
            cliente.EhValido().Should().BeFalse(because: "Cliente Possui Inconsistências");
            cliente.ValidationResult.Errors.Should().HaveCountGreaterThanOrEqualTo(1);

            // Assert
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never);
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service Fluent Assertion Tests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.ObterTodos())
                .Returns(_clienteTestsAutoMockerFixture.ObterClientesVariados());

            // Act
            var clientes = _clienteService.ObterTodosAtivos();

            // Assert 
            Assert.True(clientes.Any());
            Assert.False(clientes.Count(c => !c.Ativo) > 0);

            // Assert - Usando Fluent Assertion
            clientes.Should().HaveCountGreaterThanOrEqualTo(1).And.OnlyHaveUniqueItems(); //Não pode ter itens repetidos
            clientes.Should().NotContain(c => !c.Ativo);

            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);

            //Valida se um metodo é executado menor ou igual a 50 Millisegundos
            // Esse tipo de validação seria melhor em um caso de teste de integração, aqui ele so está escrito por questão de aprendizado
            _clienteService.ExecutionTimeOf(c => c.ObterTodosAtivos())
                .Should().BeLessThanOrEqualTo(50.Milliseconds(), "É executado milhares de vezes pos segundo");
        }
    }
}