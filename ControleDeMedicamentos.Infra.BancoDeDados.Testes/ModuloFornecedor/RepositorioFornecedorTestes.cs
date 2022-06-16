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
        private readonly Fornecedor fornecedor;
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

            fornecedor = new()
            {
                Nome = "Alan",
                Telefone = "49998165491",
                Email = "alan@email.com",
                Cidade = "Lages",
                UF = "SC"
            };

            repositorioFornecedor = new();
        }

        [TestMethod]
        public void Deve_Inserir_Fornecedor()
        {
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
            repositorioFornecedor.Inserir(fornecedor);

            // action
            repositorioFornecedor.Excluir(fornecedor);

            // assert
            Fornecedor fornecedorEncontrado = repositorioFornecedor.SelecionarPorId(fornecedor.Id);

            Assert.IsNull(fornecedorEncontrado);
        }

        [TestMethod]
        public void Deve_Selecionar_Todos_Os_Fornecedores()
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

            repositorioFornecedor.Inserir(fornecedor);
            repositorioFornecedor.Inserir(fornecedor2);

            // action
            List<Fornecedor> fornecedoresEncontrados = repositorioFornecedor.SelecionarTodos();

            // assert
            Assert.AreEqual(2, fornecedoresEncontrados.Count);

            Assert.AreEqual("Alan", fornecedoresEncontrados[0].Nome);
            Assert.AreEqual("James", fornecedoresEncontrados[1].Nome);
        }

        [TestMethod]
        public void Deve_Selecionar_Fornecedor_Por_Id()
        {
            // arrange
            repositorioFornecedor.Inserir(fornecedor);

            // action
            Fornecedor fornecedorEncontrado = repositorioFornecedor.SelecionarPorId(fornecedor.Id);

            // assert
            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedor, fornecedorEncontrado);
        }
    }
}