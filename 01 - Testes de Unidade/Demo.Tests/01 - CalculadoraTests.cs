namespace Demo.Tests
{
    public class CalculadoraTests
    {
        [Fact(DisplayName = "Somar valores")] //Um fato -> 2 + 2 é = 4
        [Trait("Calculadora", "Calculadora Tests")]
        public void Calculadora_Somar_RetornarValorSoma()
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            var resultado = calculadora.Somar(2, 2);

            // Assert
            Assert.Equal(expected: 4, actual: resultado);
        }

        [Theory(DisplayName = "Somar Diversos valores ")] //Ideia de uma Teoria (Preciso de cenarios para a teoria para testar a verdade)
        [Trait("Calculadora", "Calculadora Tests")]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 4)]
        [InlineData(4, 2, 6)]
        [InlineData(7, 3, 10)]
        [InlineData(6, 6, 12)]
        [InlineData(9, 9, 18)]
        public void Calculadora_Somar_RetornarValoresSomaCorretos(double v1, double v2, double total)
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            var resultado = calculadora.Somar(v1, v2);

            // Assert
            Assert.Equal(expected: total, actual: resultado);
        }
    }
}
