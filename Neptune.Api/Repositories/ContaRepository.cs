using System;
using System.Linq;
using System.Collections.Generic;
using Neptune.Models;
using System.Threading.Tasks;

namespace Neptune.Api.Services
{
    public class ContaRepository : IContaRepository
    {
        private readonly List<ContaModel> Contas = new()
        {
            new ContaModel(1, "NuConta", 1000M),
            new ContaModel(2, "NuPoup", 1000M)
        };

        public async Task<List<ContaModel>> ObterTodas()
        {
            return Contas;
        }

        public async Task<ContaModel> Obter(int id)
        {
            return Contas.FirstOrDefault(x => x.Id == id);
        }

        public async Task<ContaModel> Criar(ContaModel conta)
        {
            var novaEntidade = new ContaModel(GetNextId(),
                                         conta.Nome,
                                         conta.SaldoInicial);

            Contas.Add(novaEntidade);

            return novaEntidade;
        }

        public async Task<ContaModel> Atualizar(ContaModel conta)
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
