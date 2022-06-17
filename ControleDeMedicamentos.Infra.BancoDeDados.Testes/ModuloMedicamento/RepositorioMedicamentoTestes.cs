using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using ControleDeMedicamentos.Infra.BancoDeDados.Compartilhado;
using ControleDeMedicamentos.Infra.BancoDeDados.ModuloFornecedor;
using ControleDeMedicamentos.Infra.BancoDeDados.ModuloMedicamento;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace ControleDeMedicamentos.Infra.BancoDeDados.Testes.ModuloMedicamento
{
    [TestClass]
    public class RepositorioMedicamentoTestes : ConexaoSql
    {
        private readonly Fornecedor fornecedor;
        private readonly Medicamento medicamento;

        private readonly RepositorioFornecedor repositorioFornecedor;
        private readonly RepositorioMedicamento repositorioMedicamento;

        public RepositorioMedicamentoTestes()
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

            repositorioFornecedor = new();
            repositorioMedicamento = new();
        }

        [TestMethod]
        public void Deve_Inserir_Medicamento()
        {
            // arrange
            repositorioFornecedor.Inserir(fornecedor);

            // action
            repositorioMedicamento.Inserir(medicamento);

            // assert
            Medicamento MedicamentoEncontrado = repositorioMedicamento.SelecionarPorId(medicamento.Id);

            Assert.IsNotNull(MedicamentoEncontrado);
            Assert.AreEqual(medicamento, MedicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_Editar_Medicamento()
        {
            // arrange
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);

            Fornecedor fornecedor2 = new()
            {
                Nome = "James",
                Telefone = "11984675506",
                Email = "james@email.com",
                Cidade = "Guarulhos",
                UF = "SP"
            };

            repositorioFornecedor.Inserir(fornecedor2);

            Medicamento MedicamentoAtualizado = repositorioMedicamento.SelecionarPorId(medicamento.Id);

            MedicamentoAtualizado.Nome = "Nimesulida";
            MedicamentoAtualizado.Descricao = "Anti-Inflamatório";
            MedicamentoAtualizado.Lote = "N-001";
            MedicamentoAtualizado.Validade = new DateTime(2023, 08, 20);
            MedicamentoAtualizado.QuantidadeDisponivel = 100;
            MedicamentoAtualizado.Fornecedor = fornecedor2;

            // action
            repositorioMedicamento.Editar(MedicamentoAtualizado);

            // assert 
            Medicamento MedicamentoEncontrado = repositorioMedicamento.SelecionarPorId(medicamento.Id);

            Assert.IsNotNull(MedicamentoEncontrado);
            Assert.AreEqual(MedicamentoAtualizado, MedicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_Excluir_Medicamento()
        {
            // arrange
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);

            // action
            repositorioMedicamento.Excluir(medicamento);

            // assert
            Medicamento MedicamentoEncontrado = repositorioMedicamento.SelecionarPorId(medicamento.Id);

            Assert.IsNull(MedicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_Selecionar_Todos_Os_Medicamentos()
        {
            // arrange
            Fornecedor fornecedor2 = new()
            {
                Nome = "James",
                Telefone = "11984675506",
                Email = "james@email.com",
                Cidade = "Guarulhos",
                UF = "SP"
            };

            Medicamento medicamento2 = new()
            {
                Nome = "Nimesulida",
                Descricao = "Anti-Inflamatório",
                Lote = "N-001",
                Validade = new DateTime(2025, 5, 15),
                QuantidadeDisponivel = 100,
                Fornecedor = fornecedor
            };

            repositorioFornecedor.Inserir(fornecedor);
            repositorioFornecedor.Inserir(fornecedor2);

            repositorioMedicamento.Inserir(medicamento);
            repositorioMedicamento.Inserir(medicamento2);

            // action
            List<Medicamento> medicamentosEncontrados = repositorioMedicamento.SelecionarTodos();

            // assert
            Assert.AreEqual(2, medicamentosEncontrados.Count);

            Assert.AreEqual("Paracetamol", medicamentosEncontrados[0].Nome);
            Assert.AreEqual("Nimesulida", medicamentosEncontrados[1].Nome);
        }

        [TestMethod]
        public void Deve_Selecionar_Medicamento_Por_Id()
        {
            // arrange
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);

            // action
            Medicamento medicamentoEncontrado = repositorioMedicamento.SelecionarPorId(medicamento.Id);

            // assert
            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(medicamento, medicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_Selecionar_Medicamentos_Com_Baixo_Estoque()
        {
            // arrange
            medicamento.QuantidadeDisponivel = 4;

            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);

            // action
            List<Medicamento> medicamentoEncontrados = repositorioMedicamento.SelecionarMedicamentosComBaixoEstoque();

            // assert
            Assert.AreEqual(1, medicamentoEncontrados.Count);

            Assert.AreEqual("Paracetamol", medicamentoEncontrados[0].Nome);
        }
    }
}