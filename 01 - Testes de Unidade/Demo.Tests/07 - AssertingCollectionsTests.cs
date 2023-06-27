namespace Demo.Tests
{
    public class AssertingCollectionsTests
    {
        [Fact(DisplayName = "Retorna Funcionario Sem Habilidades")]
        [Trait("Collections", "Teste de Coleções")]
        public void Funcionario_Habilidades_NaoDevePossuirHabilidadesVazias()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Eduardo", 10000);

            // Assert
            Assert.All(funcionario.Habilidades, habilidade => Assert.False(string.IsNullOrWhiteSpace(habilidade)));
        }

        [Fact(DisplayName = "Retorna Funcionario Com Habilidades Basicas")]
        [Trait("Collections", "Teste de Coleções")]
        public void Funcionario_Habilidades_JuniorDevePossuirHabilidadeBasica()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Eduardo", 1000);

            // Assert
            Assert.Contains("OOP", funcionario.Habilidades);
        }


        [Fact(DisplayName = "Retorna Funcionario Com Habilidades Avançadas")]
        [Trait("Collections", "Teste de Coleções")]
        public void Funcionario_Habilidades_JuniorNaoDevePossuirHabilidadeAvancada()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Eduardo", 1000);

            // Assert
            Assert.DoesNotContain("Microservices", funcionario.Habilidades);
        }


        [Fact(DisplayName = "Retorna Funcionario Com Todas as Habilidades")]
        [Trait("Collections", "Teste de Coleções")]
        public void Funcionario_Habilidades_SeniorDevePossuirTodasHabilidades()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Eduardo", 15000);

            var habilidadesBasicas = new[]
            {
                "Lógica de Programação",
                "OOP",
                "Testes",
                "Microservices"
            };

            // Assert
            Assert.Equal(habilidadesBasicas, funcionario.Habilidades);
        }
    }
}