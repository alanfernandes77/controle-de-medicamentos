using FluentValidation;
using System.Text.RegularExpressions;

namespace ControleDeMedicamentos.Dominio.ModuloMedicamento
{
    public class ValidadorMedicamento : AbstractValidator<Medicamento>
    {
        public ValidadorMedicamento()
        {
            RuleFor(x => x.Nome)
                .Matches(new Regex(@"^[ a-zA-Z-à-ü]{3,60}$")).WithMessage("Nome informado é inválido.")
                .NotEmpty().WithMessage("Campo 'Nome' é obrigatório.");

            RuleFor(x => x.Descricao)
                .Matches(new Regex(@"^[ a-zA-Z-à-ü]{10,80}$")).WithMessage("Descrição informada é inválida.")
                .NotEmpty().WithMessage("Campo 'Descrição' é obrigatório.");

            RuleFor(x => x.Lote)
                .Matches(new Regex(@"^[-/a-zA-Z-0-9]{5}$")).WithMessage("Lote informado é inválido.")
                .NotEmpty().WithMessage("Campo 'Lote' é obrigatório.");

            RuleFor(x => x.Validade)
                .GreaterThan(DateTime.Now).WithMessage("Data de validade informada é inválida.");

            RuleFor(x => x.QuantidadeDisponivel)
                .GreaterThan(0).WithMessage("Quantidade disponível informada é inválida.");

            RuleFor(x => x.Fornecedor)
                .NotNull().WithMessage("Campo 'Fornecedor' é obrigatório.");
        }
    }
}