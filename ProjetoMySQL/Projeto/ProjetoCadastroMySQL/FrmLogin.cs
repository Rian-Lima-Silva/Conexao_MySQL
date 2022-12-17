using System;
using System.Windows.Forms;
using ProjetoCadastroMySQL.App.Controller;
using System.Threading;

namespace ProjetoCadastroMySQL
{
    public partial class FrmLogin : Form
    {
        Thread th;
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (ValidarDados())
            {
                if(LoginController.VerficarLogin(txtNome.Text,txtSenha.Text)== true)
                {
                    //FrmMenu frm = new FrmMenu();
                    //frm.Show();

                    th = new Thread(opennewform);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Senha ou Nome está incorreto!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNome.Clear();
                    txtSenha.Clear();
                    txtNome.Focus();
                }
            }
        }

        /// <summary>
        /// Abre uma nova Thread que abre o FrmMenu
        /// </summary>
        private void opennewform()
        {
            Application.Run(new FrmMenu());
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparForm();
        }

        private void LimparForm()
        {
            txtNome.Clear();
            txtSenha.Clear();
        }

        private bool ValidarDados()
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Digite seu usuário!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtNome.Clear();
                txtNome.Focus();
                return false;
            }
            else if (txtSenha.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Digite sua senha!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtSenha.Clear();
                txtSenha.Focus();
                return false;
            }

            return true;
        }
    }
}
