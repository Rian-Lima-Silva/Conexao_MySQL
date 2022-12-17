using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoCadastroMySQL.App.Model;
using System.Windows.Forms;
using System.Data;

namespace ProjetoCadastroMySQL.App.Controller
{
    public class UsuarioController
    {
        public UsuarioController() { }

        public static DataGridView CarregarTabela(DataGridView grid)
        {
            var usuario_dao = new UsuarioModel();

            grid.DataSource = usuario_dao.GetAllRows();

            //Id
            grid.Columns[0].HeaderText = "ID";
            grid.Columns[0].Visible = false;

            //Nome
            grid.Columns[1].HeaderText = "Usuários";
            grid.Columns[1].Width = 205;
            

            //Senha
            grid.Columns[2].HeaderText = "Senhas";
            grid.Columns[2].Width = 320;


            grid.Columns[2].Visible = false;

            return grid;
        }
        
        public static DataGridView PesquisarNome(DataGridView grid,string nome)
        {
            var usuario_dao = new UsuarioModel();

            grid.DataSource = usuario_dao.SearchByName(nome);

            
            //Id
            grid.Columns[0].HeaderText = "ID";
            grid.Columns[0].Visible = false;

            //Nome
            grid.Columns[1].HeaderText = "Nome";
            grid.Columns[1].Width = 100;

            //Senha
            grid.Columns[2].HeaderText = "Senhas";
            grid.Columns[2].Width = 320;
            grid.Columns[2].Visible = false;


            return grid;
        }

        /// <summary>
        /// Apenas Salvar
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public static bool Adicionar(string nome,string senha)
        {
            var usuario_dao = new UsuarioModel();
            return usuario_dao.Insert(nome,senha);
        }
        
        /// <summary>
        /// Salvar e Editar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nome"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public static bool Salvar(int id,string nome,string senha)
        {
            if (id > 0)
            {
                var usuario_dao = new UsuarioModel();
                return usuario_dao.Update(id, nome, senha);
            }
            else
            {
                var usuario_dao = new UsuarioModel();
                return usuario_dao.Insert(nome, senha);
            }
        }

        public static bool Editar(int id,string nome,string senha)
        {
            if (id > 0)
            {
                var usuario_dao = new UsuarioModel();
                return usuario_dao.Update(id, nome, senha);
            }
            else
            {
                return false;
            }
        }

        public static bool Excluir(int id)
        {
            if (id > 0)
            {
                var usuario_dao = new UsuarioModel();
                DialogResult result = MessageBox.Show("Você tem certeza que deseja excluir esse usuário?", "Cadastro de Usuário",
                                                       MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    return usuario_dao.Delete(id);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Selecione um usuário para excluir!", "Cadastro de Usuário",
                                 MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

    }
}
