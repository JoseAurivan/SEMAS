using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace CadastroHabCMAS.ViewModel.UserViewModel
{
    public class UserCreateViewModel
    {
        [Required(ErrorMessage = "Campo Matrícula é obrigatório")]
        public string Matricula { get; set; }
        
        [Required(ErrorMessage = "Campo CPF é obrigatório")]
        [MaxLength(11, ErrorMessage = "CPF deve ter 11 caracteres")]
        [MinLength(11, ErrorMessage= "CPF deve ter 11 caracteres")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "CPF deve ser numerico")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Campo Senha é obrigatório")]
        public string Senha { get; set; }


        public User GetModel(string cpf, string matricula, string senha)
        {
            return new User()
            {
                Matricula = matricula,
                Username = cpf,
                Password = senha
            };
        }
    }
}