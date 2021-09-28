using System;
using System.Collections.Generic;
using System.Linq;
using Neptune.Models;

namespace Neptune.Web.ViewModel
{
    public class TransacoesViewModel
    {
        //TODO: trocar esse valor 
        public decimal SaldoUltimoDiaMesAnterior { get { return 333; } }

        public string UltimoDiaMesAnterior { get; private set; }
        public List<DiaViewModel> Dias { get; set; } = new();


        public TransacoesViewModel(IEnumerable<Transacao> transacoesModel)
        {
            var data = transacoesModel.First().Data;
            UltimoDiaMesAnterior = new DateTime(data.Year, data.Month, 1).AddDays(-1).ToString("dd/MM/yyyy");

            var dias = transacoesModel
                .GroupBy(item => new
                {
                    item.Data.Month,
                    item.Data.Day
                })
                .Select(x => x.First())
                .Select(d => d.Data)
                .ToList();

            foreach (var dia in dias)
            {
                var transacoesModelDia = transacoesModel.Where(x => x.Data.Day == dia.Day);
                Dias.Add(new DiaViewModel(dia, transacoesModelDia));
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
        public decimal SaldoDoDia { get; set; }
        public DateTime Data { get; set; }

        public DiaViewModel(DateTime data, IEnumerable<Transacao> transacoesModel)
        {
            Data = data;
            foreach (Transacao transacaoModel in transacoesModel)
            {
                Transacoes.Add(new TransacaoViewModel(transacaoModel));
            }
        }

        public decimal ObterSaldoDoDia()
        {
            return Transacoes.Sum(x => x.Valor);
        }

        public void AdicionarTransacao(TransacaoViewModel transacaoViewModel)
        {
            Transacoes.Add(transacaoViewModel);
        }
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
