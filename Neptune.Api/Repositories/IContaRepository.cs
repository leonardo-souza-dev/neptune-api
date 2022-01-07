using System.Collections.Generic;
using System.Threading.Tasks;
using Neptune.Models;

namespace Neptune.Api.Services
{
    public interface IContaRepository
    {
        Task<List<ContaModel>> ObterTodas();
        Task<ContaModel> Obter(int id);
        Task<ContaModel> Criar(ContaModel conta);
        Task<ContaModel> Atualizar(ContaModel Conta);
    }
}
