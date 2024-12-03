using System;
using IniciandoTestes.Domain.Enum;

namespace IniciandoTestes.Domain.Entities
{
    public class Candidato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int NumeroInscricao { get; set; }        
        public DateTime Nascimento { get; set; }
        public string Cpf { get; set; }
        public Escolaridade Escolaridade { get; set; }
        public Concurso Concurso { get; set; }
    }
}
