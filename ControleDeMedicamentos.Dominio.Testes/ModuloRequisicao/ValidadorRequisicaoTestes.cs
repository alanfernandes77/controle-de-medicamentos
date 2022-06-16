using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using ControleDeMedicamentos.Dominio.ModuloPaciente;
using ControleDeMedicamentos.Dominio.ModuloRequisicao;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleDeMedicamentos.Dominio.Testes.ModuloRequisicao
{
    [TestClass]
    public class ValidadorRequisicaoTestes
    {
        private readonly Funcionario funcionario;
        private readonly Paciente paciente;
        private readonly Fornecedor fornecedor;
        private readonly Medicamento medicamento;
        private readonly Requisicao requisicao;

        private readonly ValidadorRequisicao validador;

        public ValidadorRequisicaoTestes()
        {
            funcionario = new()
            {
                Nome = "Alan",
                Usuario = "username.954",
                Senha = "459@password!username"
            };

            paciente = new()
            {
                Nome = "Alan",
                CartaoSUS = "123456789123456"
            };

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

            requisicao = new()
            {
                Funcionario = funcionario,
                Paciente = paciente,
                Medicamento = medicamento,
                QuantidadeMedicamento = 10,
                DataRequisicao = DateTime.Now.Date
            };

            validador = new();
        }

        [TestMethod]
        public void Funcionario_Deve_Ser_Obrigatorio()
        {
            // arrange
            requisicao.Funcionario = null;

            // action
            ValidationResult resultado = validador.Validate(requisicao);

            // assert
            Assert.AreEqual("Campo 'Funcionário' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Paciente_Deve_Ser_Obrigatorio()
        {
            // arrange
            requisicao.Paciente = null;

            // action
            ValidationResult resultado = validador.Validate(requisicao);

            // assert
            Assert.AreEqual("Campo 'Paciente' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Medicamento_Deve_Ser_Obrigatorio()
        {
            // arrange
            requisicao.Medicamento = null;
            
            // action
            ValidationResult resultado = validador.Validate(requisicao);

            // assert
            Assert.AreEqual("Campo 'Medicamento' é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Quantidade_Medicamento_Deve_Ser_Valida()
        {
            // arrange 
            requisicao.QuantidadeMedicamento = 0;

            // action
            ValidationResult resultado = validador.Validate(requisicao);

            // assert
            Assert.AreEqual("Quantidade Medicamento informada é inválida.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Quantidade_Medicamento_Deve_Ser_Menor_Ou_Igual_Que_Quantidade_Disponivel()
        {
            // arrange 
            medicamento.QuantidadeDisponivel = 10;
            requisicao.QuantidadeMedicamento = 20;

            // action
            ValidationResult resultado = validador.Validate(requisicao);

            // assert
            Assert.AreEqual("Quantidade Medicamento não disponível.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Data_Requisicao_Deve_Ser_Valida()
        {
            // arrange 
            requisicao.DataRequisicao = new(2022, 6, 17);

            // action
            ValidationResult resultado = validador.Validate(requisicao);

            // assert
            Assert.AreEqual("Erro ao salvar a data da requisição.", resultado.Errors[0].ErrorMessage);
        }
    }
}