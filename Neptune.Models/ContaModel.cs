using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Neptune.Models
{
    public class ContaModel
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public decimal SaldoInicial { get; set; }

        public ContaModel(int id, string nome, decimal saldoInicial)
        {
            Id = id;
            Nome = nome;
            SaldoInicial = saldoInicial;
        }
    }
}
