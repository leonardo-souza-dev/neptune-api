using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Neptune.Models;

namespace Neptune.Web.ViewModel
{
    public class TransacoesViewModel
    {
        //TODO: trocar esse valor 
        public decimal SaldoUltimoDiaMesAnterior { get; private set; }
        public string UltimoDiaMesAnterior { get { return _ultimoDiaMesAnterior.ToString("dd/MM/yyyy"); } }
        public List<DiaViewModel> Dias { get; private set; } = new();

        private IEnumerable<Transacao> _transacoesModel;
        private DateTime _ultimoDiaMesAnterior
        {
            get
            {
                var data = _transacoesModel.First().Data;
                return new DateTime(data.Year, data.Month, 1).AddDays(-1);
            }
        }


        public TransacoesViewModel(IEnumerable<Transacao> transacoesModel, decimal saldoUltimoDiaMesAnterior)
        {
            transacoesModel.ToList().Sort((x, y) => x.Data.CompareTo(y.Data));
            var transacoes = transacoesModel;
            _transacoesModel = transacoes;
            SaldoUltimoDiaMesAnterior = saldoUltimoDiaMesAnterior;

            var dias = transacoes
                .GroupBy(item => new {item.Data.Month, item.Data.Day}).Select(x => x.First())
                .Select(d => d.Data).ToList();

            var saldoDiaAnterior = 0M;
            for (var index = 0; index < dias.Count; index++)
            {
                var dia = dias[index];
                var transacoesDia = transacoes.Where(x => x.Data.Day == dia.Day);

                if (index == 0)
                {
                    var diaViewModel = new DiaViewModel(dia, transacoesDia, saldoUltimoDiaMesAnterior);
                    saldoDiaAnterior = diaViewModel.ObterSaldoDoDia();
                    Dias.Add(diaViewModel);
                }
                else
                {
                    var diaViewModel = new DiaViewModel(dia, transacoesDia, saldoDiaAnterior);
                    saldoDiaAnterior = diaViewModel.ObterSaldoDoDia();
                    Dias.Add(diaViewModel);
                }
            }
        }

        public void AdicionarTransacao(TransacaoViewModel transacaoViewModel)
        {
            var dia = Dias.FirstOrDefault(x => (x.Data.Day == transacaoViewModel.Data.Day &&
                x.Data.Month == transacaoViewModel.Data.Month &&
                x.Data.Year == transacaoViewModel.Data.Year));

            dia.AdicionarTransacao(transacaoViewModel);
        }
    }

    public class DiaViewModel
    {
        public List<TransacaoViewModel> Transacoes { get; set; } = new();
        public decimal SaldoDoDiaAnterior { get; set; }
        public DateTime Data { get; set; }

        public DiaViewModel(DateTime data, IEnumerable<Transacao> transacoesModel, decimal saldoDoDiaAnterior)
        {
            Data = data;
            SaldoDoDiaAnterior = saldoDoDiaAnterior;
            foreach (Transacao transacaoModel in transacoesModel)
            {
                Transacoes.Add(new TransacaoViewModel(transacaoModel));
            }
        }

        public decimal ObterSaldoDoDia() =>
            SaldoDoDiaAnterior - Transacoes.Sum(x => x.Valor);

        public void AdicionarTransacao(TransacaoViewModel transacaoViewModel) =>
            Transacoes.Add(transacaoViewModel);
    }

    public class TransacaoViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Conta { get; set; }
        public int ContaId { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }

        public TransacaoViewModel(Transacao transacao)
        {
            Id = transacao.Id;
            Descricao = transacao.Descricao;
            ContaId = transacao.ContaId;
            Valor = transacao.Valor;
            Data = transacao.Data;
        }
    }
}
