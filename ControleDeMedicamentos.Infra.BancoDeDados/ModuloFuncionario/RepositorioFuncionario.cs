using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Infra.BancoDeDados.Compartilhado;
using FluentValidation;
using FluentValidation.Results;
using System.Data.SqlClient;

namespace ControleDeMedicamentos.Infra.BancoDeDados.ModuloFuncionario
{
    public class RepositorioFuncionario : ConexaoSql, IRepositorio<Funcionario>
    {
        public ValidationResult Inserir(Funcionario funcionario)
        {
            ValidationResult resultadoValidacao = ObterValidador().Validate(funcionario);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            using (Conexao = new(StringConexao))
            {
                string query =
                    @"INSERT INTO [TBFuncionario] 
                        (
                            [NOME],
                            [USUARIO],
                            [SENHA]
                        )

                        VALUES 

                        (
                            @NOME,
                            @USUARIO,
                            @SENHA
                        ); 

                        SELECT SCOPE_IDENTITY();";

                using SqlCommand comando = new(query, Conexao);

                comando.Parameters.AddWithValue("@NOME", funcionario.Nome);
                comando.Parameters.AddWithValue("@USUARIO", funcionario.Usuario);
                comando.Parameters.AddWithValue("@SENHA", funcionario.Senha);

                Conexao.Open();

                funcionario.Id = Convert.ToInt32(comando.ExecuteScalar());

                return resultadoValidacao;
            }
        }

        public ValidationResult Editar(Funcionario funcionario)
        {
            ValidationResult resultadoValidacao = ObterValidador().Validate(funcionario);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            using (Conexao = new(StringConexao))
            {
                string query =
                    @"UPDATE [TBFuncionario] 
                        SET

                            [NOME] = @NOME,
                            [USUARIO] = @USUARIO,
                            [SENHA] = @SENHA

                        WHERE
                            
                            [ID] = @ID;";

                using SqlCommand comando = new(query, Conexao);

                comando.Parameters.AddWithValue("@ID", funcionario.Id);
                comando.Parameters.AddWithValue("@NOME", funcionario.Nome);
                comando.Parameters.AddWithValue("@USUARIO", funcionario.Usuario);
                comando.Parameters.AddWithValue("@SENHA", funcionario.Senha);

                Conexao.Open();

                comando.ExecuteNonQuery();

                return resultadoValidacao;
            }
        }

        public ValidationResult Excluir(Funcionario funcionario)
        {
            using (Conexao = new(StringConexao))
            {
                string query = @"DELETE FROM [TBFuncionario] WHERE [ID] = @ID;";

                using SqlCommand comando = new(query, Conexao);

                comando.Parameters.AddWithValue("@ID", funcionario.Id);

                Conexao.Open();

                ValidationResult resultadoValidacao = ObterValidador().Validate(funcionario);

                if (resultadoValidacao.IsValid)
                    comando.ExecuteNonQuery();

                return resultadoValidacao;
            }
        }

        public List<Funcionario> SelecionarTodos()
        {
            using (Conexao = new(StringConexao))
            {
                string query =
                    @"SELECT 
                            [ID],
                            [NOME],
                            [USUARIO],
                            [SENHA]
                        FROM
                            [TBFuncionario]";

                using SqlCommand comando = new(query, Conexao);

                Conexao.Open();

                using SqlDataReader leitor = comando.ExecuteReader();

                List<Funcionario> funcionarios = new();

                while (leitor.Read())
                {
                    Funcionario funcionario = new()
                    {
                        Id = Convert.ToInt32(leitor["ID"]),
                        Nome = Convert.ToString(leitor["NOME"]),
                        Usuario = Convert.ToString(leitor["USUARIO"]),
                        Senha = Convert.ToString(leitor["SENHA"])
                    };

                    funcionarios.Add(funcionario);
                }

                return funcionarios;
            }
        }

        public Funcionario SelecionarPorId(int id)
        {
            using (Conexao = new(StringConexao))
            {
                string query =
                    @"SELECT 
                            [ID],
	                        [NOME],
	                        [USUARIO],
                            [SENHA]

                        FROM

	                        [TBFuncionario]

                        WHERE [ID] = @ID";

                using SqlCommand comando = new(query, Conexao);

                comando.Parameters.AddWithValue("@ID", id);

                Conexao.Open();

                using SqlDataReader leitor = comando.ExecuteReader();

                Funcionario funcionario = null;

                if (leitor.Read())
                    funcionario = new()
                    {
                        Id = Convert.ToInt32(leitor["ID"]),
                        Nome = Convert.ToString(leitor["NOME"]),
                        Usuario = Convert.ToString(leitor["USUARIO"]),
                        Senha = Convert.ToString(leitor["SENHA"])
                    };

                return funcionario;
            }
        }

        public AbstractValidator<Funcionario> ObterValidador()
        {
            return new ValidadorFuncionario();
        }
    }
}