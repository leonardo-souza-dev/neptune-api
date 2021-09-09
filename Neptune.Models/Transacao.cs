using System;

namespace Neptune.Models
{
    public class Transacao 
    {
        public byte IdView { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int Id { get; set; }

        public Transacao()
        {
        }

        public Transacao(int id, byte idView, DateTime data, string descricao, decimal valor)
        {
            Id = id;
            IdView = idView;
            Data = data;
            Descricao = descricao;
            Valor = valor;
        }
    }
}
