using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Neptune.Domain
{
    public class Dia
    {
        public int NumeroDia { get; private set; }
        public DateTime Data { get; private set; }
        public List<Transacao> Transacoes { get; private set; } = new List<Transacao>();

        public Dia(DateTime data, List<Transacao> transacoes)
        {
            NumeroDia = data.Day;
            Data = data;

            foreach (var transacao in transacoes)
            {
                Transacoes.Add(new Transacao(transacao.Id, transacao.Data, transacao.Descricao, transacao.Valor, transacao.ContaId));
            }

            Transacoes.Sort((x, y) => x.Data.CompareTo(y.Data));
        }
    }
}
