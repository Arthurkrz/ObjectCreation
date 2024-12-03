using Xunit;
using IniciandoTestes.Services;
using IniciandoTestes.Domain.Contracts.RepositoryContracts;
using Moq;

namespace IniciandoTestes.Tests.ServiceTests
{
    public class CalculadoraServiceTest
    {
        private readonly CalculadoraService _sut;
        private readonly Mock<ICalculadoraRepository> _mockRepository;

        public CalculadoraServiceTest()
        {
            _mockRepository = new Mock<ICalculadoraRepository>();
            _sut = new CalculadoraService(_mockRepository.Object);
        }

        [Fact]
        public void CalculadoraDeveRetornarNegativo()
        {
            //Act & Assert
            double result = _sut.SomarNumeros(0,0);
            Assert.False(result > 0);
        }

        [Theory]
        [InlineData(2,3,5)]
        [InlineData(4,9,13)]
        [InlineData(32,45,77)]
        [InlineData(12,3,15)]
        [InlineData(8,16,24)]        
        public void SomarNumero_DeveCalcularComSucesso_QuandoNumerosPositivos
                    (double n1, double n2, double expectedResult)
        {
            // Act & Assert
            double result = _sut.SomarNumeros(n1, n2);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(2, -3, -1)]
        [InlineData(-1, 80, -1)]
        [InlineData(-5, 29, -1)]
        [InlineData(-7, 89, -1)]
        [InlineData(50, -32, -1)]
        public void SomarNumeros_DeveRetornarMenosUm_QuandoNumerosNegativos
                    (double n1, double n2, double expectedResult)
        {
            // Act & Assert
            double result = _sut.SomarNumeros(n1, n2);
            Assert.Equal(expectedResult, result);
        }
    }
}
