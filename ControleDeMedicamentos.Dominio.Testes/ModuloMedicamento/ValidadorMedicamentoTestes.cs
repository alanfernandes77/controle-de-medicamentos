using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleDeMedicamentos.Dominio.Testes.ModuloMedicamento
{
    [TestClass]
    public class ValidadorMedicamentoTeste
    {
        [TestMethod]
        public void Nome_Deve_Ser_Obrigatorio()
        {
            // arrange
            Medicamento novoMedicamento = new()
            {
                Nome = null,
                Descricao = "Remédio para dor de cabeça",
                Lote = "P-001",
                Validade = new DateTime(2022, 8, 20),
                QuantidadeDisponivel = 50,
                Fornecedor = new()
                {
                    Nome = "Alan",
                    Telefone = "49998165491",
                    Email = "alan@email.com",
                    Cidade = "Lages",
                    UF = "SC"
                }
            };

            ValidadorMedicamento validador = new();

            // action
            ValidationResult resultado = validador.Validate(novoMedicamento);

            // assert
            Assert.AreEqual("Campo 'Nome' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Nome_Deve_Ser_Valido()
        {
            // arrange
            Medicamento novoMedicamento = new()
            {
                Nome = " @$#4x_",
                Descricao = "Remédio para dor de cabeça",
                Lote = "P-001",
                Validade = new DateTime(2022, 8, 20),
                QuantidadeDisponivel = 50,
                Fornecedor = new()
                {
                    Nome = "Alan",
                    Telefone = "49998165491",
                    Email = "alan@email.com",
                    Cidade = "Lages",
                    UF = "SC"
                }
            };

            ValidadorMedicamento validador = new();

            // action
            ValidationResult resultado = validador.Validate(novoMedicamento);

            // assert
            Assert.AreEqual("Nome informado é inválido.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Descricao_Deve_Ser_Obrigatorio()
        {
            // arrange
            Medicamento novoMedicamento = new()
            {
                Nome = "Paracetamol",
                Descricao = null,
                Lote = "P-001",
                Validade = new DateTime(2022, 8, 20),
                QuantidadeDisponivel = 50,
                Fornecedor = new()
                {
                    Nome = "Alan",
                    Telefone = "49998165491",
                    Email = "alan@email.com",
                    Cidade = "Lages",
                    UF = "SC"
                }
            };

            ValidadorMedicamento validador = new();

            // action
            ValidationResult resultado = validador.Validate(novoMedicamento);

            // assert
            Assert.AreEqual("Campo 'Descrição' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Descricao_Deve_Ser_Valido()
        {
            // arrange
            Medicamento novoMedicamento = new()
            {
                Nome = "Paracetamol",
                Descricao = "  2%@%_+ _ 2%@%!@ @__ @",
                Lote = "P-001",
                Validade = new DateTime(2022, 8, 20),
                QuantidadeDisponivel = 50,
                Fornecedor = new()
                {
                    Nome = "Alan",
                    Telefone = "49998165491",
                    Email = "alan@email.com",
                    Cidade = "Lages",
                    UF = "SC"
                }
            };

            ValidadorMedicamento validador = new();

            // action
            ValidationResult resultado = validador.Validate(novoMedicamento);

            // assert
            Assert.AreEqual("Descrição informada é inválida.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Lote_Deve_Ser_Obrigatorio()
        {
            // arrange
            Medicamento novoMedicamento = new()
            {
                Nome = "Paracetamol",
                Descricao = "Remédio para dor de cabeça",
                Lote = null,
                Validade = new DateTime(2022, 8, 20),
                QuantidadeDisponivel = 50,
                Fornecedor = new()
                {
                    Nome = "Alan",
                    Telefone = "49998165491",
                    Email = "alan@email.com",
                    Cidade = "Lages",
                    UF = "SC"
                }
            };

            ValidadorMedicamento validador = new();

            // action
            ValidationResult resultado = validador.Validate(novoMedicamento);

            // assert
            Assert.AreEqual("Campo 'Lote' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Lote_Deve_Ser_Valido()
        {
            // arrange
            Medicamento novoMedicamento = new()
            {
                Nome = "Paracetamol",
                Descricao = "Remédio para dor de cabeça",
                Lote = "  $@% _a47",
                Validade = new DateTime(2022, 8, 20),
                QuantidadeDisponivel = 50,
                Fornecedor = new()
                {
                    Nome = "Alan",
                    Telefone = "49998165491",
                    Email = "alan@email.com",
                    Cidade = "Lages",
                    UF = "SC"
                }
            };

            ValidadorMedicamento validador = new();

            // action
            ValidationResult resultado = validador.Validate(novoMedicamento);

            // assert
            Assert.AreEqual("Lote informado é inválido.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Validade_Deve_Ser_Valida()
        {
            // arrange
            Medicamento novoMedicamento = new()
            {
                Nome = "Paracetamol",
                Descricao = "Remédio para dor de cabeça",
                Lote = "P-001",
                Validade = new DateTime(2019, 12, 5),
                QuantidadeDisponivel = 50,
                Fornecedor = new()
                {
                    Nome = "Alan",
                    Telefone = "49998165491",
                    Email = "alan@email.com",
                    Cidade = "Lages",
                    UF = "SC"
                }
            };

            ValidadorMedicamento validador = new();

            // action
            ValidationResult resultado = validador.Validate(novoMedicamento);

            // assert
            Assert.AreEqual("Data de validade informada é inválida.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void QuantidadeDisponivel_Deve_Ser_Valida()
        {
            // arrange
            Medicamento novoMedicamento = new()
            {
                Nome = "Paracetamol",
                Descricao = "Remédio para dor de cabeça",
                Lote = "P-001",
                Validade = new DateTime(2022, 12, 5),
                QuantidadeDisponivel = -1,
                Fornecedor = new()
                {
                    Nome = "Alan",
                    Telefone = "49998165491",
                    Email = "alan@email.com",
                    Cidade = "Lages",
                    UF = "SC"
                }
            };

            ValidadorMedicamento validador = new();

            // action
            ValidationResult resultado = validador.Validate(novoMedicamento);

            // assert
            Assert.AreEqual("Quantidade disponível informada é inválida.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Fornecedor_Deve_Ser_Obrigatorio()
        {
            // arrange
            Medicamento novoMedicamento = new()
            {
                Nome = "Paracetamol",
                Descricao = "Remédio para dor de cabeça",
                Lote = "P-001",
                Validade = new DateTime(2022, 8, 20),
                QuantidadeDisponivel = 50,
                Fornecedor = null
            };

            ValidadorMedicamento validador = new();

            // action
            ValidationResult resultado = validador.Validate(novoMedicamento);

            // assert
            Assert.AreEqual("Campo 'Fornecedor' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }
    }
}