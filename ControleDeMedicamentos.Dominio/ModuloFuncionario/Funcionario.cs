using ControleDeMedicamentos.Dominio.Compartilhado;

namespace ControleDeMedicamentos.Dominio.ModuloFuncionario
{
    public class Funcionario : EntidadeBase<Funcionario>
    {
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Funcionario funcionario &&
                   Id == funcionario.Id &&
                   Nome == funcionario.Nome &&
                   Usuario == funcionario.Usuario &&
                   Senha == funcionario.Senha;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nome, Usuario, Senha);
        }
    }
}