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
            Concurso concurso2;
            Concurso concurso3;
            Concurso concurso4;

            Candidato candidato;
            Candidato candidato2;
            Candidato candidato3;
            Candidato candidato4;

            int matricula = 0;
            int matricula2 = 0;
            int matricula3 = 0;
            int matricula4 = 0;


            concurso = ConcursoMother.GetConcursoPorEscolaridade(Escolaridade.Fundamental);
            candidato = CandidaturaMother.GetCandidatoAptoPorEscolaridade
                                         (Escolaridade.Superior, Escolaridade.Fundamental);
            matricula = CandidaturaMother.GetCandidatoAptoPorEscolaridade
                                         (Escolaridade.Superior, Escolaridade.Fundamental).Id;

            _mockRepository.Setup(x => x.GetConcurso(It.IsAny<int>())).Returns(concurso);
            _mockRepository.Setup(x => x.AdicionaCandidato(It.IsAny<Candidato>()))
                                                           .Returns(matricula);


            concurso2 = ConcursoMother.GetConcursoPorEscolaridade(Escolaridade.Fundamental);
            candidato2 = CandidaturaMother.GetCandidatoAptoPorEscolaridade
                                           (Escolaridade.Medio, Escolaridade.Fundamental);
            matricula2 = CandidaturaMother.GetCandidatoAptoPorEscolaridade
                                           (Escolaridade.Medio, Escolaridade.Fundamental).Id;

            _mockRepository.Setup(x => x.GetConcurso(It.IsAny<int>())).Returns(concurso2);
            _mockRepository.Setup(x => x.AdicionaCandidato(It.IsAny<Candidato>()))
                                                           .Returns(matricula2);


            concurso3 = ConcursoMother.GetConcursoPorEscolaridade(Escolaridade.Medio);
            candidato3 = CandidaturaMother.GetCandidatoAptoPorEscolaridade
                                           (Escolaridade.Medio, Escolaridade.Medio);
            matricula3 = CandidaturaMother.GetCandidatoAptoPorEscolaridade
                                           (Escolaridade.Medio, Escolaridade.Medio).Id;

            _mockRepository.Setup(x => x.GetConcurso(It.IsAny<int>())).Returns(concurso3);
            _mockRepository.Setup(x => x.AdicionaCandidato(It.IsAny<Candidato>()))
                                                           .Returns(matricula3);


            concurso4 = ConcursoMother.GetConcursoPorEscolaridade(Escolaridade.Fundamental);
            candidato4 = CandidaturaMother.GetCandidatoAptoPorEscolaridade
                                           (Escolaridade.Fundamental, Escolaridade.Fundamental);
            matricula4 = CandidaturaMother.GetCandidatoAptoPorEscolaridade
                                           (Escolaridade.Fundamental, Escolaridade.Fundamental).Id;

            _mockRepository.Setup(x => x.GetConcurso(It.IsAny<int>())).Returns(concurso4);
            _mockRepository.Setup(x => x.GetConcurso(It.IsAny<int>())).Returns(concurso4);


            // Act & Assert
            var exception1 = Record.Exception(() => _sut.CriarCandidatura(candidato));
            Assert.Null(exception1);

            var exception2 = Record.Exception(() => _sut.CriarCandidatura(candidato2));
            Assert.Null(exception2);

            var exception3 = Record.Exception(() => _sut.CriarCandidatura(candidato3));
            Assert.Null(exception3);

            var exception4 = Record.Exception(() => _sut.CriarCandidatura(candidato4));
            Assert.Null(exception4);
        }

        [Fact]
        public void CriarCandidatura_DeveEmitirException_QuandoCandidatoInvalido()
        {
            // Arrange
            Concurso concurso;
            Candidato candidato;
            Candidato candidato2 = null;

            concurso = ConcursoMother.GetConcursoPorEscolaridade(Escolaridade.Medio);
            candidato = CandidaturaMother.GetCandidatoInvalido();

            _mockRepository.Setup(x => x.CandidatoEhValido(candidato)).Returns(false);
            _mockRepository.Setup(x => x.CandidatoEhValido(candidato2)).Returns(false);

            // Act & Assert 
            Assert.Throws<ArgumentException>(() => _sut.CriarCandidatura(candidato));
            Assert.Throws<ArgumentException>(() => _sut.CriarCandidatura(candidato2)); 
        }

        [Fact]
        public void CriarCandidatura_DeveEmitirException_QuandoCandidatoInapto()
        {
            // Arrange
            Concurso concurso;
            Concurso concurso2;
            Candidato candidato;
            Candidato candidato2;

            concurso = ConcursoMother.GetConcursoPorEscolaridade(Escolaridade.Superior);
            concurso2 = ConcursoMother.GetConcursoPorEscolaridade(Escolaridade.Medio);
            candidato = CandidaturaMother.GetCandidatoInapto(concurso);
            candidato2 = CandidaturaMother.GetCandidatoInapto(concurso2);

            _mockRepository.Setup(x => x.CandidatoEhValido(candidato)).Returns(true);
            _mockRepository.Setup(x => x.CandidatoEhValido(candidato2)).Returns(true);
            _mockRepository.Setup(x => x.GetConcurso(candidato.Concurso.Id)).Returns(concurso);
            _mockRepository.Setup(x => x.GetConcurso(candidato2.Concurso.Id)).Returns(concurso2);

            // Act & Assert
            Assert.Throws<Exception>(() => _sut.CriarCandidatura(candidato));
            Assert.Throws<Exception>(() => _sut.CriarCandidatura(candidato2));
        }
    }
}
