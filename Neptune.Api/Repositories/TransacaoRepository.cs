using System;
using System.Linq;
using System.Collections.Generic;
using Neptune.Models;
using System.Threading.Tasks;

namespace Neptune.Api.Services
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly List<TransacaoModel> _transacoes = new();

        public TransacaoRepository()
        {
            _transacoes.Add(new TransacaoModel(1, DateTime.Now.AddMonths(-1), "Pão", 1M, 1));
            _transacoes.Add(new TransacaoModel(1, DateTime.Now.AddMonths(-1), "Rendimento", 10M, 2));

            _transacoes.Add(new TransacaoModel(3, DateTime.Now, "Café", 2M, 1));
            _transacoes.Add(new TransacaoModel(3, DateTime.Now, "Rendimento", 2M, 2));
        }

        public async Task<List<TransacaoModel>> ObterTodas()
        {
            return _transacoes;
        }

        public async Task<TransacaoModel> Obter(int id)
        {
            return _transacoes.FirstOrDefault(x => x.Id == id);
        }

        public async Task<TransacaoModel> Criar(TransacaoModel transacao)
        {
            var novaEntidade = new TransacaoModel(GetNextId(),
                                         transacao.Data,
                                         transacao.Descricao,
                                         transacao.Valor,
                                         transacao.ContaId);

            _transacoes.Add(novaEntidade);

            return novaEntidade;
        }

        public async Task<TransacaoModel> Atualizar(TransacaoModel transacao)
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
