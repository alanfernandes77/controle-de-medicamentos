using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using ControleDeMedicamentos.Infra.BancoDeDados.Compartilhado;
using FluentValidation;
using FluentValidation.Results;
using System.Data.SqlClient;

namespace ControleDeMedicamentos.Infra.BancoDeDados.ModuloMedicamento
{
    public class RepositorioMedicamento : ConexaoSql, IRepositorio<Medicamento>
    {
        public ValidationResult Inserir(Medicamento medicamento)
        {
            ValidationResult resultadoValidacao = ObterValidador().Validate(medicamento);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            using (Conexao = new(StringConexao))
            {
                string query =
                    @"INSERT INTO [TBMedicamento] 
                        (
                            [NOME],
                            [DESCRICAO],
                            [LOTE],
                            [VALIDADE],
                            [QUANTIDADEDISPONIVEL],
                            [FORNECEDOR_ID]
                        )

                        VALUES 

                        (
                            @NOME,
                            @DESCRICAO,
                            @LOTE,
                            @VALIDADE,
                            @QUANTIDADEDISPONIVEL,
                            @FORNECEDOR_ID
                        )

                        SELECT SCOPE_IDENTITY()";

                using SqlCommand comando = new(query, Conexao);

                comando.Parameters.AddWithValue("@ID", medicamento.Id);
                comando.Parameters.AddWithValue("@NOME", medicamento.Nome);
                comando.Parameters.AddWithValue("@DESCRICAO", medicamento.Descricao);
                comando.Parameters.AddWithValue("@LOTE", medicamento.Lote);
                comando.Parameters.AddWithValue("@VALIDADE", medicamento.Validade);
                comando.Parameters.AddWithValue("@QUANTIDADEDISPONIVEL", medicamento.QuantidadeDisponivel);
                comando.Parameters.AddWithValue("@FORNECEDOR_ID", medicamento.Fornecedor.Id);

                Conexao.Open();

                medicamento.Id = Convert.ToInt32(comando.ExecuteScalar());

                return resultadoValidacao;
            }
        }

        public ValidationResult Editar(Medicamento medicamento)
        {
            ValidationResult resultadoValidacao = ObterValidador().Validate(medicamento);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            using (Conexao = new(StringConexao))
            {
                string query =
                    @"UPDATE [TBMedicamento] 
                        SET

                            [NOME] = @NOME,
                            [DESCRICAO] = @DESCRICAO,
                            [LOTE] = @LOTE,
                            [VALIDADE] = @VALIDADE,
                            [QUANTIDADEDISPONIVEL] = @QUANTIDADEDISPONIVEL,
                            [FORNECEDOR_ID] = @FORNECEDOR_ID

                        WHERE
                            
                            [ID] = @ID;";

                using SqlCommand comando = new(query, Conexao);

                comando.Parameters.AddWithValue("@ID", medicamento.Id);
                comando.Parameters.AddWithValue("@NOME", medicamento.Nome);
                comando.Parameters.AddWithValue("@DESCRICAO", medicamento.Descricao);
                comando.Parameters.AddWithValue("@LOTE", medicamento.Lote);
                comando.Parameters.AddWithValue("@VALIDADE", medicamento.Validade);
                comando.Parameters.AddWithValue("@QUANTIDADEDISPONIVEL", medicamento.QuantidadeDisponivel);
                comando.Parameters.AddWithValue("@FORNECEDOR_ID", medicamento.Fornecedor.Id);

                Conexao.Open();

                comando.ExecuteNonQuery();

                return resultadoValidacao;
            }
        }

        public ValidationResult Excluir(Medicamento medicamento)
        {
            using (Conexao = new(StringConexao))
            {
                string query = @"DELETE FROM [TBMedicamento] WHERE [ID] = @ID;";

                using SqlCommand comando = new(query, Conexao);

                comando.Parameters.AddWithValue("@ID", medicamento.Id);

                Conexao.Open();

                ValidationResult resultadoValidacao = ObterValidador().Validate(medicamento);

                if (resultadoValidacao.IsValid)
                    comando.ExecuteNonQuery();

                return resultadoValidacao;
            }
        }

