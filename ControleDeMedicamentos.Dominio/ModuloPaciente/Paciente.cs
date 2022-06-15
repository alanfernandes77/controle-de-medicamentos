using ControleDeMedicamentos.Dominio.Compartilhado;

namespace ControleDeMedicamentos.Dominio.ModuloPaciente
{
    public class Paciente : EntidadeBase<Paciente>
    {
        public string Nome { get; set; }
        public string CartaoSUS { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Paciente paciente &&
                   Id == paciente.Id &&
                   Nome == paciente.Nome &&
                   CartaoSUS == paciente.CartaoSUS;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nome, CartaoSUS);
        }
    }
}