using FluentValidation;
using System.Text.RegularExpressions;

namespace ControleDeMedicamentos.Dominio.ModuloPaciente
{
    public class ValidadorPaciente : AbstractValidator<Paciente>
    {
        public ValidadorPaciente()
        {
            RuleFor(x => x.Nome)
                .Matches(new Regex(@"^[ a-zA-Z-à-ü]{3,60}$")).WithMessage("Nome informado é inválido.")
                .NotEmpty().WithMessage("Campo 'Nome' é obrigatório.");

            RuleFor(x => x.CartaoSUS)
                .Matches(new Regex(@"^[0-9]{15}$")).WithMessage("Cartão SUS informado é inválido.")
                .NotEmpty().WithMessage("Campo 'Cartão SUS' é obrigatório.");
        }
    }
}