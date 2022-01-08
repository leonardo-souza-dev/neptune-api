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

        public List<Transacao> ObterTodas()
        {
            return _transacaoRepository.ObterTodas();
        }

        public List<Transacao> ObterPorContaEMes(int contaId, int mes, int ano)
        {
            return _transacaoRepository.ObterPorContaEMes(contaId, mes, ano);
        }

        public Transacao Criar(Transacao transacao)
        {
            return _transacaoRepository.Criar(transacao);
        }

        public Transacao Atualizar(Transacao transacao)
        {
            return _transacaoRepository.Atualizar(transacao);
        }
    }
}
