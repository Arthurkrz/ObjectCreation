using IniciandoTestes.Domain.Contracts.ServiceContracts;
using IniciandoTestes.Domain.Contracts.RepositoryContracts;
using System;

namespace IniciandoTestes.Services
{
    public class CalculadoraService : ICalculadoraService
    {
        private readonly ICalculadoraRepository _calculadoraRepository;

        public CalculadoraService(ICalculadoraRepository calculadoraRepository)
        {
            _calculadoraRepository = calculadoraRepository;
        }

        public double SomarNumeros(double n1, double n2)
        {
            if (n1 < 0 || n2 < 0)
            {
                return -1;
            }

            return n1 + n2;
        }
    }
}
