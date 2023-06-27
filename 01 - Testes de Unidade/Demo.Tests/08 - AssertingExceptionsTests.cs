namespace Demo.Tests
{
    public class AssertingExceptionsTests
    {
        [Fact(DisplayName = "Erro de divisão por zero")]
        [Trait("Exceptions", "Teste de Exceções e Null")]
        public void Calculadora_Dividir_DeveRetornarErroDivisaoPorZero()
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act & Assert
            //Assert.Throws<DivideByZeroException>(() => calculadora.Dividir(10, 0));
            Assert.True(double.IsInfinity(calculadora.Dividir(10, 0)));
        }


        [Fact(DisplayName = "Salario inferior permitido")]
        [Trait("Exceptions", "Teste de Exceções e Null")]
        public void Funcionario_Salario_DeveRetornarErroSalarioInferiorPermitido()
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<Exception>(() => FuncionarioFactory.Criar("Eduardo", 250));

            Assert.Equal("Salario inferior ao permitido", exception.Message);
        }
    }
}