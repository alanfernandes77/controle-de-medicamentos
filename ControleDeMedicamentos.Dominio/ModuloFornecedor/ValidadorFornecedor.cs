using FluentValidation;
using System.Text.RegularExpressions;

namespace ControleDeMedicamentos.Dominio.ModuloFornecedor
{
    public class ValidadorFornecedor : AbstractValidator<Fornecedor>
    {
        public ValidadorFornecedor()
        {
            RuleFor(x => x.Nome)
                .Matches(new Regex(@"^[ a-zA-Z-à-ü]{3,60}$")).WithMessage("Nome informado é inválido.")
                .NotEmpty().WithMessage("Campo 'Nome' é obrigatório.");

            RuleFor(x => x.Telefone)
                .Matches(new Regex(@"^\d{2}\d{4,5}\d{4}$")).WithMessage("Telefone informado é inválido.")
                .NotEmpty().WithMessage("Campo 'Telefone' é obrigatório.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("E-mail informado é inválido.")
                .NotEmpty().WithMessage("Campo 'E-mail' é obrigatório.");

            RuleFor(x => x.Cidade)
                .Matches(new Regex(@"^[ a-zA-Z-à-ü]{3,60}$")).WithMessage("Cidade informada é inválida.")
                .NotEmpty().WithMessage("Campo 'Cidade' é obrigatório.");

            RuleFor(x => x.UF)
                .Matches(new Regex(@"^(AC|AL|AP|AM|BA|CE|DF|ES|GO|MA|MT|MS|MG|PA|PB|PR|PE|PI|RJ|RN|RS|RO|RR|SC|SP|SE|TO)$")).WithMessage("UF informada é inválida.")
                .NotEmpty().WithMessage("Campo 'UF' é obrigatório.");
        }
    }
}