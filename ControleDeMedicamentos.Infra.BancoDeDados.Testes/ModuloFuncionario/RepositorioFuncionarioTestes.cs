using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Infra.BancoDeDados.Compartilhado;
using ControleDeMedicamentos.Infra.BancoDeDados.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace ControleDeMedicamentos.Infra.BancoDeDados.Testes.ModuloFuncionario
{
    [TestClass]
    public class RepositorioFuncionarioTestes : ConexaoSql
    {
        private readonly Funcionario funcionario;
        private readonly RepositorioFuncionario repositorioFuncionario;

        public RepositorioFuncionarioTestes()
        {
            using (Conexao = new(StringConexao))
            {
                string query =
                    @"DELETE FROM TBFuncionario;
                    DBCC CHECKIDENT (TBFuncionario, RESEED, 0)";

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

            repositorioFuncionario = new();
        }

        [TestMethod]
        public void Deve_Inserir_Funcionario()
        {
            // action
            repositorioFuncionario.Inserir(funcionario);

            // assert
            Funcionario funcionarioEncontrado = repositorioFuncionario.SelecionarPorId(funcionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionario, funcionarioEncontrado);
        }

        [TestMethod]
        public void Deve_Editar_Funcionario()
        {
            // arrange
            repositorioFuncionario.Inserir(funcionario);

            Funcionario funcionarioAtualizado = repositorioFuncionario.SelecionarPorId(funcionario.Id);

            funcionarioAtualizado.Nome = "James";
            funcionarioAtualizado.Usuario = "username.135";
            funcionarioAtualizado.Senha = "358@username!password";

            // action
            repositorioFuncionario.Editar(funcionarioAtualizado);

            // assert 
            Funcionario funcionarioEncontrado = repositorioFuncionario.SelecionarPorId(funcionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionarioAtualizado, funcionarioEncontrado);
        }

        [TestMethod]
        public void Deve_Excluir_Funcionario()
        {
            // arrange
            repositorioFuncionario.Inserir(funcionario);

            // action
            repositorioFuncionario.Excluir(funcionario);

            // assert
            Funcionario funcionarioEncontrado = repositorioFuncionario.SelecionarPorId(funcionario.Id);

            Assert.IsNull(funcionarioEncontrado);
        }

        [TestMethod]
        public void Deve_Selecionar_Todos_Os_Funcionarios()
        {
            // arrange
            Funcionario funcionario2 = new()
            {
                Nome = "James",
                Usuario = "username.135",
                Senha = "358@username!password"
            };

            repositorioFuncionario.Inserir(funcionario);
            repositorioFuncionario.Inserir(funcionario2);

            // action
            List<Funcionario> funcionariosEncontrados = repositorioFuncionario.SelecionarTodos();

            // assert
            Assert.AreEqual(2, funcionariosEncontrados.Count);

            Assert.AreEqual("Alan", funcionariosEncontrados[0].Nome);
            Assert.AreEqual("James", funcionariosEncontrados[1].Nome);
        }

        [TestMethod]
        public void Deve_Selecionar_Funcionario_Por_Id()
        {
            // arrange
            repositorioFuncionario.Inserir(funcionario);

            // action
            Funcionario funcionarioEncontrado = repositorioFuncionario.SelecionarPorId(funcionario.Id);

            // assert
            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionario, funcionarioEncontrado);
        }
    }
}