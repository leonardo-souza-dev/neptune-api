using System;
using System.Linq;
using System.Collections.Generic;
using Neptune.Models;
using System.Threading.Tasks;

namespace Neptune.Api.Services
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly List<Transacao> Transacoes = new()
        {
            new Transacao(1, 1, DateTime.Now, "Pão1", new Random().Next(1, 100)),
            new Transacao(2, 2, DateTime.Now, "Café2", new Random().Next(1, 100)),
            new Transacao(3, 3, DateTime.Now, "Manteiga3", new Random().Next(1, 100))
        };

        public async Task<List<Transacao>> ObterTodas()
        {
            return Transacoes;
        }

        public async Task<Transacao> Obter(int id)
        {
            return Transacoes.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Transacao> Criar(Transacao transacao)
        {
            var novaTransacao = new Transacao(GetNextId(),
                                         transacao.IdView,
                                         transacao.Data,
                                         transacao.Descricao,
                                         transacao.Valor);

            Transacoes.Add(novaTransacao);

            return novaTransacao;
        }

        public async Task<Transacao> Atualizar(Transacao transacao)
        {
            var transacaoEditada = Transacoes.FirstOrDefault(x => x.Id == transacao.Id);

            transacaoEditada.IdView = transacao.IdView;
            transacaoEditada.Data = transacao.Data;
            transacaoEditada.Descricao = transacao.Descricao;
            transacaoEditada.Valor = transacao.Valor;

            return transacaoEditada;
        }

        private int GetNextId()
        {
            return Transacoes.Max(x => x.Id) + 1;
        }
    }
}
