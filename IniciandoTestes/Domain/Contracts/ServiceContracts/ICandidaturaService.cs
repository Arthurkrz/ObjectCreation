using IniciandoTestes.Domain.Entities;

namespace IniciandoTestes.Domain.Contracts.ServiceContracts
{
    internal interface ICandidaturaService
    {
        int CriarCandidatura(Candidato candidato);
    }
}
