using ControleDeMedicamentos.Dominio.Compartilhado;
using ControleDeMedicamentos.Dominio.ModuloFornecedor;

namespace ControleDeMedicamentos.Dominio.ModuloMedicamento
{
    public class Medicamento : EntidadeBase<Medicamento>
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Lote { get; set; }
        public DateTime Validade { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public Fornecedor Fornecedor { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Medicamento medicamento &&
                   Id == medicamento.Id &&
                   Nome == medicamento.Nome &&
                   Descricao == medicamento.Descricao &&
                   Lote == medicamento.Lote &&
                   Validade == medicamento.Validade &&
                   QuantidadeDisponivel == medicamento.QuantidadeDisponivel &&
                   EqualityComparer<Fornecedor>.Default.Equals(Fornecedor, medicamento.Fornecedor);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nome, Descricao, Lote, Validade, QuantidadeDisponivel, Fornecedor);
        }
    }
}