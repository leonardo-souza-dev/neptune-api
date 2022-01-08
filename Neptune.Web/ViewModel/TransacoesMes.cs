using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Neptune.Domain;

namespace Neptune.Web.ViewModel
{
    public class TransacoesMes 
    {
        public decimal SaldoUltimoDiaMesAnterior { get; private set; }
        public string UltimoDiaMesAnterior { get { return _ultimoDiaMesAnterior.ToString("dd/MM/yyyy"); } }
        public List<Dia> Dias { get; private set; } = new();

        public List<Conta> Contas { get; private set; } = new List<Conta>();

        public int Ano;
        public int Mes;

        private DateTime _ultimoDiaMesAnterior
        {
            get
            {
                return new DateTime(Ano, Mes, 1).AddDays(-1);
            }
        }

        public TransacoesMes(int ano, 
                             int mes, 
                             IEnumerable<TransacaoDomain> transacoesModel, 
                             decimal saldoUltimoDiaMesAnterior, 
                             List<ContaDomain> contasAtivasModel,
                             List<ContaDomain> todasContasModel)
        {
            Ano = ano;
            Mes = mes;

            transacoesModel.ToList().Sort((x, y) => x.Data.CompareTo(y.Data));

            SaldoUltimoDiaMesAnterior = saldoUltimoDiaMesAnterior;

            var dias = transacoesModel
                .GroupBy(item => new {item.Data.Month, item.Data.Day}).Select(x => x.First())
                .Select(d => d.Data).ToList();

            var saldoDiaAnterior = 0M;
            for (var index = 0; index < dias.Count; index++)
            {
                var dia = dias[index];
                var transacoesDia = transacoesModel.Where(x => x.Data.Day == dia.Day);

                if (index == 0)
                {
                    var diaViewModel = new Dia(dia, transacoesDia, saldoUltimoDiaMesAnterior);
                    saldoDiaAnterior = diaViewModel.ObterSaldoDoDia();
                    Dias.Add(diaViewModel);
                }
                else
                {
                    var diaViewModel = new Dia(dia, transacoesDia, saldoDiaAnterior);
                    saldoDiaAnterior = diaViewModel.ObterSaldoDoDia();
                    Dias.Add(diaViewModel);
                }
            }

            todasContasModel.ForEach(x => 
            {
                var ativo = false;
                foreach (var contaAtiva in contasAtivasModel)
                {
                    if (x.Id == contaAtiva.Id)
                    {
                        ativo = true;
                    }
                }
                Contas.Add(new Conta(x.Id, x.Nome, ativo)); 
            });
        }

        public void AdicionarTransacao(Transacao transacaoViewModel)
        {
            var dia = Dias.FirstOrDefault(x => (x.Data.Day == transacaoViewModel.Data.Day &&
                x.Data.Month == transacaoViewModel.Data.Month &&
                x.Data.Year == transacaoViewModel.Data.Year));

            if (dia == null)
            {
                var diaAnterior = Dias.FirstOrDefault(x => (x.Data.Day < transacaoViewModel.Data.AddDays(-1).Day &&
                                                            x.Data.Month == transacaoViewModel.Data.Month &&
                                                            x.Data.Year == transacaoViewModel.Data.Year));

                var saldoDoDiaAnterior = diaAnterior.ObterSaldoDoDia();
                var transacoes = new List<TransacaoDomain>() { new TransacaoDomain(transacaoViewModel.Id, transacaoViewModel.Data, transacaoViewModel.Descricao, transacaoViewModel.Valor, transacaoViewModel.ContaId) };

                var novoDia = new Dia(transacaoViewModel.Data, transacoes, saldoDoDiaAnterior);

                Dias.Add(novoDia);                
            }
            else
                dia.AdicionarTransacao(transacaoViewModel);

            Dias.Sort((x, y) => x.Data.CompareTo(y.Data));
        }

        public int ObterMesAnterior()
        {
            return new DateTime(Ano, Mes, 1).AddMonths(-1).Month;
        }

        public int ObterMesSeguinte()
        {
            return new DateTime(Ano, Mes, 1).AddMonths(1).Month;
        }

        public int ObterAnoDoMesAnterior()
        {
            return new DateTime(Ano, Mes, 1).AddMonths(-1).Year;
        }

        public int ObterAnoDoMesSeguinte()
        {
            return new DateTime(Ano, Mes, 1).AddMonths(1).Year;
        }
    }
}
