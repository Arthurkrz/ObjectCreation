using Bogus;
using IniciandoTestes.Domain.Entities;
using IniciandoTestes.Domain.Enum;
using System;

namespace IniciandoTestes.Tests.Builders
{
    internal class FuncionarioBuilder
    {
        private Funcionario _funcionario;
        private readonly Faker _faker;

        public FuncionarioBuilder()
        {
            _funcionario = new Funcionario();
            _faker = new Faker();
        }

        public Funcionario Build() => _funcionario;

        public FuncionarioBuilder NomeValido
                                  (string nome = null)
        {
            if (nome == null)
                nome = _faker.Name.FullName();
            _funcionario.Nome = nome;
            return this;
        }

        public FuncionarioBuilder NomeInvalido
                                  (string nomeInvalido = null)
        {
            if (nomeInvalido == null)
                nomeInvalido = "a";

            _funcionario.Nome = nomeInvalido;
            return this;
        }

        public FuncionarioBuilder DataNascimentoValida
                                  (DateTime data = default)
        {
            if (data == default)
                data = _faker.Date.Past(50, 
                       DateTime.Now.AddYears(-21));

            _funcionario.Nascimento = data;
            return this;
        }

        public FuncionarioBuilder DataNascimentoInvalida
                                  (DateTime dataInvalida = default)
        {
            if (dataInvalida == default)
                dataInvalida = DateTime.Now.AddYears(-20);

            _funcionario.Nascimento = dataInvalida;
            return this;
        }

        public FuncionarioBuilder SalarioValido
                                  (Senioridade senioridadeFuncionario, 
                                   int salario = 0)
        {
            if (salario == 0)
            {
                switch (senioridadeFuncionario)
                {
                    case Senioridade.NA:
                        break;

                    case Senioridade.Junior:
                        salario = _faker.Random.Int(3201, 5499);
                        break;

                    case Senioridade.Pleno:
                        salario = _faker.Random.Int(5499, 7999);
                        break;

                    case Senioridade.Senior:
                        salario = _faker.Random.Int(8001, 49999);
                        break;
                }
            }

            _funcionario.Salario = salario;
            return this;
        }

        public FuncionarioBuilder SalarioInvalido
                                  (int salario)
        {
            _funcionario.Salario = salario;
            return this;
        }

        public FuncionarioBuilder SenioridadeValida
                                  (Senioridade senioridade)
        {
            _funcionario.Senioridade = senioridade;
            return this;
        }
    }
}
