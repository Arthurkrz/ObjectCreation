using System;
using System.Collections.Generic;
using System.Text;
using IniciandoTestes.Domain.Entities;

namespace IniciandoTestes.Domain.Contracts.RepositoryContracts
{
    public interface IFuncionarioRepository
    {
        public void AdicionarFuncionario(Funcionario funcionario);
    }
}
