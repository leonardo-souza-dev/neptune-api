using System.Collections.Generic;
using System.Threading.Tasks;
using Neptune.Models;

namespace Neptune.Api.Services
{
    public interface IContaRepository
    {
        Task<List<Conta>> ObterTodas();
        Task<Conta> Obter(int id);
        Task<Conta> Criar(Conta conta);
        Task<Conta> Atualizar(Conta Conta);
    }
}
