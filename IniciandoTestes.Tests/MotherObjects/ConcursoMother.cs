using System;
using System.Collections.Generic;
using System.Text;
using IniciandoTestes.Domain.Entities;
using IniciandoTestes.Domain.Enum;
using Bogus;
using Xunit;
using Moq;

namespace IniciandoTestes.Tests.MotherObjects
{
    internal static class ConcursoMother
    {
        public static Concurso GetConcursoPorEscolaridade
                               (Escolaridade escolaridade)
        {
            Faker<Concurso> faker = new Faker<Concurso>();
            faker.RuleFor(x => x.Id, cc => cc.Random.Int(1000, 200000))
                 .RuleFor(x => x.Titulo, cc => cc.Name.FindName())
                 .RuleFor(x => x.Data, cc => cc.Date.Between
                                               (DateTime.Now,
                                                DateTime.Now.AddYears(1)))
                 .RuleFor(x => x.Local, cc => cc.Address.StreetAddress())
                 .RuleFor(x => x.Escolaridade, escolaridade);

            return faker.Generate();
        }
    }
}
