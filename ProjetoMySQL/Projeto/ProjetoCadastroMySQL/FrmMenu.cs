using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;

namespace ProjetoCadastroMySQL
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void MenuCliente_Click(object sender, EventArgs e)
        {
            FrmCadastroCliente frm = new FrmCadastroCliente();
            frm.ShowDialog();
        }

        private void MenuCadastrarUsuario_Click(object sender, EventArgs e)
        {
            FrmCadastroUsuario frm = new FrmCadastroUsuario();
            frm.ShowDialog();
        }

        private void MenuFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MenuUsuario_Click(object sender, EventArgs e)
        {
            FrmCadastroUsuario frm = new FrmCadastroUsuario();
            frm.ShowDialog();
        }

        private void MenuProdutos_Click(object sender, EventArgs e)
        {
            //string keyAdd1 = System.Configuration.ConfigurationManager.AppSettings["k1"];
            //string keyAdd2 = System.Configuration.ConfigurationManager.AppSettings["k2"];
            //string keyAdd3 = System.Configuration.ConfigurationManager.AppSettings["k3"];
            //MessageBox.Show("Key 1: "+keyAdd1+"\nKey 2: "+keyAdd2+"\nKey 3: "+keyAdd3,"Testando");
        }

        private void CloseForm(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMenu_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
