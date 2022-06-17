using ControleDeMedicamentos.Dominio.ModuloPaciente;
using ControleDeMedicamentos.Infra.BancoDeDados.Compartilhado;
using FluentValidation;
using FluentValidation.Results;
using System.Data.SqlClient;

namespace ControleDeMedicamentos.Infra.BancoDeDados.ModuloPaciente
{
    public class RepositorioPaciente : ConexaoSql, IRepositorio<Paciente>
    {
        public ValidationResult Inserir(Paciente paciente)
        {
            ValidationResult resultadoValidacao = ObterValidador().Validate(paciente);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            using (Conexao = new(StringConexao))
            {
                string query =
                    @"INSERT INTO [TBPaciente] 
                        (
                            [NOME],
                            [CARTAOSUS]
                        )

                        VALUES 

                        (
                            @NOME,
                            @CARTAOSUS
                        ); 

                        SELECT SCOPE_IDENTITY();";

                using SqlCommand comando = new(query, Conexao);

                comando.Parameters.AddWithValue("@NOME", paciente.Nome);
                comando.Parameters.AddWithValue("@CARTAOSUS", paciente.CartaoSUS);

                Conexao.Open();

                paciente.Id = Convert.ToInt32(comando.ExecuteScalar());

                return resultadoValidacao;
            }
        }

        public ValidationResult Editar(Paciente paciente)
        {
            ValidationResult resultadoValidacao = ObterValidador().Validate(paciente);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            using (Conexao = new(StringConexao))
            {
                string query =
                    @"UPDATE [TBPaciente] 
                        SET

                            [NOME] = @NOME,
                            [CARTAOSUS] = @CARTAOSUS

                        WHERE
                            
                            [ID] = @ID;";

                using SqlCommand comando = new(query, Conexao);

                comando.Parameters.AddWithValue("@ID", paciente.Id);
                comando.Parameters.AddWithValue("@NOME", paciente.Nome);
                comando.Parameters.AddWithValue("@CARTAOSUS", paciente.CartaoSUS);

                Conexao.Open();

                comando.ExecuteNonQuery();

                return resultadoValidacao;
            }
        }

        public ValidationResult Excluir(Paciente paciente)
        {
            using (Conexao = new(StringConexao))
            {
                string query = @"DELETE FROM [TBPaciente] WHERE [ID] = @ID;";

                using SqlCommand comando = new(query, Conexao);

                comando.Parameters.AddWithValue("@ID", paciente.Id);

                Conexao.Open();

                ValidationResult resultadoValidacao = ObterValidador().Validate(paciente);

                if (resultadoValidacao.IsValid)
                    comando.ExecuteNonQuery();

                return resultadoValidacao;
            }
        }

        public List<Paciente> SelecionarTodos()
        {
            using (Conexao = new(StringConexao))
            {
                string query =
                    @"SELECT 
                            [ID],
                            [NOME],
                            [CARTAOSUS]
                        FROM
                            [TBPaciente]";

                using SqlCommand comando = new(query, Conexao);

                Conexao.Open();

                using SqlDataReader leitor = comando.ExecuteReader();

                List<Paciente> pacientes = new();

                while (leitor.Read())
                {
                    Paciente paciente = new()
                    {
                        Id = Convert.ToInt32(leitor["ID"]),
                        Nome = Convert.ToString(leitor["NOME"]),
                        CartaoSUS = Convert.ToString(leitor["CARTAOSUS"])
                    };

                    pacientes.Add(paciente);
                }

                return pacientes;
            }
        }

        public Paciente SelecionarPorId(int id)
        {
            using (Conexao = new(StringConexao))
            {
                string query =
                    @"SELECT 
                            [ID],
	                        [NOME],
	                        [CARTAOSUS]

                        FROM

	                        [TBPaciente]

                        WHERE [ID] = @ID";

                using SqlCommand comando = new(query, Conexao);

                comando.Parameters.AddWithValue("@ID", id);

                Conexao.Open();

                using SqlDataReader leitor = comando.ExecuteReader();

                Paciente paciente = null;

                if (leitor.Read())
                    paciente = new()
                    {
                        Id = Convert.ToInt32(leitor["ID"]),
                        Nome = Convert.ToString(leitor["NOME"]),
                        CartaoSUS = Convert.ToString(leitor["CARTAOSUS"])
                    };

                return paciente;
            }
        }

        public AbstractValidator<Paciente> ObterValidador()
        {
            return new ValidadorPaciente();
        }
    }
}