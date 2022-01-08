using System.Collections.Generic;

namespace Neptune.Web.ViewModel
{
    public class Conta
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        private bool _ativo;
        public bool Ativo 
        { 
            get 
            {
                return _ativo;
            } 
            set
            {
                _ativo = value;
                NotificarInteressados();
            }
        }

        public void AdicionarInteressado(IInteressado interessado)
        {
            Interessados.Add(interessado);
        }

        public void NotificarInteressados()
        {
            foreach (var interessado in Interessados)
            {
                interessado.Atualizar();
            }
        }

        public List<IInteressado> Interessados { get; set; } = new ();

        public Conta(int id, string nome, bool ativo)
        {
            Id = id;
            Nome = nome;
            Ativo = ativo;
        }
    }
}
