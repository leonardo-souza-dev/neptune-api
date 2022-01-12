using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Neptune.Domain
{
    public class Mes
    {
        public MesTransacao MesTransacao { get; private set; }
        public decimal SaldoUltimoDiaMesAnterior { get; private set; }
        public List<Dia> Dias { get; private set; } = new List<Dia>();

        public Mes(MesTransacao mesTransacao, decimal saldoUltimoDiaMesAnterior, List<Transacao> transacoes)
        {
            MesTransacao = mesTransacao;
            SaldoUltimoDiaMesAnterior = saldoUltimoDiaMesAnterior;

            var transacoesDiaGrupo = transacoes.GroupBy(x => x.Data.Day).ToList();
            decimal saldoDiaAnterior = 0;

            for(int i = 0; i < transacoesDiaGrupo.Count(); i++)
            {
                var transacoesDia = transacoesDiaGrupo[i];

                var dataDia = new DateTime(mesTransacao.Ano, mesTransacao.Mes, transacoesDia.Key);
                Dia dia = null;

                if (i == 0)
                {
                    dia = new Dia(dataDia, transacoesDia.ToList(), saldoUltimoDiaMesAnterior);
                    saldoDiaAnterior = dia.SaldoDoDia;
                }
                else
                {
                    dia = new Dia(dataDia, transacoesDia.ToList(), saldoDiaAnterior);
                }

                Dias.Add(dia);
            }
        }
    }
}
