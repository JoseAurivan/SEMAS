using System.ComponentModel.DataAnnotations;
using Domain.Models;


namespace CadastroHabCMAS.ViewModel.UserViewModel
{
    public class UserLoginViewModel
    {
        [Required]
        [MaxLength(11, ErrorMessage = "CPF deve ter 11 caracteres")]
        [MinLength(11, ErrorMessage= "CPF deve ter 11 caracteres")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "CPF deve ser numerico")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Campo Senha é obrigatório")]
        public string Password { get; set; }
        
    }
}