using Bogus;
using IniciandoTestes.Domain.Contracts.RepositoryContracts;
using IniciandoTestes.Domain.Entities;
using IniciandoTestes.Domain.Enum;
using IniciandoTestes.Services;
using IniciandoTestes.Tests.Builders;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace IniciandoTestes.Tests.ServiceTests
{
    public class CandidaturaServiceTest
    {
        private readonly Faker _faker;
        private readonly Mock<ICandidaturaRepository> _mockRepository;
        private readonly CandidaturaService _sut;

        public CandidaturaServiceTest()
        {
            _faker = new Faker();
            _mockRepository = new Mock<ICandidaturaRepository>();
            _sut = new CandidaturaService(_mockRepository.Object);
        }

        [Theory]
        [MemberData(nameof(GetCandidatosAptos))]
        public void CriarCandidatura_DeveCriarComSucesso_QuandoDadosValidos
                    (Candidato candidato)
        {
            // Arrange
            Concurso concurso = new ConcursoBuilder().IDValido()
                                                     .TituloValido()
                                                     .DataValida()
                                                     .LocalValido()
                                                     .EscolaridadeValida
                                                     (candidato.Escolaridade)
                                                     .Build();

            _mockRepository.Setup(x => x.CandidatoEhValido
                                        (candidato))
                                        .Returns(true);
            _mockRepository.Setup(x => x.GetConcurso
                                        (candidato.Concurso.Id))
                                        .Returns(concurso);
            _mockRepository.Setup(x => x.CandidatoAptoAoConcurso
                                        (candidato, concurso))
                                        .Returns(true);
            
            // Act & Assert
            _sut.CriarCandidatura(candidato);
        }

        [Fact]
        public void CriarCandidatura_DeveEmitirException_QuandoCandidatoInvalido()
        {
            // Arrange
            Candidato candidatoNulo = null;
            Candidato candidato = new CandidaturaBuilder().NomeValido()
                                                          .DataNascimentoInvalida()
                                                          .EscolaridadeValida
                                                          (Escolaridade.Superior)
                                                          .CPFValido()
                                                          .NumerodeInscricaoValido()
                                                          .IDValido()
                                                          .ConcursoValido()
                                                          .Build();

            _mockRepository.Setup(x => x.CandidatoEhValido(candidato))
                           .Returns(false);
            _mockRepository.Setup(x => x.CandidatoEhValido(candidatoNulo))
                           .Returns(false);

            // Act & Assert 
            Assert.Throws<ArgumentException>(() => _sut.CriarCandidatura
                                                            (candidato));
            Assert.Throws<ArgumentException>(() => _sut.CriarCandidatura
                                                        (candidatoNulo));
        }

        [Theory]
        [MemberData(nameof(GetCandidatosInaptos))]
        public void CriarCandidatura_DeveEmitirException_QuandoCandidatoInapto
                    (Candidato candidatoInapto)
        {
            // Arrange
            _mockRepository.Setup(x => x.CandidatoEhValido
                            (candidatoInapto))
                           .Returns(true);
            _mockRepository.Setup(x => x.GetConcurso
                            (candidatoInapto.Concurso.Id))
                           .Returns(candidatoInapto.Concurso);

            // Act & Assert
            Assert.Throws<Exception>(() => _sut.CriarCandidatura
                                               (candidatoInapto));
        }
        public static IEnumerable<object[]> GetCandidatosAptos()
        {
            var _faker = new Faker();

            yield return new object[]
            {
                new CandidaturaBuilder().NomeValido()
                                        .DataNascimentoValida()
                                        .EscolaridadeValida
                                        (Escolaridade.Fundamental)
                                        .CPFValido()
                                        .NumerodeInscricaoValido()
                                        .IDValido()
                                        .ConcursoValido()
                                        .Build()
            };

            yield return new object[]
            {
                new CandidaturaBuilder().NomeValido()
                                        .DataNascimentoValida()
                                        .EscolaridadeValida
                                        (Escolaridade.Superior)
                                        .CPFValido()
                                        .NumerodeInscricaoValido()
                                        .IDValido()
                                        .ConcursoValido()
                                        .Build()
            };

            yield return new object[]
            {
                new CandidaturaBuilder().NomeValido()
                                        .DataNascimentoValida()
                                        .EscolaridadeValida
                                        (Escolaridade.Medio)
                                        .CPFValido()
                                        .NumerodeInscricaoValido()
                                        .IDValido()
                                        .ConcursoValido()
                                        .Build()
            };
        }

        public static IEnumerable<object[]> GetCandidatosInaptos()
        {
            var _faker = new Faker();

            yield return new object[]
            {
                new CandidaturaBuilder().NomeValido()
                                        .DataNascimentoValida()
                                        .EscolaridadeValida
                                        (Escolaridade.Fundamental)
                                        .CPFValido()
                                        .NumerodeInscricaoValido()
                                        .IDValido()
                                        .ConcursoInvalido()
                                        .Build()
            };

            yield return new object[]
            {
                new CandidaturaBuilder().NomeValido()
                                        .DataNascimentoValida()
                                        .EscolaridadeValida
                                        (Escolaridade.Medio)
                                        .CPFValido()
                                        .NumerodeInscricaoValido()
                                        .IDValido()
                                        .ConcursoInvalido()
                                        .Build()
            };

        }
    }
}