        public List<Medicamento> SelecionarTodos()
        {
            using (Conexao = new(StringConexao))
            {
                string query =
                    @"SELECT 
	                        MEDICAMENTO.ID,
	                        MEDICAMENTO.NOME,
	                        MEDICAMENTO.DESCRICAO,
	                        MEDICAMENTO.LOTE,
	                        MEDICAMENTO.VALIDADE,
	                        MEDICAMENTO.QUANTIDADEDISPONIVEL,
	                        MEDICAMENTO.FORNECEDOR_ID,

	                        FORNECEDOR.NOME AS FORNECEDOR_NOME,
	                        FORNECEDOR.TELEFONE AS FORNECEDOR_TELEFONE,
	                        FORNECEDOR.EMAIL AS FORNECEDOR_EMAIL,
	                        FORNECEDOR.CIDADE AS FORNECEDOR_CIDADE,
	                        FORNECEDOR.UF AS FORNECEDOR_UF

                        FROM 

                        [TBMEDICAMENTO] AS MEDICAMENTO 

                        INNER JOIN

                        [TBFORNECEDOR] AS FORNECEDOR 

                        ON MEDICAMENTO.FORNECEDOR_ID = FORNECEDOR.ID";

                using SqlCommand comando = new(query, Conexao);

                Conexao.Open();

                using SqlDataReader leitor = comando.ExecuteReader();

                List<Medicamento> medicamentos = new();

                while (leitor.Read())
                {
                    Medicamento medicamento = new()
                    {
                        Id = Convert.ToInt32(leitor["ID"]),
                        Nome = Convert.ToString(leitor["NOME"]),
                        Descricao = Convert.ToString(leitor["DESCRICAO"]),
                        Lote = Convert.ToString(leitor["LOTE"]),
                        Validade = Convert.ToDateTime(leitor["VALIDADE"]),
                        QuantidadeDisponivel = Convert.ToInt32(leitor["QUANTIDADEDISPONIVEL"]),
                        Fornecedor = new()
                        {
                            Id = Convert.ToInt32(leitor["FORNECEDOR_ID"]),
                            Nome = Convert.ToString(leitor["FORNECEDOR_NOME"]),
                            Telefone = Convert.ToString(leitor["FORNECEDOR_TELEFONE"]),
                            Email = Convert.ToString(leitor["FORNECEDOR_EMAIL"]),
                            Cidade = Convert.ToString(leitor["FORNECEDOR_CIDADE"]),
                            UF = Convert.ToString(leitor["FORNECEDOR_UF"])
                        }
                    };

                    medicamentos.Add(medicamento);
                }

                return medicamentos;
            }
        }

        public Medicamento SelecionarPorId(int id)
        {
            using (Conexao = new(StringConexao))
            {
                string query =
                    @"SELECT 
	                        MEDICAMENTO.ID,
	                        MEDICAMENTO.NOME,
	                        MEDICAMENTO.DESCRICAO,
	                        MEDICAMENTO.LOTE,
	                        MEDICAMENTO.VALIDADE,
	                        MEDICAMENTO.QUANTIDADEDISPONIVEL,

                            FORNECEDOR.ID AS FORNECEDOR_ID,
	                        FORNECEDOR.NOME AS FORNECEDOR_NOME,
	                        FORNECEDOR.TELEFONE AS FORNECEDOR_TELEFONE,
	                        FORNECEDOR.EMAIL AS FORNECEDOR_EMAIL,
	                        FORNECEDOR.CIDADE AS FORNECEDOR_CIDADE,
	                        FORNECEDOR.UF AS FORNECEDOR_UF

                        FROM 

                        [TBMEDICAMENTO] AS MEDICAMENTO 

                        INNER JOIN

                        [TBFORNECEDOR] AS FORNECEDOR 

                        ON MEDICAMENTO.FORNECEDOR_ID = FORNECEDOR.ID

                        WHERE MEDICAMENTO.ID = @ID";

                using SqlCommand comando = new(query, Conexao);

                comando.Parameters.AddWithValue("@ID", id);

                Conexao.Open();

                using SqlDataReader leitor = comando.ExecuteReader();

                Medicamento medicamento = null;

                if (leitor.Read())
                    medicamento = new()
                    {
                        Id = Convert.ToInt32(leitor["ID"]),
                        Nome = Convert.ToString(leitor["NOME"]),
                        Descricao = Convert.ToString(leitor["DESCRICAO"]),
                        Lote = Convert.ToString(leitor["LOTE"]),
                        Validade = Convert.ToDateTime(leitor["VALIDADE"]),
                        QuantidadeDisponivel = Convert.ToInt32(leitor["QUANTIDADEDISPONIVEL"]),
                        Fornecedor = new()
                        {
                            Id = Convert.ToInt32(leitor["FORNECEDOR_ID"]),
                            Nome = Convert.ToString(leitor["FORNECEDOR_NOME"]),
                            Telefone = Convert.ToString(leitor["FORNECEDOR_TELEFONE"]),
                            Email = Convert.ToString(leitor["FORNECEDOR_EMAIL"]),
                            Cidade = Convert.ToString(leitor["FORNECEDOR_CIDADE"]),
                            UF = Convert.ToString(leitor["FORNECEDOR_UF"])
                        }
                    };

                return medicamento;
            }
        }

