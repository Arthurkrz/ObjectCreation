using Bogus;
using System.Collections.Generic;
using System;
using Moq;
using Xunit;
using IniciandoTestes.Domain.Entities;
using IniciandoTestes.Domain.Contracts.RepositoryContracts;
using IniciandoTestes.Services;
using IniciandoTestes.Tests.MotherObjects;
using IniciandoTestes.Domain.Enum;

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
        public void AdicionarFuncionario_DeveConcluir_QuandoDadosValidos
                    (Funcionario funcionario)
        {
            //Act & Assert
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
            Funcionario funcionario = FuncionarioMother.
                                      GetFuncionarioNomeCurto
                                      (Senioridade.Senior);

            // Act & Assert
            Assert.Throws<FormatException>(() => _sut.AdicionarFuncionario
                                                      (funcionario));
        }

        [Fact]
        public void AdicionarFuncionario_DeveEmitirException_QuandoNascimentoInvalido()
        {
            // Arrange
            Funcionario funcionario = FuncionarioMother.
                                      GetFuncionarioNascimentoInvalido
                                      (Senioridade.Senior);

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
               FuncionarioMother.GetFuncionarioValidoPorSenioridade
                                 (Senioridade.Junior)
            };

            yield return new object[] 
            {
                FuncionarioMother.GetFuncionarioValidoPorSenioridade
                                  (Senioridade.Pleno)
            };

            yield return new object[]
            {
                FuncionarioMother.GetFuncionarioValidoPorSenioridade
                                  (Senioridade.Senior)
            };
        }

        public static IEnumerable<object[]> GetFuncionariosSalariosInvalidos()
        {
            var _faker = new Faker();

            yield return new object[]
            {
                new Funcionario
                {
                    Nome = _faker.Name.FullName(),
                    Nascimento = _faker.Date.Between(DateTime.Now.AddDays(-21), 
                                                     DateTime.Now.AddDays(-50)),
                    Senioridade = Senioridade.Junior,
                    Salario = 3000
                }
            };

            yield return new object[]
            {
                new Funcionario
                {
                    Nome = _faker.Name.FullName(),
                    Nascimento = _faker.Date.Between(DateTime.Now.AddDays(-21),
                                                     DateTime.Now.AddDays(-50)),
                    Senioridade = Senioridade.Junior,
                    Salario = 3200
                }
            };

            yield return new object[]
            {
                new Funcionario
                {
                    Nome = _faker.Name.FullName(),
                    Nascimento = _faker.Date.Between(DateTime.Now.AddDays(-21),
                                                     DateTime.Now.AddDays(-50)),
                    Senioridade = Senioridade.Junior,
                    Salario = 6000
                }
            };

            yield return new object[]
            {
                new Funcionario
                {
                    Nome = _faker.Name.FullName(),
                    Nascimento = _faker.Date.Between(DateTime.Now.AddDays(-21),
                                                     DateTime.Now.AddDays(-50)),
                    Senioridade = Senioridade.Junior,
                    Salario = 5500
                }
            };

            yield return new object[]
            {
                new Funcionario
                {
                    Nome = _faker.Name.FullName(),
                    Nascimento = _faker.Date.Between(DateTime.Now.AddDays(-21),
                                                     DateTime.Now.AddDays(-50)),
                    Senioridade = Senioridade.Pleno,
                    Salario = 5000
                }
            };

            yield return new object[]
            {
                new Funcionario
                {
                    Nome = _faker.Name.FullName(),
                    Nascimento = _faker.Date.Between(DateTime.Now.AddDays(-21),
                                                     DateTime.Now.AddDays(-50)),
                    Senioridade = Senioridade.Pleno,
                    Salario = 5500
                }
            };

            yield return new object[]
            {
                new Funcionario
                {
                    Nome = _faker.Name.FullName(),
                    Nascimento = _faker.Date.Between(DateTime.Now.AddDays(-21),
                                                     DateTime.Now.AddDays(-50)),
                    Senioridade = Senioridade.Pleno,
                    Salario = 9000
                }
            };

            yield return new object[]
            {
                new Funcionario
                {
                    Nome = _faker.Name.FullName(),
                    Nascimento = _faker.Date.Between(DateTime.Now.AddDays(-21),
                                                     DateTime.Now.AddDays(-50)),
                    Senioridade = Senioridade.Pleno,
                    Salario = 8000
                }
            };

            yield return new object[]
            {
                new Funcionario
                {
                    Nome = _faker.Name.FullName(),
                    Nascimento = _faker.Date.Between(DateTime.Now.AddDays(-21), 
                                                     DateTime.Now.AddDays(50)),
                    Senioridade = Senioridade.Senior,
                    Salario = 7000
                }
            };

            yield return new object[]
            {
                new Funcionario
                {
                    Nome = _faker.Name.FullName(),
                    Nascimento = _faker.Date.Between(DateTime.Now.AddDays(-21),
                                                     DateTime.Now.AddDays(50)),
                    Senioridade = Senioridade.Senior,
                    Salario = 8000
                }
            };

            yield return new object[]
            {
                new Funcionario
                {
                    Nome = _faker.Name.FullName(),
                    Nascimento = _faker.Date.Between(DateTime.Now.AddDays(-21),
                                                     DateTime.Now.AddDays(50)),
                    Senioridade = Senioridade.Senior,
                    Salario = 51000
                }
            };

            yield return new object[]
            {
                new Funcionario
                {
                    Nome = _faker.Name.FullName(),
                    Nascimento = _faker.Date.Between(DateTime.Now.AddDays(-21),
                                                     DateTime.Now.AddDays(50)),
                    Senioridade = Senioridade.Senior,
                    Salario = 50000
                }
            };
        }
    }
}
