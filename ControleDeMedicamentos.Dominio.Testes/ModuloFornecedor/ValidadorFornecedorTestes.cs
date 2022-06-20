using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleDeMedicamentos.Dominio.Testes.ModuloFornecedor
{
    [TestClass]
    public class ValidadorFornecedorTestes
    {
        private Fornecedor? fornecedor;
        private readonly ValidadorFornecedor validador;

        public ValidadorFornecedorTestes()
        {
            validador = new();
        }

        [TestMethod]
        public void Nome_Deve_Ser_Obrigatorio()
        {
            // arrange
            fornecedor = NovoFornecedor();
            fornecedor.Nome = null;

            // action
            TestValidationResult<Fornecedor> resultado = validador.TestValidate(fornecedor);

            // assert
            resultado.ShouldHaveValidationErrorFor(fornecedor => fornecedor.Nome);
        }

        [TestMethod]
        public void Nome_Deve_Ser_Valido()
        {
            // arrange
            fornecedor = NovoFornecedor();
            fornecedor.Nome = "Alan_Fernandes!?";

            // action
            TestValidationResult<Fornecedor> resultado = validador.TestValidate(fornecedor);

            // assert
            resultado.ShouldHaveValidationErrorFor(fornecedor => fornecedor.Nome);
        }

        [TestMethod]
        public void Telefone_Deve_Ser_Obrigatorio()
        {
            // arrange
            fornecedor = NovoFornecedor();
            fornecedor.Telefone = null;

            // action
            TestValidationResult<Fornecedor> resultado = validador.TestValidate(fornecedor);

            // assert
            resultado.ShouldHaveValidationErrorFor(fornecedor => fornecedor.Telefone);
        }

        [TestMethod]
        public void Telefone_Deve_Ser_Valido()
        {
            // arrange
            fornecedor = NovoFornecedor();
            fornecedor.Telefone = "qwertyuiopa";

            // action
            TestValidationResult<Fornecedor> resultado = validador.TestValidate(fornecedor);

            // assert
            resultado.ShouldHaveValidationErrorFor(fornecedor => fornecedor.Telefone);
        }

        [TestMethod]
        public void Email_Deve_Ser_Obrigatorio()
        {
            // arrange
            fornecedor = NovoFornecedor();
            fornecedor.Email = null;

            // action
            TestValidationResult<Fornecedor> resultado = validador.TestValidate(fornecedor);

            // assert
            resultado.ShouldHaveValidationErrorFor(fornecedor => fornecedor.Email);
        }

        [TestMethod]
        public void Email_Deve_Ser_Valido()
        {
            // arrange
            fornecedor = NovoFornecedor();
            fornecedor.Email = "alanemail.com";

            // action
            TestValidationResult<Fornecedor> resultado = validador.TestValidate(fornecedor);

            // assert
            resultado.ShouldHaveValidationErrorFor(fornecedor => fornecedor.Email);
        }

        [TestMethod]
        public void Cidade_Deve_Ser_Obrigatorio()
        {
            // arrange
            fornecedor = NovoFornecedor();
            fornecedor.Cidade = null;

            // action
            TestValidationResult<Fornecedor> resultado = validador.TestValidate(fornecedor);

            // assert
            resultado.ShouldHaveValidationErrorFor(fornecedor => fornecedor.Cidade);
        }

        [TestMethod]
        public void Cidade_Deve_Ser_Valida()
        {
            // arrange
            fornecedor = NovoFornecedor();
            fornecedor.Cidade = "_Lag?es";

            // action
            TestValidationResult<Fornecedor> resultado = validador.TestValidate(fornecedor);

            // assert
            resultado.ShouldHaveValidationErrorFor(fornecedor => fornecedor.Cidade);
        }

        [TestMethod]
        public void UF_Deve_Ser_Obrigatorio()
        {
            // arrange
            fornecedor = NovoFornecedor();
            fornecedor.UF = null;

            // action
            TestValidationResult<Fornecedor> resultado = validador.TestValidate(fornecedor);

            // assert
            resultado.ShouldHaveValidationErrorFor(fornecedor => fornecedor.UF);
        }

        [TestMethod]
        public void UF_Deve_Ser_Valida()
        {
            // arrange
            fornecedor = NovoFornecedor();
            fornecedor.UF = "XX";

            // action
            TestValidationResult<Fornecedor> resultado = validador.TestValidate(fornecedor);

            // assert
            resultado.ShouldHaveValidationErrorFor(fornecedor => fornecedor.UF);
        }

        private Fornecedor NovoFornecedor()
        {
            return new()
            {
                Nome = "Alan",
                Telefone = "49998165491",
                Email = "alan@email.com",
                Cidade = "Lages",
                UF = "SC"
            };
        }
    }
}