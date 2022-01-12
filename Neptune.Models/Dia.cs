using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Neptune.Domain
{
    public class Dia
    {
        public DateTime Data { get; private set; }
        public List<Transacao> Transacoes { get; private set; } = new List<Transacao>();

        public decimal? _saldoDoDia;
        public decimal SaldoDoDia 
        { 
            get
            {
                if (!_saldoDoDia.HasValue)
                {
                    throw new NullReferenceException("SaldoDoDia");
                }
                else
                {
                    return _saldoDoDia.Value;
                }
            }
            set
            {
                _saldoDoDia = value;
            }
        }

        private decimal _somaTransacoes 
        { 
            get
            {
                return Transacoes.Sum(x => x.Valor);
            }
        }

        public Dia(DateTime data, List<Transacao> transacoes, decimal saldoUltimoDiaMesAnterior)
        {
            Data = data;

            foreach (var transacao in transacoes)
            {
                Transacoes.Add(new Transacao(transacao.Id, transacao.Data, transacao.Descricao, transacao.Valor, transacao.ContaId));
            }

            Transacoes.Sort((x, y) => x.Data.CompareTo(y.Data));

            SaldoDoDia = saldoUltimoDiaMesAnterior - Transacoes.Sum(x => x.Valor);
        }

        public void Atualizar(decimal saldoDiaAnterior)
        {
            _saldoDoDia = saldoDiaAnterior - _somaTransacoes;
        }
    }
}
