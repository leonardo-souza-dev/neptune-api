using Neptune.Domain;
using System;
using System.Collections.Generic;

namespace Neptune.Application
{
    public interface IContaService
    {
        List<ContaDomain> ObterTodas();
        ContaDomain Obter(int id);
    }
}
