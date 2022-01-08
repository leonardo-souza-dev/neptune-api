using Neptune.Domain;
using System;
using System.Collections.Generic;

namespace Neptune.Application
{
    public interface ITransacaoService
    {
        List<TransacaoDomain> ObterTodas();
        List<TransacaoDomain> ObterPorConta(int contaId);
    }
}
