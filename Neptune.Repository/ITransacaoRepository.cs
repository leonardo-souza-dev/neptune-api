using System.Collections.Generic;
using System.Threading.Tasks;
using Neptune.Domain;

namespace Neptune.Infra
{
    public interface ITransacaoRepository
    {
        List<Transacao> ObterTodas();
        Transacao Obter(int id);
        Task<List<Transacao>> Obter(int ano, int mes);
        Transacao Criar(Transacao transacao);
        Transacao Atualizar(Transacao transacao);
    }
}
