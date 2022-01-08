using System;
using System.Collections.Generic;
using System.Linq;
using Neptune.Domain;

namespace Neptune.Web.ViewModel
{
    public class NovaTransacao
    {
        public DateTime Data { get; set; } = DateTime.Now;
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int ContaId { get; set; }

        public TransacaoDomain ToDomain()
        {
            return new TransacaoDomain(Data, Descricao, Valor, ContaId);
        }
    }
}
