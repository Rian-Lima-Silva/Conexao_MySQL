using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace ProjetoCadastroMySQL
{
    public partial class FrmCadastroCliente : Form
    {

        /// <summary>
        /// Id Selecionado
        /// </summary>
        private int id = 0;

        /// <summary>
        /// Caminha da imagem
        /// </summary>
        private string foto;

        /// <summary>
        /// Verifica-se a imagem é alterada
        /// </summary>
        private bool alterarFoto;

        public string[] txt= {"Opcao 1","Opcao 2","Opcao 3","Opcao 4"};
  

        public FrmCadastroCliente()
        {
            InitializeComponent();

        }

        private void FrmCadastro_Load(object sender, EventArgs e)
        {
           LimparFoto();
           ClienteController.CarregarTabelaCliente(Grid);
            //for (int i = 0; i < txt.Length; i++)
            //{
            //    listView1.Items.Add(new ListViewItem(txt[i]));
            //}
            //ClienteController.CarragarDataBases(listView1);

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparFoto();
            LimparForm();
            txtNome.Focus();
            Habilitar_Button(false, true, false, false, true, true);
            Habilitar_TextBox(true);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (ValidarDados())
            {
               if (ClienteController.Salvar(txtNome.Text, txtEndereco.Text, txtCPF.Text, txtTelefone.Text,this.foto))
               {
                   LimparForm();
                   LimparFoto();
                   Habilitar_TextBox(true);
                   Habilitar_Button(true,false,true,true,true,false);
               }
            }

           ClienteController.CarregarTabelaCliente(Grid);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (ValidarDados())
            {
                if (ClienteController.Editar(this.id, txtNome.Text, txtEndereco.Text, txtCPF.Text, txtTelefone.Text, this.foto, alterarFoto))
                {
                    LimparForm();
                    LimparFoto();
                    Habilitar_TextBox(false);
                    Habilitar_Button(true, false, false, true, true, false);
                }
            }

          ClienteController.CarregarTabelaCliente(Grid);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {           
          if (ClienteController.Excluir(this.id))
          {
             LimparForm();
             LimparFoto();
             Habilitar_TextBox(false);
             Habilitar_Button(true, false, false, false, false, false);
          }
          ClienteController.CarregarTabelaCliente(Grid);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.id = 0;
            LimparForm();
            LimparFoto();
            btnNovo.Focus();
            Habilitar_TextBox(false);
            Habilitar_Button(true, false, false, false, false, false);
        }

        private void btnImagem_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Imagens(*.jpg;*.png) | *.jpg;*png";//Mostrar jpg e png

            if (file.ShowDialog() == DialogResult.OK)
            {
                //pegar caminho da imagem selecionada
                foto = file.FileName.ToString();
                pictureBox.ImageLocation = foto;
                alterarFoto = true;
            }
            else
            {
                alterarFoto = false;
            }
         
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            ClienteController.Procurar(Grid,txtBuscar.Text);
        }

        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > -1)
            {
                alterarFoto = false;
                Habilitar_TextBox(true);
                Habilitar_Button(false, false, true, true, true, true);

                this.id          = Convert.ToInt32(Grid.CurrentRow.Cells[0].Value.ToString());
                txtNome.Text     = Grid.CurrentRow.Cells[1].Value.ToString();
                txtEndereco.Text = Grid.CurrentRow.Cells[2].Value.ToString();
                txtCPF.Text      = Grid.CurrentRow.Cells[3].Value.ToString();
                txtTelefone.Text = Grid.CurrentRow.Cells[4].Value.ToString();

                //Pegar Foto
                //DBNull.Value - se o valor do banco de dados(database) veio nulo               
                if(Grid.CurrentRow.Cells[5].Value != DBNull.Value)
                {
                    byte[] imagem_grid = (byte[])Grid.Rows[e.RowIndex].Cells[5].Value;
                    MemoryStream ms = new MemoryStream(imagem_grid);
                    pictureBox.Image = Image.FromStream(ms);  
                }
                else 
                {
                    pictureBox.Image = Properties.Resources.baseline_group_black_48dp;
                }
            }
            else
            {
                return;
            }
        }




        /// <summary>
        /// Controla ativação ou desativação dos botões
        /// </summary>
        /// <param name="novo"></param>
        /// <param name="salvar"></param>
        /// <param name="editar"></param>
        /// <param name="excluir"></param>
        /// <param name="cancelar"></param>
        private void Habilitar_Button(bool novo, bool salvar, bool editar, bool excluir, bool cancelar, bool imagem)
        {
            btnNovo.Enabled = novo;
            btnSalvar.Enabled = salvar;
            btnEditar.Enabled = editar;
            btnExcluir.Enabled = excluir;
            btnCancelar.Enabled = cancelar;
            btnImagem.Enabled = imagem;
        }

        /// <summary>
        /// Limpa todos os campos do formulario
        /// </summary>
        private void LimparForm()
        {
            txtNome.Clear();
            txtEndereco.Clear();
            txtCPF.Clear();
            txtTelefone.Clear();
        }

        /// <summary>
        /// Habilita(true) ou desabilita(false) todas as textBox do formulario
        /// </summary>
        /// <param name="ativo"></param>
        /// <param name="Id_Visible"> controla a visibilidade e habilitação de id</param>
        private void Habilitar_TextBox(bool ativo)
        {
            txtNome.Enabled = ativo;
            txtEndereco.Enabled = ativo;
            txtCPF.Enabled = ativo;
            txtTelefone.Enabled = ativo;
        }

        /// <summary>
        /// Verifica se todos os TextBox tem algum valor
        /// </summary>
        /// <returns>true or false</returns>
        private bool ValidarDados()
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Digite um Nome!", "Tela de Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNome.Focus();
                txtNome.Clear();
                return false;
            }
            else if (txtEndereco.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Digite seu Endereço!", "Tela de Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEndereco.Focus();
                txtEndereco.Clear();
                return false;
            }
            else if (txtCPF.Text == "   .   .   -" && txtCPF.Text.Length < 14)
            {
                MessageBox.Show("Digite um CPF!", "Tela de Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCPF.Focus();
                return false;
            }
            else if (txtTelefone.Text == "(  )      -" && txtTelefone.Text.Length < 14)
            {
                MessageBox.Show("Digite um Telefone!", "Tela de Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTelefone.Focus();
                return false;
            }

            return true;
        }

        private void LimparFoto()
        {
            pictureBox.Image = Properties.Resources.baseline_group_black_48dp;
            foto = "ft/baseline_group_black_48dp.png";
        }

        private void ItemRecarregar_Click(object sender, EventArgs e)
        {

        }
    }//Partial Class

}//Namespace
    
