using System;
using System.Linq;
using System.Collections.Generic;
using Neptune.Models;
using System.Threading.Tasks;

namespace Neptune.Api.Services
{
    public class ContaRepository : IContaRepository
    {
        private readonly List<Conta> Contas = new()
        {
            new Conta(1, "NuConta", 100M),
            new Conta(2, "NuPoup", 10M)
        };

        public async Task<List<Conta>> ObterTodas()
        {
            return Contas;
        }

        public async Task<Conta> Obter(int id)
        {
            return Contas.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Conta> Criar(Conta conta)
        {
            var novaEntidade = new Conta(GetNextId(),
                                         conta.Nome,
                                         conta.SaldoInicial);

            Contas.Add(novaEntidade);

            return novaEntidade;
        }

        public async Task<Conta> Atualizar(Conta conta)
        {
            var contaAtualizar = Contas.FirstOrDefault(x => x.Id == conta.Id);

            contaAtualizar.Nome = conta.Nome;
            contaAtualizar.SaldoInicial = conta.SaldoInicial;
            
            return contaAtualizar;
        }

        private int GetNextId()
        {
            return Contas.Max(x => x.Id) + 1;
        }
    }
}
