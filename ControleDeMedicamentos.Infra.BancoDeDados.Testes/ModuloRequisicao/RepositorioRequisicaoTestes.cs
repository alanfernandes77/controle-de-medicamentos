using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using ControleDeMedicamentos.Dominio.ModuloPaciente;
using ControleDeMedicamentos.Dominio.ModuloRequisicao;
using ControleDeMedicamentos.Infra.BancoDeDados.Compartilhado;
using ControleDeMedicamentos.Infra.BancoDeDados.ModuloFornecedor;
using ControleDeMedicamentos.Infra.BancoDeDados.ModuloFuncionario;
using ControleDeMedicamentos.Infra.BancoDeDados.ModuloMedicamento;
using ControleDeMedicamentos.Infra.BancoDeDados.ModuloPaciente;
using ControleDeMedicamentos.Infra.BancoDeDados.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace ControleDeMedicamentos.Infra.BancoDeDados.Testes.ModuloRequisicao
{
    [TestClass]
    public class RepositorioRequisicaoTestes : ConexaoSql
    {
        private readonly Funcionario funcionario;
        private readonly Paciente paciente;
        private readonly Fornecedor fornecedor;
        private readonly Medicamento medicamento;
        private readonly Requisicao requisicao;

        private readonly RepositorioFuncionario repositorioFuncionario;
        private readonly RepositorioPaciente repositorioPaciente;
        private readonly RepositorioFornecedor repositorioFornecedor;
        private readonly RepositorioMedicamento repositorioMedicamento;
        private readonly RepositorioRequisicao repositorioRequisicao;

        public RepositorioRequisicaoTestes()
        {
            using (Conexao = new(StringConexao))
            {
                string query =
                    @"DELETE FROM TBRequisicao;
                    DBCC CHECKIDENT (TBRequisicao, RESEED, 0)

                    DELETE FROM TBMedicamento;
                    DBCC CHECKIDENT (TBMedicamento, RESEED, 0)

                    DELETE FROM TBFornecedor;
                    DBCC CHECKIDENT (TBFornecedor, RESEED, 0)

                    DELETE FROM TBFuncionario;
                    DBCC CHECKIDENT (TBFuncionario, RESEED, 0)

                    DELETE FROM TBPaciente;
                    DBCC CHECKIDENT (TBPaciente, RESEED, 0)";

                SqlCommand comando = new(query, Conexao);

                Conexao.Open();

                comando.ExecuteNonQuery();
            }

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

            repositorioFuncionario = new();
            repositorioPaciente = new();
            repositorioFornecedor = new();
            repositorioMedicamento = new();
            repositorioRequisicao = new();
        }

        [TestMethod]
        public void Deve_Inserir_Requisicao()
        {
            // arrange
            repositorioFuncionario.Inserir(funcionario);
            repositorioPaciente.Inserir(paciente);
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);

            // action
            repositorioRequisicao.Inserir(requisicao);

            // assert
            Requisicao requisicaoEncontrada = repositorioRequisicao.SelecionarPorId(requisicao.Id);
            
            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(requisicao, requisicaoEncontrada);
        }

        [TestMethod]
        public void Deve_Editar_Requisicao()
        {
            // arrange
            repositorioFuncionario.Inserir(funcionario);
            repositorioPaciente.Inserir(paciente);
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);
            repositorioRequisicao.Inserir(requisicao);

            Funcionario funcionario2 = new()
            {
                Nome = "James",
                Usuario = "username.678",
                Senha = "678@password!username"
            };

            Paciente paciente2 = new()
            {
                Nome = "James",
                CartaoSUS = "456789123456789"
            };

            Fornecedor fornecedor2 = new()
            {
                Nome = "James",
                Telefone = "49998468794",
                Email = "james@email.com",
                Cidade = "Guarulhos",
                UF = "SP"
            };

            Medicamento medicamento2 = new()
            {
                Nome = "Nimesulida",
                Descricao = "Anti-Inflamatório",
                Lote = "N-001",
                Validade = new DateTime(2025, 10, 5),
                QuantidadeDisponivel = 30,
                Fornecedor = fornecedor2
            };

            repositorioFuncionario.Inserir(funcionario2);
            repositorioPaciente.Inserir(paciente2);
            repositorioFornecedor.Inserir(fornecedor2);
            repositorioMedicamento.Inserir(medicamento2);

            Requisicao requisicaoAtualizada = repositorioRequisicao.SelecionarPorId(requisicao.Id);

            requisicaoAtualizada.Funcionario = funcionario2;
            requisicaoAtualizada.Paciente = paciente2;
            requisicaoAtualizada.Medicamento = medicamento2;
            requisicaoAtualizada.QuantidadeMedicamento = 10;

            // action
            repositorioRequisicao.Editar(requisicaoAtualizada);

            // assert
            Requisicao requisicaoEncontrada = repositorioRequisicao.SelecionarPorId(requisicao.Id);

            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(requisicaoAtualizada, requisicaoEncontrada);
        }

        [TestMethod]
        public void Deve_Excluir_Requisicao()
        {
            // arrange
            repositorioFuncionario.Inserir(funcionario);
            repositorioPaciente.Inserir(paciente);
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);
            repositorioRequisicao.Inserir(requisicao);

            // action
            repositorioRequisicao.Excluir(requisicao);

            // assert
            Requisicao requisicaoEncontrada = repositorioRequisicao.SelecionarPorId(requisicao.Id);

            Assert.IsNull(requisicaoEncontrada);
        }

        [TestMethod]
        public void Deve_Selecionar_Todas_As_Requisicoes()
        {
            // arrange
            repositorioFuncionario.Inserir(funcionario);
            repositorioPaciente.Inserir(paciente);
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);
            repositorioRequisicao.Inserir(requisicao);

            Funcionario funcionario2 = new()
            {
                Nome = "James",
                Usuario = "username.678",
                Senha = "678@password!username"
            };

            Paciente paciente2 = new()
            {
                Nome = "James",
                CartaoSUS = "456789123456789"
            };

            Fornecedor fornecedor2 = new()
            {
                Nome = "James",
                Telefone = "49998468794",
                Email = "james@email.com",
                Cidade = "Guarulhos",
                UF = "SP"
            };

            Medicamento medicamento2 = new()
            {
                Nome = "Nimesulida",
                Descricao = "Anti-Inflamatório",
                Lote = "N-001",
                Validade = new DateTime(2025, 10, 5),
                QuantidadeDisponivel = 30,
                Fornecedor = fornecedor2
            };

            Requisicao requisicao2 = new()
            {
                Funcionario = funcionario2,
                Paciente = paciente2,
                Medicamento = medicamento2,
                QuantidadeMedicamento = 10,
                DataRequisicao = DateTime.Now.Date
            };

            repositorioFuncionario.Inserir(funcionario2);
            repositorioPaciente.Inserir(paciente2);
            repositorioFornecedor.Inserir(fornecedor2);
            repositorioMedicamento.Inserir(medicamento2);
            repositorioRequisicao.Inserir(requisicao2);

            // action
            List<Requisicao> requisicoesEncontradas = repositorioRequisicao.SelecionarTodos();

            // assert
            Assert.AreEqual(2, requisicoesEncontradas.Count);
            Assert.AreEqual(requisicao, requisicoesEncontradas[0]);
            Assert.AreEqual(requisicao2, requisicoesEncontradas[1]);
        }

        [TestMethod]
        public void Deve_Selecionar_Requisicao_Por_Id()
        {
            // arrange
            repositorioFuncionario.Inserir(funcionario);
            repositorioPaciente.Inserir(paciente);
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);
            repositorioRequisicao.Inserir(requisicao);

            // action
            Requisicao requisicaoEncontrada = repositorioRequisicao.SelecionarPorId(requisicao.Id);

            // assert
            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(requisicao, requisicaoEncontrada);
        }
    }
}