        public AbstractValidator<Medicamento> ObterValidador()
        {
            return new ValidadorMedicamento();
        }

        public List<Medicamento> SelecionarMedicamentosComBaixoEstoque()
        {
            using (Conexao = new(StringConexao))
            {
                string query =
                    @"SELECT 
	                        MEDICAMENTO.ID,
	                        MEDICAMENTO.NOME,
	                        MEDICAMENTO.DESCRICAO,
	                        MEDICAMENTO.LOTE,
	                        MEDICAMENTO.VALIDADE,
	                        MEDICAMENTO.QUANTIDADEDISPONIVEL,
	                        MEDICAMENTO.FORNECEDOR_ID,

	                        FORNECEDOR.NOME AS FORNECEDOR_NOME,
	                        FORNECEDOR.TELEFONE AS FORNECEDOR_TELEFONE,
	                        FORNECEDOR.EMAIL AS FORNECEDOR_EMAIL,
	                        FORNECEDOR.CIDADE AS FORNECEDOR_CIDADE,
	                        FORNECEDOR.UF AS FORNECEDOR_UF

                        FROM 

                        [TBMEDICAMENTO] AS MEDICAMENTO 

                        INNER JOIN

                        [TBFORNECEDOR] AS FORNECEDOR 

                        ON MEDICAMENTO.FORNECEDOR_ID = FORNECEDOR.ID

                        WHERE MEDICAMENTO.QuantidadeDisponivel < 10";

                using SqlCommand comando = new(query, Conexao);

                Conexao.Open();

                SqlDataReader leitor = comando.ExecuteReader();

                List<Medicamento> medicamentos = new();

                while (leitor.Read())
                {
                    Medicamento medicamento = new()
                    {
                        Id = Convert.ToInt32(leitor["ID"]),
                        Nome = Convert.ToString(leitor["NOME"]),
                        Descricao = Convert.ToString(leitor["DESCRICAO"]),
                        Lote = Convert.ToString(leitor["LOTE"]),
                        Validade = Convert.ToDateTime(leitor["VALIDADE"]),
                        QuantidadeDisponivel = Convert.ToInt32(leitor["QUANTIDADEDISPONIVEL"]),
                        Fornecedor = new()
                        {
                            Id = Convert.ToInt32(leitor["FORNECEDOR_ID"]),
                            Nome = Convert.ToString(leitor["FORNECEDOR_NOME"]),
                            Telefone = Convert.ToString(leitor["FORNECEDOR_TELEFONE"]),
                            Email = Convert.ToString(leitor["FORNECEDOR_EMAIL"]),
                            Cidade = Convert.ToString(leitor["FORNECEDOR_CIDADE"]),
                            UF = Convert.ToString(leitor["FORNECEDOR_UF"])
                        }
                    };

                    medicamentos.Add(medicamento);
                }

                return medicamentos;
            }
        }
    }
}