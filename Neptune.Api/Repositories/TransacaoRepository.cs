using System;
using System.Linq;
using System.Collections.Generic;
using Neptune.Models;
using System.Threading.Tasks;

namespace Neptune.Api.Services
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly List<Transacao> _transacoes = new();

        public TransacaoRepository()
        {
            _transacoes.Add(new Transacao(1, DateTime.Now.AddMonths(-1), "Pão", 1M, 1));
            _transacoes.Add(new Transacao(2, DateTime.Now.AddDays(-7), "Café", 4M, 1));
            _transacoes.Add(new Transacao(3, DateTime.Now, "Bolacha", 2M, 1));
            _transacoes.Add(new Transacao(4, DateTime.Now.AddDays(1), "Manteiga", 10M, 1));
        }

        public async Task<List<Transacao>> ObterTodas()
        {
            return _transacoes;
        }

        public async Task<Transacao> Obter(int id)
        {
            return _transacoes.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Transacao> Criar(Transacao transacao)
        {
            var novaEntidade = new Transacao(GetNextId(),
                                         transacao.Data,
                                         transacao.Descricao,
                                         transacao.Valor,
                                         transacao.ContaId);

            _transacoes.Add(novaEntidade);

            return novaEntidade;
        }

        public async Task<Transacao> Atualizar(Transacao transacao)
        {
            var transacaoEditada = _transacoes.FirstOrDefault(x => x.Id == transacao.Id);

            transacaoEditada.Data = transacao.Data;
            transacaoEditada.Descricao = transacao.Descricao;
            transacaoEditada.Valor = transacao.Valor;
            transacaoEditada.ContaId = transacao.ContaId;

            return transacaoEditada;
        }

        private int GetNextId()
        {
            return _transacoes.Max(x => x.Id) + 1;
        }
    }
}
