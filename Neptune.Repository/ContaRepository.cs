using System;
using System.Linq;
using System.Collections.Generic;
using Neptune.Domain;
using System.Threading.Tasks;

namespace Neptune.Infra
{
    public class ContaRepository : IContaRepository
    {
        //TODO: trocar por base de dados real

        private readonly List<Conta> Contas = new()
        {
            new Conta(1, "NuConta", 1000M),
            new Conta(2, "NuPoup", 1000M)
        };

        public async Task<List<Conta>> ObterTodas()
        {
            return Contas;
        }

        public async Task<List<Conta>> Obter(int[] ids)
        {
            return Contas.Where(x => ids.Contains(x.Id)).ToList();
        }

        public Conta Obter(int id)
        {
            return Contas.FirstOrDefault(x => x.Id == id);
        }

        public Conta Criar(Conta conta)
        {
            var novaEntidade = new Conta(GetNextId(), conta.Nome, conta.SaldoInicial);

            Contas.Add(novaEntidade);

            return novaEntidade;
        }

        public Conta Atualizar(Conta conta)
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
