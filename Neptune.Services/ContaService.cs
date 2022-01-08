using Neptune.Domain;
using Neptune.Infra;
using System;
using System.Collections.Generic;

namespace Neptune.Application
{
    public class ContaService : IContaService
    {
        private readonly IContaRepository _contaRepository;

        public ContaService(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public List<Conta> ObterTodas()
        {
            return _contaRepository.ObterTodas();
        }

        public Conta Obter(int id)
        {
            return _contaRepository.Obter(id);
        }
    }
}
