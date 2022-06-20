using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleDeMedicamentos.Dominio.Testes.ModuloMedicamento
{
    [TestClass]
    public class ValidadorMedicamentoTeste
    {
        private Medicamento? medicamento;
        private readonly ValidadorMedicamento validador;
        public ValidadorMedicamentoTeste()
        {
            validador = new();
        }

        [TestMethod]
        public void Nome_Deve_Ser_Obrigatorio()
        {
            // arrange
            medicamento = NovoMedicamento();
            medicamento.Nome = null;

            // action
            TestValidationResult<Medicamento> resultado = validador.TestValidate(medicamento);

            // assert
            resultado.ShouldHaveValidationErrorFor(medicamento => medicamento.Nome);
        }

        [TestMethod]
        public void Nome_Deve_Ser_Valido()
        {
            // arrange
            medicamento = NovoMedicamento();
            medicamento.Nome = " @$#4x_";

            // action
            TestValidationResult<Medicamento> resultado = validador.TestValidate(medicamento);

            // assert
            resultado.ShouldHaveValidationErrorFor(medicamento => medicamento.Nome);
        }

        [TestMethod]
        public void Descricao_Deve_Ser_Obrigatorio()
        {
            // arrange
            medicamento = NovoMedicamento();
            medicamento.Descricao = null;

            // action
            TestValidationResult<Medicamento> resultado = validador.TestValidate(medicamento);

            // assert
            resultado.ShouldHaveValidationErrorFor(medicamento => medicamento.Descricao);
        }

        [TestMethod]
        public void Descricao_Deve_Ser_Valido()
        {
            // arrange
            medicamento = NovoMedicamento();
            medicamento.Descricao = "  2%@%_+ _ 2%@%!@ @__ @";

            // action
            TestValidationResult<Medicamento> resultado = validador.TestValidate(medicamento);

            // assert
            resultado.ShouldHaveValidationErrorFor(medicamento => medicamento.Descricao);
        }

        [TestMethod]
        public void Lote_Deve_Ser_Obrigatorio()
        {
            // arrange
            medicamento = NovoMedicamento();
            medicamento.Lote = null;

            // action
            TestValidationResult<Medicamento> resultado = validador.TestValidate(medicamento);

            // assert
            resultado.ShouldHaveValidationErrorFor(medicamento => medicamento.Lote);
        }

        [TestMethod]
        public void Lote_Deve_Ser_Valido()
        {
            // arrange
            medicamento = NovoMedicamento();
            medicamento.Lote = "  $@% _a47";

            // action
            TestValidationResult<Medicamento> resultado = validador.TestValidate(medicamento);

            // assert
            resultado.ShouldHaveValidationErrorFor(medicamento => medicamento.Lote);
        }

        [TestMethod]
        public void Validade_Deve_Ser_Valida()
        {
            // arrange
            medicamento = NovoMedicamento();
            medicamento.Validade = new(2019, 12, 5);

            // action
            TestValidationResult<Medicamento> resultado = validador.TestValidate(medicamento);

            // assert
            resultado.ShouldHaveValidationErrorFor(medicamento => medicamento.Validade);
        }

        [TestMethod]
        public void QuantidadeDisponivel_Deve_Ser_Valida()
        {
            // arrange
            medicamento = NovoMedicamento();
            medicamento.QuantidadeDisponivel = -1;

            // action
            TestValidationResult<Medicamento> resultado = validador.TestValidate(medicamento);

            // assert
            resultado.ShouldHaveValidationErrorFor(medicamento => medicamento.QuantidadeDisponivel);
        }

        [TestMethod]
        public void Fornecedor_Deve_Ser_Obrigatorio()
        {
            // arrange
            medicamento = NovoMedicamento();
            medicamento.Fornecedor = null;

            // action
            TestValidationResult<Medicamento> resultado = validador.TestValidate(medicamento);

            // assert
            resultado.ShouldHaveValidationErrorFor(medicamento => medicamento.Fornecedor);
        }

        private Medicamento NovoMedicamento()
        {
            return new()
            {
                Nome = "Paracetamol",
                Descricao = "Analgésico",
                Lote = "P-001",
                Validade = new DateTime(2022, 8, 20),
                QuantidadeDisponivel = 50,
                Fornecedor = NovoFornecedor()
            };
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