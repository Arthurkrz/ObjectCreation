using System;
using IniciandoTestes.Domain.Entities;
using IniciandoTestes.Domain.Contracts.RepositoryContracts;
using IniciandoTestes.Domain.Contracts.ServiceContracts;
using IniciandoTestes.Domain.Enum;

namespace IniciandoTestes.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public void AdicionarFuncionario(Funcionario funcionario)
        {
            if (funcionario == null)
            {
                throw new Exception("Funcionario não pode ser nulo");
            }

            if (funcionario.Nome.Length < 3)
            {
                throw new FormatException("Formato incorreto de nome.");
            }

            if (funcionario.Nascimento > DateTime.Now.AddYears(-21))
            {
                throw new Exception("Funcionario muito novo para o cargo");
            }

            switch (funcionario.Senioridade)
            {
                case Senioridade.Junior:
                    {
                        if (funcionario.Salario <= 3200 || funcionario.Salario >= 5500)
                            throw new Exception("Salario incompatível com o cargo");
                        break;
                    }

                case Senioridade.Pleno:
                    {
                        if (funcionario.Salario <= 5500 || funcionario.Salario >= 8000)
                            throw new Exception("Salario incompatível com o cargo");
                        break;
                    }

                case Senioridade.Senior:
                    {
                        if (funcionario.Salario <= 8000 || funcionario.Salario >= 500000)
                            throw new Exception("Salario incompatível com o cargo");
                        break;
                    }
            }

        }

    }
}
