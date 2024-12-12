using Bogus;
using IniciandoTestes.Domain.Entities;
using IniciandoTestes.Domain.Enum;
using System;

namespace IniciandoTestes.Tests.Builders
{
    internal class CandidaturaBuilder
    {
        private Candidato _candidato;
        private readonly Faker _faker;

        public CandidaturaBuilder()
        {
            _candidato = new Candidato();
            _faker = new Faker();
        }

        public Candidato Build() => _candidato;

        public CandidaturaBuilder NomeValido(string nome = null)
        {
            if (string.IsNullOrEmpty(null))
                nome = _faker.Name.FirstName();

            _candidato.Nome = nome;
            return this;
        }

        public CandidaturaBuilder DataNascimentoValida(DateTime data = default)
        {
            if (data == default)
                data = _faker.Date.Past(50, DateTime.Now.AddYears(-21));

            _candidato.Nascimento = data;
            return this;
        }

        public CandidaturaBuilder EscolaridadeValida(Escolaridade escolaridadeCandidato)
        {
            _candidato.Escolaridade = escolaridadeCandidato;
            return this;
        }

        public CandidaturaBuilder CPFValido(string cpf = null)
        {
            if (cpf == null)
                cpf = _faker.Random.Word();

            _candidato.Cpf = cpf;
            return this;
        }

        public CandidaturaBuilder NumerodeInscricaoValido(int num = 0)
        {
            if (num == 0)
                num = _faker.Random.Int(1, 200000);

            _candidato.NumeroInscricao = num;
            return this;
        }

        public CandidaturaBuilder IDValido(int id = 0)
        {
            if (id == 0)
                id = _faker.Random.Int(1, 200000);

            _candidato.Id = id;
            return this;
        }

        public CandidaturaBuilder ConcursoValido
                                  (Concurso concurso = null)
        {
            if (concurso == null)
                concurso = new ConcursoBuilder().IDValido()
                                                .TituloValido()
                                                .DataValida()
                                                .LocalValido()
                                                .EscolaridadeValida
                                                (_candidato.Escolaridade)
                                                .Build();

            _candidato.Concurso = concurso;
            return this;
        }

        public CandidaturaBuilder ConcursoInvalido
                                  (Concurso concursoEscolaridadeInvalida = null)
        {
            if (concursoEscolaridadeInvalida == null)
            {
                switch (_candidato.Escolaridade)
                {
                    case Escolaridade.Fundamental:
                        concursoEscolaridadeInvalida = new ConcursoBuilder()
                                                           .IDValido()
                                                           .TituloValido()
                                                           .DataValida()
                                                           .LocalValido()
                                                           .EscolaridadeValida
                                                           (Escolaridade.Medio)
                                                           .Build();
                        break;

                    case Escolaridade.Medio:
                        concursoEscolaridadeInvalida = new ConcursoBuilder()
                                                           .IDValido()
                                                           .TituloValido()
                                                           .DataValida()
                                                           .LocalValido()
                                                           .EscolaridadeValida
                                                           (Escolaridade.Superior)
                                                           .Build();
                        break;

                    case Escolaridade.Superior:
                        throw new ApplicationException("A escolaridade do candidato " +
                                                       "não pode ser superior para " +
                                                       "realizar o teste de inaptidão.");
                }
            }

            _candidato.Concurso = concursoEscolaridadeInvalida;
            return this;
        }

        public CandidaturaBuilder DataNascimentoInvalida(DateTime dataInvalida = default)
        {
            if (dataInvalida == default)
                dataInvalida = _faker.Date.Past(20, DateTime.Now);

            _candidato.Nascimento = dataInvalida;
            return this;
        }
    }
}