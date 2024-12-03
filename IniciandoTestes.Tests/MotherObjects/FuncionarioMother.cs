using IniciandoTestes.Domain.Entities;
using IniciandoTestes.Domain.Enum;
using Bogus;
using System;

namespace IniciandoTestes.Tests.MotherObjects
{
    internal static class FuncionarioMother
    {
        public static Funcionario GetFuncionarioValidoPorSenioridade(Senioridade senioridade)
        {
            Faker<Funcionario> faker = new Faker<Funcionario>();
            faker.RuleFor(x => x.Nome, f => f.Name.FindName())
                 .RuleFor(x => x.Nascimento, f => f.Date.Past(50, DateTime.Now.AddYears(-21)))
                 .RuleFor(x => x.Senioridade, senioridade);

            switch (senioridade)
            {
                case Senioridade.Junior:
                    faker.RuleFor(x => x.Salario, f => f.Random.Double(3200, 5500));
                    break;
                case Senioridade.Pleno:
                    faker.RuleFor(x => x.Salario, f => f.Random.Double(5500, 8000));
                    break;
                case Senioridade.Senior:
                    faker.RuleFor(x => x.Salario, f => f.Random.Double(8000, 20000));
                    break;
            }

            return faker.Generate();

        }
    }
}
