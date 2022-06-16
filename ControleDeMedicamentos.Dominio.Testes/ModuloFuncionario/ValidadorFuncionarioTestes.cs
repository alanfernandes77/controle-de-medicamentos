using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleDeMedicamentos.Dominio.Testes.ModuloFuncionario
{
    [TestClass]
    public class ValidadorFuncionarioTestes
    {
        private readonly Funcionario funcionario;
        private readonly ValidadorFuncionario validador;

        public ValidadorFuncionarioTestes()
        {
            funcionario = new()
            {
                Nome = "Alan",
                Usuario = "username.954",
                Senha = "459@password!username"
            };

            validador = new();
        }

        [TestMethod]
        public void Nome_Deve_Ser_Obrigatorio()
        {
            // arrange
            funcionario.Nome = null;

            // action
            ValidationResult resultado = validador.Validate(funcionario);

            // assert
            Assert.AreEqual("Campo 'Nome' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Nome_Deve_Ser_Valido()
        {
            // arrange
            funcionario.Nome = "Afsdgll@#@GSDLgsfd";

            // action
            ValidationResult resultado = validador.Validate(funcionario);

            // assert
            Assert.AreEqual("Nome informado é inválido.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Usuario_Deve_Ser_Obrigatorio()
        {
            // arrange
            funcionario.Usuario = null;

            // action
            ValidationResult resultado = validador.Validate(funcionario);

            // assert
            Assert.AreEqual("Campo 'Usuário' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Usuario_Deve_Ser_Valido()
        {
            // arrange
            funcionario.Usuario = "username@678811667!3344a7d41x97d1saj4";

            // action
            ValidationResult resultado = validador.Validate(funcionario);

            // assert
            Assert.AreEqual("Usuário informado é inválido.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Senha_Deve_Ser_Obrigatorio()
        {
            // arrange
            funcionario.Senha = null;

            // action
            ValidationResult resultado = validador.Validate(funcionario);

            // assert
            Assert.AreEqual("Campo 'Senha' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Senha_Deve_Ser_Valida()
        {
            // arrange
            funcionario.Senha = "459@@#!  hfdjd  %#@!@#d!use";

            // action
            ValidationResult resultado = validador.Validate(funcionario);

            // assert
            Assert.AreEqual("Senha informada é inválida.", resultado.Errors[0].ErrorMessage);
        }
    }
}