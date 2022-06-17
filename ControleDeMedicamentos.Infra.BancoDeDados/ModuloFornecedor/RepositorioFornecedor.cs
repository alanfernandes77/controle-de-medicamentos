using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using ControleDeMedicamentos.Infra.BancoDeDados.Compartilhado;
using FluentValidation;
using FluentValidation.Results;
using System.Data.SqlClient;

namespace ControleDeMedicamentos.Infra.BancoDeDados.ModuloFornecedor
{
    public class RepositorioFornecedor : ConexaoSql, IRepositorio<Fornecedor>
    {

        public ValidationResult Inserir(Fornecedor fornecedor)
        {
            ValidationResult resultadoValidacao = ObterValidador().Validate(fornecedor);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            using (Conexao = new(StringConexao))
            {
                string query =
                    @"INSERT INTO [TBFornecedor] 
                        (
                            [NOME],
                            [TELEFONE],
                            [EMAIL],
                            [CIDADE],
                            [UF]
                        )

                        VALUES 

                        (
                            @NOME,
                            @TELEFONE,
                            @EMAIL,
                            @CIDADE,
                            @UF
                        ); 

                        SELECT SCOPE_IDENTITY();";

                using SqlCommand comando = new(query, Conexao);

                comando.Parameters.AddWithValue("@NOME", fornecedor.Nome);
                comando.Parameters.AddWithValue("@TELEFONE", fornecedor.Telefone);
                comando.Parameters.AddWithValue("@EMAIL", fornecedor.Email);
                comando.Parameters.AddWithValue("@CIDADE", fornecedor.Cidade);
                comando.Parameters.AddWithValue("@UF", fornecedor.UF);

                Conexao.Open();

                fornecedor.Id = Convert.ToInt32(comando.ExecuteScalar());

                return resultadoValidacao;
            }
        }

        public ValidationResult Editar(Fornecedor fornecedor)
        {
            ValidationResult resultadoValidacao = ObterValidador().Validate(fornecedor);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            using (Conexao = new(StringConexao))
            {
                string query =
                    @"UPDATE [TBFornecedor] 
                        SET

                            [NOME] = @NOME,
                            [TELEFONE] = @TELEFONE,
                            [EMAIL] = @EMAIL,
                            [CIDADE] = @CIDADE,
                            [UF] = @UF

                        WHERE
                            
                            [ID] = @ID;";

                using SqlCommand comando = new(query, Conexao);

                comando.Parameters.AddWithValue("@ID", fornecedor.Id);
                comando.Parameters.AddWithValue("@NOME", fornecedor.Nome);
                comando.Parameters.AddWithValue("@TELEFONE", fornecedor.Telefone);
                comando.Parameters.AddWithValue("@EMAIL", fornecedor.Email);
                comando.Parameters.AddWithValue("@CIDADE", fornecedor.Cidade);
                comando.Parameters.AddWithValue("@UF", fornecedor.UF);

                Conexao.Open();

                comando.ExecuteNonQuery();

                return resultadoValidacao;
            }
        }

        public ValidationResult Excluir(Fornecedor fornecedor)
        {
            using (Conexao = new(StringConexao))
            {
                string query = @"DELETE FROM [TBFornecedor] WHERE [ID] = @ID;";

                using SqlCommand comando = new(query, Conexao);

                comando.Parameters.AddWithValue("@ID", fornecedor.Id);

                Conexao.Open();

                ValidationResult resultadoValidacao = ObterValidador().Validate(fornecedor);

                if (resultadoValidacao.IsValid)
                    comando.ExecuteNonQuery();

                return resultadoValidacao;
            }
        }

        public List<Fornecedor> SelecionarTodos()
        {
            using (Conexao = new(StringConexao))
            {
                string query =
                    @"SELECT 
                            [ID],
                            [NOME],
                            [TELEFONE],
                            [EMAIL],
                            [CIDADE],
                            [UF]

                        FROM

                            [TBFornecedor]";

                using SqlCommand comando = new(query, Conexao);

                Conexao.Open();

                using SqlDataReader leitor = comando.ExecuteReader();

                List<Fornecedor> fornecedores = new();

                while (leitor.Read())
                {
                    Fornecedor fornecedor = new()
                    {
                        Id = Convert.ToInt32(leitor["ID"]),
                        Nome = Convert.ToString(leitor["NOME"]),
                        Telefone = Convert.ToString(leitor["TELEFONE"]),
                        Email = Convert.ToString(leitor["EMAIL"]),
                        Cidade = Convert.ToString(leitor["CIDADE"]),
                        UF = Convert.ToString(leitor["UF"])
                    };

                    fornecedores.Add(fornecedor);
                }

                return fornecedores;
            }
        }

        public Fornecedor SelecionarPorId(int id)
        {
            using (Conexao = new(StringConexao))
            {
                string query =
                    @"SELECT 
                            [ID],
                            [NOME],
                            [TELEFONE],
                            [EMAIL],
                            [CIDADE],
                            [UF]

                        FROM
                            [TBFornecedor]

                        WHERE [ID] = @ID";

                using SqlCommand comando = new(query, Conexao);

                comando.Parameters.AddWithValue("@ID", id);

                Conexao.Open();

                using SqlDataReader leitor = comando.ExecuteReader();

                Fornecedor fornecedor = null;

                if (leitor.Read())
                    fornecedor = new()
                    {
                        Id = Convert.ToInt32(leitor["ID"]),
                        Nome = Convert.ToString(leitor["NOME"]),
                        Telefone = Convert.ToString(leitor["TELEFONE"]),
                        Email = Convert.ToString(leitor["EMAIL"]),
                        Cidade = Convert.ToString(leitor["CIDADE"]),
                        UF = Convert.ToString(leitor["UF"])
                    };

                return fornecedor;
            }
        }

        public AbstractValidator<Fornecedor> ObterValidador()
        {
            return new ValidadorFornecedor();
        }
    }
}