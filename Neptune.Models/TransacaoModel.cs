using System;
using System.ComponentModel.DataAnnotations;

namespace Neptune.Domain
{
    public class TransacaoDomain
    {
        public int Id { get; set; }

        [Required]
        public DateTime Data { get; set; } = DateTime.Now;

        [Required]
        public string Descricao { get; set; }

        [Required]
        public decimal Valor { get; set; }

        public int ContaId { get; set; }

        public TransacaoDomain()
        {
        }

        public TransacaoDomain(int id, DateTime data, string descricao, decimal valor, int contaId)
        {
            Id = id;
            Data = data;
            Descricao = descricao;
            Valor = valor;
            ContaId = contaId;
        }

        public TransacaoDomain(DateTime data, string descricao, decimal valor, int contaId)
        {
            Data = data;
            Descricao = descricao;
            Valor = valor;
            ContaId = contaId;
        }
    }
}
