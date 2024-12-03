using IniciandoTestes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IniciandoTestes.Domain.Contracts.ServiceContracts
{
    internal interface IClienteService
    {
        void AddCliente(Cliente cliente);

        string ExemploAtrasadinhoQueNaoAvisaEDepoisEncheOSaco();
    }
}
