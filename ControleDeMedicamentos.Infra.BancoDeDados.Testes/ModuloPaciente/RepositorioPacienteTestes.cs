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
        private readonly Paciente paciente;
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

            paciente = new()
            {
                Nome = "Alan",
                CartaoSUS = "665879454872154"
            };
        }

        [TestMethod]
        public void Deve_Inserir_Paciente()
        {
            // action
            repositorioPaciente.Inserir(paciente);

            // assert
            Paciente pacienteEncontrado = repositorioPaciente.SelecionarPorId(paciente.Id);

            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(paciente, pacienteEncontrado);
        }

        [TestMethod]
        public void Deve_Editar_Paciente()
        {
            // arrange
            repositorioPaciente.Inserir(paciente);

            Paciente pacienteAtualizado = repositorioPaciente.SelecionarPorId(paciente.Id);

            pacienteAtualizado.Nome = "James";
            pacienteAtualizado.CartaoSUS = "115789451682314";

            // action
            repositorioPaciente.Editar(pacienteAtualizado);

            // assert
            Paciente pacienteEncontrado = repositorioPaciente.SelecionarPorId(paciente.Id);

            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(pacienteAtualizado, pacienteEncontrado);
        }

        [TestMethod]
        public void Deve_Excluir_Paciente()
        {
            // arrange
            repositorioPaciente.Inserir(paciente);

            // action
            repositorioPaciente.Excluir(paciente);

            // assert
            Paciente pacienteEncontrado = repositorioPaciente.SelecionarPorId(paciente.Id);

            Assert.IsNull(pacienteEncontrado);
        }

        [TestMethod]
        public void Deve_Selecionar_Todos_Os_Pacientes()
        {
            // arrange
            Paciente paciente2 = new()
            {
                Nome = "James",
                CartaoSUS = "115789451682314"
            };

            repositorioPaciente.Inserir(paciente);
            repositorioPaciente.Inserir(paciente2);

            // action
            List<Paciente> pacientesEncontrados = repositorioPaciente.SelecionarTodos();

            // assert
            Assert.AreEqual(2, pacientesEncontrados.Count);

            Assert.AreEqual("Alan", pacientesEncontrados[0].Nome);
            Assert.AreEqual("James", pacientesEncontrados[1].Nome);
        }

        [TestMethod]
        public void Deve_Selecionar_Paciente_Por_Id()
        {
            // arrange
            repositorioPaciente.Inserir(paciente);

            // action
            Paciente pacienteEncontrado = repositorioPaciente.SelecionarPorId(paciente.Id);

            // assert
            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(paciente, pacienteEncontrado);
        }
    }
}