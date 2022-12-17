using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace ProjetoCadastroMySQL
{
    public class MySQL
    {
        /// <summary>
        /// Servidor Local
        /// </summary>
        private string conecteString = ConfigurationManager.ConnectionStrings["DbAula"].ConnectionString.ToString();
        

        /// <summary>
        /// Variavel que carrega conexão
        /// </summary>
        public MySqlConnection conexao = null;
        


        /// <summary>
        /// Metodo de Abertura de conexao com MySQL
        /// </summary>
        public void AbrirConexao()
        {
            try
            {
                conexao = new MySqlConnection(conecteString);
                conexao.Open();
            }
            catch (Exception ex)
            {
                //if(ex.HResult == -2147467259)
                //    MessageBox.Show("Incapaz de conectar com algum dos servidores MySQL","Conexao - AbrirConexao");
                //else
                    MessageBox.Show("Número do Erro: "+ex.HResult+"\n\nErro no Servidor: "+ex.Message,"Servidor - Abrir Conexao");
            }
        }

        /// <summary>
        /// Metodo para fechar Conexao com MySQL
        /// </summary>
        public void FecharConexao()
        {
            try
            {
                conexao = new MySqlConnection(conecteString);
                conexao.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Número do Erro: " + ex.HResult + "\n\nErro no Servidor: " + ex.Message, "Servidor - Fechar Conexao");
            }
        }
    }
}
