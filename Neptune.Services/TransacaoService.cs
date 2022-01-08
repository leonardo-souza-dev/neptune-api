using Neptune.Domain;
using Neptune.Infra;
using System;
using System.Collections.Generic;

namespace Neptune.Application
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;

        public TransacaoService(ITransacaoRepository transacaoRepository)
        {
            _transacaoRepository = transacaoRepository;
        }

        public List<TransacaoDomain> ObterTodas()
        {
            return _transacaoRepository.ObterTodas();
        }

        public List<TransacaoDomain> ObterPorConta(int contaId)
        {
            return new List<TransacaoDomain>();
        }
    }
}
