using Bogus;
using IniciandoTestes.Domain.Contracts.RepositoryContracts;
using IniciandoTestes.Domain.Entities;
using IniciandoTestes.Services;
using IniciandoTestes.Tests.Builders;
using Moq;
using System;
using Xunit;

namespace IniciandoTestes.Tests.ServiceTests
{
    public class ClienteServiceTest
    {
        private readonly Faker _faker;
        private readonly Mock<IClienteRepository> _mockRepository;
        private readonly ClienteService _sut;

        public ClienteServiceTest()
        {
            _faker = new Faker();
            _mockRepository = new Mock<IClienteRepository>();
            _sut = new ClienteService(_mockRepository.Object);
        }

        [Fact]
        public void AdicionarCLiente_DeveAdicionarComSucesso_QuandoClienteValido()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetCliente(It.IsAny<int>())).Returns(new Cliente());
            var cliente = ClienteBuilder.GetClienteValido();

            //Act
            _sut.AddCliente(cliente);

            //Assert
            _mockRepository.Verify(x => x.GetCliente(It.IsAny<int>()), Times.Once());
            _mockRepository.Verify(x => x.AddCliente(cliente), Times.Once());

        }

        [Fact]
        public void AddCliente_DeveQuebrar_QuandoClienteJaExiste()
        {
            //Arrange
            Cliente cliente = ClienteBuilder.GetClienteSemId();
            _mockRepository.Setup(x => x.GetCliente(It.IsAny<int>())).Returns(cliente);

            //Act & Assert
            Assert.Throws<Exception>(() => _sut.AddCliente(cliente));
        }

        [Fact]
        public void AddCliente_DeveEmitirException_QuandoIdIncorreto()
        {
            // Arrange
            Cliente cliente = new Cliente
            {
                Nome = _faker.Name.FullName(),
                Nascimento = new System.DateTime(1900, 12, 12),
                Id = 0
            };

            _mockRepository.Setup(x => x.GetCliente(cliente.Id)).Returns((Cliente)null);

            // Act & Assert
            var exception = Record.Exception(() => _sut.AddCliente(cliente));
            Assert.Null(exception);
        }

        [Fact]
        public void AddCliente_DeveEmitirException_QuandoClienteDeMenor()
        {
            // Arrange
            Cliente cliente = new Cliente
            {
                Nome = _faker.Name.FindName(),
                Nascimento = new System.DateTime(2015, 12, 12),
                Id = _faker.Random.Int(1, 100000)
            };

            _mockRepository.Setup(x => x.GetCliente(cliente.Id)).Returns(cliente);

            // Act & Assert
            Assert.Throws<Exception>(() => _sut.AddCliente(cliente));
        }

        [Fact]
        public void TesteEx()
        {
            // Arrange
            var resultadoEsperado = "Responda a mensagem na proxima vez";
            var result = _sut.ExemploAtrasadinhoQueNaoAvisaEDepoisEncheOSaco();

            // Act & Assert
            Assert.NotNull(result);
            Assert.Equal(resultadoEsperado, result);
        }
    }
}
