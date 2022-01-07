using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Neptune.Models;

namespace Neptune.Web.ViewModel
{
    public class ContasViewModel
    {
        public List<ContaViewModel> Contas { get; } = new List<ContaViewModel>();

        public ContasViewModel(List<Conta> contas)
        {
            contas.ForEach(x => Contas.Add(new ContaViewModel(x.Id, x.Nome)));
        }

    }

    public record ContaViewModel(int Id, string Nome);
}
