using System;
using IniciandoTestes.Domain.Entities;
using IniciandoTestes.Domain.Enum;
using System.Collections.Generic;
using System.Text;
using Bogus;
using Xunit;
using Moq;

namespace IniciandoTestes.Tests.MotherObjects
{
    internal static class CandidaturaMother
    {
        public static Candidato GetCandidatoAptoPorEscolaridade(Escolaridade escolaridadeCandidato, 
                                                                Escolaridade escolaridadeConcurso)
        {
            Faker<Candidato> faker = new Faker<Candidato>();
            faker.RuleFor(x => x.Nome, c => c.Name.FindName())
                 .RuleFor(x => x.Nascimento, c => c.Date.Past(50, DateTime.Now.AddYears(-21)))
                 .RuleFor(x => x.Escolaridade, escolaridadeCandidato)
                 .RuleFor(x => x.Cpf, c => c.Random.Words(1))
                 .RuleFor(x => x.NumeroInscricao, c => c.Random.Int(200, 300))
                 .RuleFor(x => x.Id, c => c.Random.Int(1000, 200000))
                 .RuleFor(x => x.Concurso, ConcursoMother.GetConcursoPorEscolaridade
                                                     (escolaridadeConcurso));

            return faker.Generate();
        }

        public static Candidato GetCandidatoInapto(Concurso concurso)
        {
            Escolaridade escolaridadeCandidato = Escolaridade.NA;
            if (concurso.Escolaridade == Escolaridade.Fundamental)
            {
                throw new ApplicationException("A escolaridade do concurso não" +
                                               " pode ser do tipo 'Fundamental'.");
            }
            else if (concurso.Escolaridade == Escolaridade.Medio)
            {
                escolaridadeCandidato = Escolaridade.Fundamental;
            }
            else if (concurso.Escolaridade == Escolaridade.Superior)
            {
                escolaridadeCandidato = Escolaridade.Medio;
            }

            Faker<Candidato> faker = new Faker<Candidato>();
            faker.RuleFor(x => x.Nome, c => c.Name.FirstName())
                 .RuleFor(x => x.Nascimento, c => c.Date.Past(50, DateTime.Now.AddYears(-21)))
                 .RuleFor(x => x.Escolaridade, escolaridadeCandidato)
                 .RuleFor(x => x.Cpf, c => c.Random.Words(1))
                 .RuleFor(x => x.NumeroInscricao, c => c.Random.Int(200, 300))
                 .RuleFor(x => x.Id, c => c.Random.Int(1000, 200000))
                 .RuleFor(x => x.Concurso, ConcursoMother.GetConcursoPorEscolaridade
                                                     (concurso.Escolaridade));

            return faker.Generate();
        }

        public static Candidato GetCandidatoInvalido()
        {
            Faker<Candidato> faker = new Faker<Candidato>();
            faker.RuleFor(x => x.Nome, c => c.Name.FirstName())
                 .RuleFor(x => x.Nascimento, c => c.Date.Past(20, DateTime.Now))
                 .RuleFor(x => x.Escolaridade, Escolaridade.Superior)
                 .RuleFor(x => x.Cpf, c => c.Random.Words(1))
                 .RuleFor(x => x.NumeroInscricao, c => c.Random.Int(200, 300))
                 .RuleFor(x => x.Id, c => c.Random.Int(1000, 200000))
                 .RuleFor(x => x.Concurso, ConcursoMother.GetConcursoPorEscolaridade
                                                     (Escolaridade.Fundamental));

            return faker.Generate();
        }
    }
}