using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleDeMedicamentos.Dominio.Testes.ModuloFornecedor
{
    [TestClass]
    public class ValidadorFornecedorTestes
    {
        [TestMethod]
        public void Nome_Deve_Ser_Obrigatorio()
        {
            // arrange
            Fornecedor fornecedor = new()
            {
                Nome = null,
                Telefone = "49998165491",
                Email = "alan@email.com",
                Cidade = "Lages",
                UF = "SC"
            };

            ValidadorFornecedor validador = new();

            // action
            ValidationResult resultado = validador.Validate(fornecedor);

            // assert
            Assert.AreEqual("Campo 'Nome' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Nome_Deve_Ser_Valido()
        {
            // arrange
            Fornecedor fornecedor = new()
            {
                Nome = "Alan_Fernandes!?",
                Telefone = "49998165491",
                Email = "alan@email.com",
                Cidade = "Lages",
                UF = "SC"
            };

            ValidadorFornecedor validador = new();

            // action
            ValidationResult resultado = validador.Validate(fornecedor);

            // assert
            Assert.AreEqual("Nome informado é inválido.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Telefone_Deve_Ser_Obrigatorio()
        {
            // arrange
            Fornecedor fornecedor = new()
            {
                Nome = "Alan",
                Telefone = null,
                Email = "alan@email.com",
                Cidade = "Lages",
                UF = "SC"
            };

            ValidadorFornecedor validador = new();

            // action
            ValidationResult resultado = validador.Validate(fornecedor);

            // assert
            Assert.AreEqual("Campo 'Telefone' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Telefone_Deve_Ser_Valido()
        {
            // arrange
            Fornecedor fornecedor = new()
            {
                Nome = "Alan",
                Telefone = "qwertyuiopa",
                Email = "alan@email.com",
                Cidade = "Lages",
                UF = "SC"
            };

            ValidadorFornecedor validador = new();

            // action
            ValidationResult resultado = validador.Validate(fornecedor);

            // assert
            Assert.AreEqual("Telefone informado é inválido.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Email_Deve_Ser_Obrigatorio()
        {
            // arrange
            Fornecedor fornecedor = new()
            {
                Nome = "Alan",
                Telefone = "49998165491",
                Email = null,
                Cidade = "Lages",
                UF = "SC"
            };

            ValidadorFornecedor validador = new();

            // action
            ValidationResult resultado = validador.Validate(fornecedor);

            // assert
            Assert.AreEqual("Campo 'E-mail' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Email_Deve_Ser_Valido()
        {
            // arrange
            Fornecedor fornecedor = new()
            {
                Nome = "Alan",
                Telefone = "49998165491",
                Email = "alanemail.com",
                Cidade = "Lages",
                UF = "SC"
            };

            ValidadorFornecedor validador = new();

            // action
            ValidationResult resultado = validador.Validate(fornecedor);

            // assert
            Assert.AreEqual("E-mail informado é inválido.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Cidade_Deve_Ser_Obrigatorio()
        {
            // arrange
            Fornecedor fornecedor = new()
            {
                Nome = "Alan",
                Telefone = "49998165491",
                Email = "alan@email.com",
                Cidade = null,
                UF = "SC"
            };

            ValidadorFornecedor validador = new();

            // action
            ValidationResult resultado = validador.Validate(fornecedor);

            // assert
            Assert.AreEqual("Campo 'Cidade' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Cidade_Deve_Ser_Valida()
        {
            // arrange
            Fornecedor fornecedor = new()
            {
                Nome = "Alan",
                Telefone = "49998165491",
                Email = "alan@email.com",
                Cidade = "_Lag?es",
                UF = "SC"
            };

            ValidadorFornecedor validador = new();

            // action
            ValidationResult resultado = validador.Validate(fornecedor);

            // assert
            Assert.AreEqual("Cidade informada é inválida.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void UF_Deve_Ser_Obrigatorio()
        {
            // arrange
            Fornecedor fornecedor = new()
            {
                Nome = "Alan",
                Telefone = "49998165491",
                Email = "alan@email.com",
                Cidade = "Lages",
                UF = null
            };

            ValidadorFornecedor validador = new();

            // action
            ValidationResult resultado = validador.Validate(fornecedor);

            // assert
            Assert.AreEqual("Campo 'UF' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void UF_Deve_Ser_Valida()
        {
            // arrange
            Fornecedor fornecedor = new()
            {
                Nome = "Alan",
                Telefone = "49998165491",
                Email = "alan@email.com",
                Cidade = "Lages",
                UF = "XX"
            };

            ValidadorFornecedor validador = new();

            // action
            ValidationResult resultado = validador.Validate(fornecedor);

            // assert
            Assert.AreEqual("UF informada é inválida.", resultado.Errors[0].ErrorMessage);
        }
    }
}