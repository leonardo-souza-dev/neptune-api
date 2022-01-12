using Neptune.Domain;
using Neptune.Infra;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Util;

namespace Neptune.Application
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IContaRepository _contaRepository;

        public TransacaoService(ITransacaoRepository transacaoRepository, IContaRepository contaRepository)
        {
            _transacaoRepository = transacaoRepository;
            _contaRepository = contaRepository;
        }

        public List<Transacao> ObterTodas()
        {
            return _transacaoRepository.ObterTodas();
        }

        public async Task<Mes> ObterMes(MesTransacao mesTransacao)
        {
            var transacoes = await _transacaoRepository.Obter(mesTransacao.Ano, mesTransacao.Mes);
            
            var saldoUltimoDiaMesAnterior = await ObterSaldoUltimoDia(mesTransacao);

            var mes = new Mes(mesTransacao, saldoUltimoDiaMesAnterior, transacoes);

            return mes;
        }

        public Transacao Criar(Transacao transacao)
        {
            return _transacaoRepository.Criar(transacao);
        }

        public Transacao Atualizar(Transacao transacao)
        {
            return _transacaoRepository.Atualizar(transacao);
        }

        private async Task<decimal> ObterSaldoUltimoDia(MesTransacao mesTransacao)
        {
            var mesAnterior = mesTransacao.ObterMesAnterior();
            var transacoes = await _transacaoRepository.Obter(mesAnterior.Ano, mesAnterior.Mes);
            var contas = await _contaRepository.ObterTodas();
            var saldoInicialContas = contas.Sum(x => x.SaldoInicial);
            var somaMes = transacoes.Sum(x => x.Valor);

            return saldoInicialContas - somaMes;
        }
    }
}
