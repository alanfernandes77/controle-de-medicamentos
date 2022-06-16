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

            When(r => r.Medicamento == null, () =>
            {
                RuleFor(x => x.Medicamento)
                    .NotNull().WithMessage("Campo 'Medicamento' é obrigatório.");

            }).Otherwise(() =>

            {
                RuleFor(x => x.QuantidadeMedicamento)
                    .GreaterThan(0).WithMessage("Quantidade Medicamento informada é inválida.")
                    .LessThan(x => x.Medicamento.QuantidadeDisponivel).WithMessage("Quantidade Medicamento não disponível.");
            });

            RuleFor(x => x.DataRequisicao)
                .Equal(DateTime.Now.Date).WithMessage("Erro ao salvar a data da requisição.");
        }
    }
}