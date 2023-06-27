namespace Demo.Tests
{
    public class AssertingObjectTypesTests
    {
        [Fact(DisplayName = "Retornar Tipo do Funcionario")]
        [Trait("Objects", "Teste de Objetos")]
        public void FuncionarioFactory_Criar_DeveRetornarTipoFuncionario()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Eduardo", 10000);

            // Assert
            Assert.IsType<Funcionario>(funcionario);
        }

        [Fact(DisplayName = "Retornar Derivação Pessoa")]
        [Trait("Objects", "Teste de Objetos")]
        public void FuncionarioFactory_Criar_DeveRetornarTipoDerivadoPessoa()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Eduardo", 10000);

            // Assert
            Assert.IsAssignableFrom<Pessoa>(funcionario);
        }
    }
}