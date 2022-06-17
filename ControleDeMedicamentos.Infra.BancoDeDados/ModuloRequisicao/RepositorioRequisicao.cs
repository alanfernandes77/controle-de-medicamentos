using ControleDeMedicamentos.Dominio.ModuloFornecedor;
using ControleDeMedicamentos.Dominio.ModuloFuncionario;
using ControleDeMedicamentos.Dominio.ModuloMedicamento;
using ControleDeMedicamentos.Dominio.ModuloPaciente;
using ControleDeMedicamentos.Dominio.ModuloRequisicao;
using ControleDeMedicamentos.Infra.BancoDeDados.Compartilhado;
using FluentValidation;
using FluentValidation.Results;
using System.Data.SqlClient;

namespace ControleDeMedicamentos.Infra.BancoDeDados.ModuloRequisicao
{
    public class RepositorioRequisicao : ConexaoSql, IRepositorio<Requisicao>
    {
        public ValidationResult Inserir(Requisicao requisicao)
        {
            ValidationResult resultadoValidacao = ObterValidador().Validate(requisicao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            using (Conexao = new(StringConexao))
            {
                string query =
                    @"INSERT INTO [TBREQUISICAO]
	                        (
		                        [FUNCIONARIO_ID],
		                        [PACIENTE_ID],
		                        [MEDICAMENTO_ID],
		                        [QUANTIDADEMEDICAMENTO],
		                        [DATA]
	                        )

	                        VALUES

	                        ( 
		                        @FUNCIONARIO_ID,
		                        @PACIENTE_ID,
		                        @MEDICAMENTO_ID,
		                        @QUANTIDADEMEDICAMENTO,
		                        @DATA

	                        )

	                        SELECT SCOPE_IDENTITY()";

                using SqlCommand comando = new(query, Conexao);

                comando.Parameters.AddWithValue("FUNCIONARIO_ID", requisicao.Funcionario.Id);
                comando.Parameters.AddWithValue("PACIENTE_ID", requisicao.Paciente.Id);
                comando.Parameters.AddWithValue("MEDICAMENTO_ID", requisicao.Medicamento.Id);
                comando.Parameters.AddWithValue("QUANTIDADEMEDICAMENTO", requisicao.QuantidadeMedicamento);
                comando.Parameters.AddWithValue("DATA", requisicao.DataRequisicao);

                Conexao.Open();

                requisicao.Id = Convert.ToInt32(comando.ExecuteScalar());

                return resultadoValidacao;
            }
        }

        public ValidationResult Editar(Requisicao requisicao)
        {
            ValidationResult resultadoValidacao = ObterValidador().Validate(requisicao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            using (Conexao = new(StringConexao))
            {
                string query =
                    @"UPDATE [TBRequisicao] 
	                        SET
		                        [Funcionario_Id] = @FUNCIONARIO_ID,
		                        [Paciente_Id] = @PACIENTE_ID,
		                        [Medicamento_Id] = @MEDICAMENTO_ID,
		                        [QuantidadeMedicamento] = @QUANTIDADEMEDICAMENTO,
		                        [DATA] = @DATA
	                        WHERE
		                        [ID] = @ID";

                using SqlCommand comando = new(query, Conexao);

                comando.Parameters.AddWithValue("@ID", requisicao.Id);
                comando.Parameters.AddWithValue("@FUNCIONARIO_ID", requisicao.Funcionario.Id);
                comando.Parameters.AddWithValue("@PACIENTE_ID", requisicao.Paciente.Id);
                comando.Parameters.AddWithValue("@MEDICAMENTO_ID", requisicao.Medicamento.Id);
                comando.Parameters.AddWithValue("@QUANTIDADEMEDICAMENTO", requisicao.QuantidadeMedicamento);
                comando.Parameters.AddWithValue("@DATA", requisicao.DataRequisicao);

                Conexao.Open();

                comando.ExecuteNonQuery();

                return resultadoValidacao;
            }
        }

        public ValidationResult Excluir(Requisicao requisicao)
        {
            using (Conexao = new(StringConexao))
            {
                string query = @"DELETE FROM [TBRequisicao] WHERE [ID] = @ID;";

                using SqlCommand comando = new(query, Conexao);

                comando.Parameters.AddWithValue("@ID", requisicao.Id);

                Conexao.Open();

                ValidationResult resultadoValidacao = ObterValidador().Validate(requisicao);

                if (resultadoValidacao.IsValid)
                    comando.ExecuteNonQuery();

                return resultadoValidacao;
            }
        }

        public List<Requisicao> SelecionarTodos()
        {
            using (Conexao = new(StringConexao))
            {
                string query =
                      @"SELECT 
	                        REQUISICAO.ID AS ID,
	                        REQUISICAO.QUANTIDADEMEDICAMENTO AS QUANTIDADEMEDICAMENTO,
	                        REQUISICAO.DATA AS DATAREQUISICAO,

	                        FUNCIONARIO.ID AS FUNCIONARIO_ID,
	                        FUNCIONARIO.NOME AS FUNCIONARIO_NOME,
	                        FUNCIONARIO.USUARIO AS FUNCIONARIO_USUARIO,
	                        FUNCIONARIO.SENHA AS FUNCIONARIO_SENHA,

	                        PACIENTE.ID AS PACIENTE_ID,
	                        PACIENTE.NOME AS PACIENTE_NOME,
	                        PACIENTE.CARTAOSUS AS PACIENTE_CARTAOSUS,

	                        MEDICAMENTO.ID AS MEDICAMENTO_ID,
	                        MEDICAMENTO.NOME AS MEDICAMENTO_NOME,
	                        MEDICAMENTO.DESCRICAO AS MEDICAMENTO_DESCRICAO,
	                        MEDICAMENTO.LOTE AS MEDICAMENTO_LOTE,
	                        MEDICAMENTO.VALIDADE AS MEDICAMENTO_VALIDADE,
	                        MEDICAMENTO.QUANTIDADEDISPONIVEL AS MEDICAMENTO_QUANTIDADEDISPONIVEL,

	                        FORNECEDOR.ID AS FORNECEDOR_ID,
	                        FORNECEDOR.NOME AS FORNECEDOR_NOME,
	                        FORNECEDOR.TELEFONE AS FORNECEDOR_TELEFONE,
	                        FORNECEDOR.EMAIL AS FORNECEDOR_EMAIL,
	                        FORNECEDOR.CIDADE AS FORNECEDOR_CIDADE,
	                        FORNECEDOR.UF AS FORNECEDOR_UF

                        FROM [TBREQUISICAO] AS REQUISICAO 

	                        INNER JOIN [TBFUNCIONARIO] AS FUNCIONARIO

                        ON 
	                        FUNCIONARIO.ID = REQUISICAO.FUNCIONARIO_ID

	                        INNER JOIN [TBPACIENTE] AS PACIENTE

                        ON 
	                        PACIENTE.ID = REQUISICAO.PACIENTE_ID

	                        INNER JOIN [TBMEDICAMENTO] AS MEDICAMENTO

                        ON 
	                        MEDICAMENTO.ID = REQUISICAO.MEDICAMENTO_ID

	                        INNER JOIN [TBFORNECEDOR] AS FORNECEDOR

                        ON
	                        FORNECEDOR.ID = MEDICAMENTO.FORNECEDOR_ID";

                using SqlCommand comando = new(query, Conexao);

                Conexao.Open();

                using SqlDataReader leitor = comando.ExecuteReader();

                List<Requisicao> requisicoes = new();

                while (leitor.Read())
                {
                    Funcionario funcionario = new()
                    {
                        Id = Convert.ToInt32(leitor["FUNCIONARIO_ID"]),
                        Nome = Convert.ToString(leitor["FUNCIONARIO_NOME"]),
                        Usuario = Convert.ToString(leitor["FUNCIONARIO_USUARIO"]),
                        Senha = Convert.ToString(leitor["FUNCIONARIO_SENHA"])
                    };

                    Paciente paciente = new()
                    {
                        Id = Convert.ToInt32(leitor["PACIENTE_ID"]),
                        Nome = Convert.ToString(leitor["PACIENTE_NOME"]),
                        CartaoSUS = Convert.ToString(leitor["PACIENTE_CARTAOSUS"])
                    };

                    Fornecedor fornecedor = new()
                    {
                        Id = Convert.ToInt32(leitor["FORNECEDOR_ID"]),
                        Nome = Convert.ToString(leitor["FORNECEDOR_NOME"]),
                        Telefone = Convert.ToString(leitor["FORNECEDOR_TELEFONE"]),
                        Email = Convert.ToString(leitor["FORNECEDOR_EMAIL"]),
                        Cidade = Convert.ToString(leitor["FORNECEDOR_CIDADE"]),
                        UF = Convert.ToString(leitor["FORNECEDOR_UF"])
                    };

                    Medicamento medicamento = new()
                    {
                        Id = Convert.ToInt32(leitor["MEDICAMENTO_ID"]),
                        Nome = Convert.ToString(leitor["MEDICAMENTO_NOME"]),
                        Descricao = Convert.ToString(leitor["MEDICAMENTO_DESCRICAO"]),
                        Lote = Convert.ToString(leitor["MEDICAMENTO_LOTE"]),
                        Validade = Convert.ToDateTime(leitor["MEDICAMENTO_VALIDADE"]),
                        QuantidadeDisponivel = Convert.ToInt32(leitor["MEDICAMENTO_QUANTIDADEDISPONIVEL"]),
                        Fornecedor = fornecedor
                    };

                    Requisicao requisicao = new()
                    {
                        Id = Convert.ToInt32(leitor["ID"]),
                        Funcionario = funcionario,
                        Paciente = paciente,
                        Medicamento = medicamento,
                        QuantidadeMedicamento = Convert.ToInt32(leitor["QUANTIDADEMEDICAMENTO"]),
                        DataRequisicao = Convert.ToDateTime(leitor["DATAREQUISICAO"])
                    };

                    requisicoes.Add(requisicao);
                }

                return requisicoes;
            }
        }

        public Requisicao SelecionarPorId(int id)
        {
            using (Conexao = new(StringConexao))
            {
                string query =
                    @"SELECT 
	                        REQUISICAO.ID AS ID,
	                        REQUISICAO.QUANTIDADEMEDICAMENTO AS QUANTIDADEMEDICAMENTO,
	                        REQUISICAO.DATA AS DATAREQUISICAO,

	                        FUNCIONARIO.ID AS FUNCIONARIO_ID,
	                        FUNCIONARIO.NOME AS FUNCIONARIO_NOME,
	                        FUNCIONARIO.USUARIO AS FUNCIONARIO_USUARIO,
	                        FUNCIONARIO.SENHA AS FUNCIONARIO_SENHA,

	                        PACIENTE.ID AS PACIENTE_ID,
	                        PACIENTE.NOME AS PACIENTE_NOME,
	                        PACIENTE.CARTAOSUS AS PACIENTE_CARTAOSUS,

	                        MEDICAMENTO.ID AS MEDICAMENTO_ID,
	                        MEDICAMENTO.NOME AS MEDICAMENTO_NOME,
	                        MEDICAMENTO.DESCRICAO AS MEDICAMENTO_DESCRICAO,
	                        MEDICAMENTO.LOTE AS MEDICAMENTO_LOTE,
	                        MEDICAMENTO.VALIDADE AS MEDICAMENTO_VALIDADE,
	                        MEDICAMENTO.QUANTIDADEDISPONIVEL AS MEDICAMENTO_QUANTIDADEDISPONIVEL,

	                        FORNECEDOR.ID AS FORNECEDOR_ID,
	                        FORNECEDOR.NOME AS FORNECEDOR_NOME,
	                        FORNECEDOR.TELEFONE AS FORNECEDOR_TELEFONE,
	                        FORNECEDOR.EMAIL AS FORNECEDOR_EMAIL,
	                        FORNECEDOR.CIDADE AS FORNECEDOR_CIDADE,
	                        FORNECEDOR.UF AS FORNECEDOR_UF

                        FROM [TBREQUISICAO] AS REQUISICAO 

	                        INNER JOIN [TBFUNCIONARIO] AS FUNCIONARIO

                        ON 
	                        FUNCIONARIO.ID = REQUISICAO.FUNCIONARIO_ID

	                        INNER JOIN [TBPACIENTE] AS PACIENTE

                        ON 
	                        PACIENTE.ID = REQUISICAO.PACIENTE_ID

	                        INNER JOIN [TBMEDICAMENTO] AS MEDICAMENTO

                        ON 
	                        MEDICAMENTO.ID = REQUISICAO.MEDICAMENTO_ID

	                        INNER JOIN [TBFORNECEDOR] AS FORNECEDOR

                        ON
	                        FORNECEDOR.ID = MEDICAMENTO.FORNECEDOR_ID

                        WHERE REQUISICAO.ID = @ID";

                using SqlCommand comando = new(query, Conexao);

                comando.Parameters.AddWithValue("@ID", id);

                Conexao.Open();

                using SqlDataReader leitor = comando.ExecuteReader();

                Requisicao requisicao = null;

                if (leitor.Read())
                {
                    Funcionario funcionario = new()
                    {
                        Id = Convert.ToInt32(leitor["FUNCIONARIO_ID"]),
                        Nome = Convert.ToString(leitor["FUNCIONARIO_NOME"]),
                        Usuario = Convert.ToString(leitor["FUNCIONARIO_USUARIO"]),
                        Senha = Convert.ToString(leitor["FUNCIONARIO_SENHA"])
                    };

                    Paciente paciente = new()
                    {
                        Id = Convert.ToInt32(leitor["PACIENTE_ID"]),
                        Nome = Convert.ToString(leitor["PACIENTE_NOME"]),
                        CartaoSUS = Convert.ToString(leitor["PACIENTE_CARTAOSUS"])
                    };

                    Fornecedor fornecedor = new()
                    {
                        Id = Convert.ToInt32(leitor["FORNECEDOR_ID"]),
                        Nome = Convert.ToString(leitor["FORNECEDOR_NOME"]),
                        Telefone = Convert.ToString(leitor["FORNECEDOR_TELEFONE"]),
                        Email = Convert.ToString(leitor["FORNECEDOR_EMAIL"]),
                        Cidade = Convert.ToString(leitor["FORNECEDOR_CIDADE"]),
                        UF = Convert.ToString(leitor["FORNECEDOR_UF"])
                    };

                    Medicamento medicamento = new()
                    {
                        Id = Convert.ToInt32(leitor["MEDICAMENTO_ID"]),
                        Nome = Convert.ToString(leitor["MEDICAMENTO_NOME"]),
                        Descricao = Convert.ToString(leitor["MEDICAMENTO_DESCRICAO"]),
                        Lote = Convert.ToString(leitor["MEDICAMENTO_LOTE"]),
                        Validade = Convert.ToDateTime(leitor["MEDICAMENTO_VALIDADE"]),
                        QuantidadeDisponivel = Convert.ToInt32(leitor["MEDICAMENTO_QUANTIDADEDISPONIVEL"]),
                        Fornecedor = fornecedor
                    };

                    requisicao = new()
                    {
                        Id = Convert.ToInt32(leitor["ID"]),
                        Funcionario = funcionario,
                        Paciente = paciente,
                        Medicamento = medicamento,
                        QuantidadeMedicamento = Convert.ToInt32(leitor["QUANTIDADEMEDICAMENTO"]),
                        DataRequisicao = Convert.ToDateTime(leitor["DATAREQUISICAO"])
                    };
                }

                return requisicao;
            }
        }

        public AbstractValidator<Requisicao> ObterValidador()
        {
            return new ValidadorRequisicao();
        }
    }
}