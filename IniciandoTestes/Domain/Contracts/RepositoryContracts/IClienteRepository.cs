using IniciandoTestes.Domain.Entities;
using System;
using System.Collections.Generic;

namespace IniciandoTestes.Domain.Contracts.RepositoryContracts
{
    public interface IClienteRepository
    {
        Cliente GetCliente(int id);
        Cliente GetCliente(string nome);
        List<Cliente> GetAll();
        void AddCliente(Cliente cliente);

    }
}
