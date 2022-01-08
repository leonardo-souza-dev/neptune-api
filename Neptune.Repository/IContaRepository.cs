using System.Collections.Generic;
using System.Threading.Tasks;
using Neptune.Domain;

namespace Neptune.Infra
{
    public interface IContaRepository
    {
        List<Conta> ObterTodas();
        Conta Obter(int id);
        Conta Criar(Conta conta);
        Conta Atualizar(Conta Conta);
    }
}
