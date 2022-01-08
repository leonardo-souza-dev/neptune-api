using System.Collections.Generic;
using System.Threading.Tasks;
using Neptune.Domain;

namespace Neptune.Infra
{
    public interface ITransacaoRepository
    {
        List<TransacaoDomain> ObterTodas();
        TransacaoDomain Obter(int id);
        TransacaoDomain Criar(TransacaoDomain transacao);
        TransacaoDomain Atualizar(TransacaoDomain transacao);
    }
}
