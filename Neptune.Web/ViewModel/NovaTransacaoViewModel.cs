using System;
using System.Collections.Generic;
using System.Linq;
using Neptune.Models;

namespace Neptune.Web.ViewModel
{
    public class NovaTransacaoViewModel
    {
        public DateTime Data { get; set; } = DateTime.Now;
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int ContaId { get; set; }

        public Transacao ToModel()
        {
            return new Transacao(Data, Descricao, Valor, ContaId);
        }
    }
}
