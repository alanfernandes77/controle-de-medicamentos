using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using ControleDeMedicamentos.Infra.BancoDeDados.Compartilhado;
using ControleDeMedicamentos.Infra.BancoDeDados.ModuloFornecedor;
using ControleDeMedicamentos.Infra.BancoDeDados.ModuloMedicamento;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace ControleDeMedicamentos.Infra.BancoDeDados.Testes.ModuloFornecedor
{
    [TestClass]
    public class RepositorioFornecedorTestes : ConexaoSql
    {
        private readonly RepositorioMedicamento repositorioMedicamento;
        private readonly RepositorioFornecedor repositorioFornecedor;
        public RepositorioFornecedorTestes()
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
        public void Deve_Inserir_Fornecedor()
        {
            // arrange
            Fornecedor fornecedor = new()
            {
                Nome = "Alan",
                Telefone = "49998165491",
                Email = "alan@email.com",
                Cidade = "Lages",
                UF = "SC"
            };

            // action
            repositorioFornecedor.Inserir(fornecedor);

            // assert
            Fornecedor fornecedorEncontrado = repositorioFornecedor.SelecionarPorId(fornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedor, fornecedorEncontrado);
        }

        [TestMethod]
        public void Deve_Editar_Fornecedor()
        {
            // arrange
            Fornecedor fornecedor = new()
            {
                Nome = "Alan",
                Telefone = "49998165491",
                Email = "alan@email.com",
                Cidade = "Lages",
                UF = "SC"
            };

            repositorioFornecedor.Inserir(fornecedor);

            Fornecedor fornecedorAtualizado = repositorioFornecedor.SelecionarPorId(fornecedor.Id);

            fornecedorAtualizado.Nome = "James";
            fornecedorAtualizado.Telefone = "11984675506";
            fornecedorAtualizado.Email = "james@email.com";
            fornecedorAtualizado.Cidade = "Guarulhos";
            fornecedorAtualizado.UF = "SP";

            // action
            repositorioFornecedor.Editar(fornecedorAtualizado);

            // assert 
            Fornecedor fornecedorEncontrado = repositorioFornecedor.SelecionarPorId(fornecedorAtualizado.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedorAtualizado, fornecedorEncontrado);
        }

        [TestMethod]
        public void Deve_Excluir_Fornecedor()
        {
            // arrange
            Fornecedor fornecedor = new()
            {
                Nome = "Alan",
                Telefone = "49998165491",
                Email = "alan@email.com",
                Cidade = "Lages",
                UF = "SC"
            };

            repositorioFornecedor.Inserir(fornecedor);

            // action
            repositorioFornecedor.Excluir(fornecedor);

            Fornecedor fornecedorEncontrado = repositorioFornecedor.SelecionarPorId(fornecedor.Id);

            // assert
            Assert.IsNull(fornecedorEncontrado);
        }

        [TestMethod]
        public void Nao_Deve_Excluir_Fornecedor_Se_Ligado_A_Medicamento()
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

            Fornecedor fornecedorEncontrado = repositorioFornecedor.SelecionarPorId(medicamento.Fornecedor.Id);

            // action
            ValidationResult resultado = repositorioFornecedor.Excluir(fornecedorEncontrado);

            // assert
            Assert.AreEqual("Não é possível remover este fornecedor, pois ele está relacionado a um medicamento.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Deve_Selecionar_Todos_Os_Fornecedores()
        {
            // arrange
            Fornecedor fornecedor1 = new()
            {
                Nome = "Alan",
                Telefone = "49998165491",
                Email = "alan@email.com",
                Cidade = "Lages",
                UF = "SC"
            };

            Fornecedor fornecedor2 = new()
            {
                Nome = "James",
                Telefone = "11984675506",
                Email = "james@email.com",
                Cidade = "Guarulhos",
                UF = "SP"
            };

            Fornecedor fornecedor3 = new()
            {
                Nome = "Rafael",
                Telefone = "51998451710",
                Email = "rafael@email.com",
                Cidade = "Gravataí",
                UF = "RS"
            };

            repositorioFornecedor.Inserir(fornecedor1);
            repositorioFornecedor.Inserir(fornecedor2);
            repositorioFornecedor.Inserir(fornecedor3);

            // action
            List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarTodos();

            // assert
            Assert.AreEqual(3, fornecedores.Count);

            Assert.AreEqual("Alan", fornecedores[0].Nome);
            Assert.AreEqual("James", fornecedores[1].Nome);
            Assert.AreEqual("Rafael", fornecedores[2].Nome);
        }

        [TestMethod]
        public void Deve_Selecionar_Fornecedor_Por_Id()
        {
            // arrange
            Fornecedor fornecedor = new()
            {
                Nome = "Alan",
                Telefone = "49998165491",
                Email = "alan@email.com",
                Cidade = "Lages",
                UF = "SC"
            };

            repositorioFornecedor.Inserir(fornecedor);

            // action
            Fornecedor fornecedorEncontrado = repositorioFornecedor.SelecionarPorId(fornecedor.Id);

            // assert
            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedor, fornecedorEncontrado);
        }
    }
}