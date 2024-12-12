using IniciandoTestes.Domain.Entities;

namespace IniciandoTestes.Domain.Contracts.ServiceContracts
{
    internal interface IClienteService
    {
        void AddCliente(Cliente cliente);
        string ExemploAtrasadinhoQueNaoAvisaEDepoisEncheOSaco();
    }
}
