using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;

namespace ProjetoCadastroMySQL
{
    /// <summary>
    /// Consulta com MySQL Tabela Cliente
    /// </summary>
    public class ClienteModel:MySQL
    {
        string sql;
        MySqlCommand cmd;

        /// <summary>
        /// Objeto Construtor de CRUD cliente
        /// </summary>
        /// <remarks>Create (INSERT)</remarks>
        /// <remarks>Read (SELECT)</remarks>
        /// <remarks>Update (UPDATE)</remarks>
        /// <remarks>Create (DELETE)</remarks>
        public ClienteModel() { }

        /// <summary>
        /// Metodo que pesquisa por um dado especifico com base no nome
        /// </summary>
        /// <param name="nome"></param>
        /// <returns>DataTable</returns>
        public DataTable SearchByName(string nome)
        {
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                AbrirConexao();
                sql = "SELECT * FROM cliente WHERE nome LIKE @nome ORDER BY nome ASC";
                cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@nome","%"+nome+"%");
                da.SelectCommand = cmd;
                da.Fill(dt);
                //return dt;
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
        /// Metodo de Busca os TODOS os dados da tabela
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetAllRows()
        {
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable dt = new DataTable();

            try
            { 
                AbrirConexao();
                sql = "SELECT * FROM cliente ORDER BY id DESC";
                cmd = new MySqlCommand(sql, conexao);
                da.SelectCommand = cmd;
                da.Fill(dt);
                //return dt;
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
        /// Metodo de inserção de dados na Tabela cliente
        /// </summary>
        /// <remarks>Create (INSERT)</remarks>
        /// <param name="nome"></param>
        /// <param name="endereco"></param>
        /// <param name="cpf"></param>
        /// <param name="telefone"></param>
        public bool Insert(string nome, string endereco, string cpf, string telefone, byte[] imagem)
        {
            try
            {
                AbrirConexao();

                sql = "INSERT INTO cliente(nome,endereco,cpf,telefone,imagem) VALUES (@nome,@endereco,@cpf,@telefone,@imagem)";
                cmd = new MySqlCommand(sql,conexao);
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@cpf", cpf);
                cmd.Parameters.AddWithValue("@telefone", telefone);
                cmd.Parameters.AddWithValue("@endereco", endereco);
                cmd.Parameters.AddWithValue("@imagem", imagem);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registro de cliente adicionado com sucesso!", "Cadastro de Cliente",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2147467259)
                {
                    MessageBox.Show("Esse CPF já está cadastrado", "Consulta com o Banco de Dados", 
                                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                MessageBox.Show("Número do Erro: " + ex.HResult + "\n\nErro no Insert: " + ex.Message, "Consulta com o Banco de Dados");
                return false;
            }
            finally
            {
                FecharConexao();
            }
        }

        /// <summary>
        /// Metodo de atualização de dados na Tabela cliente
        /// </summary>
        /// <remarks>Update (UPDATE)</remarks>
        /// <param name="nome"></param>
        /// <param name="endereco"></param>
        /// <param name="cpf"></param>
        /// <param name="telefone"></param>
        /// <param name="id"></param>
        /// <param name="imagem"></param>
        public bool Update(int id,string nome, string endereco, string cpf, string telefone,byte[] imagem)
        {
            try
            {
                AbrirConexao();
                sql = "UPDATE cliente SET nome=@nome, endereco=@endereco, cpf=@cpf, telefone=@telefone, imagem=@imagem WHERE id=@id";
                cmd = new MySqlCommand(sql,conexao);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@endereco", endereco);
                cmd.Parameters.AddWithValue("@cpf", cpf);
                cmd.Parameters.AddWithValue("@telefone", telefone);
                cmd.Parameters.AddWithValue("@imagem", imagem);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Registro de cliente atualizadado com sucesso!", "Cadastro de Cliente",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2147467259)
                {
                    MessageBox.Show("Esse CPF já está cadastrado", "Consulta com o Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                    MessageBox.Show("Número do Erro: " + ex.HResult + "\n\nErro no Update: " + ex.Message, "Consulta com o Banco de Dados");
                return false;
            }
            finally
            {
                FecharConexao();
            }
        }       
        
        /// <summary>
        /// Metodo de atualização de dados na Tabela cliente
        /// </summary>
        /// <remarks>Update (UPDATE)</remarks>
        /// <param name="nome"></param>
        /// <param name="endereco"></param>
        /// <param name="cpf"></param>
        /// <param name="telefone"></param>
        /// <param name="id"></param>
        public bool Update(int id,string nome, string endereco, string cpf, string telefone)
        {
            try
            {
                AbrirConexao();
                sql = "UPDATE cliente SET nome=@nome, endereco=@endereco, cpf=@cpf, telefone=@telefone WHERE id=@id";
                cmd = new MySqlCommand(sql,conexao);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@endereco", endereco);
                cmd.Parameters.AddWithValue("@cpf", cpf);
                cmd.Parameters.AddWithValue("@telefone", telefone);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registro de cliente atualizadado com sucesso!", "Cadastro de Cliente",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;

            }
            catch (Exception ex)
            {
                if(ex.HResult == -2147467259)
                {
                    MessageBox.Show("Esse CPF já está cadastrado", "Consulta com o Banco de Dados",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return false;
                }
                else
                    MessageBox.Show("Número do Erro: " + ex.HResult + "\n\nErro no Update: " + ex.Message, "Consulta com o Banco de Dados");
                    return false;
            }
            finally
            {
                FecharConexao();
            }
        }

        /// <summary>
        /// Metodo de exlusão de dados na Tabela cliente
        /// </summary>
        /// <remarks>Delete (Delete)</remarks>
        /// <param name="id"></param>
        public bool Delete(int id)
        {
            try
            {
                AbrirConexao();
                sql = "DELETE FROM cliente WHERE id=@id";
                cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registro de cliente deletado com sucesso!", "Cadastro de Cliente",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        
    }
}
