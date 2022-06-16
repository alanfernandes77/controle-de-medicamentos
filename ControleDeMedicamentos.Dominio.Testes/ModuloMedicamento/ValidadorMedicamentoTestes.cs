using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleDeMedicamentos.Dominio.Testes.ModuloMedicamento
{
    [TestClass]
    public class ValidadorMedicamentoTeste
    {
        private readonly Fornecedor fornecedor;
        private readonly Medicamento medicamento;
        private readonly ValidadorMedicamento validador;
        public ValidadorMedicamentoTeste()
        {
            fornecedor = new()
            {
                Nome = "Alan",
                Telefone = "49998165491",
                Email = "alan@email.com",
                Cidade = "Lages",
                UF = "SC"
            };

            medicamento = new()
            {
                Nome = "Paracetamol",
                Descricao = "Analgésico",
                Lote = "P-001",
                Validade = new DateTime(2022, 8, 20),
                QuantidadeDisponivel = 50,
                Fornecedor = fornecedor
            };

            validador = new();
        }

        [TestMethod]
        public void Nome_Deve_Ser_Obrigatorio()
        {
            // arrange
            medicamento.Nome = null;

            // action
            ValidationResult resultado = validador.Validate(medicamento);

            // assert
            Assert.AreEqual("Campo 'Nome' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Nome_Deve_Ser_Valido()
        {
            // arrange
            medicamento.Nome = " @$#4x_";

            // action
            ValidationResult resultado = validador.Validate(medicamento);

            // assert
            Assert.AreEqual("Nome informado é inválido.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Descricao_Deve_Ser_Obrigatorio()
        {
            // arrange
            medicamento.Descricao = null; 

            // action
            ValidationResult resultado = validador.Validate(medicamento);

            // assert
            Assert.AreEqual("Campo 'Descrição' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Descricao_Deve_Ser_Valido()
        {
            // arrange
            medicamento.Descricao = "  2%@%_+ _ 2%@%!@ @__ @";

            // action
            ValidationResult resultado = validador.Validate(medicamento);

            // assert
            Assert.AreEqual("Descrição informada é inválida.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Lote_Deve_Ser_Obrigatorio()
        {
            // arrange
            medicamento.Lote = null;

            // action
            ValidationResult resultado = validador.Validate(medicamento);

            // assert
            Assert.AreEqual("Campo 'Lote' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Lote_Deve_Ser_Valido()
        {
            // arrange
            medicamento.Lote = "  $@% _a47";

            // action
            ValidationResult resultado = validador.Validate(medicamento);

            // assert
            Assert.AreEqual("Lote informado é inválido.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Validade_Deve_Ser_Valida()
        {
            // arrange
            medicamento.Validade = new(2019, 12, 5);

            // action
            ValidationResult resultado = validador.Validate(medicamento);

            // assert
            Assert.AreEqual("Data de validade informada é inválida.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void QuantidadeDisponivel_Deve_Ser_Valida()
        {
            // arrange
            medicamento.QuantidadeDisponivel = -1;

            // action
            ValidationResult resultado = validador.Validate(medicamento);

            // assert
            Assert.AreEqual("Quantidade disponível informada é inválida.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Fornecedor_Deve_Ser_Obrigatorio()
        {
            // arrange
            medicamento.Fornecedor = null;

            // action
            ValidationResult resultado = validador.Validate(medicamento);

            // assert
            Assert.AreEqual("Campo 'Fornecedor' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }
    }
}