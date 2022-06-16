using ControleDeMedicamentos.Dominio.Compartilhado;
using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using ControleDeMedicamentos.Dominio.ModuloPaciente;

namespace ControleDeMedicamentos.Dominio.ModuloRequisicao
{
    public class Requisicao : EntidadeBase<Requisicao>
    {
        public Funcionario Funcionario { get; set; }
        public Paciente Paciente { get; set; }
        public Medicamento Medicamento { get; set; }
        public int QuantidadeMedicamento { get; set; }
        public DateTime DataRequisicao { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Requisicao requisicao &&
                   Id == requisicao.Id &&
                   EqualityComparer<Funcionario>.Default.Equals(Funcionario, requisicao.Funcionario) &&
                   EqualityComparer<Paciente>.Default.Equals(Paciente, requisicao.Paciente) &&
                   EqualityComparer<Medicamento>.Default.Equals(Medicamento, requisicao.Medicamento) &&
                   QuantidadeMedicamento == requisicao.QuantidadeMedicamento &&
                   DataRequisicao == requisicao.DataRequisicao;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Funcionario, Paciente, Medicamento, QuantidadeMedicamento, DataRequisicao);
        }
    }
}