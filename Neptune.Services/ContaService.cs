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

        public List<ContaDomain> ObterTodas()
        {
            return _contaRepository.ObterTodas();
        }

        public ContaDomain Obter(int id)
        {
            return _contaRepository.Obter(id);
        }
    }
}
