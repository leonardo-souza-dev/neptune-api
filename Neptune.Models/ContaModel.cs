﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Neptune.Domain
{
    public class ContaDomain
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public decimal SaldoInicial { get; set; }

        public ContaDomain(int id, string nome, decimal saldoInicial)
        {
            Id = id;
            Nome = nome;
            SaldoInicial = saldoInicial;
        }
    }
}
