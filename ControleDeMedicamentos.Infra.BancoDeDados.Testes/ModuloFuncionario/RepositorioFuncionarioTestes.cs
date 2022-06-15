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

            repositorioFuncionario = new();
        }

        [TestMethod]
        public void Deve_Inserir_Funcionario()
        {
            // arrange
            Funcionario novoFuncionario = new()
            {
                Nome = "Alan",
                Usuario = "username.954",
                Senha = "459@password!username"
            };

            // action
            repositorioFuncionario.Inserir(novoFuncionario);

            // assert
            Funcionario funcionarioEncontrado = repositorioFuncionario.SelecionarPorId(novoFuncionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(novoFuncionario, funcionarioEncontrado);
        }

        [TestMethod]
        public void Deve_Editar_Funcionario()
        {
            // arrange
            Funcionario novoFuncionario = new()
            {
                Nome = "Alan",
                Usuario = "username.954",
                Senha = "459@password!username"
            };

            repositorioFuncionario.Inserir(novoFuncionario);

            Funcionario funcionarioAtualizado = repositorioFuncionario.SelecionarPorId(novoFuncionario.Id);

            funcionarioAtualizado.Nome = "James";
            funcionarioAtualizado.Usuario = "username.135";
            funcionarioAtualizado.Senha = "358@username!password";

            // action
            repositorioFuncionario.Editar(funcionarioAtualizado);

            // assert 
            Funcionario funcionarioEncontrado = repositorioFuncionario.SelecionarPorId(novoFuncionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionarioAtualizado.Id, funcionarioEncontrado.Id);
            Assert.AreEqual(funcionarioAtualizado.Nome, funcionarioEncontrado.Nome);
            Assert.AreEqual(funcionarioAtualizado.Usuario, funcionarioEncontrado.Usuario);
            Assert.AreEqual(funcionarioAtualizado.Senha, funcionarioEncontrado.Senha);
        }

        [TestMethod]
        public void Deve_Excluir_Funcionario()
        {
            // arrange
            Funcionario novoFuncionario = new()
            {
                Nome = "Alan",
                Usuario = "username.954",
                Senha = "459@password!username"
            };

            repositorioFuncionario.Inserir(novoFuncionario);

            // action
            repositorioFuncionario.Excluir(novoFuncionario);

            Funcionario funcionarioEncontrado = repositorioFuncionario.SelecionarPorId(novoFuncionario.Id);

            // assert
            Assert.IsNull(funcionarioEncontrado);
        }

        [TestMethod]
        public void Deve_Selecionar_Todos_Os_Funcionarios()
        {
            // arrange
            Funcionario funcionario = new()
            {
                Nome = "Alan",
                Usuario = "username.954",
                Senha = "459@password!username"
            };

            Funcionario funcionario2 = new()
            {
                Nome = "James",
                Usuario = "username.135",
                Senha = "358@username!password"
            };

            Funcionario funcionario3 = new()
            {
                Nome = "Rafael",
                Usuario = "username.357",
                Senha = "869@password!username"
            };

            repositorioFuncionario.Inserir(funcionario);
            repositorioFuncionario.Inserir(funcionario2);
            repositorioFuncionario.Inserir(funcionario3);

            // action
            List<Funcionario> funcionarios = repositorioFuncionario.SelecionarTodos();

            // assert
            Assert.AreEqual(3, funcionarios.Count);

            Assert.AreEqual("Alan", funcionarios[0].Nome);
            Assert.AreEqual("James", funcionarios[1].Nome);
            Assert.AreEqual("Rafael", funcionarios[2].Nome);
        }

        [TestMethod]
        public void Deve_Selecionar_Funcionario_Por_Id()
        {
            // arrange
            Funcionario funcionario = new()
            {
                Nome = "Alan",
                Usuario = "username.954",
                Senha = "459@password!username"
            };

            repositorioFuncionario.Inserir(funcionario);

            // action
            Funcionario funcionarioEncontrado = repositorioFuncionario.SelecionarPorId(funcionario.Id);

            // assert
            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionario, funcionarioEncontrado);
        }
    }
}