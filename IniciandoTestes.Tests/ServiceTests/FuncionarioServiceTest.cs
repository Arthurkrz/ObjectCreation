using Bogus;
using IniciandoTestes.Domain.Contracts.RepositoryContracts;
using IniciandoTestes.Domain.Entities;
using IniciandoTestes.Domain.Enum;
using IniciandoTestes.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using IniciandoTestes.Tests.Builders;

namespace IniciandoTestes.Tests.ServiceTests
{
    public class FuncionarioServiceTest
    {
        private readonly Faker _faker;
        private readonly Mock<IFuncionarioRepository> _mockRepository;
        private readonly FuncionarioService _sut;

        public FuncionarioServiceTest()
        {
            _faker = new Faker();
            _mockRepository = new Mock<IFuncionarioRepository>();
            _sut = new FuncionarioService(_mockRepository.Object);
        }

        [Theory]
        [MemberData(nameof(GetFuncionariosData))]
        public void AdicionarFuncionario_DeveConcluir_QuandoDadosValidos(Funcionario funcionario)
        {
            // Act & Assert
            _sut.AdicionarFuncionario(funcionario);
        }

        [Fact]
        public void AdicionarFuncionario_DeveEmitirException_QuandoFuncionarioNulo()
        {
            // Arrange
            Funcionario funcionario = null;

            // Act & Assert
            Assert.Throws<Exception>(() => _sut.AdicionarFuncionario(funcionario));
        }

        [Fact]
        public void AdicionarFuncionario_DeveEmitirException_QuandoNomeCurto()
        {
            // Arrange
            Funcionario funcionario = new FuncionarioBuilder().NomeInvalido()
                                                              .DataNascimentoValida()
                                                              .SalarioValido
                                                              (Senioridade.Senior)
                                                              .SenioridadeValida
                                                              (Senioridade.Senior)
                                                              .Build();

            // Act & Assert
            Assert.Throws<FormatException>(() => _sut.AdicionarFuncionario
                                                     (funcionario));
        }

        [Fact]
        public void AdicionarFuncionario_DeveEmitirException_QuandoNascimentoInvalido()
        {
            // Arrange
            Funcionario funcionario = new FuncionarioBuilder().NomeValido()
                                                              .DataNascimentoInvalida()
                                                              .SalarioValido
                                                              (Senioridade.Senior)
                                                              .SenioridadeValida
                                                              (Senioridade.Senior)
                                                              .Build();

            // Act & Assert
            Assert.Throws<Exception>(() => _sut.AdicionarFuncionario(funcionario));
        }

        [Theory]
        [MemberData(nameof(GetFuncionariosSalariosInvalidos))]
        public void AdicionarFuncionario_DeveEmitirException_QuandoSalarioInvalido
                    (Funcionario funcionario)
        {
            // Act & Assert
            Assert.Throws<Exception>(() => _sut.AdicionarFuncionario(funcionario));
        }

        public static IEnumerable<object[]> GetFuncionariosData()
        {
            yield return new object[]
            {
                new FuncionarioBuilder().NomeValido()
                                        .DataNascimentoValida()
                                        .SalarioValido
                                        (Senioridade.Junior)
                                        .SenioridadeValida
                                        (Senioridade.Junior)
                                        .Build()
            };

            yield return new object[]
            {
                new FuncionarioBuilder().NomeValido()
                                        .DataNascimentoValida()
                                        .SalarioValido
                                        (Senioridade.Pleno)
                                        .SenioridadeValida
                                        (Senioridade.Pleno)
                                        .Build()
            };

            yield return new object[]
            {
                new FuncionarioBuilder().NomeValido()
                                        .DataNascimentoValida()
                                        .SalarioValido
                                        (Senioridade.Senior)
                                        .SenioridadeValida
                                        (Senioridade.Senior)
                                        .Build()
            };
        }

        public static IEnumerable<object[]> GetFuncionariosSalariosInvalidos()
        {
            var _faker = new Faker();

            yield return new object[]
            {
                new FuncionarioBuilder().NomeValido()
                                        .DataNascimentoValida()
                                        .SalarioInvalido(3000)
                                        .SenioridadeValida
                                        (Senioridade.Junior)
                                        .Build()
            };

            yield return new object[]
            {
                new FuncionarioBuilder().NomeValido()
                                        .DataNascimentoValida()
                                        .SalarioInvalido(3200)
                                        .SenioridadeValida
                                        (Senioridade.Junior)
                                        .Build()
            };

            yield return new object[]
            {
                new FuncionarioBuilder().NomeValido()
                                        .DataNascimentoValida()
                                        .SalarioInvalido(6000)
                                        .SenioridadeValida
                                        (Senioridade.Junior)
                                        .Build()
            };

            yield return new object[]
            {
                new FuncionarioBuilder().NomeValido()
                                        .DataNascimentoValida()
                                        .SalarioInvalido(5500)
                                        .SenioridadeValida
                                        (Senioridade.Junior)
                                        .Build()
            };

            yield return new object[]
            {
                new FuncionarioBuilder().NomeValido()
                                        .DataNascimentoValida()
                                        .SalarioInvalido(5000)
                                        .SenioridadeValida
                                        (Senioridade.Pleno)
                                        .Build()
            };

            yield return new object[]
            {
                new FuncionarioBuilder().NomeValido()
                                        .DataNascimentoValida()
                                        .SalarioInvalido(5500)
                                        .SenioridadeValida
                                        (Senioridade.Pleno)
                                        .Build()
            };

            yield return new object[]
            {
                new FuncionarioBuilder().NomeValido()
                                        .DataNascimentoValida()
                                        .SalarioInvalido(9000)
                                        .SenioridadeValida
                                        (Senioridade.Pleno)
                                        .Build()
            };

            yield return new object[]
            {
                new FuncionarioBuilder().NomeValido()
                                        .DataNascimentoValida()
                                        .SalarioInvalido(8000)
                                        .SenioridadeValida
                                        (Senioridade.Pleno)
                                        .Build()
            };

            yield return new object[]
            {
                new FuncionarioBuilder().NomeValido()
                                        .DataNascimentoValida()
                                        .SalarioInvalido(7000)
                                        .SenioridadeValida
                                        (Senioridade.Senior)
                                        .Build()
            };

            yield return new object[]
            {
                new FuncionarioBuilder().NomeValido()
                                        .DataNascimentoValida()
                                        .SalarioInvalido(8000)
                                        .SenioridadeValida
                                        (Senioridade.Senior)
                                        .Build()
            };

            yield return new object[]
            {
                new FuncionarioBuilder().NomeValido()
                                        .DataNascimentoValida()
                                        .SalarioInvalido(510000)
                                        .SenioridadeValida
                                        (Senioridade.Senior)
                                        .Build()
            };

            yield return new object[]
            {
                new FuncionarioBuilder().NomeValido()
                                        .DataNascimentoValida()
                                        .SalarioInvalido(500000)
                                        .SenioridadeValida
                                        (Senioridade.Senior)
                                        .Build()
            };
        }
    }
}
