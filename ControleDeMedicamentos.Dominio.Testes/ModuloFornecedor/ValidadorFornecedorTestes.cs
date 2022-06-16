using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleDeMedicamentos.Dominio.Testes.ModuloFornecedor
{
    [TestClass]
    public class ValidadorFornecedorTestes
    {
        private readonly Fornecedor fornecedor;
        private readonly ValidadorFornecedor validador;

        public ValidadorFornecedorTestes()
        {
            fornecedor = new()
            {
                Nome = "Alan",
                Telefone = "49998165491",
                Email = "alan@email.com",
                Cidade = "Lages",
                UF = "SC"
            };

            validador = new();
        }

        [TestMethod]
        public void Nome_Deve_Ser_Obrigatorio()
        {
            // arrange
            fornecedor.Nome = null;

            // action
            ValidationResult resultado = validador.Validate(fornecedor);

            // assert
            Assert.AreEqual("Campo 'Nome' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Nome_Deve_Ser_Valido()
        {
            // arrange
            fornecedor.Nome = "Alan_Fernandes!?";

            // action
            ValidationResult resultado = validador.Validate(fornecedor);

            // assert
            Assert.AreEqual("Nome informado é inválido.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Telefone_Deve_Ser_Obrigatorio()
        {
            // arrange
            fornecedor.Telefone = null;

            // action
            ValidationResult resultado = validador.Validate(fornecedor);

            // assert
            Assert.AreEqual("Campo 'Telefone' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Telefone_Deve_Ser_Valido()
        {
            // arrange
            fornecedor.Telefone = "qwertyuiopa";

            // action
            ValidationResult resultado = validador.Validate(fornecedor);

            // assert
            Assert.AreEqual("Telefone informado é inválido.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Email_Deve_Ser_Obrigatorio()
        {
            // arrange
            fornecedor.Email = null;

            // action
            ValidationResult resultado = validador.Validate(fornecedor);

            // assert
            Assert.AreEqual("Campo 'E-mail' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Email_Deve_Ser_Valido()
        {
            // arrange
            fornecedor.Email = "alanemail.com";

            // action
            ValidationResult resultado = validador.Validate(fornecedor);

            // assert
            Assert.AreEqual("E-mail informado é inválido.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Cidade_Deve_Ser_Obrigatorio()
        {
            // arrange
            fornecedor.Cidade = null;

            // action
            ValidationResult resultado = validador.Validate(fornecedor);

            // assert
            Assert.AreEqual("Campo 'Cidade' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Cidade_Deve_Ser_Valida()
        {
            // arrange
            fornecedor.Cidade = "_Lag?es";

            // action
            ValidationResult resultado = validador.Validate(fornecedor);

            // assert
            Assert.AreEqual("Cidade informada é inválida.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void UF_Deve_Ser_Obrigatorio()
        {
            // arrange
            fornecedor.UF = null;

            // action
            ValidationResult resultado = validador.Validate(fornecedor);

            // assert
            Assert.AreEqual("Campo 'UF' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void UF_Deve_Ser_Valida()
        {
            // arrange
            fornecedor.UF = "XX";

            // action
            ValidationResult resultado = validador.Validate(fornecedor);

            // assert
            Assert.AreEqual("UF informada é inválida.", resultado.Errors[0].ErrorMessage);
        }
    }
}