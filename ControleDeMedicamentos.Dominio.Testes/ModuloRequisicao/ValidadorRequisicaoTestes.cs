using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using ControleDeMedicamentos.Dominio.ModuloPaciente;
using ControleDeMedicamentos.Dominio.ModuloRequisicao;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleDeMedicamentos.Dominio.Testes.ModuloRequisicao
{
    [TestClass]
    public class ValidadorRequisicaoTestes
    {
        private Requisicao? requisicao;

        private readonly ValidadorRequisicao validador;

        public ValidadorRequisicaoTestes()
        {
            validador = new();
        }

        [TestMethod]
        public void Funcionario_Deve_Ser_Obrigatorio()
        {
            // arrange
            requisicao = NovaRequisicao();
            requisicao.Funcionario = null;

            // action
            TestValidationResult<Requisicao> resultado = validador.TestValidate(requisicao);

            // assert
            resultado.ShouldHaveValidationErrorFor(requisicao => requisicao.Funcionario);
        }

        [TestMethod]
        public void Paciente_Deve_Ser_Obrigatorio()
        {
            // arrange
            requisicao = NovaRequisicao();
            requisicao.Paciente = null;

            // action
            TestValidationResult<Requisicao> resultado = validador.TestValidate(requisicao);

            // assert
            resultado.ShouldHaveValidationErrorFor(requisicao => requisicao.Paciente);
        }

        [TestMethod]
        public void Medicamento_Deve_Ser_Obrigatorio()
        {
            // arrange
            requisicao = NovaRequisicao();
            requisicao.Medicamento = null;

            // action
            TestValidationResult<Requisicao> resultado = validador.TestValidate(requisicao);

            // assert
            resultado.ShouldHaveValidationErrorFor(requisicao => requisicao.Medicamento);
        }

        [TestMethod]
        public void Quantidade_Medicamento_Deve_Ser_Valida()
        {
            // arrange 
            requisicao = NovaRequisicao();
            requisicao.QuantidadeMedicamento = 0;

            // action
            TestValidationResult<Requisicao> resultado = validador.TestValidate(requisicao);

            // assert
            resultado.ShouldHaveValidationErrorFor(requisicao => requisicao.QuantidadeMedicamento);
        }

        [TestMethod]
        public void Quantidade_Medicamento_Deve_Ser_Menor_Ou_Igual_Que_Quantidade_Disponivel()
        {
            // arrange 
            requisicao = NovaRequisicao();

            requisicao.Medicamento.QuantidadeDisponivel = 10;
            requisicao.QuantidadeMedicamento = 20;

            // action
            TestValidationResult<Requisicao> resultado = validador.TestValidate(requisicao);

            // assert
            resultado.ShouldHaveValidationErrorFor(requisicao => requisicao.QuantidadeMedicamento);
        }

        [TestMethod]
        public void Data_Requisicao_Deve_Ser_Valida()
        {
            // arrange 
            requisicao = NovaRequisicao();
            requisicao.DataRequisicao = new(2025, 10, 8);

            // action
            TestValidationResult<Requisicao> resultado = validador.TestValidate(requisicao);

            // assert
            resultado.ShouldHaveValidationErrorFor(requisicao => requisicao.DataRequisicao);
        }

        private Requisicao NovaRequisicao()
        {
            return new()
            {
                Funcionario = NovoFuncionario(),
                Paciente = NovoPaciente(),
                Medicamento = NovoMedicamento(),
                QuantidadeMedicamento = 10,
                DataRequisicao = DateTime.Now.Date
            };
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

        private Funcionario NovoFuncionario()
        {
            return new()
            {
                Nome = "Alan",
                Usuario = "username.954",
                Senha = "459@password!username"
            };
        }

        private Paciente NovoPaciente()
        {
            return new()
            {
                Nome = "Alan",
                CartaoSUS = "123456789123456"
            };
        }
    }
}