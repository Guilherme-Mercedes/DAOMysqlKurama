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
            string query = "INSERT INTO funcionarios (nome, cpf, data_nascimento, usuario, senha) VALUES (@nome, @cpf, @nascimento, @usuario, @senha)";
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
        public bool AlterarFuncionario(string id, string nome, string cpf, string nascimento, string usuario, string senha)
        {
            string query = "UPDATE funcionarios SET nome = @nome, cpf = @cpf, data_nascimento = @nascimento, usuario = @usuario, senha = @senha WHERE id_funcionario = @id";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, mConn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
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
                throw new Exception("Erro ao alterar funcionário: " + ex.Message);
            }
        }

        public bool CadastrarPeriferico(string nome, string modelo, string marca, string garantia, string ano, decimal precoVenda, decimal precoAluguel, string status)
        {
            string query = @"INSERT INTO perifericos (nome, modelo, marca, garantia_venda, ano_fabricacao, preco_venda, preco_aluguel, status) 
                            VALUES (@nome, @modelo, @marca, @garantia, @ano, @venda, @aluguel, @status)";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, mConn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@modelo", modelo);
                    cmd.Parameters.AddWithValue("@marca", marca);
                    cmd.Parameters.AddWithValue("@garantia", garantia);
                    cmd.Parameters.AddWithValue("@ano", ano);
                    cmd.Parameters.AddWithValue("@venda", precoVenda);
                    cmd.Parameters.AddWithValue("@aluguel", precoAluguel);
                    cmd.Parameters.AddWithValue("@status", status);
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar o periférico: " + ex.Message);
            }
        }
        public bool AlterarPeriferico(string id, string nome, string modelo, string marca, string garantia, string ano, decimal venda, decimal aluguel) 
        {
            string query = @"UPDATE perifericos SET nome = @nome, modelo = @modelo, marca = @marca, garantia_venda = @garantia, ano_fabricacao = @ano, 
                            preco_venda = @venda, preco_aluguel = @aluguel WHERE id_periferico = @id";

            try
            {
                using(MySqlCommand cmd = new MySqlCommand(query, mConn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@modelo", modelo);
                    cmd.Parameters.AddWithValue("@marca", marca);
                    cmd.Parameters.AddWithValue("@garantia", garantia);
                    cmd.Parameters.AddWithValue("@ano", ano);
                    cmd.Parameters.AddWithValue("@venda", venda);
                    cmd.Parameters.AddWithValue("@aluguel", aluguel);
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao alterar o periférico: " + ex.Message);
            }
        }
        public bool AtualizarStatusPeriferico(string id, string status)
        {
            string query = "UPDATE perifericos SET status = @status WHERE id_periferico = @id";
            using (MySqlCommand cmd = new MySqlCommand(query, mConn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@status", status);
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public bool AlugarPeriferico(string nome, string cpf, string telefone, string dataNascimento, string diasAluguel, string valorTotal, string dataDevolucao, string horarioDiaAluguel, string id)
        {
            string query = @"INSERT INTO alugueis (nome_cliente, cpf_cliente, telefone_cliente, data_nascimento, dias_aluguel, valor_total, data_devolucao, data_hora_aluguel, id_periferico) 
                             VALUES (@nome, @cpf, @telefone, @data_nascimento, @dias_aluguel, @valor_total, @data_devolucao, @horario_e_dia_do_aluguel, @id_periferico)";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, mConn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@cpf", cpf);
                    cmd.Parameters.AddWithValue("@telefone", telefone);
                    cmd.Parameters.AddWithValue("@data_nascimento", dataNascimento);
                    cmd.Parameters.AddWithValue("@dias_aluguel", diasAluguel);
                    cmd.Parameters.AddWithValue("@valor_total", valorTotal);
                    cmd.Parameters.AddWithValue("@data_devolucao", dataDevolucao);
                    cmd.Parameters.AddWithValue("@data_hora_aluguel", horarioDiaAluguel);
                    cmd.Parameters.AddWithValue("@id_periferico", id);
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao alugar o periférico: " + ex.Message);
            }
        }

        public bool VenderPeriferico(string nome, string cpf, string telefone, string dataNascimento, decimal precoVenda, string dataVenda, string idPeriferico)
        {
            string query = @"INSERT INTO vendas (nome_cliente, cpf_cliente, telefone_cliente, data_nascimento, preco_venda, data_venda, id_periferico)
                    VALUES (@nome, @cpf, @telefone, @data_nascimento, @preco_venda, @data_venda, @id_periferico)";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, mConn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@cpf", cpf);
                    cmd.Parameters.AddWithValue("@telefone", telefone);
                    cmd.Parameters.AddWithValue("@data_nascimento", dataNascimento);
                    cmd.Parameters.AddWithValue("@preco_venda", precoVenda);
                    cmd.Parameters.AddWithValue("@data_venda", dataVenda);
                    cmd.Parameters.AddWithValue("@id_periferico", idPeriferico);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao registrar a venda: " + ex.Message);
            }
        }

        public bool FazerLogin(string usuario, string senha)
        {
            string query = "SELECT * FROM funcionarios WHERE usuario = @usuario AND senha = @senha";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, mConn))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@senha", senha);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        return reader.HasRows; // Retorna true se houver resultados
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao fazer login: " + ex.Message);
            }
        }
        public bool RemoverAluguelPorId(string id)
        {
            string query = "DELETE FROM alugueis WHERE id_aluguel = @id";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, mConn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao remover o aluguel: " + ex.Message);
            }
        }
        public bool RemoverFuncionarioPorId(string id)
        {
            string query = "DELETE FROM funcionarios WHERE id_funcionario = @id";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, mConn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao remover funcionário: " + ex.Message);
            }
        }
        public bool RemoverPerifericoPorId(string id)
        {
            string query = "DELETE FROM perifericos WHERE id_periferico = @id";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, mConn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao remover periférico: " + ex.Message);
            }
        }

        // Métodos de seleção para diferentes tabelas
        //para facilitar o uso, os nomes dos métodos seguem o padrão Select<Tabela>()
        public DataTable SelectLogin() => ExecutarSelect("SELECT * FROM funcionarios");

        public DataTable SelectPeriferico() => ExecutarSelect("SELECT * FROM perifericos");

        public DataTable SelectVendas() => ExecutarSelect("SELECT * FROM vendas");

        public DataTable SelectAlugueis() => ExecutarSelect("SELECT * FROM alugueis");

        public DataTable AlugueisUsuario() => ExecutarSelect("SELECT nome_cliente, dias_aluguel, data_devolução, valor_total, data_hora_aluguel, id_periferico FROM alugueis");

        public DataTable VendasUsuario() => ExecutarSelect("SELECT nome_cliente, preço_venda, data_venda, id_periferico FROM vendas");
    }


}
