using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Neptune.Models;

namespace Neptune.Web.ViewModel
{
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
