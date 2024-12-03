using IniciandoTestes.Domain.Entities;
using System;
using Bogus;
using System.Collections.Generic;

namespace IniciandoTestes.Tests.MotherObjects
{
    internal static class ClienteMother
    {
        public static Cliente GetClienteValido()
        {

            var faker = new Faker<Cliente>();
            faker.RuleFor(x => x.Nome, f => f.Name.FullName())
                 .RuleFor(x => x.Nascimento, f => f.Date.Past
                                                    (50, DateTime.Now.AddYears(-18)))
                 .RuleFor(x => x.Id, f => f.Random.Int());

            return faker.Generate();
        }

        public static Cliente GetClienteSemId()
        {
            Faker<Cliente> faker = new Faker<Cliente>();
            faker.RuleFor(x => x.Nome, f => f.Name.FullName())
                 .RuleFor(x => x.Nascimento, f => f.Date.Past
                                                    (50, DateTime.Now.AddYears(-18)));

            return faker.Generate();
        }
    }
}

