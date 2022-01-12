using Neptune.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Neptune.Application
{
    public interface IContaService
    {
        Task<List<Conta>> ObterTodas();
        Conta Obter(int id);
    }
}
