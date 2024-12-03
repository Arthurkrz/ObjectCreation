using IniciandoTestes.Domain.Contracts.RepositoryContracts;
using IniciandoTestes.Domain.Entities;
using System;
using System.Collections.Generic;

namespace IniciandoTestes.Tests.Fakes
{
    internal class ClienteRepositoryFake : IClienteRepository
    {
        public void AddCliente(Cliente cliente) { }

        public List<Cliente> GetAll()
        {
            return new List<Cliente>();
        }

        public Cliente GetCliente(int id)
        {
            int idFake = 1261421885;
            if (id == idFake )
                return new Cliente()
                {
                    Nome = "Arthur",
                    Nascimento = new DateTime(2003, 04, 12),
                    Id = idFake
                };

            return null;
        }

        public Cliente GetCliente(string nome)
        {
            throw new NotImplementedException();
        }
    }
}
