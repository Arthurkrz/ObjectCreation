using Bogus;
using IniciandoTestes.Domain.Entities;
using IniciandoTestes.Domain.Enum;
using System;

namespace IniciandoTestes.Tests.Builders
{
    internal class ConcursoBuilder
    {
        private readonly Concurso _concurso;
        private readonly Faker _faker;

        public ConcursoBuilder()
        {
            _concurso = new Concurso();
            _faker = new Faker();
        }

        public Concurso Build() => _concurso;

        public ConcursoBuilder IDValido(int id = 0)
        {
            if (id == 0)
                id = _faker.Random.Int(1, 200000);

            _concurso.Id = id;
            return this;
        }

        public ConcursoBuilder TituloValido(string titulo = null)
        {
            if (titulo == null)
                titulo = _faker.Random.Word();

            _concurso.Titulo = titulo;
            return this;
        }

        public ConcursoBuilder DataValida(DateTime data = default)
        {
            if (data == default)
                data = _faker.Date.Between(DateTime.Now, DateTime.Now.AddYears(1));

            _concurso.Data = data;
            return this;
        }

        public ConcursoBuilder LocalValido(string local = null)
        {
            if (local == null)
                local = _faker.Address.StreetAddress();

            _concurso.Local = local;
            return this;
        }

        public ConcursoBuilder EscolaridadeValida(Escolaridade escolaridadeConcurso)
        {
            _concurso.Escolaridade = escolaridadeConcurso;
            return this;
        }
    }
}
