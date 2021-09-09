using System;
using System.ComponentModel.DataAnnotations;

namespace Neptune.Models
{
    public class Transacao
    {
        public int Id { get; set; }

        [Required]
        public DateTime Data { get; set; } = DateTime.Now;

        [Required]
        public string Descricao { get; set; }

        [Required]
        public decimal Valor { get; set; }

        public byte IdView { get; set; }

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
