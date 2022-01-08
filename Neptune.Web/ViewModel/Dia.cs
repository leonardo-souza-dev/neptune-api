using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Neptune.Domain;

namespace Neptune.Web.ViewModel
{
    public class Dia
    {
        public List<Transacao> Transacoes { get; } = new();
        private decimal SaldoDoDiaAnterior { get; }
        public DateTime Data { get; }

        public Dia(DateTime data, IEnumerable<TransacaoDomain> transacoesModel, decimal saldoDoDiaAnterior)
        {
            Data = data;
            SaldoDoDiaAnterior = saldoDoDiaAnterior;
            foreach (var transacaoModel in transacoesModel)
            {
                Transacoes.Add(new Transacao(transacaoModel));
            }
        }

        public decimal ObterSaldoDoDia() =>
            SaldoDoDiaAnterior - Transacoes.Sum(x => x.Valor);

        public void AdicionarTransacao(Transacao transacaoViewModel) =>
            Transacoes.Add(transacaoViewModel);
    }
}
