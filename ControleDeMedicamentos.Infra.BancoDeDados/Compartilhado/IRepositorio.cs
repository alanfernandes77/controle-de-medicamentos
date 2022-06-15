using ControleDeMedicamentos.Dominio.Compartilhado;
using FluentValidation;
using FluentValidation.Results;

namespace ControleDeMedicamentos.Infra.BancoDeDados.Compartilhado
{
    public interface IRepositorio<T> where T : EntidadeBase<T>
    {
        ValidationResult Inserir(T t);
        ValidationResult Editar(T t);
        ValidationResult Excluir(T t);
        List<T> SelecionarTodos();
        T SelecionarPorId(int id);
        AbstractValidator<T> ObterValidador();
    }
}