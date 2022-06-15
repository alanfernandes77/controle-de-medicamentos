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
        private readonly RepositorioMedicamento repositorioMedicamento;
        private readonly RepositorioFornecedor repositorioFornecedor;

        public RepositorioMedicamentoTestes()
        {
            using (Conexao = new(StringConexao))
            {
                string query =
                    @"DELETE FROM TBMedicamento;
                    DBCC CHECKIDENT (TBMedicamento, RESEED, 0)

                    DELETE FROM TBFornecedor;
                    DBCC CHECKIDENT (TBFornecedor, RESEED, 0)";

                SqlCommand comando = new(query, Conexao);

                Conexao.Open();

                comando.ExecuteNonQuery();
            }

            repositorioMedicamento = new();
            repositorioFornecedor = new(repositorioMedicamento);
        }

        [TestMethod]
        public void Deve_Inserir_Medicamento()
        {
            // arrange
            Medicamento medicamento = new()
            {
                Nome = "Paracetamol",
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

            repositorioFornecedor.Inserir(medicamento.Fornecedor);

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
            Medicamento medicamento = new()
            {
                Nome = "Paracetamol",
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

            repositorioFornecedor.Inserir(medicamento.Fornecedor);
            repositorioMedicamento.Inserir(medicamento);

            Medicamento MedicamentoAtualizado = repositorioMedicamento.SelecionarPorId(medicamento.Id);

            MedicamentoAtualizado.Nome = "Nimesulida";
            MedicamentoAtualizado.Descricao = "Anti-Inflamatório";
            MedicamentoAtualizado.Lote = "N-001";
            MedicamentoAtualizado.Validade = new DateTime(2023, 08, 20);
            MedicamentoAtualizado.QuantidadeDisponivel = 100;
            MedicamentoAtualizado.Fornecedor = new()
            {
                Nome = "James",
                Telefone = "11984675506",
                Email = "james@email.com",
                Cidade = "Guarulhos",
                UF = "SP"
            };

            repositorioFornecedor.Inserir(MedicamentoAtualizado.Fornecedor);

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
            Medicamento medicamento = new()
            {
                Nome = "Paracetamol",
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

            repositorioFornecedor.Inserir(medicamento.Fornecedor);
            repositorioMedicamento.Inserir(medicamento);

            // action
            repositorioMedicamento.Excluir(medicamento);

            Medicamento MedicamentoEncontrado = repositorioMedicamento.SelecionarPorId(medicamento.Id);

            // assert
            Assert.IsNull(MedicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_Selecionar_Todos_Os_Medicamentos()
        {
            // arrange
            Medicamento medicamento = new()
            {
                Nome = "Paracetamol",
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

            Medicamento medicamento2 = new()
            {
                Nome = "Nimesulida",
                Descricao = "Anti-Inflamatório",
                Lote = "N-001",
                Validade = new DateTime(2025, 5, 15),
                QuantidadeDisponivel = 100,
                Fornecedor = new()
                {
                    Nome = "James",
                    Telefone = "11984675506",
                    Email = "james@email.com",
                    Cidade = "Guarulhos",
                    UF = "SP"
                }
            };

            Medicamento medicamento3 = new()
            {
                Nome = "Rivotril",
                Descricao = "Remédio para programador",
                Lote = "R-001",
                Validade = new DateTime(2030, 10, 5),
                QuantidadeDisponivel = 200,
                Fornecedor = new()
                {
                    Nome = "Rafael",
                    Telefone = "51998451710",
                    Email = "rafael@email.com",
                    Cidade = "Gravataí",
                    UF = "RS"
                }
            };

            repositorioFornecedor.Inserir(medicamento.Fornecedor);
            repositorioFornecedor.Inserir(medicamento2.Fornecedor);
            repositorioFornecedor.Inserir(medicamento3.Fornecedor);

            repositorioMedicamento.Inserir(medicamento);
            repositorioMedicamento.Inserir(medicamento2);
            repositorioMedicamento.Inserir(medicamento3);

            // action
            List<Medicamento> medicamentos = repositorioMedicamento.SelecionarTodos();

            // assert
            Assert.AreEqual(3, medicamentos.Count);

            Assert.AreEqual("Paracetamol", medicamentos[0].Nome);
            Assert.AreEqual("Nimesulida", medicamentos[1].Nome);
            Assert.AreEqual("Rivotril", medicamentos[2].Nome);
        }

        [TestMethod]
        public void Deve_Selecionar_Medicamento_Por_Id()
        {
            // arrange
            Medicamento medicamento = new()
            {
                Nome = "Paracetamol",
                Descricao = "Remédio para dor de cabeça",
                Lote = "P-001",
                Validade = new DateTime(2022, 8, 20),
                QuantidadeDisponivel = 7,
                Fornecedor = new()
                {
                    Nome = "Alan",
                    Telefone = "49998165491",
                    Email = "alan@email.com",
                    Cidade = "Lages",
                    UF = "SC"
                }
            };

            repositorioFornecedor.Inserir(medicamento.Fornecedor);
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
            Medicamento medicamento = new()
            {
                Nome = "Paracetamol",
                Descricao = "Remédio para dor de cabeça",
                Lote = "P-001",
                Validade = new DateTime(2022, 8, 20),
                QuantidadeDisponivel = 7,
                Fornecedor = new()
                {
                    Nome = "Alan",
                    Telefone = "49998165491",
                    Email = "alan@email.com",
                    Cidade = "Lages",
                    UF = "SC"
                }
            };

            Medicamento medicamento2 = new()
            {
                Nome = "Nimesulida",
                Descricao = "Anti-Inflamatório",
                Lote = "N-001",
                Validade = new DateTime(2025, 5, 15),
                QuantidadeDisponivel = 10,
                Fornecedor = new()
                {
                    Nome = "James",
                    Telefone = "11984675506",
                    Email = "james@email.com",
                    Cidade = "Guarulhos",
                    UF = "SP"
                }
            };

            Medicamento medicamento3 = new()
            {
                Nome = "Rivotril",
                Descricao = "Remédio para programador",
                Lote = "R-001",
                Validade = new DateTime(2030, 10, 5),
                QuantidadeDisponivel = 7,
                Fornecedor = new()
                {
                    Nome = "Rafael",
                    Telefone = "51998451710",
                    Email = "rafael@email.com",
                    Cidade = "Gravataí",
                    UF = "RS"
                }
            };

            repositorioFornecedor.Inserir(medicamento.Fornecedor);
            repositorioFornecedor.Inserir(medicamento2.Fornecedor);
            repositorioFornecedor.Inserir(medicamento3.Fornecedor);

            repositorioMedicamento.Inserir(medicamento);
            repositorioMedicamento.Inserir(medicamento2);
            repositorioMedicamento.Inserir(medicamento3);

            // action
            List<Medicamento> medicamentos = repositorioMedicamento.SelecionarMedicamentosComBaixoEstoque();

            // assert
            Assert.AreEqual(2, medicamentos.Count);

            Assert.AreEqual("Paracetamol", medicamentos[0].Nome);
            Assert.AreEqual("Rivotril", medicamentos[1].Nome);
        }
    }
}