using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace DAOMysql
{
    public class DAOMysql
    {
        public MySqlConnection mConn;
        public MySqlDataAdapter mAdapter;

        // Construtor da classe DAOMysql
        public DAOMysql()
        {
            //Atenção: Este projeto é apenas para fins educacionais. A senha e o usuário estão explícitos propositalmente
            //para facilitar testes e demonstração.
            mConn = new MySqlConnection("server=localhost;port=3306;database=BancoPeriferico;uid=root;password=1234;");
            try
            {
                //abre a conexao
                mConn.Open();
            }

            catch (System.Exception ex)
            {
                throw new Exception("Erro ao estabelecer uma conexao com o banco de dados: " + ex.Message);
            }
        }
        private DataTable ExecutarSelect(string query)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, mConn);
                mAdapter = new MySqlDataAdapter();
                mAdapter.SelectCommand = cmd;
                DataTable dt = new DataTable();
                mAdapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Metodos para usar o banco de dados
        public bool InserirFuncionario(string nome, string cpf, string nascimento, string usuario, string senha)
        {
            string query = "INSERT INTO loginfuncionario (nome, cpf, data_nascimento, usuario, senha) VALUES (@nome, @cpf, @nascimento, @usuario, @senha)";
            try
            {
                //evita vazamento de memória e conexões presas no banco.
                using (MySqlCommand cmd = new MySqlCommand(query, mConn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@cpf", cpf);
                    cmd.Parameters.AddWithValue("@nascimento", nascimento);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir funcionário: " + ex.Message);
            }
        }


        // Métodos de seleção para diferentes tabelas
        //para facilitar o uso, os nomes dos métodos seguem o padrão Select<Tabela>()
        public DataTable SelectLogin() => ExecutarSelect("SELECT * FROM loginfuncionario");

        public DataTable SelectPeriferico() => ExecutarSelect("SELECT * FROM perifericos");

        public DataTable SelectVendas() => ExecutarSelect("SELECT * FROM venda");

        public DataTable SelectAlugueis() => ExecutarSelect("SELECT * FROM aluguel");

        public DataTable AlugueisUsuario() => ExecutarSelect("SELECT nome, dias_aluguel, data_devolução, valor_total, horario_e_dia_do_aluguel, id_periferico FROM aluguel");

        public DataTable VendasUsuario() => ExecutarSelect("SELECT nome, preço_venda, dia_venda, id_periferico FROM venda");
    }


}
