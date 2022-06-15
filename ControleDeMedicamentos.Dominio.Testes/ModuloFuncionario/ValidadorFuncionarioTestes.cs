using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleDeMedicamentos.Dominio.Testes.ModuloFuncionario
{
    [TestClass]
    public class ValidadorFuncionarioTestes
    {
        [TestMethod]
        public void Nome_Deve_Ser_Obrigatorio()
        {
            // arrange
            Funcionario funcionario = new()
            {
                Nome = null,
                Usuario = "username.954",
                Senha = "459@password!username"
            };

            ValidadorFuncionario validador = new();

            // action
            ValidationResult resultado = validador.Validate(funcionario);

            // assert
            Assert.AreEqual("Campo 'Nome' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Nome_Deve_Ser_Valido()
        {
            // arrange
            Funcionario funcionario = new()
            {
                Nome = "Afsdgll@#@GSDLgsfd",
                Usuario = "username.954",
                Senha = "459@password!username"
            };

            ValidadorFuncionario validador = new();

            // action
            ValidationResult resultado = validador.Validate(funcionario);

            // assert
            Assert.AreEqual("Nome informado é inválido.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Usuario_Deve_Ser_Obrigatorio()
        {
            // arrange
            Funcionario funcionario = new()
            {
                Nome = "Alan",
                Usuario = null,
                Senha = "459@password!username"
            };

            ValidadorFuncionario validador = new();

            // action
            ValidationResult resultado = validador.Validate(funcionario);

            // assert
            Assert.AreEqual("Campo 'Usuário' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Usuario_Deve_Ser_Valido()
        {
            // arrange
            Funcionario funcionario = new()
            {
                Nome = "Alan",
                Usuario = "username@678811667!3344a7d41x97d1saj4",
                Senha = "459@password!username"
            };

            ValidadorFuncionario validador = new();

            // action
            ValidationResult resultado = validador.Validate(funcionario);

            // assert
            Assert.AreEqual("Usuário informado é inválido.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Senha_Deve_Ser_Obrigatorio()
        {
            // arrange
            Funcionario funcionario = new()
            {
                Nome = "Alan",
                Usuario = "username.954",
                Senha = null
            };

            ValidadorFuncionario validador = new();

            // action
            ValidationResult resultado = validador.Validate(funcionario);

            // assert
            Assert.AreEqual("Campo 'Senha' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Senha_Deve_Ser_Valida()
        {
            // arrange
            Funcionario funcionario = new()
            {
                Nome = "Alan",
                Usuario = "username.954",
                Senha = "459@@#!  hfdjd  %#@!@#d!use"
            };

            ValidadorFuncionario validador = new();

            // action
            ValidationResult resultado = validador.Validate(funcionario);

            // assert
            Assert.AreEqual("Senha informada é inválida.", resultado.Errors[0].ErrorMessage);
        }
    }
}