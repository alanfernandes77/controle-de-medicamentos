using ControleDeMedicamentos.Dominio.Compartilhado;

namespace ControleDeMedicamentos.Dominio.ModuloFornecedor
{
    public class Fornecedor : EntidadeBase<Fornecedor>
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Fornecedor fornecedor &&
                   Id == fornecedor.Id &&
                   Nome == fornecedor.Nome &&
                   Telefone == fornecedor.Telefone &&
                   Email == fornecedor.Email &&
                   Cidade == fornecedor.Cidade &&
                   UF == fornecedor.UF;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nome, Telefone, Email, Cidade, UF);
        }
    }
}