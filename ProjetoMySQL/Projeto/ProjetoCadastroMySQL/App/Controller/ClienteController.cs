using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.ComponentModel;

namespace ProjetoCadastroMySQL
{ 
    public class ClienteController
    {
        /// <summary>
        /// Adicionar dados no banco de dados(insert)
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="endereco"></param>
        /// <param name="cpf"></param>
        /// <param name="telefone"></param>
        /// <param name="imagem"></param>
        /// <returns></returns>
        public static bool Salvar(string nome,string endereco, string cpf, string telefone, string imagem)
        {
            var clienteModel = new ClienteModel();
            
            return clienteModel.Insert(nome, endereco, cpf, telefone, Img(imagem));

        }


        /// <summary>
        /// Editar dados no banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nome"></param>
        /// <param name="endereco"></param>
        /// <param name="cpf"></param>
        /// <param name="telefone"></param>
        /// <param name="imagem"></param>
        /// <param name="alterar"></param>
        /// <returns></returns>
        public static bool Editar(int id,string nome, string endereco, string cpf, string telefone,string imagem,bool alterar=false)
        {
            var clienteModel = new ClienteModel();

            if (id > 0)
            {
                if (alterar == true)
                {
                    return clienteModel.Update(id, nome, endereco, cpf, telefone, Img(imagem));
                }
                else
                {
                    return clienteModel.Update(id, nome, endereco, cpf, telefone);
                }
            }
            else
            {
                MessageBox.Show("Esse registro não existe", "Cadastro de Cliente - Atualizar",
                                 MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        /// <summary>
        /// Excluir dados da Tabela Cliente
        /// </summary>
        /// <param name="id"></param>
        public static bool Excluir(int id)
        {
            if(id > 0)
            {
                var clienteModel = new ClienteModel();

                DialogResult result = MessageBox.Show("Você tem certeza que deseja excluir esse registro?", "Cadastro de Cliente - Excluir",
                                                       MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    clienteModel.Delete(id);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }


        /// <summary>
        /// Metodo para enviar imagem para o banco de dados(Controller)
        /// </summary>
        /// <returns></returns>
        private static byte[] Img(string foto)
        {
            byte[] imagem_byte = null;

            if (foto == "")
            {
                return null;
            }

            FileStream fs = new FileStream(foto, FileMode.Open, FileAccess.Read);

            BinaryReader br = new BinaryReader(fs);

            imagem_byte = br.ReadBytes((int)fs.Length);

            return imagem_byte;

        }

        /// <summary>
        /// Mostra dados ESPECIFICO com base no Nome na tabela cliente
        /// </summary>
        /// <param name="Grid"></param>
        /// <param name="nome"></param>
        /// <returns></returns>
        public static DataGridView Procurar(DataGridView Grid,string nome)
        {
            Grid.DataSource = new ClienteModel().SearchByName(nome);

            //Id
            Grid.Columns[0].HeaderText = "Código";
            Grid.Columns[0].Width = 80;
            Grid.Columns[0].Visible = false;

            //Nome
            Grid.Columns[1].HeaderText = "Nome";
            Grid.Columns[1].Width = 150;

            //Endereco
            Grid.Columns[2].HeaderText = "Endereço";
            Grid.Columns[2].Width = 230;

            //CPF
            Grid.Columns[3].HeaderText = "CPF";
            Grid.Columns[3].Width = 150;

            //Telefone
            Grid.Columns[4].HeaderText = "Telefone";
            Grid.Columns[4].Width = 150;

            //Imagem
            Grid.Columns[5].HeaderText = "Imagem";
            Grid.Columns[5].Visible = false;

            return Grid;
        }

        /// <summary>
        /// Mostra TODOS os dados da tabela cliente
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        public static DataGridView CarregarTabelaCliente(DataGridView dataGridView)
        {
            dataGridView.DataSource= new ClienteModel().GetAllRows();

            //Id
            dataGridView.Columns[0].HeaderText = "Código";
            dataGridView.Columns[0].Width = 80;
            dataGridView.Columns[0].Visible = false;

            //Nome
            dataGridView.Columns[1].HeaderText = "Nome";
            dataGridView.Columns[1].Width = 150;

            //Endereco
            dataGridView.Columns[2].HeaderText = "Endereço";
            dataGridView.Columns[2].Width = 210;

            //CPF
            dataGridView.Columns[3].HeaderText = "CPF";
            dataGridView.Columns[3].Width = 150;

            //Telefone
            dataGridView.Columns[4].HeaderText = "Telefone";
            dataGridView.Columns[4].Width = 120;

            //Imagem
            dataGridView.Columns[5].HeaderText = "Imagem";
            dataGridView.Columns[5].Visible = false;

            return dataGridView;
        }
    }//Classe

}//NameSpace
