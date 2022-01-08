using System.Collections.Generic;
using System.Threading.Tasks;
using Neptune.Domain;

namespace Neptune.Infra
{
    public interface ITransacaoRepository
    {
        List<Transacao> ObterTodas();
        Transacao Obter(int id);
        Transacao Criar(Transacao transacao);
        Transacao Atualizar(Transacao transacao);
        List<Transacao> ObterPorContaEMes(int contaId, int mes, int ano);
    }
}
