using System;
using System.Collections.Generic;
using System.Text;
using IniciandoTestes.Domain.Entities;

namespace IniciandoTestes.Domain.Contracts.ServiceContracts
{
    internal interface IFuncionarioService
    {
        public void AdicionarFuncionario(Funcionario funcionario);
    }
}
