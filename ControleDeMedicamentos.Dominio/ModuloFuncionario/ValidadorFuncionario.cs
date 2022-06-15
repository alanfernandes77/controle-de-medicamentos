using FluentValidation;
using System.Text.RegularExpressions;

namespace ControleDeMedicamentos.Dominio.ModuloFuncionario
{
    public class ValidadorFuncionario : AbstractValidator<Funcionario>
    {
        public ValidadorFuncionario()
        {
            RuleFor(x => x.Nome)
                .Matches(new Regex(@"^[ a-zA-Z-à-ü]{3,60}$")).WithMessage("Nome informado é inválido.")
                .NotEmpty().WithMessage("Campo 'Nome' é obrigatório.");

            RuleFor(x => x.Usuario)
                .Matches(new Regex(@"^[.@a-zA-Z-à-ü0-9]{3,60}$")).WithMessage("Usuário informado é inválido.")
                .NotEmpty().WithMessage("Campo 'Usuário' é obrigatório.");

            RuleFor(x => x.Senha)
                .Matches(new Regex(@"^[.!@#$%&*a-zA-Z-à-ü0-9]{10,25}$")).WithMessage("Senha informada é inválida.")
                .NotEmpty().WithMessage("Campo 'Senha' é obrigatório.");
        }
    }
}