using IniciandoTestes.Domain.Entities;
using System;

namespace IniciandoTestes.Domain.Contracts.RepositoryContracts
{
    public interface ICandidaturaRepository
    {
        public Concurso GetConcurso(int id);
        public int AdicionaCandidato(Candidato candidato);
        public int CriarCandidatura(Candidato candidato);
        public bool CandidatoAptoAoConcurso(Candidato candidato, Concurso concurso);
        public bool CandidatoEhValido(Candidato candidato);
    }
}
