using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace ProjetoCadastroMySQL.App.Model
{
    /// <summary>
    /// Consulta com MySQL Tabela Login
    /// </summary>
    public class LoginModel:MySQL
    {
        string sql;
        MySqlCommand cmd;

        public LoginModel() { }

        public bool Login(string nome,string senha)
        {
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                AbrirConexao();

                sql = "SELECT * FROM login WHERE nome=@nome AND senha=@senha";
                cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@nome",nome);
                cmd.Parameters.AddWithValue("@senha",senha);
                da.SelectCommand = cmd;
                da.Fill(dt);

                return dt.Rows.Count > 0 ? true : false;     
            }
            catch (Exception ex)
            {
                MessageBox.Show("Número do Erro: " + ex.HResult + "\n\nErro no Login: " + ex.Message, "Consulta com o Banco de Dados");
                return false;
            }
            finally
            {
                FecharConexao();
            }



        }

        
    }
}
