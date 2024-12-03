using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using Xunit;
using Moq;
using IniciandoTestes.Domain.Contracts.RepositoryContracts;
using IniciandoTestes.Services;
using IniciandoTestes.Domain.Entities;
using IniciandoTestes.Domain.Enum;
using IniciandoTestes.Tests.MotherObjects;

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

        [Fact]
        public void CriarCandidatura_DeveCriarComSucesso_QuandoDadosValidos()
        {
            // Arrange
            Concurso concurso;
            Candidato candidato;
            int matricula = 0;

            concurso = ConcursoMother.GetConcursoPorEscolaridade(Escolaridade.Fundamental);
            candidato = CandidaturaMother.GetCandidatoAptoPorEscolaridade
                                         (Escolaridade.Superior, Escolaridade.Fundamental);
            matricula = CandidaturaMother.GetCandidatoAptoPorEscolaridade
                                         (Escolaridade.Superior, Escolaridade.Fundamental).Id;

            _mockRepository.Setup(x => x.GetConcurso(It.IsAny<int>())).Returns(concurso);
            _mockRepository.Setup(x => x.AdicionaCandidato(It.IsAny<Candidato>()))
                                                           .Returns(matricula);

            // Act & Assert
            var exception = Record.Exception(() => _sut.CriarCandidatura(candidato));
            Assert.Null(exception);
        }

        [Fact]
        public void CriarCandidatura_DeveEmitirException_QuandoCandidatoInvalido()
        {
            // Arrange
            Concurso concurso;
            concurso = ConcursoMother.GetConcursoPorEscolaridade(Escolaridade.Medio);

            Candidato candidato = new Candidato
            {
                Id = _faker.Random.Int(1, 200),
                Nome = _faker.Name.FirstName(),
                NumeroInscricao = _faker.Random.Int(1, 200000),
                Nascimento = _faker.Date.Past(18, DateTime.Now),
                Cpf = _faker.Random.Word(),
                Escolaridade = Escolaridade.Medio,
                Concurso = concurso
            };

            _mockRepository.Setup(x => x.CandidatoEhValido(candidato)).Returns(false);

            // Act & Assert 
            Assert.Throws<ArgumentException>(() => _sut.CriarCandidatura(candidato));
        }

        [Fact]
        public void CriarCandidatura_DeveEmitirException_QuandoCandidatoInapto()
        {
            // Arrange
            Concurso concurso;
            concurso = ConcursoMother.GetConcursoPorEscolaridade(Escolaridade.Superior);

            Candidato candidato = new Candidato
            {
                Id = _faker.Random.Int(1, 200),
                Nome = _faker.Name.FirstName(),
                NumeroInscricao = _faker.Random.Int(1, 200000),
                Nascimento = new System.DateTime(1995, 12, 12),
                Cpf = _faker.Random.Word(),
                Escolaridade = Escolaridade.Medio,
                Concurso = concurso
            };
            
            _mockRepository.Setup(x => x.CandidatoAptoAoConcurso
                                         (candidato, concurso))
                                         .Returns(false);
            // Act & Assert
            Assert.Throws<Exception>(() => _sut.CriarCandidatura(candidato));
        }

        [Theory]
        [MemberData(nameof(GetCandidatosAptos))]
        public void CandidatoAptoAoConcurso_DeveRetornarTrue_QuandoCandidatoApto
                    (Candidato candidato, Concurso concurso)
        {
            // Act & Assert
            _sut.CandidatoAptoAoConcurso(candidato, concurso);
        }

        [Fact]
        public void CandidatoAptoAoConcurso_DeveRetornarFalse_QuandoCandidatoInapto()
        {
            // Arrange
            Candidato candidato;
            Concurso concurso;

            candidato = CandidaturaMother.GetCandidatoAptoPorEscolaridade
                                          (Escolaridade.Fundamental,
                                           Escolaridade.Superior);

            concurso = ConcursoMother.GetConcursoPorEscolaridade
                                      (Escolaridade.Superior);

            // Act & Assert
            Assert.False(_sut.CandidatoAptoAoConcurso(candidato, concurso));
        }

        [Fact]
        public void CandidatoEhValido_DeveRetornarTrue_QuandoCandidatoValido()
        {
            // Arrange
            Candidato candidato;
            candidato = CandidaturaMother.GetCandidatoAptoPorEscolaridade
                                          (Escolaridade.Superior,
                                           Escolaridade.Fundamental);

            // Act & Assert
            _sut.CandidatoEhValido(candidato);
        }

        [Fact]
        public void CandidatoEhValido_DeveRetornarFalse_QuandoDataInvalida()
        {
            // Arrange
            Concurso concurso;
            concurso = ConcursoMother.GetConcursoPorEscolaridade(Escolaridade.Medio);

            Candidato candidato = new Candidato
            {
                Id = _faker.Random.Int(1, 200),
                Nome = _faker.Name.FirstName(),
                NumeroInscricao = _faker.Random.Int(1, 200000),
                Nascimento = _faker.Date.Past(18, DateTime.Now),
                Cpf = _faker.Random.Word(),
                Escolaridade = Escolaridade.Medio,
                Concurso = concurso
            };

            // Act & Assert
            Assert.False(_sut.CandidatoEhValido(candidato));
        }

        [Fact]
        public void CandidatoEhValido_DeveRetornarFalse_QuandoObjetoNulo()
        {
            // Arrange
            Candidato candidato = null;

            // Act & Assert
            Assert.False(_sut.CandidatoEhValido(candidato));
        }

        public static IEnumerable<object[]> GetCandidatosAptos()
        {
            yield return new object[]
            {
                CandidaturaMother.GetCandidatoAptoPorEscolaridade
                                  (Escolaridade.Superior, 
                                   Escolaridade.Fundamental),

                ConcursoMother.GetConcursoPorEscolaridade
                               (Escolaridade.Fundamental)
            };

            yield return new object[]
            {
                CandidaturaMother.GetCandidatoAptoPorEscolaridade
                                  (Escolaridade.Medio, 
                                   Escolaridade.Fundamental),

                ConcursoMother.GetConcursoPorEscolaridade
                               (Escolaridade.Fundamental)
            };

            yield return new object[]
            {
                CandidaturaMother.GetCandidatoAptoPorEscolaridade
                                  (Escolaridade.Fundamental, 
                                   Escolaridade.Fundamental),

                ConcursoMother.GetConcursoPorEscolaridade
                               (Escolaridade.Fundamental)
            };

        }
    }
}
