using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Neptune.Models;

namespace Neptune.Web.ViewModel
{
    public class DiaViewModel
    {
        public List<TransacaoViewModel> Transacoes { get; } = new();
        private decimal SaldoDoDiaAnterior { get; }
        public DateTime Data { get; }

        public DiaViewModel(DateTime data, IEnumerable<Transacao> transacoesModel, decimal saldoDoDiaAnterior)
        {
            Data = data;
            SaldoDoDiaAnterior = saldoDoDiaAnterior;
            foreach (var transacaoModel in transacoesModel)
            {
                Transacoes.Add(new TransacaoViewModel(transacaoModel));
            }
        }

        public decimal ObterSaldoDoDia() =>
            SaldoDoDiaAnterior - Transacoes.Sum(x => x.Valor);

        public void AdicionarTransacao(TransacaoViewModel transacaoViewModel) =>
            Transacoes.Add(transacaoViewModel);
    }
}
