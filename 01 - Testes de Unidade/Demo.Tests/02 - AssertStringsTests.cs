namespace Demo.Tests
{
    public class AssertStringsTests
    {
        [Fact(DisplayName = "Retornar Nome Completo")]
        [Trait("Strings", "Teste de Strings")]
        public void StringsTools_UnirNomes_RetornarNomeCompleto()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Eduardo", "Pires");

            // Assert
            Assert.Equal("Eduardo Pires", nomeCompleto);
        }

        [Fact(DisplayName = "Nome Deve Ignorar Caixa Alta")]
        [Trait("Strings", "Teste de Strings")]
        public void StringsTools_UnirNomes_DeveIgnorarCase()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Eduardo", "Pires");

            // Assert
            Assert.Equal("EDUARDO PIRES", nomeCompleto, true);
        }

        [Fact(DisplayName = "Nome Deve Conter Texto Especifico")]
        [Trait("Strings", "Teste de Strings")]
        public void StringsTools_UnirNomes_DeveConterTrecho()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Eduardo", "Pires");

            // Assert
            Assert.Contains("ardo", nomeCompleto);
        }

        [Fact(DisplayName = "Nome Deve Começar Com Caracter Especifico")]
        [Trait("Strings", "Teste de Strings")]
        public void StringsTools_UnirNomes_DeveComecarCom()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Eduardo", "Pires");

            // Assert
            Assert.StartsWith("Edu", nomeCompleto);
        }

        [Fact(DisplayName = "Nome Deve Terminar Com Caracter Especifico")]
        [Trait("Strings", "Teste de Strings")]
        public void StringsTools_UnirNomes_DeveAcabarCom()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Eduardo", "Pires");

            // Assert
            Assert.EndsWith("res", nomeCompleto);
        }

        [Fact(DisplayName = "Nome Deve Estar Valido")]
        [Trait("Strings", "Teste de Strings")]
        public void StringsTools_UnirNomes_ValidarExpressaoRegular()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Eduardo", "Pires");

            // Assert
            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", nomeCompleto);
        }
    }
}