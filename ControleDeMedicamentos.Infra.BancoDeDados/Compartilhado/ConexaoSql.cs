using System.Data.SqlClient;

namespace ControleDeMedicamentos.Infra.BancoDeDados.Compartilhado
{
    public class ConexaoSql
    {
        protected string StringConexao = @"
            Data Source=(LocalDB)\MSSqlLocalDB;
            Initial Catalog=DBControleDeMedicamentos;   
            Integrated Security=True;
            Pooling=False";

        protected SqlConnection? Conexao = null;
    }
}