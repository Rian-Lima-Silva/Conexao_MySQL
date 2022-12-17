using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoCadastroMySQL.App.Model;

namespace ProjetoCadastroMySQL.App.Controller
{
    public class LoginController
    {
        public LoginController() { }

        public static bool VerficarLogin(string nome, string senha)
        {
            var loginModel = new LoginModel();
            return loginModel.Login(nome, senha);
        }
    }
}
