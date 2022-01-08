using Neptune.Domain;
using System;
using System.Collections.Generic;

namespace Neptune.Application
{
    public interface ITransacaoService
    {
        List<Transacao> ObterTodas();
        List<Transacao> ObterPorContaEMes(int contaId, int mes, int ano);
        Transacao Criar(Transacao transacao);
        Transacao Atualizar(Transacao transacao);
    }
}
