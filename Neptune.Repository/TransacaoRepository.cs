using System;
using System.Linq;
using System.Collections.Generic;
using Neptune.Domain;
using System.Threading.Tasks;

namespace Neptune.Infra
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly List<Transacao> _transacoes = new();

        public TransacaoRepository()
        {
            _transacoes.Add(new Transacao(1, DateTime.Now.AddMonths(-1), "Saladaaaaa", 1M, 1));
            _transacoes.Add(new Transacao(2, DateTime.Now.AddMonths(-1), "Rendimento", 10M, 2));

            // TEMP - janeiro
            // hoje
            _transacoes.Add(new Transacao(3, DateTime.Now, "Cafééééééé", 1M, 1));
            _transacoes.Add(new Transacao(5, DateTime.Now, "Paoooooooo", 1M, 1));
            
            //amanha
            _transacoes.Add(new Transacao(5, DateTime.Now.AddDays(1), "Outroo_jan", 1M, 1));


            _transacoes.Add(new Transacao(6, DateTime.Now.AddDays(1), "Rendimento", 2M, 2));
            _transacoes.Add(new Transacao(7, DateTime.Now.AddDays(2), "Rendimento", 20M, 2));

            _transacoes.Add(new Transacao(7, DateTime.Now.AddMonths(1), "Uber", 20M, 2));
        }

        public List<Transacao> ObterTodas()
        {
            return _transacoes;
        }

        public Transacao Obter(int id)
        {
            return _transacoes.FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<Transacao>> Obter(int ano, int mes)
        {
            return _transacoes.Where(x => x.Data.Month == mes && x.Data.Year == ano).ToList();
        }

        public Transacao Criar(Transacao transacao)
        {
            var novaEntidade = new Transacao(GetNextId(),
                                         transacao.Data,
                                         transacao.Descricao,
                                         transacao.Valor,
                                         transacao.ContaId);

            _transacoes.Add(novaEntidade);

            return novaEntidade;
        }

        public Transacao Atualizar(Transacao transacao)
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
