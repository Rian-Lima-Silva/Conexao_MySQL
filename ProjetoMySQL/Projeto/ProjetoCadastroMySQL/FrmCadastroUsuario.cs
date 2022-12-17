using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjetoCadastroMySQL.App.Controller;

namespace ProjetoCadastroMySQL
{
    public partial class FrmCadastroUsuario : Form
    {
        private int id = 0;
        public FrmCadastroUsuario()
        {
            InitializeComponent();
           
        }
        private void FrmCadastroUsuario_Load(object sender, EventArgs e)
        {
            UsuarioController.CarregarTabela(Grid);
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (ValidarDados())
            {
                UsuarioController.Salvar(this.id,txtNome.Text,txtSenha.Text);
                LimparForm();
            }
            UsuarioController.CarregarTabela(Grid);
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.id = 0;
            LimparForm();
        }

        private void ItemEditar_Click(object sender, EventArgs e)
        {
           if(UsuarioController.Editar(this.id, txtNome.Text, txtSenha.Text))
           {
                LimparForm();

           }
           UsuarioController.CarregarTabela(Grid);
        }

        private void ItemExcluir_Click(object sender, EventArgs e)
        {
            if (UsuarioController.Excluir(this.id))
            {
                LimparForm();
            }
            UsuarioController.CarregarTabela(Grid);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (UsuarioController.Excluir(this.id))
            {
                LimparForm();
            }
            UsuarioController.CarregarTabela(Grid);
        }

        private void ItemRecarregar_Click(object sender, EventArgs e)
        {
            UsuarioController.CarregarTabela(Grid);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            UsuarioController.PesquisarNome(Grid,txtBuscar.Text);
        }
        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.id = Convert.ToInt16(Grid.CurrentRow.Cells[0].Value.ToString());
            txtNome.Text = Grid.CurrentRow.Cells[1].Value.ToString();
            txtSenha.Text = Grid.CurrentRow.Cells[2].Value.ToString();

            btnTextEdit();
        }



        private void LimparForm()
        {
            this.id = 0;
            txtNome.Clear();
            txtSenha.Clear();

            btnTextEdit();
        }

        private bool ValidarDados()
        {
            if (txtNome.Text.Trim()=="")
            {
                MessageBox.Show("Digite algum Nome!","Cadastrar Usuario",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                txtNome.Clear();
                txtNome.Focus();
                return false;
            }
            else if (txtSenha.Text.Trim() == "")
            {
                MessageBox.Show("Digite algum Senha!", "Cadastrar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtSenha.Clear();
                txtSenha.Focus();
                return false;
            }

            return true;
        }


        private void btnTextEdit()
        {
            if (this.id > 0)
            {
                btnCadastrar.Text = "Editar";
                btnExcluir.Visible = true;
            }
            else
            {
                btnCadastrar.Text = "Cadastrar";
                btnExcluir.Visible = false;
            }
        }

    }
}
