using System;
using System.Linq;
using System.Collections.Generic;
using Neptune.Models;
using System.Threading.Tasks;

namespace Neptune.Api.Services
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly List<Transacao> Transacoes = new();

        public TransacaoRepository()
        {
            Transacoes.Add(new Transacao(1, DateTime.Now, "Pão1", new Random().Next(1, 100), 1));
            Transacoes.Add(new Transacao(2, DateTime.Now, "Café2", new Random().Next(1, 100), 1));
            Transacoes.Add(new Transacao(3, DateTime.Now.AddDays(-1), "Manteiga3", new Random().Next(1, 100), 1));
        }

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
            var novaEntidade = new Transacao(GetNextId(),
                                         transacao.Data,
                                         transacao.Descricao,
                                         transacao.Valor,
                                         transacao.ContaId);

            Transacoes.Add(novaEntidade);

            return novaEntidade;
        }

        public async Task<Transacao> Atualizar(Transacao transacao)
        {
            var transacaoEditada = Transacoes.FirstOrDefault(x => x.Id == transacao.Id);

            transacaoEditada.Data = transacao.Data;
            transacaoEditada.Descricao = transacao.Descricao;
            transacaoEditada.Valor = transacao.Valor;
            transacaoEditada.ContaId = transacao.ContaId;

            return transacaoEditada;
        }

        private int GetNextId()
        {
            return Transacoes.Max(x => x.Id) + 1;
        }
    }
}
