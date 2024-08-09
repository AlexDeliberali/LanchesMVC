using System.ComponentModel.DataAnnotations;

namespace LanchesMVC.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o usuário")]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        //Permite que o campo no formulário é exibido os caracteres de senha
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }


    }
}
