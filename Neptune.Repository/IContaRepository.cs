using System.Collections.Generic;
using System.Threading.Tasks;
using Neptune.Domain;

namespace Neptune.Infra
{
    public interface IContaRepository
    {
        Task<List<Conta>> ObterTodas();
        Task<List<Conta>> Obter(int[] ids);
        Conta Obter(int id);
        Conta Criar(Conta conta);
        Conta Atualizar(Conta Conta);
    }
}
