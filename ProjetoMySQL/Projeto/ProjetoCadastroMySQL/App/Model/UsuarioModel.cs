using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace ProjetoCadastroMySQL.App.Model
{
    /// <summary>
    /// Classe de Consulta MySQL para usuario
    /// </summary>
    public class UsuarioModel:MySQL
    {
        string sql;
        MySqlCommand cmd;

        public UsuarioModel() { }

        /// <summary>
        /// Mostra todos os dados da tabela
        /// </summary>
        /// <returns>Todos os usuarios do banco de dados(DataTable)</returns>
        public DataTable GetAllRows()
        {
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                AbrirConexao();
                sql = "SELECT * FROM login ORDER BY id DESC";
                cmd = new MySqlCommand(sql, conexao);
                da.SelectCommand = cmd;
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Número do Erro: " + ex.HResult + "\n\nErro no GetAllRows: " + ex.Message, "Consulta com o Banco de Dados");
            }
            finally
            {
                FecharConexao();
            }

            return dt;
        } 

        /// <summary>
        /// Seleciona um usuario especifico do banco de dados
        /// </summary>
        /// <param name="nome"></param>
        /// <returns>os dados de determinado usuario(DataTable)</returns>
        public DataTable SearchByName(string nome)
        {
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                AbrirConexao();
                sql = "SELECT * FROM login WHERE nome LIKE @nome ORDER BY nome ASC";
                cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@nome", "%" + nome + "%");
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Número do Erro: " + ex.HResult + "\n\nErro no SearchByName: " + ex.Message, "Consulta com o Banco de Dados");
            }
            finally
            {
                FecharConexao();
            }

            return dt;
        }


        /// <summary>
        /// Adiciona novos registros ao banco de dados
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public bool Insert(string nome, string senha)
        {
            try
            {
                AbrirConexao();
                sql = "INSERT INTO login(nome,senha) VALUES(@nome,@senha)";
                cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@nome",nome);
                cmd.Parameters.AddWithValue("@senha",senha);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Usuário adicionado com sucesso!", "Cadastro de Usuário", MessageBoxButtons.OK,MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Número do Erro: " + ex.HResult + "\n\nErro no Insert: " + ex.Message, "Consulta com o Banco de Dados");
                return false;
            }
            finally
            {
                FecharConexao();
            }
        }

        /// <summary>
        /// Atualiza um registro existente 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nome"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public bool Update(int id,string nome, string senha)
        {
            if (VerifyPassWord(senha))
            {
                try
                {
                    AbrirConexao();
                    sql = "UPDATE login SET nome=@nome, senha=@senha WHERE id=@id";
                    cmd = new MySqlCommand(sql, conexao);
                    cmd.Parameters.AddWithValue("@id",id);
                    cmd.Parameters.AddWithValue("@nome",nome);
                    cmd.Parameters.AddWithValue("@senha",senha);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Usuário atualizado com sucesso!", "Cadastro de Usuário", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Número do Erro: " + ex.HResult + "\n\nErro no Update: " + ex.Message, "Consulta com o Banco de Dados");
                    return false;
                }
                finally
                {
                    FecharConexao();
                }
            }

            return false;
        }

        /// <summary>
        /// Apaga um usuario do Banco de Dados
        /// </summary>
        /// <param name="id"></param>
        public bool Delete(int id)
        {
            try
            {
                AbrirConexao();
                sql = "DELETE FROM login WHERE id=@id";
                cmd = new MySqlCommand(sql,conexao);
                cmd.Parameters.AddWithValue("@id",id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Usuário excluido com sucesso!", "Cadastro de Usuário", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Número do Erro: " + ex.HResult + "\n\nErro no Delete: " + ex.Message, "Consulta com o Banco de Dados");
                return false;
            }
            finally
            {
                FecharConexao();
            }
        }

        private bool VerifyPassWord(string senha)
        {
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                AbrirConexao();
                sql = "SELECT * FROM login WHERE senha=@senha";
                cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@senha", senha);
                da.SelectCommand = cmd;
                da.Fill(dt);

                if(dt.Rows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Você tem certeza que deseja alterar a senha desse usuário?", "Cadastro de Usuário",
                                                       MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Número do Erro: " + ex.HResult + "\n\nErro no SearchByName: " + ex.Message, "Consulta com o Banco de Dados");
                return false;
            }
            finally
            {
                FecharConexao();
            }
        }

    }
}
