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

        private readonly List<ContaDomain> Contas = new()
        {
            new ContaDomain(1, "NuConta", 1000M),
            new ContaDomain(2, "NuPoup", 1000M)
        };

        public List<ContaDomain> ObterTodas()
        {
            return Contas;
        }

        public ContaDomain Obter(int id)
        {
            
            return Contas.FirstOrDefault(x => x.Id == id);
        }

        public ContaDomain Criar(ContaDomain conta)
        {
            var novaEntidade = new ContaDomain(GetNextId(), conta.Nome, conta.SaldoInicial);

            Contas.Add(novaEntidade);

            return novaEntidade;
        }

        public ContaDomain Atualizar(ContaDomain conta)
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
