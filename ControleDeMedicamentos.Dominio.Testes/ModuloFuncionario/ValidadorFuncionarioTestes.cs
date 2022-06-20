using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleDeMedicamentos.Dominio.Testes.ModuloFuncionario
{
    [TestClass]
    public class ValidadorFuncionarioTestes
    {
        private Funcionario? funcionario;
        private readonly ValidadorFuncionario validador;

        public ValidadorFuncionarioTestes()
        {
            validador = new();
        }

        [TestMethod]
        public void Nome_Deve_Ser_Obrigatorio()
        {
            // arrange
            funcionario = NovoFuncionario();
            funcionario.Nome = null;

            // action
            TestValidationResult<Funcionario> resultado = validador.TestValidate(funcionario);

            // assert
            resultado.ShouldHaveValidationErrorFor(funcionario => funcionario.Nome);
        }

        [TestMethod]
        public void Nome_Deve_Ser_Valido()
        {
            // arrange
            funcionario = NovoFuncionario();
            funcionario.Nome = "Afsdgll@#@GSDLgsfd";

            // action
            TestValidationResult<Funcionario> resultado = validador.TestValidate(funcionario);

            // assert
            resultado.ShouldHaveValidationErrorFor(funcionario => funcionario.Nome);
        }

        [TestMethod]
        public void Usuario_Deve_Ser_Obrigatorio()
        {
            // arrange
            funcionario = NovoFuncionario();
            funcionario.Usuario = null;

            // action
            TestValidationResult<Funcionario> resultado = validador.TestValidate(funcionario);

            // assert
            resultado.ShouldHaveValidationErrorFor(funcionario => funcionario.Usuario);
        }

        [TestMethod]
        public void Usuario_Deve_Ser_Valido()
        {
            // arrange
            funcionario = NovoFuncionario();
            funcionario.Usuario = "username@678811667!3344a7d41x97d1saj4";

            // action
            TestValidationResult<Funcionario> resultado = validador.TestValidate(funcionario);

            // assert
            resultado.ShouldHaveValidationErrorFor(funcionario => funcionario.Usuario);
        }

        [TestMethod]
        public void Senha_Deve_Ser_Obrigatorio()
        {
            // arrange
            funcionario = NovoFuncionario();
            funcionario.Senha = null;

            // action
            TestValidationResult<Funcionario> resultado = validador.TestValidate(funcionario);

            // assert
            resultado.ShouldHaveValidationErrorFor(funcionario => funcionario.Senha);
        }

        [TestMethod]
        public void Senha_Deve_Ser_Valida()
        {
            // arrange
            funcionario = NovoFuncionario();
            funcionario.Senha = "459@@#!  hfdjd  %#@!@#d!use";

            // action
            TestValidationResult<Funcionario> resultado = validador.TestValidate(funcionario);

            // assert
            resultado.ShouldHaveValidationErrorFor(funcionario => funcionario.Senha);
        }

        private Funcionario NovoFuncionario()
        {
            return new()
            {
                Nome = "Alan",
                Usuario = "username.954",
                Senha = "459@password!username"
            };
        }

    }
}