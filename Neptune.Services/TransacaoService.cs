using Neptune.Domain;
using Neptune.Infra;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<List<Dia>> ObterPorDataEContas(int ano, int mes, int[] contasId)
        {
            var transacoes = await _transacaoRepository.Obter(ano, mes, contasId);

            var dias = new List<Dia>();

            var transacoesDiaGrupo = transacoes.GroupBy(x => x.Data.Day);

            foreach (var transacoesDiaItem in transacoesDiaGrupo)
            {
                var numeroDia = transacoesDiaItem.Key;
                var dia = new Dia(new DateTime(ano, mes, numeroDia), transacoesDiaItem.ToList());
                dias.Add(dia);
            }

            return dias;
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
