using FluentValidation;

namespace ControleDeMedicamentos.Dominio.ModuloRequisicao
{
    public class ValidadorRequisicao : AbstractValidator<Requisicao>
    {
        public ValidadorRequisicao()
        {
            RuleFor(x => x.Funcionario)
                .NotNull().WithMessage("Campo 'Funcionário' é obrigatório.");

            RuleFor(x => x.Paciente)
                .NotNull().WithMessage("Campo 'Paciente' é obrigatório.");

            RuleFor(x => x.Medicamento)
                .NotNull().WithMessage("Campo 'Medicamento' é obrigatório.");

            RuleFor(x => x.QuantidadeMedicamento)
                .NotEmpty().WithMessage("Campo 'Quantidade Medicamento' é obrigatório.")
                .GreaterThan(0).WithMessage("Quantidade Medicamento informada é inválida.")
                .LessThanOrEqualTo(x => x.Medicamento.QuantidadeDisponivel).WithMessage("Quantidade Medicamento não disponível.");

            RuleFor(x => x.DataRequisicao)
                .Equal(DateTime.Now.Date).WithMessage("Erro ao salvar a data da requisição.");
        }
    }
}