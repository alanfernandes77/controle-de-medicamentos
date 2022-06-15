using ControleDeMedicamentos.Dominio.ModuloPaciente;
using ControleDeMedicamentos.Infra.BancoDeDados.Compartilhado;
using ControleDeMedicamentos.Infra.BancoDeDados.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace ControleDeMedicamentos.Infra.BancoDeDados.Testes.ModuloPaciente
{
    [TestClass]
    public class RepositorioPacienteTestes : ConexaoSql
    {
        private readonly RepositorioPaciente repositorioPaciente;

        public RepositorioPacienteTestes()
        {
            using (Conexao = new(StringConexao))
            {
                string query =
                    @"DELETE FROM TBPaciente;
                    DBCC CHECKIDENT (TBPaciente, RESEED, 0)";

                SqlCommand comando = new(query, Conexao);

                Conexao.Open();

                comando.ExecuteNonQuery();
            }

            repositorioPaciente = new();
        }

        [TestMethod]
        public void Deve_Inserir_Paciente()
        {
            // arrange
            Paciente novoPaciente = new()
            {
                Nome = "Alan",
                CartaoSUS = "665879454872154"
            };

            // action
            repositorioPaciente.Inserir(novoPaciente);

            // assert
            Paciente pacienteEncontrado = repositorioPaciente.SelecionarPorId(novoPaciente.Id);

            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(novoPaciente, pacienteEncontrado);
        }

        [TestMethod]
        public void Deve_Editar_Paciente()
        {
            // arrange
            Paciente novoPaciente = new()
            {
                Nome = "Alan",
                CartaoSUS = "665879454872154"
            };

            repositorioPaciente.Inserir(novoPaciente);

            Paciente pacienteAtualizado = repositorioPaciente.SelecionarPorId(novoPaciente.Id);

            pacienteAtualizado.Nome = "James";
            pacienteAtualizado.CartaoSUS = "115789451682314";

            // action
            repositorioPaciente.Editar(pacienteAtualizado);

            // assert
            Paciente pacienteEncontrado = repositorioPaciente.SelecionarPorId(novoPaciente.Id);

            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(pacienteAtualizado.Id, pacienteEncontrado.Id);
            Assert.AreEqual(pacienteAtualizado.Nome, pacienteEncontrado.Nome);
            Assert.AreEqual(pacienteAtualizado.CartaoSUS, pacienteEncontrado.CartaoSUS);
        }

        [TestMethod]
        public void Deve_Excluir_Paciente()
        {
            // arrange
            Paciente novoPaciente = new()
            {
                Nome = "Alan",
                CartaoSUS = "665879454872154"
            };

            repositorioPaciente.Inserir(novoPaciente);

            // action
            repositorioPaciente.Excluir(novoPaciente);

            // assert
            Paciente pacienteEncontrado = repositorioPaciente.SelecionarPorId(novoPaciente.Id);

            Assert.IsNull(pacienteEncontrado);
        }

        [TestMethod]
        public void Deve_Selecionar_Todos_Os_Pacientes()
        {
            // arrange
            Paciente paciente = new()
            {
                Nome = "Alan",
                CartaoSUS = "665879454872154"
            };

            Paciente paciente2 = new()
            {
                Nome = "James",
                CartaoSUS = "115789451682314"
            };

            Paciente paciente3 = new()
            {
                Nome = "Rafael",
                CartaoSUS = "957847164798512"
            };

            repositorioPaciente.Inserir(paciente);
            repositorioPaciente.Inserir(paciente2);
            repositorioPaciente.Inserir(paciente3);

            // action
            List<Paciente> pacientes = repositorioPaciente.SelecionarTodos();

            // assert
            Assert.AreEqual(3, pacientes.Count);

            Assert.AreEqual("Alan", pacientes[0].Nome);
            Assert.AreEqual("James", pacientes[1].Nome);
            Assert.AreEqual("Rafael", pacientes[2].Nome);
        }

        [TestMethod]
        public void Deve_Selecionar_Paciente_Por_Id()
        {
            // arrange
            Paciente paciente = new()
            {
                Nome = "Alan",
                CartaoSUS = "665879454872154"
            };

            repositorioPaciente.Inserir(paciente);

            // action
            Paciente pacienteEncontrado = repositorioPaciente.SelecionarPorId(paciente.Id);

            // assert
            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(paciente, pacienteEncontrado);
        }
    }
}