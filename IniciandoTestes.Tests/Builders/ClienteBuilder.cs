using Bogus;
using IniciandoTestes.Domain.Entities;
using System;

namespace IniciandoTestes.Tests.Builders
{
    internal class ClienteBuilder
    {
        private readonly Cliente _cliente;
        private readonly Faker _faker;

        public ClienteBuilder()
        {
            _cliente = new Cliente();
            _faker = new Faker();
        }

        public void NomeValido(string nome = null)
        {

        }

        public void NascimentoValido(DateTime data = default)
        {
            
        }

        public void IDValido(int id = 0)
        {

        }

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

