using Neptune.Domain;
using System;
using System.Collections.Generic;

namespace Neptune.Application
{
    public interface IContaService
    {
        List<Conta> ObterTodas();
        Conta Obter(int id);
    }
}
