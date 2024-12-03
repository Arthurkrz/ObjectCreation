using System;
using IniciandoTestes.Domain.Enum;

namespace IniciandoTestes.Domain.Entities
{
    public class Concurso
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime Data { get; set; }
        public string Local { get; set; }
        public Escolaridade Escolaridade { get; set; }
    }
}
