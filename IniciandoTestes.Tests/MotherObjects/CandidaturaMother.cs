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
    }
}