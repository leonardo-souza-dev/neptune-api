using System.Collections.Generic;
using System.Threading.Tasks;
using Neptune.Domain;

namespace Neptune.Infra
{
    public interface IContaRepository
    {
        List<ContaDomain> ObterTodas();
        ContaDomain Obter(int id);
        ContaDomain Criar(ContaDomain conta);
        ContaDomain Atualizar(ContaDomain Conta);
    }
}
